namespace Day06;
public class Guard
{
    public int[] Location { get; set; } = [];
    public char Direction { get; set; }
    public List<int[]> LocationHistory { get; set; } = [];
    public List<int[]> DistinctLocations
    {
        get
        {
            var locationHistoryDistinct = new List<int[]>();
            foreach (var location in LocationHistory)
            {
                bool exists = false;
                foreach (var history in locationHistoryDistinct)
                {
                    if (history[0] == location[0] && history[1] == location[1])
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                    locationHistoryDistinct.Add(location);
            }
            return locationHistoryDistinct;
        }
    }
    public int[] NextLocation()
    {
        return Direction switch
        {
            '^' => [Location[0] - 1, Location[1]],
            'V' => [Location[0] + 1, Location[1]],
            '<' => [Location[0], Location[1] - 1],
            '>' => [Location[0], Location[1] + 1],
            _ => [],
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
