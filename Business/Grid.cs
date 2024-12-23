using System.Collections;

namespace Business;

/// <summary>
/// Represents a grid of characters.
/// </summary>
public class Grid : IEnumerable<Grid.Location>
{
    private readonly char[] _raw;
    public int Width { get; }
    public int Height { get; }
    public char BlankSpace { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Grid"/> class with the specified grid and optional blank space character.
    /// </summary>
    /// <param name="grid">The grid of characters.</param>
    /// <param name="blankSpace">The character to be treated as a blank space. Default is ' '.</param>
    /// <exception cref="ArgumentException">Thrown when the grid is null or empty.</exception>
    public Grid(List<List<char>> grid, char blankSpace = ' ')
    {
        if (grid == null || grid.Count == 0 || grid[0].Count == 0)
        {
            throw new ArgumentException("Grid cannot be null or empty.");
        }

        Height = grid.Count;
        Width = grid[0].Count;
        _raw = new char[Width * Height];
        BlankSpace = blankSpace;

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                _raw[y * Width + x] = grid[y][x];
            }
        }
    }

    /// <summary>
    /// Represents a location within the grid.
    /// </summary>
    public struct Location
    {
        public (int, int) Coordinates { get; set; }
        public char Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> struct with the specified coordinates and value.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="value">The value at the location.</param>
        public Location(int x, int y, char value)
        {
            Coordinates = (x, y);
            Value = value;
        }

        /// <summary>
        /// Calculates the Manhattan distance to another location.
        /// </summary>
        /// <param name="other">The other location.</param>
        /// <returns>The Manhattan distance to the other location.</returns>
        public int DistanceTo(Location other)
        {
            return Math.Abs(Coordinates.Item1 - other.Coordinates.Item1) + Math.Abs(Coordinates.Item2 - other.Coordinates.Item2);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is Location other)
            {
                return Coordinates == other.Coordinates;
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Coordinates.GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Location: ({Coordinates.Item1}, {Coordinates.Item2}), Value: {Value}";
        }

        /// <summary>
        /// Moves the location up.
        /// </summary>
        public void MoveUp() => Coordinates = (Coordinates.Item1, Coordinates.Item2 - 1);

        /// <summary>
        /// Moves the location down.
        /// </summary>
        public void MoveDown() => Coordinates = (Coordinates.Item1, Coordinates.Item2 + 1);

        /// <summary>
        /// Moves the location left.
        /// </summary>
        public void MoveLeft() => Coordinates = (Coordinates.Item1 - 1, Coordinates.Item2);

        /// <summary>
        /// Moves the location right.
        /// </summary>
        public void MoveRight() => Coordinates = (Coordinates.Item1 + 1, Coordinates.Item2);

        /// <summary>
        /// Gets the neighboring location above.
        /// </summary>
        /// <returns>The neighboring location above.</returns>
        public Location GetNeighborUp() => new Location(Coordinates.Item1, Coordinates.Item2 - 1, Value);

        /// <summary>
        /// Gets the neighboring location below.
        /// </summary>
        /// <returns>The neighboring location below.</returns>
        public Location GetNeighborDown() => new Location(Coordinates.Item1, Coordinates.Item2 + 1, Value);

        /// <summary>
        /// Gets the neighboring location to the left.
        /// </summary>
        /// <returns>The neighboring location to the left.</returns>
        public Location GetNeighborLeft() => new Location(Coordinates.Item1 - 1, Coordinates.Item2, Value);

        /// <summary>
        /// Gets the neighboring location to the right.
        /// </summary>
        /// <returns>The neighboring location to the right.</returns>
        public Location GetNeighborRight() => new Location(Coordinates.Item1 + 1, Coordinates.Item2, Value);
    }

    /// <summary>
    /// Checks if the specified coordinates are out of bounds.
    /// </summary>
    /// <param name="x">The x-coordinate.</param>
    /// <param name="y">The y-coordinate.</param>
    /// <returns><c>true</c> if the coordinates are out of bounds; otherwise, <c>false</c>.</returns>
    public bool IsOutOfBounds(int x, int y)
    {
        return x < 0 || x >= Width || y < 0 || y >= Height;
    }

    /// <summary>
    /// Gets the value at the specified coordinates.
    /// </summary>
    /// <param name="x">The x-coordinate.</param>
    /// <param name="y">The y-coordinate.</param>
    /// <returns>The value at the specified coordinates.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the coordinates are out of bounds.</exception>
    public char GetValueAt(int x, int y)
    {
        if (IsOutOfBounds(x, y))
        {
            throw new ArgumentOutOfRangeException("Coordinates are out of bounds.");
        }
        return _raw[y * Width + x];
    }

    /// <summary>
    /// Finds all locations with the specified value.
    /// </summary>
    /// <param name="value">The value to search for.</param>
    /// <returns>A list of locations with the specified value.</returns>
    public List<Location> FindLocationsWithValue(char value)
    {
        var locations = new List<Location>();
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (_raw[y * Width + x] == value)
                {
                    locations.Add(new Location(x, y, value));
                }
            }
        }
        return locations;
    }

    /// <summary>
    /// Displays the grid.
    /// </summary>
    public void Display()
    {
        for (int y = 0; y < Height; y++)
        {
            var row = new Span<char>(_raw, y * Width, Width);
            Console.WriteLine(string.Join(" ", row.ToArray()));
        }
    }

    /// <summary>
    /// Creates a clone of the grid.
    /// </summary>
    /// <returns>A new <see cref="Grid"/> instance that is a clone of the current grid.</returns>
    public Grid Clone()
    {
        var newGrid = new char[_raw.Length];
        Array.Copy(_raw, newGrid, _raw.Length);
        return new Grid(newGrid, Width, Height, BlankSpace);
    }

    // Private constructor for cloning
    private Grid(char[] raw, int width, int height, char blankSpace)
    {
        _raw = raw;
        Width = width;
        Height = height;
        BlankSpace = blankSpace;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the grid.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the grid.</returns>
    public IEnumerator<Location> GetEnumerator()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                yield return new Location(x, y, _raw[y * Width + x]);
            }
        }
    }

    /// <summary>
    /// Returns an enumerator that iterates through the grid.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the grid.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
