using System.Collections;
using UnityEngine;

public class UIFadeOut : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasgroup;

    [SerializeField] private float TimetoFade;
    private IEnumerator FadeOutCoroutine()
    {
        canvasgroup.alpha = 0;
        while (canvasgroup.alpha < 1)
        {
            canvasgroup.alpha += TimetoFade * Time.deltaTime;
            yield return null;
        }
    }
    public void FadeOut()
    {
        GameManager.instance.isSceneActive = false;
        StartCoroutine(FadeOutCoroutine());
    }
}
