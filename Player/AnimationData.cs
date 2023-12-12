using Constants;

public struct AnimationData
{
    public int StartIdx { get; private set; }
    public int FrameCount { get; private set; }

    public AnimationData(int animInt)
    {
        StartIdx = animInt >> Preference.FrameBitSize;
        FrameCount = animInt & Preference.FrameBitLayer;
    }

    public AnimationData(Anim anim) : this((int)anim)
    {
    }

    public int GetAnimIdxByDirection(Direction dir)
    {
        if (StartIdx == (int)Anim.DIE)
        {
            return StartIdx;
        }

        return StartIdx + FrameCount * (int)dir;
    }
}
