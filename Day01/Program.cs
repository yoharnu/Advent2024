List<int> listA = [];
List<int> listB = [];
var sr = new StreamReader(new FileStream("./input/input.txt", FileMode.Open));

string? line;
while ((line = sr.ReadLine()) != null)
{
    var split = line.Split();
    listA.Add(int.Parse(split.First()));
    listB.Add(int.Parse(split.Last()));
}
listA.Sort();
listB.Sort();

int solutionOne = PartOne(listA, listB);
int solutionTwo = PartTwo(listA, listB);

Console.WriteLine("Part 1: {0}", solutionOne);
Console.WriteLine("Part 2: {0}", solutionTwo);

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
        if (mapB.ContainsKey(a))
            sum += a * mapB[a];
    }
    return sum;
}