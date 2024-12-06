namespace Day06;
public class Guard
{
    public int[] Location { get; set; } = [];
    public char Direction { get; set; }
    public int[] NextLocation()
    {
        return Direction switch
        {
            '^' => [Location[0] - 1, Location[1]],
            'v' => [Location[0] + 1, Location[1]],
            '<' => [Location[0], Location[1] - 1],
            '>' => [Location[0], Location[1] + 1],
            _ => [],
        };
    }
}
