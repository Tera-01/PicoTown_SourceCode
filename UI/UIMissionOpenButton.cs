using UnityEngine;
using UnityEngine.UI;

public class UIMissionOpenButton : UIBase
{
    [SerializeField] private Button _missionOpenBtn;
    private void Start()
    {
        _missionOpenBtn.onClick.AddListener(OpenUIMissionWindow);
    }
    public void OpenUIMissionWindow()
    {
        GameManager.instance.IsButtonClick = true;
        UIManager.instance.GetUI<UIMissionCanvas>();
        GameManager.instance.PauseTime();
    }
}
