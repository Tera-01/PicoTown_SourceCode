using Constants;
using System.Collections;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private SkinsItemData skinData;
    [SerializeField] private Character character;
    private SpriteRenderer[] srs;
    private SpriteRenderer sr;

    private int selectedIdx_Start;
    private int selectedIdx_Last;
    private int curIdx;
    private bool isPlayAnim = false;
    private Anim curAnim;
    private Direction _dir;
    private bool _isDirChanged;

    private WaitForSeconds wait = new WaitForSeconds(Preference.FrameSecond);
    void Start()
    {
        PlayerController.Instance.OnDirectionChanged += SetDirection;
        sr = GetComponent<SpriteRenderer>();
        _dir = Direction.DOWN;
        _isDirChanged = false;
    }

    public void SetDirection(Direction dir)
    {
        _dir = dir;
        _isDirChanged = true;
    }

    public void SetSkinsItem(SkinsItemSO skinsitem)
    {
        sprites = skinsitem.Sprites;
    }

    public void Idle(Anim animation, Direction olddir)
    {
        var animData = new AnimationData(animation);
        int animIdx = animData.GetAnimIdxByDirection(_dir);

        selectedIdx_Start = animIdx;

        StopAllCoroutines();
        isPlayAnim = false;

        sr.sprite = sprites[animIdx];
    }

    public void Play(Anim animation, Direction olddir)
    {
        var animData = new AnimationData(animation);
        int animIdx = animData.GetAnimIdxByDirection(_dir);

        selectedIdx_Start = animIdx;
        selectedIdx_Last = animIdx + animData.FrameCount;

        curIdx = selectedIdx_Start;

        if (!(isPlayAnim && curAnim == Anim.WALK && animation == Anim.WALK))
        {
            selectedIdx_Start = animIdx;
            StopAllCoroutines();
            isPlayAnim = false;
            
            if (animation == Anim.WALK)
                StartCoroutine(nameof(PlayWalkAnimation));
            else
                StartCoroutine(nameof(PlayAnimation));
        }
    }

    IEnumerator PlayAnimation()
    {
        isPlayAnim = true;

        for (int curIdx = selectedIdx_Start; curIdx < selectedIdx_Last; curIdx++)
        {
            
            sr.sprite = sprites[curIdx];

            yield return wait;
        }

        character.Idle();
    }

    IEnumerator PlayWalkAnimation()
    {
        isPlayAnim = true;

        while (true)
        {
            if (_isDirChanged)
            {
                var animData = new AnimationData(Anim.WALK);
                int animIdx = animData.GetAnimIdxByDirection(_dir);
                int prevCurIdx = curIdx;
                int prevSelectedIdx_Start = selectedIdx_Start;
                selectedIdx_Start = animIdx;
                selectedIdx_Last = animIdx + animData.FrameCount;
                curIdx = prevCurIdx - prevSelectedIdx_Start + selectedIdx_Start;
            }
            
            sr.sprite = sprites[curIdx++];

            yield return wait;

            if (curIdx >= selectedIdx_Last)
                curIdx = selectedIdx_Start;
        }
    }
}
