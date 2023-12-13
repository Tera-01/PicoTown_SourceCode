namespace Constants
{
    public class Preference
    {
        public static readonly int FrameBitSize = 5;
        public static readonly int FrameBitLayer = (1 << FrameBitSize) - 1; // 31

        public static readonly float FrameSecond = 0.1f;
    }
        public enum Skins
        {
            BODY,
            EYES,
            BLUSH,
            LIPSTICK,
            TOP,
            PANTS,
            SHOES,
            HAIR,
            BEARD,
            GLASSES,
            HAT
        }
        public enum Anim
        {
            WALK = 8,           // 0 , 8
            JUMP = 1029,        // 32 , 5
            PICK_UP = 1669,     // 52 , 5
            CARRY = 2312,       // 72 , 8
            SWORD = 3332,       // 104, 4
            BLOCK = 3841,       // 120, 1
            HURT = 3969,        // 124, 1
            DIE = 4098,         // 128, 2
            PICKAXE = 4165,     // 130, 5
            AXE = 4805,         // 150, 5
            WATER = 5442,       // 170, 2
            HOE = 5701,         // 178, 5
            FISHING = 6341      // 198, 5
        }

        public enum Direction
        {
            DOWN,
            UP,
            RIGHT,
            LEFT
        }
}
