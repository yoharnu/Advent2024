using Day06;

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
    int solutionOne = PartOne(map);
    Console.WriteLine("Part 1: {0}", solutionOne);
}

static int PartOne(Map map)
{
    return 0;
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
