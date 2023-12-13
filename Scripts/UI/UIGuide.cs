using UnityEngine;

public class UIGuide : MonoBehaviour
{
    void Update()
    {
        GameManager.instance.PauseTime();
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
            GameManager.instance.StartTime();
        }
    }
}
