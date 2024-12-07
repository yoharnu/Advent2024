namespace Day06;
public class Guard((int, int) location, char direction)
{
    private (int, int) location = location;

    public (int, int) Location
    {
        get
        {
            return location;
        }

        set
        {
            LocationHistory.Add(location);
            location = value;
        }
    }
    public char Direction { get; set; } = direction;
    public List<(int, int)> LocationHistory { get; } = [];
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
