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
    var line = inputFile.ReadLine();
    if (line == null)
    {
        Console.WriteLine(" No input found.");
        return;
    }
    var stones = line.Split(' ').Select(long.Parse).ToArray();
    stopwatch.Stop();
    Console.WriteLine(" Done in {0} seconds", stopwatch.Elapsed.TotalSeconds);

    stopwatch.Restart();
    long solutionOne = PartOne(stones);
    stopwatch.Stop();
    Console.WriteLine("Part 1: {0}\t(completed in {1}s)", solutionOne, stopwatch.Elapsed.TotalSeconds);

    stopwatch.Restart();
    long solutionTwo = PartTwo(stones);
    stopwatch.Stop();
    Console.WriteLine("Part 2: {0}\t(completed in {1}s)", solutionTwo, stopwatch.Elapsed.TotalSeconds);
}

static int PartOne(long[] stones)
{
    var stoneNumbers = Blink(stones, 25);
    return stoneNumbers.Length;
}

static int PartTwo(long[] stones)
{
    var stoneNumbers = Blink(stones, 75);
    return stoneNumbers.Length;
}

static long[] Blink(long[] stones, int blinks)
{
    var stoneNumbers = new List<long>(stones);
    for (var i = 0; i < blinks; i++)
    {
        var newStoneNumbers = new List<long>();
        foreach (var stone in stoneNumbers)
        {
            newStoneNumbers.AddRange(NewStoneNumber(stone));
        }
        stoneNumbers = newStoneNumbers;
    }
    return stoneNumbers.ToArray();
}


static long[] NewStoneNumber(long stone)
{
    if (stone == 0) return [1];

    // if the number of digits in the stone is even, split the stone into two
    if (isEven(GetLongDigits(stone)))
    {
        return SplitStone(stone);
    }

    return [stone * 2024];
}

static long[] SplitStone(long stone)
{
    var midpoint = GetLongDigits(stone) / 2;

    var left = (long)Math.Truncate(stone / Math.Pow(10, midpoint));
    var right = stone % (long)Math.Pow(10, midpoint);

    return new long[] { left, right };
}

static int GetLongDigits(long number)
{
    return number.ToString().Length;
}

static bool isEven(long number)
{
    return number % 2 == 0;
}