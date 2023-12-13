using UnityEngine;
using UnityEngine.UI;

public class UINewGame : MonoBehaviour
{
    [SerializeField] private Button _newGameButton;

    private SaveManager _saveManager;
    private UITextException _text;
    private UITextException Text
    {
        get
        {
            if (_text == null)
            {
                _text = UIManager.instance.GetUI<UITextException>();
            }
            return _text;
        }
    }

    void Start()
    {
        _saveManager = SaveManager.instance;
        _newGameButton.onClick.AddListener(OpenNewGameCanvas);
    }
    
    public void OpenNewGameCanvas()
    {
        if (_saveManager.saveDataList.Count == 3)
        {
            OpenTextException();
            Text.ChangeFullSlotText();
        }
        else
        {
            GameManager.instance.IsButtonClick = true;
            UIManager.instance.GetUI<UINewGameCanvas>();
        }
    }
    public void OpenTextException()
    {
        GameManager.instance.IsButtonClick = true;
        UIManager.instance.GetUI<UITextException>();
    }
}
