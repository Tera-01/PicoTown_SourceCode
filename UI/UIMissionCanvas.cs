using UnityEngine;
using UnityEngine.UI;

public class UIMissionCanvas : UIBase
{
    [SerializeField] private Button _missionCloseBtn;

    private void Start()
    {
        _missionCloseBtn.onClick.AddListener(CloseUI);
    }
}
