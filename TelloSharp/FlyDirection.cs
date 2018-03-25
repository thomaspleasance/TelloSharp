namespace TelloSharp
{
    public enum FlyDirection
    {
        Up,
        Down,
        Left,
        Right,
        Foward,
        [TelloCommand("back")]
        Backward
    }
}