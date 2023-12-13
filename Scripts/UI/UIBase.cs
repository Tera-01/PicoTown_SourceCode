using UnityEngine;

public class UIBase : MonoBehaviour
{
    [SerializeField] private GameObject _activeWindow;

    public virtual void OpenUI()
    {
        OpenUIWindow();
    }
    
    public virtual void CloseUI()
    {
        CloseUIWindow();
    }
    
    public void OpenUIWindow()
    {
        if (_activeWindow != null)
        {
            _activeWindow.SetActive(true);
        }
        GameManager.instance.IsButtonClick = true;
        GameManager.instance.PauseTime();
    }
    
    public void CloseUIWindow()
    {
        if (_activeWindow != null)
        {
            _activeWindow.SetActive(false);
        }
        GameManager.instance.IsButtonClick = true;
        GameManager.instance.StartTime();
    }
}
