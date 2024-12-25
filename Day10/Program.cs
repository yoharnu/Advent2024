using System.Diagnostics;
using Business;

Console.WriteLine("Sample Solution:");
using (var sampleFile = new StreamReader(new FileStream("./input/sample.txt", FileMode.Open)))
    ReadFile(sampleFile);

Console.WriteLine();

Console.WriteLine("Final Solution:");
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
}

static long PartOne(Grid map)
{
    var trailheads = map.FindLocationsWithValue('0');
    var peakSum = 0;
    foreach (var trailhead in trailheads)
    {
        HashSet<Grid.Location> peaks = new HashSet<Grid.Location>();
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
                var neighbors = new List<Grid.Location> { current.GetNeighborDown(), current.GetNeighborUp(), current.GetNeighborLeft(), current.GetNeighborRight() };

                for (int i = 0; i < neighbors.Count; i++)
                {
                    var neighbor = neighbors[i];

                    if (map.IsOutOfBounds(neighbor)) continue;

                    neighbor.Value = map.GetValueAt(neighbor.X, neighbor.Y);

                    if (int.Parse(neighbor.Value.ToString()) == int.Parse(current.Value.ToString()) + 1)
                    {
                        stack.Push(neighbor);
                    }
                }
            }
        }

        peakSum += peaks.Count;
    }
    return peakSum;
}
