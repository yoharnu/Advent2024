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
    int[] safeCount = [0, 0];
    while ((line = inputFile.ReadLine()) != null)
    {
        List<int> report = line.Split().Select(x => int.Parse(x)).ToList();

        bool[] safe = new bool[2];
        safe[0] = PartOne(report);

        if (safe[0])
            safe[1] = true;
        else
            safe[1] = PartTwo(report);

        if (safe[0]) safeCount[0]++;
        if (safe[1]) safeCount[1]++;
    }

    Console.WriteLine("Part 1: {0}", safeCount[0]);
    Console.WriteLine("Part 1: {0}", safeCount[1]);
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