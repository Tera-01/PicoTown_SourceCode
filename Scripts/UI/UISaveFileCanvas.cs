using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class UISaveFileCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _textDelete;

    [SerializeField] private GameObject[] _slots;
    [SerializeField] private GameObject emptyText;

    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _okButton;
    [SerializeField] private Button _cancelButton;

    [SerializeField] private TextMeshProUGUI[] _numberText;
    [SerializeField] private TextMeshProUGUI[] _nameText;
    [SerializeField] private TextMeshProUGUI[] _farmNameText;
    [SerializeField] private TextMeshProUGUI[] _seasonText;
    [SerializeField] private TextMeshProUGUI[] _timeText;
    [SerializeField] private TextMeshProUGUI[] _goldText;

    private SaveManager _saveManager;
    private bool _isCheckSaveFile = false;

    private UIFadeOut _fadeOut;
    private UIFadeOut FadeOut
    {
        get
        {
            if (_fadeOut == null)
            {
                _fadeOut = UIManager.instance.GetUI<UIFadeOut>();
            }
            return _fadeOut;
        }
    }

    void Start()
    {
        _saveManager = SaveManager.instance;
        _closeButton.onClick.AddListener(CloseSaveFileWindow);
        _cancelButton.onClick.AddListener(CloseDeleteWindow);
        OpenLoadingWindow();
    }

    private void Update()
    {
        if (!_isCheckSaveFile)
        {
            DisplaySaveFiles();
        }
    }
    
    public void DisplaySaveFiles()
    {
        List<SaveData> saveList = _saveManager.saveDataList;

        for (int i = 0; i < saveList.Count; i++)
        {
            _slots[i].SetActive(true);
            DisplaySavedData(i, saveList[i].GameData);
        }

        _isCheckSaveFile = true;
    }

    public void DisplaySavedData(int num, GameSaveData data)
    {
        int second = (int)data.TotalPlayTime % 60;
        int minute = (int)data.TotalPlayTime / 60;
        int hour = minute / 60;
        minute %= 60;
        
        string[] seasonStrings = { "봄", "여름", "가을", "겨울" };

        _numberText[num].text = $"{num + 1}.";
        _nameText[num].text = data.PlayerName;
        _farmNameText[num].text = $"{data.FarmName} 농장";

        _timeText[num].text = $"{hour.ToString("D2")}:{minute.ToString("D2")}";

        _seasonText[num].text = $"{data.Year}년째, {seasonStrings[data.Season]}의 {data.Day}일째";
        _goldText[num].text = $"{data.Money}골드";
    }
    
    public void Slot(int slotNum)
    {
        if (_saveManager.saveDataList[slotNum] != null)
        {
            _saveManager.currentSaveFile = slotNum;
            GameManager.instance.IsButtonClick = true;
            FadeOut.FadeOut();
            Invoke(nameof(InGame), 1f);
        }
    }

    public void DeleteSlotCheck(int number)
    {
        OpenSaveFileWindow();
        _okButton.onClick.AddListener(() => DeleteSlot(number));
    }

    public void DeleteSlot(int number)
    {
        CloseSaveFileWindow();
        CloseDeleteWindow();
        int saveFileNum = _saveManager.saveDataList[number].GameData.SaveNum;
        
        SaveData removedFile = null;
        foreach (var item in _saveManager.saveDataList)
        {
            if (item.GameData.SaveNum == saveFileNum)
                removedFile = item;
        }
        _saveManager.saveDataList.Remove(removedFile);
        
        File.Delete($"{_saveManager.path}{saveFileNum}");
        _slots[number].SetActive(false);
        _isCheckSaveFile = false;
        SaveManager.instance.isFullSaveFile[number] = false;
    }
    
    public void CloseDeleteWindow()
    {
        GameManager.instance.IsButtonClick = true;
        _textDelete.SetActive(false);
    }
    
    public void OpenSaveFileWindow()
    {
        GameManager.instance.IsButtonClick = true;
        _textDelete.SetActive(true);
    }
    
    public void InGame()
    {
        SceneManager.LoadScene(1);
    }
    
    public void OpenLoadingWindow()
    {
        if (_saveManager.saveDataList.Count > 0)
        {
            emptyText.SetActive(false);
        }
        else
        {
            emptyText.SetActive(true);
        }
    }
    
    public void CloseSaveFileWindow()
    {
        GameManager.instance.IsButtonClick = true;
        gameObject.SetActive(false);
    }
}
