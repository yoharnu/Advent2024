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
    var countTwo = PartTwo(wordSearch);

    Console.WriteLine("Part 1: {0}", countOne);
    Console.WriteLine("Part 2: {0}", countTwo);
}

static int PartOne(List<List<char>> wordSearch)
{
    var count = 0;
    count += CountAllDirections(wordSearch, "XMAS");
    return count;
}

static int PartTwo(List<List<char>> wordSearch)
{
    var count = 0;
    count += CountX(wordSearch);
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

static int CountAllDirections(List<List<char>> wordSearch, string word)
{
    var count = 0;
    for (int i = 0; i < wordSearch.Count; i++)
        for (int j = 0; j < wordSearch[i].Count; j++)
        {
            if (!wordSearch[i][j].Equals(word[0]))
                continue;

            string right = wordSearch[i][j].ToString();
            string left = wordSearch[i][j].ToString();
            string up = wordSearch[i][j].ToString();
            string down = wordSearch[i][j].ToString();
            string upRight = wordSearch[i][j].ToString();
            string downRight = wordSearch[i][j].ToString();
            string upLeft = wordSearch[i][j].ToString();
            string downLeft = wordSearch[i][j].ToString();

            for (int k = 1; k < word.Length; k++)
            {
                if (j + k < wordSearch[i].Count)
                {
                    right += wordSearch[i][j + k];
                    if (i - k >= 0)
                        upRight += wordSearch[i - k][j + k];
                    if (i + k < wordSearch.Count)
                        downRight += wordSearch[i + k][j + k];
                }
                if (j - k >= 0)
                {
                    left += wordSearch[i][j - k];
                    if (i - k >= 0)
                        upLeft += wordSearch[i - k][j - k];
                    if (i + k < wordSearch.Count)
                        downLeft += wordSearch[i + k][j - k];
                }
                if (i - k >= 0)
                    up += wordSearch[i - k][j];

                if (i + k < wordSearch.Count)
                    down += wordSearch[i + k][j];
            }

            if (word.Equals(right))
                count++;
            if (word.Equals(left))
                count++;
            if (word.Equals(up))
                count++;
            if (word.Equals(down))
                count++;
            if (word.Equals(upRight))
                count++;
            if (word.Equals(downRight))
                count++;
            if (word.Equals(upLeft))
                count++;
            if (word.Equals(downLeft))
                count++;
        }
    return count;
}

static int CountX(List<List<char>> wordSearch)
{
    var count = 0;
    for (int i = 1; i < wordSearch.Count - 1; i++)
        for (int j = 1; j < wordSearch[i].Count - 1; j++)
        {
            if (!wordSearch[i][j].Equals('A'))
                continue;

            if ((wordSearch[i - 1][j - 1].Equals('M') && wordSearch[i + 1][j + 1].Equals('S')) || (wordSearch[i - 1][j - 1].Equals('S') && wordSearch[i + 1][j + 1].Equals('M')))
                if ((wordSearch[i + 1][j - 1].Equals('M') && wordSearch[i - 1][j + 1].Equals('S')) || (wordSearch[i + 1][j - 1].Equals('S') && wordSearch[i - 1][j + 1].Equals('M')))
                    count++;
        }

    return count;
}