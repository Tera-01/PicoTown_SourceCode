using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{
    [SerializeField] public Image Icon;
    [SerializeField] public TextMeshProUGUI CountText;

    public void Set(ItemObject item)
    {
        int itemId = item.ItemId;
        ItemData itemData = DataManager.instance.itemDb[itemId];
        ItemServeType type = itemData.itemServeType;
        
        Icon.gameObject.SetActive(true);
        Icon.sprite = Resources.Load<Sprite>($"Icon/{type.ToString()}/{itemData.baseName}");

        if (Icon.sprite == null)
        {
            Icon.gameObject.SetActive(false);
        }

        if (itemData.stack == true && item.Count != 1)
        {
            CountText.gameObject.SetActive(true);
            CountText.text = item.Count.ToString();
        }
        else
            CountText.gameObject.SetActive(false);
    }

    public void Clean()
    {
        Icon.sprite = null;
        Icon.gameObject.SetActive(false);
        CountText.gameObject.SetActive(false);
    }
}
