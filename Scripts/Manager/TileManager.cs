using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Constants;

public enum TileType
{
    IsPassable = 1,
    HasObject = 2,
    IsSoil = 4,
    IsTilled = 8,
    IsWatered = 16,
    IsInSeed = 32
}

public class TileManager : MonoBehaviour
{
    public class CropData
    {
        private ItemData _cropInfo;
        public ItemData CropInfo
        {
            get { return _cropInfo; }
            set
            {
                _cropInfo = value;
                CropSprites = Resources.LoadAll<Sprite>($"Crops/crops/{_cropInfo.baseName}");
                CropWetSprites = Resources.LoadAll<Sprite>($"Crops/crops_wet/{_cropInfo.baseName}");
            }
        }
        public ItemData CropSeeds;
        private int _growthDays;

        public int GrowthDays
        {
            get { return _growthDays; }
            set
            {
                _growthDays = value;
                GrowthStage = (_growthDays * 5) / CropSeeds.growthTime;
                if (_growthDays != 0 && GrowthStage == 0)
                    GrowthStage = 1;
            }
        }

        private int _growthStage;

        public int GrowthStage
        {
            get { return _growthStage; }
            set { _growthStage = (value < 5) ? value : 5; }
        }

        public Sprite[] CropSprites;
        public Sprite[] CropWetSprites;

        public CropData()
        {
            _growthDays = 0;
        }
    }
    
    [SerializeField] public Tilemap _floor;
    [SerializeField] private Tilemap _soil;
    [SerializeField] private Tilemap _collision;

    [SerializeField] private Tile _soilTile;
    [SerializeField] private Tile _plowedTile;
    [SerializeField] private Tile _wateredPlowedTile;
    
    public Dictionary<Vector3Int, TileType> TileTypes;
    public Dictionary<Vector3Int, CropData> CropOnTile;
    // public Dictionary<Vector3Int, GameObject> ObjOnTile;

    [SerializeField] private GameObject player;
    public static TileManager Instance { get; private set; }
    private SaveManager _saveManager;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        TileTypes = new Dictionary<Vector3Int, TileType>();
        CropOnTile = new Dictionary<Vector3Int, CropData>();
        // TileObject = new Dictionary<Vector3Int, GameObject>();
    }

    private void Start()
    {
        _saveManager = SaveManager.instance;
        BtnActionController.Instance.OnToolUsed += UseTool;
        BtnActionController.Instance.OnToolUsed += UseConsumables;

        foreach (var position in _floor.cellBounds.allPositionsWithin)
        {
            TileTypes.Add(position, TileType.IsPassable);
        }

        foreach (var position in _collision.cellBounds.allPositionsWithin)
        {
            DisableTileType(position, TileType.IsPassable);
        }

        foreach (var position in _soil.cellBounds.allPositionsWithin)
        {
            EnableTileType(position, TileType.IsSoil);
        }

        if (_saveManager.saveDataList[_saveManager.currentSaveFile].TileData.TileTypeData.Count != 0)
        {
            Load();
        }
    }
    
    private void EnableTileType(Vector3Int position, TileType type)
    {
        TileTypes[position] |= type;
    }

    private void DisableTileType(Vector3Int position, TileType type)
    {
        TileTypes[position] ^= type;
    }

    private bool TypeChecker(Vector3Int position, TileType type)
    {
        return (TileTypes[position] & type) == type;
    }

    public Vector3Int GetBtnPressedPosition()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 fixedPos;
        fixedPos.x = playerPos.x;
        fixedPos.y = playerPos.y - 1;
        fixedPos.z = playerPos.z;
        Vector3Int result = _floor.WorldToCell(fixedPos);
        
        Direction dir = PlayerController.Instance.CurDirection;
        if ((int)dir < 2)
        {
            result.y += dir == Direction.DOWN ? -1 : 1;
        }
        else
        {
            result.x += dir == Direction.LEFT ? -1 : 1;
        }

        return result;
    }

    public void UseConsumables(int itemId)
    {
        UseConsumablesAt(itemId, GetBtnPressedPosition());
    }

    public void UseConsumablesAt(int itemId, Vector3Int position)
    {
        ItemData item = DataManager.instance.itemDb[itemId];
        if (item.itemServeType == ItemServeType.Seed)
        {
            PlantSeed(position, item);
        }
    }

    public void UseTool(int itemId)
    {
        UseToolAt(itemId, GetBtnPressedPosition());
    }
    
    public void UseToolAt(int itemId, Vector3Int position)
    {
        Tool toolType = (Tool)itemId;
        
        switch (toolType)
        {
            case Tool.AXE:
                Axe(position);
                break;
            case Tool.HOE:
                Hoe(position);
                break;
            case Tool.PICKAXE:
                Pickaxe(position);
                break;
            case Tool.WATERINGCAN:
                WateringCan(position);
                break;
        }
    }

    void Axe(Vector3Int position)
    {
        Debug.Log("Using Axe");
    }
    
    void Hoe(Vector3Int position)
    {
        if (TypeChecker(position, TileType.IsSoil) && !TypeChecker(position, TileType.IsTilled))
        {
            // 나중에는 hasObject 조건도 필요
            Plow(position);
        }
    }
    
    void Pickaxe(Vector3Int position)
    {
        Debug.Log("Using Pickaxe");
        
        if (TypeChecker(position, TileType.IsTilled))
        {
            _soil.SetTile(position, _soilTile);
            DisableTileType(position, TileType.IsTilled);
            
            if (TypeChecker(position, TileType.IsInSeed))
            {
                DisableTileType(position, TileType.IsInSeed);
                CropOnTile.Remove(position);
            }
        }
    }
    
    void WateringCan(Vector3Int position)
    {
        if (TypeChecker(position, TileType.IsTilled) && !TypeChecker(position, TileType.IsWatered))
        {
            // 나중에는 hasObject 조건도 필요
            Water(position);
        }
    }

    public void Plow(Vector3Int position)
    {
        _soil.SetTile(position, _plowedTile);
        EnableTileType(position, TileType.IsTilled);
    }

    public void Water(Vector3Int position)
    {
        Tile newTile = _wateredPlowedTile;
        
        if (TypeChecker(position, TileType.IsInSeed))
        {
            newTile = ScriptableObject.CreateInstance<Tile>();
            CropData crop = CropOnTile[position];
            newTile.sprite = Resources.LoadAll<Sprite>($"Crops/crops_wet/{crop.CropInfo.baseName}")[crop.GrowthStage];
        }

        _soil.SetTile(position, newTile);
        EnableTileType(position, TileType.IsWatered);
    }

    public void PlantSeed(Vector3Int position, ItemData seed)
    {
        if (TypeChecker(position, TileType.IsTilled) && !TypeChecker(position, TileType.IsInSeed))
        {
            EnableTileType(position, TileType.IsInSeed);
            
            CropData crop = new CropData();
            crop.CropInfo = DataManager.instance.itemDb[seed.itemId + 100];
            crop.CropSeeds = seed;
            CropOnTile.Add(position, crop);
            SetCropTile(position);
        }
    }

    public void SetCropTile(Vector3Int position)
    {
        CropData crop = CropOnTile[position];
        Tile cropTile = ScriptableObject.CreateInstance<Tile>();

        if (TypeChecker(position, TileType.IsWatered))
        {
            cropTile.sprite = crop.CropWetSprites[crop.GrowthStage];
        }
        else
        {
            cropTile.sprite = crop.CropSprites[crop.GrowthStage];
        }

        _soil.SetTile(position, cropTile);
    }

    public void UpdateTile()
    {
        UpdateCropOnTile();
        List<Vector3Int> disableList = new();
        
        foreach (KeyValuePair<Vector3Int, TileType> item in TileTypes)
        {
            if (TypeChecker(item.Key, TileType.IsWatered))
            {
                disableList.Add(item.Key);
            }
        }

        foreach (Vector3Int item in disableList)
        {
            DisableTileType(item, TileType.IsWatered);
        }
    }

    public void UpdateCropOnTile()
    {
        foreach (KeyValuePair<Vector3Int, CropData> item in CropOnTile)
        {
            if (TypeChecker(item.Key, TileType.IsWatered))
            {
                UpdateCrop(item.Key);
            }
        }
    }

    public void UpdateCrop(Vector3Int position)
    {
        CropOnTile[position].GrowthDays += 1;
    }

    public void Save()
    {
        TileSaveData tileSaveData = new()
        {
            TileTypeData = new(),
            CropOnTileData = new ()
        };
        
        foreach (KeyValuePair<Vector3Int, TileType> item in TileTypes)
        {
            TileTypeSaveData tileTypeSaveData = new()
            {
                Position = new()
                {
                    X = item.Key.x,
                    Y = item.Key.y,
                    Z = item.Key.z
                },
                TileType = (int)item.Value
            };
            tileSaveData.TileTypeData.Add(tileTypeSaveData);
        }

        foreach (KeyValuePair<Vector3Int, CropData> item in CropOnTile)
        {
            CropOnTileSaveData cropOnTileSaveData = new()
            {
                Position = new()
                {
                    X = item.Key.x,
                    Y = item.Key.y,
                    Z = item.Key.z
                },
                CropData = new()
                {
                    CropItemId = item.Value.CropInfo.itemId,
                    CropSeedItemId = item.Value.CropSeeds.itemId,
                    GrowthDays = item.Value.GrowthDays
                }
            };
            tileSaveData.CropOnTileData.Add(cropOnTileSaveData);
        }

        _saveManager.saveDataList[_saveManager.currentSaveFile].TileData = tileSaveData;
    }

    public void Load()
    {
        TileTypes.Clear();
        CropOnTile.Clear();
        
        TileSaveData tileSaveData = _saveManager.saveDataList[_saveManager.currentSaveFile].TileData;
        foreach (TileTypeSaveData item in tileSaveData.TileTypeData)
        {
            Vector3Int position = new()
            {
                x = item.Position.X,
                y = item.Position.Y,
                z = item.Position.Z
            };

            TileTypes.Add(position, (TileType)item.TileType);
        }

        foreach (CropOnTileSaveData item in tileSaveData.CropOnTileData)
        {
            Vector3Int position = new()
            {
                x = item.Position.X,
                y = item.Position.Y,
                z = item.Position.Z
            };
            CropData cropData = new()
            {
                CropInfo = DataManager.instance.itemDb[item.CropData.CropItemId],
                CropSeeds = DataManager.instance.itemDb[item.CropData.CropSeedItemId],
                GrowthDays = item.CropData.GrowthDays
            };
            CropOnTile.Add(position, cropData);
            SetCropTile(position);
        }
    }
}
