using TMPro;
using UnityEngine;

public class UITimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _weekText;
    [SerializeField] private TextMeshProUGUI _dayText;
    [SerializeField] private TextMeshProUGUI _hourText;
    [SerializeField] private TextMeshProUGUI _minuteText;

    private void Start()
    {
        GameManager.instance.OnMinuteChanged += UpdateMinuteUI;
        GameManager.instance.OnHourChanged += UpdateHourUI;
        GameManager.instance.OnDayChanged += UpdatedayUI;
        GameManager.instance.OnWeekChanged += UpdateDayOfWeekText;

        InitTimer();
    }
    
    private void InitTimer()
    {
        GameSaveData data = SaveManager.instance.saveDataList[SaveManager.instance.currentSaveFile].GameData;
        
        UpdateMinuteUI(0);
        UpdateHourUI(6);
        UpdatedayUI(data.Day);
        UpdateDayOfWeekText(data.Week);
    }

    public void UpdateMinuteUI(int minute)
    {
        _minuteText.text = $"{minute:D2}";
    }
    
    public void UpdateHourUI(int hour)
    {
        _hourText.text = $"{hour:D2}";
    }
    
    public void UpdatedayUI(int day)
    {
        _dayText.text = $"{day:D2}";
    }
    
    public void UpdateDayOfWeekText(int day)
    {
        _weekText.text = GameManager.instance.daysOfWeek[day % 7];
    }
}
