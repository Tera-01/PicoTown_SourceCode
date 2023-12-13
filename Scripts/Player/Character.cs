using Constants;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private SpriteAnimation[] skinsSprite = new SpriteAnimation[12];

    private void Start()
    {
        JoystickMovement.Instance.OnMoveStart += Play;
        JoystickMovement.Instance.OnIdle += Idle;

        var body = SkinsItemData.Instance.GetItem(Skins.BODY, 0);
        SetSkins(Skins.BODY, body);

        var top = SkinsItemData.Instance.GetItem(Skins.TOP, 0);
        SetSkins(Skins.TOP, top);

        var pants = SkinsItemData.Instance.GetItem(Skins.PANTS, 0);
        SetSkins(Skins.PANTS, pants);

        var shoes = SkinsItemData.Instance.GetItem(Skins.SHOES, 0);
        SetSkins(Skins.SHOES, shoes);

        var hair = SkinsItemData.Instance.GetItem(Skins.HAIR, 0);
        SetSkins(Skins.HAIR, hair);

        var hat = SkinsItemData.Instance.GetItem(Skins.HAT, 0);
        SetSkins(Skins.HAT, hat);
    }

    public void SetSkins(Skins skins, SkinsItemSO skinsitem)
    {
        skinsSprite[(int)skins].SetSkinsItem(skinsitem);
    }

    public void Play(int enumint)
    {
        Anim animation = (Anim)enumint;
        
        for (int i = 0; i < skinsSprite.Length; i++)
        {
            if (skinsSprite[i] != null)
            {
                skinsSprite[i].Play(animation, PlayerController.Instance.CurDirection);
            }
        }
    }

    public void Idle()
    {
        for (int i = 0; i < skinsSprite.Length; i++)
        {
            if (skinsSprite[i] != null)
            {
                skinsSprite[i].Idle(Anim.WALK, PlayerController.Instance.CurDirection);
            }
        }
    }
}
