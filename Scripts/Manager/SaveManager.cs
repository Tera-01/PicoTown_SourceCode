using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public static readonly int maxSaveDataCount = 3;
    public List<SaveData> saveDataList = new (maxSaveDataCount);
    public string path;
    public int currentSaveFile = -1;
    public bool[] isFullSaveFile;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        path = $"{Application.persistentDataPath}/save";
        isFullSaveFile = new bool[3];
    }

    private void Start()
    {
        GetSaveFiles();
    }

    public int GetEmptySaveFile()
    {
        for (int i = 0; i < maxSaveDataCount; i++)
        {
            if (isFullSaveFile[i] == false)
                return i;
        }

        return -1;
    }

    public void GetSaveFiles()
    {
        for (int i = 0; i < maxSaveDataCount; i++)
        {
            if (File.Exists($"{path}{i}"))
            {
                SaveData curSaveFile = new SaveData();
                string rawData = File.ReadAllText($"{path}{i}");
                curSaveFile = JsonConvert.DeserializeObject<SaveData>(rawData);
                saveDataList.Add(curSaveFile);
                isFullSaveFile[i] = true;
            }
            else
            {
                isFullSaveFile[i] = false;
            }
        }
    }

    public void SetSaveFile(SaveData data)
    {
        string rawData = JsonConvert.SerializeObject(data);
        File.WriteAllText($"{path}{currentSaveFile}", rawData);
    }
}
