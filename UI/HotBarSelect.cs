using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HotBarSelect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UIHotBar hotbar;
    [SerializeField] private Image highlight;
    public int Index;

    private void Awake()
    {
        hotbar.OnSelectedChanged += Highlight;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        hotbar.SelectedSlot = Index;
    }

    public void Highlight(int index)
    {
        Color highlightOn = highlight.color;
        highlightOn.a = Index == index ? 0.4f : 0;
        highlight.color = highlightOn;
    }
}
