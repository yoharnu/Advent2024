var sampleFile = new StreamReader(new FileStream("./input/sample.txt", FileMode.Open));
Console.WriteLine("Sample Solution:");
ReadFile(sampleFile);

Console.WriteLine();

var inputFile = new StreamReader(new FileStream("./input/input.txt", FileMode.Open));
Console.WriteLine("Final Solution:");
ReadFile(inputFile);

static void ReadFile(StreamReader inputFile)
{
    List<List<char>> wordSearch = GetWordSearch(inputFile);
    var countOne = PartOne(wordSearch);

    Console.WriteLine("Part 1: {0}", countOne);
}

static int PartOne(List<List<char>> wordSearch)
{
    var count = 0;

    return count;
}

static List<List<char>> GetWordSearch(StreamReader inputFile)
{
    List<List<char>> wordSearch = new();
    string? line;
    while ((line = inputFile.ReadLine()) != null)
    {
        wordSearch.Add(line.ToList());
    }
    return wordSearch;
}
