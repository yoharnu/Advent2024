using Business;

namespace Day08;

public class AntennaGrid : Grid
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AntennaGrid"/> class with the specified grid and optional blank space character.
    /// </summary>
    /// <param name="grid">The grid of characters.</param>
    /// <param name="blankSpace">The character to be treated as a blank space. Default is ' '.</param>
    public AntennaGrid(List<List<char>> grid, char blankSpace = ' ') : base(grid, blankSpace)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AntennaGrid"/> class with the specified grid and optional blank space character.
    /// </summary>
    /// <param name="grid">The list of strings representing the grid.</param>
    /// <param name="blankSpace">The character to be treated as a blank space. Default is ' '.</param>
    public AntennaGrid(List<string> grid, char blankSpace = ' ') : base(ConvertToCharGrid(grid), blankSpace)
    {
    }

    /// <summary>
    /// Converts a list of strings to a list of list of characters.
    /// </summary>
    /// <param name="grid">The list of strings representing the grid.</param>
    /// <returns>A list of list of characters representing the grid.</returns>
    private static List<List<char>> ConvertToCharGrid(List<string> grid) => grid.Select(row => row.ToList()).ToList();

    public IEnumerable<Location> FindAntiNodes(Location a, Location b)
    {
        if (a == b)
            return [];

        var xDistance = a.X - b.X;
        var yDistance = a.Y - b.Y;

        var antinodes = new List<Location>(2);

        int newX = a.X + xDistance;
        int newY = a.Y + yDistance;
        var antinode = new Location(newX, newY, BlankSpace);
        if (!IsOutOfBounds(antinode))
            antinodes.Add(antinode);

        xDistance = b.X - a.X;
        yDistance = b.Y - a.Y;

        newX = b.X + xDistance;
        newY = b.Y + yDistance;
        antinode = new Location(newX, newY, BlankSpace);
        if (!IsOutOfBounds(antinode))
            antinodes.Add(antinode);

        return antinodes;
    }
}
