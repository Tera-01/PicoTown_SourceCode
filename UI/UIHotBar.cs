using System;
using System.Collections.Generic;
using UnityEngine;

public class UIHotBar : MonoBehaviour
{
    [SerializeField] private List<ItemIcon> icons;
    [SerializeField] private List<HotBarSelect> hotBarSlots;
    private int _selectedSlot = -1;
    public static readonly int Capacity = 9;

    private InventoryManager _inventoryManager;
    public event Action<int> OnSelectedChanged;

    public int SelectedSlot
    {
        get => _selectedSlot;
        set
        {
            if (value != -1 && _selectedSlot != value)
            {
                _inventoryManager.SelectedItemIndex = value;
                OnSelectedChanged?.Invoke(value);
            }

            _selectedSlot = value;
        }
    }

    private void Start()
    {
        _inventoryManager = InventoryManager.Instance;
        UpdateHotBar();
    }

    private void UpdateHotBar()
    {
        ItemSlot[] inventory = _inventoryManager.Inventory.itemSlots;
        
        for (int i = 0; i < Capacity; i++)
        {
            hotBarSlots[i].Index = i;
            if (inventory[i].ItemObj == null)
            {
                icons[i].Clean();
            }
            else
            {
                icons[i].Set(inventory[i].ItemObj);
            }
        }
    }
}
