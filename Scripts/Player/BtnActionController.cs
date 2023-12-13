using Constants;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BtnActionController : MonoBehaviour
{
    [SerializeField] private Character character;
    public static BtnActionController Instance;

    private Dictionary<int, Anim> _itemToAnim = new();
    public event Action<int> OnToolUsed;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _itemToAnim.Add((int)Tool.AXE,Anim.AXE);
        _itemToAnim.Add((int)Tool.HOE,Anim.HOE);
        _itemToAnim.Add((int)Tool.PICKAXE,Anim.PICKAXE);
        // _itemToAnim.Add(1103,Anim.SWORD);
        _itemToAnim.Add((int)Tool.WATERINGCAN,Anim.WATER);
        // _itemToAnim.Add(1105,Anim.FISHING);
    }

    public void ScanEquipItem()
    {
        InventoryManager inventoryManager = InventoryManager.Instance;
        int curItemIdx = inventoryManager.SelectedItemIndex;
        if (curItemIdx == -1)
            return;
        int curItemId = inventoryManager.Inventory.itemSlots[curItemIdx].ItemObj.ItemId;

        Anim anim = Anim.CARRY;
        foreach (int id in _itemToAnim.Keys)
        {
            if (curItemId == id)
            {
                _itemToAnim.TryGetValue(id, out anim);
                break;
            }
        }

        // if (anim == Anim.CARRY) ; //  먹을지 묻는 팝업
        character.Play((int)anim);
        
        OnToolUsed?.Invoke(curItemId);
    }
}
