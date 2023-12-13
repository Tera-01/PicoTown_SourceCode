using Constants;
using System.Collections.Generic;
using UnityEngine;

public class SkinsItemData : MonoBehaviour
{
    public static SkinsItemData Instance;

    [SerializeField] private List<SkinsItemSO> bodyDatas = new();
    [SerializeField] private List<SkinsItemSO> topDatas = new();
    [SerializeField] private List<SkinsItemSO> pantsDatas = new();
    [SerializeField] private List<SkinsItemSO> shoesDatas = new();
    [SerializeField] private List<SkinsItemSO> hairDatas = new();
    [SerializeField] private List<SkinsItemSO> hatDatas = new();

    private List<SkinsItemSO>[] skinsLists = new List<SkinsItemSO>[11];

    private void Awake()
    {
        Instance = this;

        skinsLists[(int)Skins.BODY] = bodyDatas;
        skinsLists[(int)Skins.TOP] = topDatas;
        skinsLists[(int)Skins.PANTS] = pantsDatas;
        skinsLists[(int)Skins.SHOES] = shoesDatas;
        skinsLists[(int)Skins.HAIR] = hairDatas;
        skinsLists[(int)Skins.HAT] = hatDatas;
    }

    public SkinsItemSO GetItem(Skins skins, int idx)
    {
        var skinsDatas = skinsLists[(int)skins];
        int selectIdx = Mathf.Min(idx, skinsDatas.Count);

        return skinsDatas[selectIdx];
    }
}
