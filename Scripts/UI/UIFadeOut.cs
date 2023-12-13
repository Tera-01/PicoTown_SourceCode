using System.Collections;
using UnityEngine;

public class UIFadeOut : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasgroup;

    [SerializeField] private float TimetoFade;
    private IEnumerator FadeOutCoroutine()
    {
        while (canvasgroup.alpha < 1)
        {
            canvasgroup.alpha += TimetoFade * Time.deltaTime;
            yield return null;
        }
        GameManager.instance.isTimePaused = false;
    }
    public IEnumerator FadeOutObjectFalse()
    {
        yield return StartCoroutine(FadeOutCoroutine());
        GameManager.instance.isSceneActive = false;
    }
    public void FadeOut()
    {
        GameManager.instance.isTimePaused = true;
        UIManager.instance.GetUI<UIFadeIn>().gameObject.SetActive(false);
        canvasgroup.alpha = 0;
        StartCoroutine(FadeOutObjectFalse());
    }
}
