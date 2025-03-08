using Business;
using System.Diagnostics;

Console.WriteLine("Sample Solution:");
using (var sampleFile = new StreamReader(new FileStream("./input/sample.txt", FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan)))
    ReadFile(sampleFile);

Console.WriteLine();

Console.WriteLine("Solution:");
using (var inputFile = new StreamReader(new FileStream("./input/input.txt", FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan)))
    ReadFile(inputFile);

static void ReadFile(StreamReader inputFile)
{
    Console.Write("Reading input...");
    var stopwatch = Stopwatch.StartNew();
    var plotMap = new Grid(Helper.ReadAllLines(inputFile));
    stopwatch.Stop();
    Console.WriteLine(" Done in {0} seconds", stopwatch.Elapsed.TotalSeconds);

    stopwatch.Restart();
    uint solutionOne = PartOne(plotMap);
    stopwatch.Stop();
    Console.WriteLine("Part 1: {0}\t(completed in {1}s)", solutionOne, stopwatch.Elapsed.TotalSeconds);
}
static uint PartOne(Grid plotMap)
{
    var plotSum = 0u;

    return plotSum;
}