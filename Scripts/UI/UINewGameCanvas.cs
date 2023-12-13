using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UINewGameCanvas : UIBase
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _okButton;

    [SerializeField] private TMP_InputField _playerNameInput;
    [SerializeField] private TMP_InputField _farmNameInput;
    [SerializeField] private TMP_InputField _favoriteInput;

    private SaveManager _saveManager;

    private UIFadeOut _fadeInOut;
    private UIFadeOut FadeInOut
    {
        get
        {
            if (_fadeInOut == null)
            {
                _fadeInOut = UIManager.instance.GetUI<UIFadeOut>();
            }
            return _fadeInOut;
        }
    }
    private UITextException _text;
    private UITextException Text
    {
        get 
        { 
            if(_text == null)
            {
                _text = UIManager.instance.GetUI<UITextException>();
            }
            return _text; 
        }
    }

    void Start()
    {
        //_fadeInOut = FindObjectOfType<UIFadeInOut>();
        _closeButton.onClick.AddListener(CloseUI);
        _okButton.onClick.AddListener(ChangeScene);

        _saveManager = SaveManager.instance;
    }

    public void ChangeScene()
    {
        if (string.IsNullOrEmpty(_playerNameInput.text) || string.IsNullOrEmpty(_farmNameInput.text) || string.IsNullOrEmpty(_favoriteInput.text))
        {
            OpenTextException();
            Text.ChangeNullValueText();
        }
        else if (!string.IsNullOrEmpty(_playerNameInput.text) && _playerNameInput.text.Length >= 7)
        {
            OpenTextException();
            Text.ChangePlayerNameValueText();
        }
        else if (!string.IsNullOrEmpty(_farmNameInput.text) && _farmNameInput.text.Length >= 7)
        {
            OpenTextException();
            Text.ChangeFarmNameValueText();
        }
        else if (!string.IsNullOrEmpty(_favoriteInput.text) && _favoriteInput.text.Length >= 7)
        {
            OpenTextException();
            Text.ChangeFavoriteValueText();
        }
        else
        {
            GameManager.instance.IsButtonClick = true;
            FadeInOut.FadeOut();
            Invoke(nameof(GameStart), 1);
        }
    }

    public void GameStart()
    {
        SaveData data = new()
        {
            GameData = new()
            {
                SaveNum = _saveManager.GetEmptySaveFile(),
                PlayerName = _playerNameInput.text,
                FarmName = _farmNameInput.text,
                FavoriteThing = _favoriteInput.text
            },
            InventoryData = new()
            {
                itemList = new()
            },
            TileData = new()
            {
                CropOnTileData = new(),
                TileTypeData = new()
            }
        };
        
        List<SlotSaveData> defaultInventory = data.InventoryData.itemList;
        defaultInventory.Add(new SlotSaveData() { Index = 0, ItemId = (int)Tool.AXE, Count = 1 });
        defaultInventory.Add(new SlotSaveData() { Index = 1, ItemId = (int)Tool.HOE, Count = 1 });
        defaultInventory.Add(new SlotSaveData() { Index = 2, ItemId = (int)Tool.WATERINGCAN, Count = 1 });
        defaultInventory.Add(new SlotSaveData() { Index = 3, ItemId = (int)Tool.PICKAXE, Count = 1 });
        defaultInventory.Add(new SlotSaveData() { Index = 4, ItemId = 3117, Count = 5 }); // 당근
        defaultInventory.Add(new SlotSaveData() { Index = 5, ItemId = 3109, Count = 5 }); // 순무
        defaultInventory.Add(new SlotSaveData() { Index = 6, ItemId = 3115, Count = 5 }); // 토마토
        defaultInventory.Add(new SlotSaveData() { Index = 7, ItemId = 3112, Count = 5 }); // 가지
        defaultInventory.Add(new SlotSaveData() { Index = 8, ItemId = 3113, Count = 5 }); // 호박

        _saveManager.saveDataList.Add(data);
        _saveManager.currentSaveFile = data.GameData.SaveNum;
        _saveManager.SetSaveFile(data);

        SceneManager.LoadScene(1);
    }
    
    public override void CloseUI()
    {
        base.CloseUI();
        ResetUIText();
    }
    
    public void OpenTextException()
    {
        GameManager.instance.IsButtonClick = true;
        UIManager.instance.GetUI<UITextException>();
    }
    
    public void ResetUIText()
    {
        _playerNameInput.text = null;
        _farmNameInput.text = null;
        _favoriteInput.text = null;
    }
}
