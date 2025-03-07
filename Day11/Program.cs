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
    var line = inputFile.ReadLine();
    if (line == null)
    {
        Console.WriteLine(" No input found.");
        return;
    }
    var stones = Array.ConvertAll(line.Split(' '), uint.Parse);
    stopwatch.Stop();
    Console.WriteLine(" Done in {0} seconds", stopwatch.Elapsed.TotalSeconds);

    stopwatch.Restart();
    uint solutionOne = PartOne(stones);
    stopwatch.Stop();
    Console.WriteLine("Part 1: {0}\t(completed in {1}s)", solutionOne, stopwatch.Elapsed.TotalSeconds);

    stopwatch.Restart();
    uint solutionTwo = PartTwo(stones);
    stopwatch.Stop();
    Console.WriteLine("Part 2: {0}\t(completed in {1}s)", solutionTwo, stopwatch.Elapsed.TotalSeconds);
}

static uint PartOne(uint[] stones)
{
    var stoneNumbers = Blink(stones, 25);
    return (uint)stoneNumbers.Length;
}

static uint PartTwo(uint[] stones)
{
    var stoneNumbers = Blink(stones, 75);
    return (uint)stoneNumbers.Length;
}

// blink the stones for the given number of times
static uint[] Blink(uint[] stones, int blinks)
{
    var stoneNumbers = stones;
    for (var i = 0; i < blinks; i++)
    {
        var newStoneNumbers = new uint[stoneNumbers.Length * 2];
        var index = 0;
        foreach (var stone in stoneNumbers)
        {
            var newStones = NewStoneNumber(stone);
            Array.Copy(newStones, 0, newStoneNumbers, index, newStones.Length);
            index += newStones.Length;
        }
        Array.Resize(ref newStoneNumbers, index);
        stoneNumbers = newStoneNumbers;
    }
    return stoneNumbers;
}

// create new stone numbers based on the given stone
static uint[] NewStoneNumber(uint stone)
{
    if (stone == 0) return new uint[] { 1 };

    // if the number of digits in the stone is even, split the stone into two
    if (IsEven(GetUIntDigits(stone)))
    {
        return SplitStone(stone);
    }

    return new uint[] { stone * 2024 };
}

// split the stone into two parts
static uint[] SplitStone(uint stone)
{
    var midpoint = GetUIntDigits(stone) / 2;

    var left = (uint)Math.Truncate(stone / Math.Pow(10, midpoint));
    var right = stone % (uint)Math.Pow(10, midpoint);

    return new uint[] { left, right };
}

// get the number of digits in the given number
static int GetUIntDigits(uint number)
{
    return number.ToString().Length;
}

// check if the given number is even
static bool IsEven(int number)
{
    return number % 2 == 0;
}
