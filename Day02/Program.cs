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
    var safeCount = 0;
    while ((line = inputFile.ReadLine()) != null)
    {
        var safe = true;
        var increaseCount = 0;
        var decreaseCount = 0;
        List<int> report = line.Split().Select(x => int.Parse(x)).ToList();
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

        if (safe)
            safeCount++;
    }

    Console.WriteLine("Part 1: {0}", safeCount);
}