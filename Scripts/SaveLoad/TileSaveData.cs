using System.Collections.Generic;

public class CustomVector3Int
{
    public int X;
    public int Y;
    public int Z;
}

public class CropSaveData
{
    public int CropItemId;
    public int CropSeedItemId;
    public int GrowthDays;
}

public class TileTypeSaveData
{
    public CustomVector3Int Position;
    public int TileType;
}

public class CropOnTileSaveData
{
    public CustomVector3Int Position;
    public CropSaveData CropData;
}

public class TileSaveData
{
    public List<TileTypeSaveData> TileTypeData;
    public List<CropOnTileSaveData> CropOnTileData;
}
