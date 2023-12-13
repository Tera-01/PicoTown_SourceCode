using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public ItemContainer Inventory;
    public int SelectedItemIndex = -1;

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

        Inventory = new ItemContainer();
        Load();
    }

    private void Load()
    {
        SaveManager saveManager = SaveManager.instance;
        List<SlotSaveData> itemList = saveManager.saveDataList[saveManager.currentSaveFile].InventoryData.itemList;

        foreach (SlotSaveData data in itemList)
        {
            Inventory.itemSlots[data.Index].ItemObj = new ItemObject(data.ItemId, data.Count);
        }
    }
}
