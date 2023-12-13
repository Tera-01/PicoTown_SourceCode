using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private Dictionary<string, GameObject> UIs = new();

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
        DontDestroyOnLoad(instance);
    }
    
    public T GetUI<T>() where T : MonoBehaviour
    {
        string uiName = typeof(T).Name;
        T comp;
        if (UIs.TryGetValue(uiName, out GameObject go) && go != null)
        {
            go.gameObject.SetActive(true);
            comp = UIs[uiName].GetComponent<T>();
        }
        else
        {
            if (UIs.TryGetValue(uiName, out GameObject test))
                UIs.Remove(uiName);
            comp = CreateUI<T>();
        }
        return comp;
    }
    
    public T CreateUI<T>() where T : MonoBehaviour
    {
        string uiName = typeof(T).Name;
        GameObject obj = Resources.Load<GameObject>("Prefabs/UI/" + uiName);
        GameObject ui = Instantiate(obj);

        T comp = ui.GetComponent<T>();
        if (comp == null)
        {
            comp = ui.AddComponent<T>();
        }
        UIs.Add(uiName, comp.gameObject);
        return comp;
    }
}
