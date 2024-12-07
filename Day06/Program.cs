using Day06;
using System.Diagnostics;

Console.WriteLine("Sample Solution:");
using (var sampleFile = new StreamReader(new FileStream("./input/sample.txt", FileMode.Open)))
    ReadFile(sampleFile);

Console.WriteLine();

Console.WriteLine("Final Solution:");
using (var inputFile = new StreamReader(new FileStream("./input/input.txt", FileMode.Open)))
    ReadFile(inputFile);

static void ReadFile(StreamReader inputFile)
{
    Map map = GetMap(inputFile);
    long startTime = Stopwatch.GetTimestamp();
    int solutionOne = PartOne(map);
    TimeSpan elapsedTime = Stopwatch.GetElapsedTime(startTime);
    Console.WriteLine("Part 1: {0}\t(completed in {1}s)", solutionOne, elapsedTime.TotalSeconds);

    startTime = Stopwatch.GetTimestamp();
    var solutionTwo = PartTwo(map);
    elapsedTime = Stopwatch.GetElapsedTime(startTime);
    Console.WriteLine("Part 2: {0}\t(completed in {1}s)", solutionTwo.Count(), elapsedTime.TotalSeconds);
}

static int PartOne(Map map)
{
    return map.GuardPath.Select(x => x.Coordinates).Where(x => !map.IsOutOfBounds(x.Item1, x.Item2)).Distinct().Count();
}

static List<(int, int)> PartTwo(Map map)
{
    var guardPath = map.GuardPath;
    var potentialObstacles = map.GuardPath.Select(x => x.Coordinates).Where(x => !map.IsOutOfBounds(x.Item1, x.Item2)).Distinct();

    var successfulObstacles = new List<(int, int)>();
    foreach (var obstacleLocation in potentialObstacles)
    {
        var y = obstacleLocation.Item1;
        var x = obstacleLocation.Item2;
        if (map.IsOutOfBounds(y, x))
            continue;
        var objectAtLocation = map.GetAtLocation(y, x);
        if (objectAtLocation == Map.ObjectType.Nothing)
        {
            if (getsStuck(map, y, x))
                successfulObstacles.Add(obstacleLocation);
        }
    }

    return successfulObstacles;
}

static bool getsStuck(Map map, int y, int x)
{
    var newMap = map.Clone();
    newMap.ObstacleLocations.Add((y, x));
    while (!newMap.IsGuardOutOfBounds())
    {
        while (newMap.CanGuardMove())
        {
            newMap.MoveGuard();
            if (newMap.Guard.IsStuck())
                return true;
        }
        newMap.Guard.TurnRight();
        if (newMap.Guard.IsStuck())
            return true;
    }
    return false;
}

static Map GetMap(StreamReader inputFile)
{
    List<List<char>> map = [];
    string? line;
    while ((line = inputFile.ReadLine()) != null)
    {
        map.Add(line.ToList());
    }
    return new Map(map);
}
