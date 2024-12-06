namespace Day06;
public class Guard
{
    public (int, int) Location { get; set; }
    public char Direction { get; set; }
    public List<(int, int)> LocationHistory { get; set; } = [];
    public List<(int, int)> DistinctLocations
    {
        get
        {
            var locationHistoryDistinct = new List<(int, int)>();
            foreach (var location in LocationHistory)
            {
                bool exists = false;
                foreach (var history in locationHistoryDistinct)
                {
                    if (history == location)
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
