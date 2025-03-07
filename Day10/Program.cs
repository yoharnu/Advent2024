using System.Diagnostics;
using Business;

Console.WriteLine("Sample Solution:");
using (var sampleFile = new StreamReader(new FileStream("./input/sample.txt", FileMode.Open)))
    ReadFile(sampleFile);

Console.WriteLine();

Console.WriteLine("Solution:");
using (var inputFile = new StreamReader(new FileStream("./input/input.txt", FileMode.Open)))
    ReadFile(inputFile);

static void ReadFile(StreamReader inputFile)
{
    Console.Write("Reading input...");
    var stopwatch = Stopwatch.StartNew();
    var map = new Grid(Helper.ReadAllLines(inputFile));
    stopwatch.Stop();
    Console.WriteLine(" Done in {0} seconds", stopwatch.Elapsed.TotalSeconds);

    stopwatch.Restart();
    long solutionOne = PartOne(map);
    stopwatch.Stop();
    Console.WriteLine("Part 1: {0}\t(completed in {1}s)", solutionOne, stopwatch.Elapsed.TotalSeconds);

    stopwatch.Restart();
    long solutionTwo = PartTwo(map);
    stopwatch.Stop();
    Console.WriteLine("Part 2: {0}\t(completed in {1}s)", solutionTwo, stopwatch.Elapsed.TotalSeconds);
}

static long PartOne(Grid map)
{
    var trailheads = map.FindLocationsWithValue('0');
    var peakSum = 0;
    foreach (var trailhead in trailheads)
    {
        var peaks = FindPeaks(map, trailhead);
        peakSum += peaks.Count;
    }
    return peakSum;
}

static long PartTwo(Grid map)
{
    var trailheads = map.FindLocationsWithValue('0');
    long ratingSum = 0;
    foreach (var trailhead in trailheads)
    {
        var rating = FindTrailheadRating(map, trailhead);
        ratingSum += rating;
    }
    return ratingSum;
}

static HashSet<Grid.Location> FindPeaks(Grid map, Grid.Location trailhead)
{
    var peaks = new HashSet<Grid.Location>();
    var stack = new Stack<Grid.Location>();
    stack.Push(trailhead);
    while (stack.Count > 0)
    {
        var current = stack.Pop();

        if (map.IsOutOfBounds(current))
            continue;

        var currentValue = int.Parse(current.Value.ToString());

        if (currentValue == 9)
        {
            peaks.Add(current);
        }
        else
        {
            var neighbors = GetNeighbors(map, current);
            foreach (var neighbor in neighbors)
            {
                if (int.Parse(neighbor.Value.ToString()) == currentValue + 1)
                {
                    stack.Push(neighbor);
                }
            }
        }
    }
    return peaks;
}

static long FindTrailheadRating(Grid map, Grid.Location trailhead)
{
    long rating = 0;
    var stack = new Stack<Grid.Location>();
    stack.Push(trailhead);
    while (stack.Count > 0)
    {
        var current = stack.Pop();

        if (map.IsOutOfBounds(current))
            continue;

        var currentValue = int.Parse(current.Value.ToString());

        if (currentValue == 9)
        {
            rating++;
        }
        else
        {
            var neighbors = GetNeighbors(map, current);
            foreach (var neighbor in neighbors)
            {
                if (int.Parse(neighbor.Value.ToString()) == currentValue + 1)
                {
                    stack.Push(neighbor);
                }
            }
        }
    }
    return rating;
}

static List<Grid.Location> GetNeighbors(Grid map, Grid.Location location)
{
    var neighbors = new List<Grid.Location>
    {
        location.GetNeighborDown(),
        location.GetNeighborUp(),
        location.GetNeighborLeft(),
        location.GetNeighborRight()
    };

    neighbors.RemoveAll(neighbor => map.IsOutOfBounds(neighbor));

    for (int i = 0; i < neighbors.Count; i++)
    {
        var neighbor = neighbors[i];
        neighbor.Value = map.GetValueAt(neighbor.X, neighbor.Y);
        neighbors[i] = neighbor;
    }

    return neighbors;
}
