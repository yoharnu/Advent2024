using Business;

Console.WriteLine("Sample Solution:");
using (var sampleFile = new StreamReader(new FileStream("./input/sample.txt", FileMode.Open)))
    ReadFile(sampleFile);

Console.WriteLine();

Console.WriteLine("Final Solution:");
using (var inputFile = new StreamReader(new FileStream("./input/input.txt", FileMode.Open)))
    ReadFile(inputFile);

static void ReadFile(StreamReader inputFile)
{
    var solutionOne = PartOne(inputFile);
    Console.WriteLine("Part 1: {0}", solutionOne);
}

static int PartOne(StreamReader inputFile)
{
    string? line;
    List<string> rulesLines = new();
    while ((line = inputFile.ReadLine()) != null)
    {
        if (line.Equals(""))
            break;

        rulesLines.Add(line);
    }
    var rules = rulesLines.Select(x => x.Split('|').Select(y => int.Parse(y)).ToArray()).ToList();
    var updates = Helper.ReadAllLines(inputFile).Select(x => x.Split(',').Select(y => int.Parse(y)).ToList()).ToList();
    //var sortedUpdates = updates.Select(x => SortPages(x, rules)).ToList();
    var sortedUpdates = updates.Where(x => IsUpdateSorted(x, rules));
    var middlePages = sortedUpdates.Select(x => GetMiddlePage(x));
    return middlePages.Sum();
}

static bool IsUpdateSorted(List<int> pages, List<int[]> rules)
{
    for (int i = 0; i < pages.Count; i++)
        for (int j = i + 1; j < pages.Count; j++)
            foreach (var rule in rules)
                if (rule[0] == pages[j] && rule[1] == pages[i])
                    return false;

    return true;
}

static List<int> SortPages(List<int> pages, List<int[]> rules)
{
    var sortedPages = new List<int>(pages);
    for (int i = 0; i < sortedPages.Count; i++)
        for (int j = i + 1; j < sortedPages.Count; j++)
        {
            foreach (var rule in rules)
            {
                if (rule[0] == sortedPages[j] && rule[1] == sortedPages[i])
                {
                    var temp = sortedPages[i];
                    sortedPages[i] = sortedPages[j];
                    sortedPages[j] = temp;
                }
            }
        }
    return sortedPages;
}

static int GetMiddlePage(List<int> pages)
{
    var x = (int)Math.Ceiling(pages.Count / 2.0);
    return pages[x - 1];
}