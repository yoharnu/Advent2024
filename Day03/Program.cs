using System.Text.RegularExpressions;

var sampleFile = new StreamReader(new FileStream("./input/sample.txt", FileMode.Open));
Console.WriteLine("Sample Solution:");
ReadFile(sampleFile);

Console.WriteLine();

var inputFile = new StreamReader(new FileStream("./input/input.txt", FileMode.Open));
Console.WriteLine("Final Solution:");
ReadFile(inputFile);

static void ReadFile(StreamReader inputFile)
{
    string contents = inputFile.ReadToEnd();
    int solutionOne = PartOne(contents);

    Console.WriteLine("Part 1: {0}", solutionOne);
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