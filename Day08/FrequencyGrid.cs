using Business;

namespace Day08;

public class FrequencyGrid : Grid
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrequencyGrid"/> class with the specified grid and optional blank space character.
    /// </summary>
    /// <param name="grid">The grid of characters.</param>
    /// <param name="blankSpace">The character to be treated as a blank space. Default is ' '.</param>
    public FrequencyGrid(List<List<char>> grid, char blankSpace = ' ') : base(grid, blankSpace)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FrequencyGrid"/> class with the specified grid and optional blank space character.
    /// </summary>
    /// <param name="grid">The list of strings representing the grid.</param>
    /// <param name="blankSpace">The character to be treated as a blank space. Default is ' '.</param>
    public FrequencyGrid(List<string> grid, char blankSpace = ' ') : base(ConvertToCharGrid(grid), blankSpace)
    {
    }

    /// <summary>
    /// Converts a list of strings to a list of list of characters.
    /// </summary>
    /// <param name="grid">The list of strings representing the grid.</param>
    /// <returns>A list of list of characters representing the grid.</returns>
    private static List<List<char>> ConvertToCharGrid(List<string> grid) => grid.Select(row => row.ToList()).ToList();

    /// <summary>
    /// Gets the distinct values from the grid, excluding the blank space character.
    /// </summary>
    /// <returns>A list of distinct characters present in the grid, excluding the blank space character.</returns>
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
