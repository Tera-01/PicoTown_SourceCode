using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string[] daysOfWeek = {"Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
    public string[] seasons = { "봄", "여름", "가을", "겨울" };

    private int _maxHour = 24;
    private int _maxMinute = 60;
    private int _maxDay = 28;

    [SerializeField] private int _currentDayIndex = 0;
    [SerializeField] private float _curTime = 0f;
    [SerializeField] private int _curMinute = 0;
    [SerializeField] private int _curHour = 6;
    [SerializeField] private int _curDay = 1;
    [SerializeField] private int _curYear = 1;
    [SerializeField] private int _curSeasonIndex = 0;
    [SerializeField] private float _gameTime;

    [SerializeField] private float _tenMinute = 7.17f;

    public event Action<int> OnMinuteChanged;
    public event Action<int> OnHourChanged;
    public event Action<int> OnDayChanged;
    public event Action<int> OnWeekChanged;
    public event Action<int> OnYearChanged;
    public event Action<int> OnSeasonChanged;
    public event Action<bool> OnButtonClick;

    public bool isTimePaused = false;
    public bool isSceneActive = false;
    private bool _isButtonClick = false;

    public float GameTime
    {
        get { return _gameTime; }
        set
        {
            _gameTime = value;
        }
    }
    public bool IsButtonClick
    {
        get { return _isButtonClick; }
        set 
        {
            _isButtonClick = value;
            OnButtonClick?.Invoke(value);
        }
    }
    
    public float Sec
    {
        get { return _curTime; }
        set
        {
            _curTime = value;
        }
    }
    
    public int Minute
    {
        get { return _curMinute; }
        set
        {
            _curMinute = value;
            OnMinuteChanged?.Invoke(value);
        }
    }
    
    public int Hour
    {
        get { return _curHour; }
        set
        {
            _curHour = value;
            OnHourChanged?.Invoke(value);
        }
    }
    
    public int Day
    {
        get { return _curDay; }
        set
        {
            _curDay = value;
            OnDayChanged?.Invoke(value);
        }
    }
    
    public int Week
    {
        get { return _currentDayIndex; }
        set
        {
            _currentDayIndex = value;
            if(_currentDayIndex > daysOfWeek.Length - 1)
            {
                _currentDayIndex = 0;
            }
            OnWeekChanged?.Invoke(_currentDayIndex);
        }
    }
    
    public int Year
    {
        get { return _curYear; }
        set
        {
            _curYear = value;
            OnYearChanged?.Invoke(value);
        }
    }
    
    public int Season
    {
        get { return _curSeasonIndex; }
        set
        {
            _curSeasonIndex = value;
            if (_curSeasonIndex > seasons.Length - 1)
            {
                _curSeasonIndex = 0;
                OnSeasonChanged?.Invoke(_curSeasonIndex);
                Year++;
            }
            else
            {
                OnSeasonChanged?.Invoke(_curSeasonIndex);
            }
        }
    }
    
    private void Start()
    {
        OnButtonClick += AudioManager.instance.PlayButtonClickSound;
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(instance);
    }
    
    private void Update()
    {
        if (!isTimePaused)
        {
            UpdateTime();
            GameTime += Time.deltaTime;
        }
        if(!isSceneActive)
        {
            SceneCheck();
        }
    }
    
    public void SceneCheck()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            isTimePaused = true;
        }
        else
        {
            UIManager.instance.GetUI<UITimer>();
            UIManager.instance.GetUI<UIMissionOpenButton>();
            UIManager.instance.GetUI<UIEnergyBar>();
            UIManager.instance.GetUI<UIInventoryOpenButton>();
            UIManager.instance.GetUI<UIHotBar>();
            UIManager.instance.GetUI<UIFadeIn>().FadeIn();
            UIManager.instance.GetUI<UIGuide>();
            GameTime = SaveManager.instance.saveDataList[SaveManager.instance.currentSaveFile].GameData.TotalPlayTime;
            isTimePaused = false;
            isSceneActive = true;
        }
    }
    
    public void UpdateTime()
    {
        _curTime += Time.deltaTime;
        if (_curTime >= _tenMinute)
        {
            _curTime = 0f;
            Minute += 10;
        }
        if (Minute >= _maxMinute)
        {
            Minute = 0;
            Hour++;
        }
        if (Hour >= _maxHour)
        {
            Hour = 0;
            Day++;
            Week++;
        }
        else if (Hour == 2)
        {
            Hour = 6;
            Minute = 0;
        }
        if (Day > _maxDay)
        {
            Day = 1;
            Season++;
        }
    }
    public void StartTime()
    {
        isTimePaused = false;
    }

    public void PauseTime()
    {
        isTimePaused = true;
    }

    public void Save()
    {
        GameSaveData gameSaveData = SaveManager.instance.saveDataList[SaveManager.instance.currentSaveFile].GameData;
        gameSaveData.Day = this.Day + 1;
        gameSaveData.Season = this.Season;
        gameSaveData.Year = this.Year;
        gameSaveData.TotalPlayTime = this.GameTime;
        
        if (this.Hour < 6)
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

        gameSaveData.Week = (gameSaveData.Day - 1) % 7;

        SaveManager.instance.saveDataList[SaveManager.instance.currentSaveFile].GameData = gameSaveData;
    }

    public void Load()
    {
        SaveManager saveManager = SaveManager.instance;
        GameSaveData gameSaveData = saveManager.saveDataList[saveManager.currentSaveFile].GameData;

        Day = gameSaveData.Day;
        Season = gameSaveData.Season;
        Week = gameSaveData.Week;
        Year = gameSaveData.Year;
        GameTime = gameSaveData.TotalPlayTime;

        _curTime = 0;
        Minute = 0;
        Hour = 6;
    }
}
