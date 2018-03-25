namespace TelloSharp
{
    public enum RotateDirection
    {
        [TelloCommand("cw")]
        Clockwise,
        [TelloCommand("ccw")]
        CounterClockwise
    }
}