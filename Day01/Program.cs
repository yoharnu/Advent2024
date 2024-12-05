var sampleFile = new StreamReader(new FileStream("./input/sample.txt", FileMode.Open));
Console.WriteLine("Sample Solution:");
ReadFile(sampleFile);

Console.WriteLine();

var inputFile = new StreamReader(new FileStream("./input/input.txt", FileMode.Open));
Console.WriteLine("Final Solution:");
ReadFile(inputFile);


static int PartOne(List<int> listA, List<int> listB)
{
    var sum = 0;
    for (int i = 0; i < Math.Min(listA.Count, listB.Count); i++)
    {
        sum += Math.Abs(listA[i] - listB[i]);
    }

    return sum;
}
static int PartTwo(List<int> listA, List<int> listB)
{
    var sum = 0;
    var mapB = listB.Distinct().ToDictionary(x => x, x => listB.Count(y => y == x));
    foreach (int a in listA)
    {
        if (mapB.TryGetValue(a, out int value))
            sum += a * value;
    }
    return sum;
}

static void ReadFile(StreamReader inputFile)
{
    List<int> listA = [];
    List<int> listB = [];

    string? line;
    while ((line = inputFile.ReadLine()) != null)
    {
        var split = line.Split();
        listA.Add(int.Parse(split.First()));
        listB.Add(int.Parse(split.Last()));
    }
    listA.Sort();
    listB.Sort();

    int solutionOne = PartOne(listA, listB);
    Console.WriteLine("Part 1: {0}", solutionOne);

    int solutionTwo = PartTwo(listA, listB);
    Console.WriteLine("Part 2: {0}", solutionTwo);
}