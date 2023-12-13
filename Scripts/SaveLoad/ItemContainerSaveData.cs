using System.Collections.Generic;

public class SlotSaveData
{
    public int Index;
    public int ItemId;
    public int Count;
}

public class ItemContainerSaveData
{
    public List<SlotSaveData> itemList = new();
}
