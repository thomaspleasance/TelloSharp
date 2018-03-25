namespace TelloSharp
{
    public enum FlipDirection
    {
        [TelloCommand("l")]
        Left,
        [TelloCommand("r")]
        Right,
        [TelloCommand("f")]
        Foward,
        [TelloCommand("b")]
        Back,
        [TelloCommand("bl")]
        Back_Left,
        [TelloCommand("br")]
        Back_Right,
        [TelloCommand("fl")]
        Front_Left,
        [TelloCommand("fr")]
        Front_Right
    }
}