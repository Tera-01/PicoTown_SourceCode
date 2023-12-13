using UnityEngine;
using UnityEngine.UI;

public class UISleepInBed : MonoBehaviour
{
    [SerializeField] private Button _selectYBtn;
    [SerializeField] private Button _selectNBtn;

    void Start()
    {
        _selectYBtn.onClick.AddListener(SelectYes);
        _selectNBtn.onClick.AddListener(SelectNo);
    }
    
    public void SelectYes()
    {
        GameManager.instance.IsButtonClick = true;
        Sleep();
    }

    public void SelectNo()
    {
        GameManager.instance.IsButtonClick = true;
        gameObject.SetActive(false);
        GameManager.instance.StartTime();
    }

    public void Sleep()
    {
        SelectNo();

        SaveData saveData = SaveManager.instance.saveDataList[SaveManager.instance.currentSaveFile];

        GameSaveData gameSaveData = saveData.GameData;

        gameSaveData.Day = GameManager.instance.Day + 1;
        gameSaveData.Season = GameManager.instance.Season;
        gameSaveData.Year = GameManager.instance.Year;
        if (GameManager.instance.Hour < 6)
        {
            gameSaveData.Day -= 1;
            if (gameSaveData.Day == 28)
            {
                gameSaveData.Season -= 1;
                if (gameSaveData.Season == 3)
                {
                    gameSaveData.Year -= 1;
                }
            }
        }

        TileManager.Instance.UpdateTile();
        TileManager.Instance.Save();
        GameManager.instance.Save();
        UIManager.instance.GetUI<UIFadeOut>().FadeOut();
        SaveManager.instance.SetSaveFile(SaveManager.instance.saveDataList[SaveManager.instance.currentSaveFile]);
        // SceneManager.LoadScene(1);
    }
}
