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
    public AntennaGrid(List<string> grid, char blankSpace = ' ') : base(grid, blankSpace)
    {
    }

    public List<Location> FindAntiNodes(Location a, Location b)
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

    public List<Location> FindAntiNodesLong(Location a, Location b)
    {
        var result = new List<Location>();

        var stack = new Stack<(Location, Location)>();
        stack.Push((a, b));

        while (stack.Count > 0)
        {
            var (currentA, currentB) = stack.Pop();
            var currentAntinodes = FindAntiNodes(currentA, currentB);

            foreach (var antinode in currentAntinodes)
            {
                if (!result.Contains(antinode))
                {
                    result.Add(antinode);
                    stack.Push((antinode, b));
                    stack.Push((antinode, a));
                }
            }
        }

        return result.ToList();
    }
    public void DisplayGridWithAntiNodes(List<Location> antinodes)
    {
        var gridClone = Clone();

        foreach (var antinode in antinodes)
        {
            if (gridClone.IsOutOfBounds(antinode.X, antinode.Y))
                continue;

            if (gridClone.GetValueAt(antinode.X, antinode.Y) == '.')
            {
                gridClone.SetValueAt(antinode.X, antinode.Y, '#');
            }
        }

        gridClone.Display();
    }

    public void DisplayAntinodesOnly(List<Location> antinodes)
    {
        // create grid the same size as the original grid
        var newGrid = new char[Height, Width];
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                newGrid[i, j] = BlankSpace;
            }
        }

        // for each antinode, set the value in the new grid to '#'
        foreach (var antinode in antinodes)
        {
            if (!IsOutOfBounds(antinode.X, antinode.Y))
            {
                newGrid[antinode.Y, antinode.X] = '#';
            }
        }

        // display the new grid
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                Console.Write(newGrid[i, j]);
            }
            Console.WriteLine();
        }
    }
}
