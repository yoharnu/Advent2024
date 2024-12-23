using Business;

namespace Day08;

public class FrequencyGrid : Grid
{
    public FrequencyGrid(List<List<char>> grid) : base(grid)
    {
    }
    public FrequencyGrid(List<string> grid) : base(ConvertToCharGrid(grid))
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
                distinctValues.Add(GetValueAt(x, y));
            }
        }
        return distinctValues.ToList();
    }
}
