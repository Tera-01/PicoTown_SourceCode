using System.Collections.Generic;

public class ItemSlot
{
    public ItemObject ItemObj;
}

public class ItemContainer
{
    public static readonly int DefaultCapacity = 27;
    public ItemSlot[] itemSlots;

    public ItemContainer()
    {
        itemSlots = new ItemSlot[DefaultCapacity];
        for (int i = 0; i < DefaultCapacity; i++)
        {
            itemSlots[i] = new ItemSlot();
        }
    }
    
    public void Save(ref List<SlotSaveData> data)
    {
        for (int i = 0; i < DefaultCapacity; i++)
        {
            ItemObject itemObj = itemSlots[i].ItemObj;
            if (itemObj != null)
            {
                SlotSaveData item = new SlotSaveData();
                item.Index = i;
                item.ItemId = itemObj.ItemId;
                item.Count = itemObj.Count;
                data.Add(item);
            }
        }
    }

    public void Load(List<SlotSaveData> data)
    {
        for (int i = 0; i < data.Count; i++)
        {
            SlotSaveData curData = data[i];
            itemSlots[curData.Index].ItemObj = new ItemObject(curData.ItemId, curData.Count);
        }
    }

    // public void Add(ItemObject itemObj)
    // {
    //     int itemId = itemObj.ItemId;
    //     int count = itemObj.Count;
    //     ItemData itemData = DataManager.instance.itemDb[itemId];
    //     
    //     if (itemData.stack) // stackable
    //     {
    //         ItemObject item = itemList.Find(x => x.ItemId == itemId);
    //         if (item != null)
    //             item.Count += count; // count limit 확인
    //         else
    //         {
    //             item = itemList.Find(x => x.ItemId == null);
    //             if (item != null)
    //             {
    //                 item.ItemInfo = itemData;
    //                 item.Count = count;
    //             }
    //         }
    //     }
    // }
}
