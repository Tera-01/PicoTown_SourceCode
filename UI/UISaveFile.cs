using UnityEngine;
using UnityEngine.UI;

public class UISaveFile : MonoBehaviour
{
    [SerializeField] private Button _loadingButton;
    
    void Start()
    {
        _loadingButton.onClick.AddListener(OpenLoading);
    }
    public void OpenLoading()
    {
        GameManager.instance.IsButtonClick = true;
        UIManager.instance.GetUI<UILoadingCanvas>();
    }
}
