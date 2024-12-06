namespace Day06;
public class Guard
{
    public (int, int) Location { get; set; }
    public char Direction { get; set; }
    public List<(int, int)> LocationHistory { get; set; } = [];
    public (int, int) NextLocation()
    {
        return Direction switch
        {
            '^' => (Location.Item1 - 1, Location.Item2),
            'V' => (Location.Item1 + 1, Location.Item2),
            '<' => (Location.Item1, Location.Item2 - 1),
            '>' => (Location.Item1, Location.Item2 + 1),
            _ => throw new InvalidOperationException()
        };
    }
    public void TurnRight()
    {
        Direction = Direction switch
        {
            '^' => '>',
            'V' => '<',
            '<' => '^',
            '>' => 'V',
            _ => throw new InvalidOperationException()
        };
    }
}
