using System.Collections;
using UnityEngine;

public class UIFadeIn : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasgroup;

    [SerializeField] private float TimetoFade;
    public IEnumerator FadeInCoroutine()
    {
        while (canvasgroup.alpha > 0)
        {
            canvasgroup.alpha -= TimetoFade * Time.deltaTime;
            yield return null;
        }
        GameManager.instance.isTimePaused = false;
    }
    public IEnumerator FadeInObjectFalse()
    {
        yield return StartCoroutine(FadeInCoroutine());
    }

    public void FadeIn()
    {
        GameManager.instance.isTimePaused = true;
        UIManager.instance.GetUI<UIFadeOut>().gameObject.SetActive(false);
        canvasgroup.alpha = 1;
        StartCoroutine(FadeInObjectFalse());
    }
}
