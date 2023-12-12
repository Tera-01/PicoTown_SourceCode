using UnityEngine;

[CreateAssetMenu(fileName = "skinsItem", menuName = "Scriptable Object/SkinsItem")]
public class SkinsItemSO : ScriptableObject
{
    [SerializeField] private Sprite[] sprites;
    public Sprite[] Sprites => sprites;
}
