using System.Diagnostics;
using Business;
using Day08;

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
    var lines = Helper.ReadAllLines(inputFile);
    var grid = new AntennaGrid(lines, '.');
    stopwatch.Stop();
    Console.WriteLine(" Done in {0} seconds", stopwatch.Elapsed.TotalSeconds);

    stopwatch.Restart();
    long solutionOne = PartOne(grid);
    stopwatch.Stop();
    Console.WriteLine("Part 1: {0}\t(completed in {1}s)", solutionOne, stopwatch.Elapsed.TotalSeconds);

    stopwatch.Restart();
    long solutionTwo = PartTwo(grid);
    stopwatch.Stop();
    Console.WriteLine("Part 2: {0}\t(completed in {1}s)", solutionTwo, stopwatch.Elapsed.TotalSeconds);
}

static int PartOne(AntennaGrid grid)
{
    var distinct = grid.GetDistinctValues();
    List<Grid.Location> antinodes = new List<Grid.Location>();
    foreach (var value in distinct)
    {
        var allValues = grid.FindLocationsWithValue(value);
        foreach (var location1 in allValues)
        {
            foreach (var location2 in allValues)
            {
                if (location1 == location2)
                    continue;
                antinodes.AddRange(grid.FindAntiNodes(location1, location2));
            }
        }
    }
    return antinodes.Distinct().Count();
}

static int PartTwo(AntennaGrid grid)
{
    var distinct = grid.GetDistinctValues();
    List<Grid.Location> antinodes = new List<Grid.Location>();
    foreach (var value in distinct)
    {
        var allValues = grid.FindLocationsWithValue(value);
        foreach (var location1 in allValues)
        {
            foreach (var location2 in allValues)
            {
                if (location1 == location2)
                    continue;
                antinodes.AddRange(grid.FindAntiNodesLong(location1, location2));
            }
        }
    }
    //grid.DisplayGridWithAntiNodes(antinodes);
    grid.DisplayAntinodesOnly(antinodes);
    return antinodes.Distinct().Count();
}