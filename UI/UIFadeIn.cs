using System.Collections;
using UnityEngine;

public class UIFadeIn : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasgroup;

    [SerializeField] private float TimetoFade;
    private IEnumerator FadeInCoroutine()
    {
        canvasgroup.alpha = 1;
        while (canvasgroup.alpha > 0)
        {
            canvasgroup.alpha -= TimetoFade * Time.deltaTime;
            yield return null;
        }
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }
}
