using Business;
using Day07;
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
    Console.Write("Reading input...");
    long startTime = Stopwatch.GetTimestamp();
    var lines = Helper.ReadAllLines(inputFile);
    TimeSpan elapsedTime = Stopwatch.GetElapsedTime(startTime);
    Console.WriteLine(" Done in {0} seconds", elapsedTime.TotalSeconds);

    startTime = Stopwatch.GetTimestamp();
    long solutionOne = PartOne(lines);
    elapsedTime = Stopwatch.GetElapsedTime(startTime);
    Console.WriteLine("Part 1: {0}\t(completed in {1}s)", solutionOne, elapsedTime.TotalSeconds);

    startTime = Stopwatch.GetTimestamp();
    long solutionTwo = PartTwo(lines);
    elapsedTime = Stopwatch.GetElapsedTime(startTime);
    Console.WriteLine("Part 2: {0}\t(completed in {1}s)", solutionTwo, elapsedTime.TotalSeconds);
}

static long PartOne(List<string> lines)
{
    long sum = 0;
    for (int i = 0; i < lines.Count; i++)
    {
        Debug.WriteLine("Calculating line {0} of {1}...", i + 1, lines.Count);
        (var solution, var values) = ParseLine(lines[i]);
        bool found = FindSolution(values, solution, ["*", "+"]);
        if (found) sum += solution;
    }
    return sum;
}

static long PartTwo(List<string> lines)
{
    long sum = 0;
    for (int i = 0; i < lines.Count; i++)
    {
        Debug.WriteLine("Calculating line {0} of {1}...", i + 1, lines.Count);
        (var solution, var values) = ParseLine(lines[i]);
        bool found = FindSolution(values, solution, ["*", "+", "||"]);
        if (found) sum += solution;
    }
    return sum;
}

static bool FindSolution(List<long> values, long givenSolution, string[] operators)
{
    Equation equation = new(values, operators);
    long x = equation.Calculate();

    if (x == givenSolution)
        return true;

    while (equation.HasNext())
    {
        equation.Next();
        x = equation.Calculate();
        if (x == givenSolution) return true;
    }

    return false;
}

static (long, List<long>) ParseLine(string line)
{
    var values = line.Split(' ');
    if (values.Length > 0)
        values[0] = values[0].Replace(":", "");
    var valueList = values.Select(x => long.Parse(x)).ToList();
    return (valueList.First(), valueList.Skip(1).ToList());
}
