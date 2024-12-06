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
                    Guard = new Guard { Direction = map[i][j], Location = (i, j) };
                }
            }
        }
        if (Guard == null)
            throw new InvalidDataException();
    }

    public bool CanGuardMove()
    {
        if (IsGuardOutOfBounds()) return false;

        var nextLocation = Guard.NextLocation();
        foreach (var location in ObstacleLocations)
        {
            if (location == nextLocation) return false;
        }

        return true;
    }

    public bool IsGuardOutOfBounds()
    {
        var y = Raw.Count - 1;
        var x = Raw[y].Count - 1;
        if (Guard.Location.Item1 > y || Guard.Location.Item2 > x || Guard.Location.Item1 < 0 || Guard.Location.Item2 < 0)
            return true;

        return false;
    }

    public void MoveGuard()
    {
        if (CanGuardMove())
        {
            Guard.LocationHistory.Add(Guard.Location);
            Guard.Location = Guard.NextLocation();
        }
    }
}
