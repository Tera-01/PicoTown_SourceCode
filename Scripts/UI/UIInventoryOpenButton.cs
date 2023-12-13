using UnityEngine;
using UnityEngine.UI;

public class UIInventoryOpenButton : MonoBehaviour
{
    [SerializeField] private Button _invenOpenBtn;
    private UIInventory _inventory;
    void Start()
    {
        _invenOpenBtn.onClick.AddListener(OpenUIInventoryWindow);
    }
    public void OpenUIInventoryWindow()
    {
        GameManager.instance.IsButtonClick = true;
        _inventory = UIManager.instance.GetUI<UIInventory>();
        _invenOpenBtn.onClick.AddListener(_inventory.UpdateInventory);
        GameManager.instance.PauseTime();
    }
}
