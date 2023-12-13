using UnityEngine;

public class ItemObject
{
    public int ItemId;
    private int _count;
    public int Count
    {
        get => _count;
        set
        {
            _count = value;
            if (_count > 999)
            {
                Debug.LogError("ItemObject count is over 999");
            }
        }
    }

    public ItemObject(int itemId, int count = 1)
    {
        this.ItemId = itemId;
        Count = count;
    }
}
