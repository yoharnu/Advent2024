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
    long startTime = Stopwatch.GetTimestamp();
    int solutionOne = PartOne();
    TimeSpan elapsedTime = Stopwatch.GetElapsedTime(startTime);
    Console.WriteLine("Part 1: {0}\t(completed in {1}s)", solutionOne, elapsedTime.TotalSeconds);
}

static int PartOne()
{
    return 0;
}