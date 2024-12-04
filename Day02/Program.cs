var sampleFile = new StreamReader(new FileStream("./input/sample.txt", FileMode.Open));
Console.WriteLine("Sample Solution:");
ReadFile(sampleFile);

Console.WriteLine();

var inputFile = new StreamReader(new FileStream("./input/input.txt", FileMode.Open));
Console.WriteLine("Final Solution:");
ReadFile(inputFile);

static void ReadFile(StreamReader inputFile)
{
    string? line;
    var safeCountOne = 0;
    var safeCountTwo = 0;
    while ((line = inputFile.ReadLine()) != null)
    {
        List<int> report = line.Split().Select(x => int.Parse(x)).ToList();

        bool safeOne = PartOne(report);
        bool safeTwo;

        if (safeOne)
            safeTwo = true;
        else
            safeTwo = PartTwo(report);

        if (safeOne) safeCountOne++;
        if (safeTwo) safeCountTwo++;
    }

    Console.WriteLine("Part 1: {0}", safeCountOne);
    Console.WriteLine("Part 1: {0}", safeCountTwo);
}

static bool PartOne(List<int> report)
{
    var safe = true;
    var increaseCount = 0;
    var decreaseCount = 0;

    for (int i = 1; i < report.Count; i++)
    {
        var diff = Math.Abs(report[i - 1] - report[i]);
        if (diff < 1 || diff > 3)
        {
            safe = false;
        }
        if (report[i - 1] < report[i])
        {
            increaseCount++;
        }
        else if (report[i - 1] > report[i])
        {
            decreaseCount++;
        }
    }

    if (increaseCount > 0 && decreaseCount > 0)
        safe = false;

    return safe;
}

static bool PartTwo(List<int> report)
{
    for (int i = 0; i < report.Count; i++)
    {
        List<int> dampenedReport = new List<int>(report);
        dampenedReport.RemoveAt(i);
        if (PartOne(dampenedReport))
            return true;
    }
    return false;
}