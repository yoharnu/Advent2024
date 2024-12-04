using System.Text.RegularExpressions;

var sampleFile1 = new StreamReader(new FileStream("./input/sample.txt", FileMode.Open));
Console.WriteLine("Sample 1 Solution:");
ReadFile(sampleFile1);

Console.WriteLine();

var sampleFile2 = new StreamReader(new FileStream("./input/sample2.txt", FileMode.Open));
Console.WriteLine("Sample 2 Solution:");
ReadFile(sampleFile2);

Console.WriteLine();

var inputFile = new StreamReader(new FileStream("./input/input.txt", FileMode.Open));
Console.WriteLine("Final Solution:");
ReadFile(inputFile);

static void ReadFile(StreamReader inputFile)
{
    string contents = inputFile.ReadToEnd();
    int solutionOne = PartOne(contents);
    int solutionTwo = PartTwo(contents);

    Console.WriteLine("Part 1: {0}", solutionOne);
    Console.WriteLine("Part 2: {0}", solutionTwo);
}

static int PartOne(string contents)
{
    Regex mul = new Regex("mul\\((\\d{1,3}),(\\d{1,3})\\)");
    int sum = 0;
    var matches = mul.Matches(contents);
    foreach (Match match in matches)
    {
        var groups = match.Groups.Values.Select(x => x.Value).ToList();
        var a = int.Parse(groups[1]);
        var b = int.Parse(groups[2]);
        sum += a * b;
    }
    return sum;
}

static int PartTwo(string contents)
{
    Regex rx = new Regex("(mul)\\((\\d{1,3}),(\\d{1,3})\\)|(do)\\(\\)|(don't)\\(\\)");
    var sum = 0;
    var matches = rx.Matches(contents);
    var enabled = true;
    foreach (Match match in matches)
    {
        var groups = match.Groups.Values.Select(x => x.Value).ToList();
        if (groups[1].Equals("mul") && enabled)
        {
            var a = int.Parse(groups[2]);
            var b = int.Parse(groups[3]);
            sum += a * b;
        }
        else if (groups[4].Equals("do"))
        {
            enabled = true;
        }
        else if (groups[5].Equals("don't"))
        {
            enabled = false;
        }
    }
    return sum;
}