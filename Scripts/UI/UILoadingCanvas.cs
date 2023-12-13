using UnityEngine;

public class UILoadingCanvas : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke(nameof(OpenSaveFileCanvas), 1f);
        Invoke(nameof(CloseLoadingWindow), 1f);
    }
    public void CloseLoadingWindow()
    {
        gameObject.SetActive(false);
    }
    public void OpenSaveFileCanvas()
    {
        UIManager.instance.GetUI<UISaveFileCanvas>();
    }
}
