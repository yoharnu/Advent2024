namespace Day06;
public class Map
{
    protected List<List<char>> Raw { get; set; }
    public List<(int, int)> ObstacleLocations { get; set; } = [];
    public Guard Guard { get; set; }
    public Map(List<List<char>> map)
    {
        Raw = map;
        char[] guard = ['v', '^', '<', '>'];
        for (int i = 0; i < map.Count; i++)
        {
            for (int j = 0; j < map[i].Count; j++)
            {
                if (map[i][j] == '#')
                {
                    ObstacleLocations.Add((i, j));
                }
                if (guard.Contains(map[i][j]))
                {
                    Guard = new Guard((i, j), map[i][j]);
                }
            }
        }
        if (Guard == null)
            throw new InvalidDataException();
    }

    public bool CanGuardMove()
    {
        return !IsGuardOutOfBounds() && !ObstacleLocations.Contains(Guard.NextLocation());
    }

    public bool IsGuardOutOfBounds()
    {
        return IsOutOfBounds(Guard.CurrentLocation.Coordinates.Item1, Guard.CurrentLocation.Coordinates.Item2);
    }

    public bool IsOutOfBounds(int y, int x)
    {
        var boundsY = Raw.Count - 1;
        var boundsX = Raw[boundsY].Count - 1;
        if (x > boundsX || x < 0 || y > boundsY || y < 0) return true;

        return false;
    }

    public void MoveGuard()
    {
        if (CanGuardMove())
        {
            var nextLocation = Guard.NextLocation();
            if (!IsOutOfBounds(nextLocation.Item1, nextLocation.Item2))
                Guard.UpdateCurrentCoordinates(nextLocation.Item1, nextLocation.Item2);
            else
                Guard.CurrentLocation.Coordinates = nextLocation;
        }
        }

    public Map Clone()
    {
        return new Map(Raw);
    }
}
