using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;

public enum ItemMainType
{
    Equipment,
    Resource,
    Crops,
    Livestock,
    Installation
}

public enum ItemServeType
{
    Tool,
    Weapon,
    Basic,
    Mineral,
    Seed,
    Fruit,
    ProcessingMachines,
    Products,
    Furniture,
    Etc
}

// public class PlayerData
// {
//     public string playerName;
//     public string farmName;
//     public string favorite;
//     public int goldText = 500;
//     public string playerGender;
//     public float playerEnergy = 270f;
//     public float playerHealth = 100f;
//     public float playerSpeed = 3f;
//     public float playerHp = 100f;
//     public int playerLevel = 1;
//     public int playerAttackPower = 1;
//     public int playerCoin = 100;
//     public int item = -1;
// }

[Serializable]
public class ItemData
{
    public int itemId;
    public ItemMainType itemMainType;
    public ItemServeType itemServeType;
    public string displayName;
    public string baseName;
    public string description;
    public int purchasePrice;
    public int sellPrice;
    public bool stack;
    public int fragility;
    public bool edibility;
    public int health;
    public int energy;
    public bool setOutdoor;
    public bool setIndoor;
    public int boundingBoxHeigth;
    public int boundingBoxWidth;
    public string cropGrowingSeason;
    public int cropHarvestExp;
    public int growthTime;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public Dictionary<int, ItemData> itemDb;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(instance);
    }

    private void Start()
    {
        ReadItemData();
    }
    
    public void ReadItemData()
    {
        itemDb = new Dictionary<int, ItemData>();
        TextAsset data = Resources.Load<TextAsset>("JSONData/itemData");
        List<ItemData> itemDatas = JsonConvert.DeserializeObject<List<ItemData>>(data.text);
        for (int i = 0; i < itemDatas.Count; i++)
        {
            itemDb.Add(itemDatas[i].itemId, itemDatas[i]);
        }
    }
}
