using Business;

namespace Day08;

public class FrequencyGrid : Grid
{
    public FrequencyGrid(List<List<char>> grid, char blankSpace = ' ') : base(grid, blankSpace)
    {
    }
    public FrequencyGrid(List<string> grid, char blankSpace = ' ') : base(ConvertToCharGrid(grid), blankSpace)
    {
    }

    private static List<List<char>> ConvertToCharGrid(List<string> grid) => grid.Select(row => row.ToList()).ToList();

    public List<char> GetDistinctValues()
    {
        var distinctValues = new HashSet<char>();
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                char value = GetValueAt(x, y);
                if (value != BlankSpace)
                {
                    distinctValues.Add(value);
                }
            }
        }
        return distinctValues.ToList();
    }
}
