namespace Day06;
public class Guard((int, int) location, char direction)
{
    public class Location
    {
        public (int, int) Coordinates { get; set; }
        public char Direction { get; set; }

        public Location Clone()
    {
            return new Location { Direction = Direction, Coordinates = Coordinates };
        }

        public override bool Equals(object? obj)
        {
            return obj is Location location &&
                   Coordinates == location.Coordinates &&
                   Direction == location.Direction;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Coordinates, Direction);
        }
    }

    public Location CurrentLocation { get; } = new Location { Coordinates = location, Direction = direction };

    public void UpdateCurrentCoordinates(int y, int x)
    {
        LocationHistory.Add(CurrentLocation.Clone());
        CurrentLocation.Coordinates = (y, x);
    }

    public void UpdateCurrentDirection(char direction)
    {
        LocationHistory.Add(CurrentLocation.Clone());
        CurrentLocation.Direction = direction;
    }

    public List<Location> LocationHistory { get; } = [];

    public (int, int) NextLocation()
    {
        var location = CurrentLocation.Coordinates;
        return CurrentLocation.Direction switch
        {
            '^' => (location.Item1 - 1, location.Item2),
            'V' => (location.Item1 + 1, location.Item2),
            '<' => (location.Item1, location.Item2 - 1),
            '>' => (location.Item1, location.Item2 + 1),
            _ => throw new InvalidOperationException()
        };
    }
    public void TurnRight()
    {
        UpdateCurrentDirection(CurrentLocation.Direction switch
        {
            '^' => '>',
            'V' => '<',
            '<' => '^',
            '>' => 'V',
            _ => throw new InvalidOperationException()
        });
    }
}
