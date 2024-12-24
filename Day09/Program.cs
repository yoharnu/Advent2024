using System.Diagnostics;
using Business;
using Day09;

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
    var lines = inputFile.ReadToEnd();
    DiskMap map = new DiskMap(lines);
    stopwatch.Stop();
    Console.WriteLine(" Done in {0} seconds", stopwatch.Elapsed.TotalSeconds);

    stopwatch.Restart();
    long solutionOne = PartOne(map);
    stopwatch.Stop();
    Console.WriteLine("Part 1: {0}\t(completed in {1}s)", solutionOne, stopwatch.Elapsed.TotalSeconds);
}

static long PartOne(DiskMap map)
{
    map.MoveFiles();
    return map.Checksum();
}