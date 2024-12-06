namespace Day06;
public class Map
{
    protected List<List<char>> Raw { get; set; }
    public List<int[]> ObstacleLocations { get; set; } = [];
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
                    ObstacleLocations.Add([i, j]);
                }
                if (guard.Contains(map[i][j]))
                {
                    Guard = new Guard { Direction = map[i][j], Location = [i, j] };
                }
            }
        }
        if (Guard == null)
            throw new InvalidDataException();
    }
    public bool GuardCanMove()
    {
        if (ObstacleLocations.Contains(Guard.NextLocation()))
            return false;

        return true;
    }
}
