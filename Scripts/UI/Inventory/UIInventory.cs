using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : UIBase
{
    [SerializeField] private List<ItemIcon> icons;

    [SerializeField] private Button _invenCloseBtn;
    
    void Start()
    {
        UpdateInventory();
        _invenCloseBtn.onClick.AddListener(CloseUI);
    }

    public void UpdateInventory()
    {
        if (icons.Count == 0)
            return;

        ItemSlot[] inventorySlots = InventoryManager.Instance.Inventory.itemSlots;
        for (int i = 0; i < ItemContainer.DefaultCapacity; i++)
        {
            if (inventorySlots[i].ItemObj == null)
            {
                icons[i].Clean();
            }
            else
            {
                icons[i].Set(inventorySlots[i].ItemObj);
            }
        }
    }
}
