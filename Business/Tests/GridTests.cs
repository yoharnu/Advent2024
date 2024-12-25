using Xunit;
using Business;
using System.Collections.Generic;

public class GridTests
{
    [Fact]
    public void Constructor_ValidGrid_InitializesCorrectly()
    {
        // Arrange
        var gridData = new List<List<char>>
        {
            new List<char> { 'A', 'B', 'C' },
            new List<char> { 'D', 'E', 'F' },
            new List<char> { 'G', 'H', 'I' }
        };

        // Act
        var grid = new Grid(gridData);

        // Assert
        Assert.Equal(3, grid.Width);
        Assert.Equal(3, grid.Height);
        Assert.Equal('A', grid.GetValueAt(0, 0));
        Assert.Equal('E', grid.GetValueAt(1, 1));
        Assert.Equal('I', grid.GetValueAt(2, 2));
    }

    [Fact]
    public void Constructor_NullOrEmptyGrid_ThrowsArgumentException()
    {
        // Arrange
        List<List<char>> nullGrid = null;
        var emptyGrid = new List<List<char>>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Grid(nullGrid));
        Assert.Throws<ArgumentException>(() => new Grid(emptyGrid));
    }

    [Fact]
    public void GetValueAt_ValidCoordinates_ReturnsCorrectValue()
    {
        // Arrange
        var gridData = new List<List<char>>
        {
            new List<char> { 'A', 'B', 'C' },
            new List<char> { 'D', 'E', 'F' },
            new List<char> { 'G', 'H', 'I' }
        };
        var grid = new Grid(gridData);

        // Act & Assert
        Assert.Equal('A', grid.GetValueAt(0, 0));
        Assert.Equal('E', grid.GetValueAt(1, 1));
        Assert.Equal('I', grid.GetValueAt(2, 2));
    }

    [Fact]
    public void GetValueAt_OutOfBoundsCoordinates_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var gridData = new List<List<char>>
        {
            new List<char> { 'A', 'B', 'C' },
            new List<char> { 'D', 'E', 'F' },
            new List<char> { 'G', 'H', 'I' }
        };
        var grid = new Grid(gridData);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => grid.GetValueAt(-1, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => grid.GetValueAt(0, -1));
        Assert.Throws<ArgumentOutOfRangeException>(() => grid.GetValueAt(3, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => grid.GetValueAt(0, 3));
    }

    [Fact]
    public void SetValueAt_ValidCoordinates_SetsCorrectValue()
    {
        // Arrange
        var gridData = new List<List<char>>
        {
            new List<char> { 'A', 'B', 'C' },
            new List<char> { 'D', 'E', 'F' },
            new List<char> { 'G', 'H', 'I' }
        };
        var grid = new Grid(gridData);

        // Act
        grid.SetValueAt(1, 1, 'X');

        // Assert
        Assert.Equal('X', grid.GetValueAt(1, 1));
    }

    [Fact]
    public void SetValueAt_OutOfBoundsCoordinates_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var gridData = new List<List<char>>
        {
            new List<char> { 'A', 'B', 'C' },
            new List<char> { 'D', 'E', 'F' },
            new List<char> { 'G', 'H', 'I' }
        };
        var grid = new Grid(gridData);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => grid.SetValueAt(-1, 0, 'X'));
        Assert.Throws<ArgumentOutOfRangeException>(() => grid.SetValueAt(0, -1, 'X'));
        Assert.Throws<ArgumentOutOfRangeException>(() => grid.SetValueAt(3, 0, 'X'));
        Assert.Throws<ArgumentOutOfRangeException>(() => grid.SetValueAt(0, 3, 'X'));
    }

    [Fact]
    public void FindLocationsWithValue_ValidValue_ReturnsCorrectLocations()
    {
        // Arrange
        var gridData = new List<List<char>>
        {
            new List<char> { 'A', 'B', 'C' },
            new List<char> { 'D', 'E', 'F' },
            new List<char> { 'G', 'H', 'I' }
        };
        var grid = new Grid(gridData);

        // Act
        var locations = grid.FindLocationsWithValue('E');

        // Assert
        Assert.Single(locations);
        Assert.Equal(1, locations[0].X);
        Assert.Equal(1, locations[0].Y);
        Assert.Equal('E', locations[0].Value);
    }

    [Fact]
    public void FindLocationsWithValue_InvalidValue_ReturnsEmptyList()
    {
        // Arrange
        var gridData = new List<List<char>>
        {
            new List<char> { 'A', 'B', 'C' },
            new List<char> { 'D', 'E', 'F' },
            new List<char> { 'G', 'H', 'I' }
        };
        var grid = new Grid(gridData);

        // Act
        var locations = grid.FindLocationsWithValue('X');

        // Assert
        Assert.Empty(locations);
    }

    [Fact]
    public void Clone_CreatesDeepCopy()
    {
        // Arrange
        var gridData = new List<List<char>>
        {
            new List<char> { 'A', 'B', 'C' },
            new List<char> { 'D', 'E', 'F' },
            new List<char> { 'G', 'H', 'I' }
        };
        var grid = new Grid(gridData);

        // Act
        var clonedGrid = grid.Clone();
        clonedGrid.SetValueAt(1, 1, 'X');

        // Assert
        Assert.Equal('E', grid.GetValueAt(1, 1)); // Original grid should not be affected
        Assert.Equal('X', clonedGrid.GetValueAt(1, 1)); // Cloned grid should have the new value
    }

    [Fact]
    public void GetDistinctValues_ReturnsCorrectValues()
    {
        // Arrange
        var gridData = new List<List<char>>
        {
            new List<char> { 'A', 'B', 'C' },
            new List<char> { 'D', 'E', 'F' },
            new List<char> { 'G', 'H', 'H' }
        };
        var grid = new Grid(gridData);

        // Act
        var distinctValues = grid.GetDistinctValues();

        // Assert
        Assert.Equal(8, distinctValues.Count);
        Assert.Contains('A', distinctValues);
        Assert.Contains('B', distinctValues);
        Assert.Contains('C', distinctValues);
        Assert.Contains('D', distinctValues);
        Assert.Contains('E', distinctValues);
        Assert.Contains('F', distinctValues);
        Assert.Contains('G', distinctValues);
        Assert.Contains('H', distinctValues);
    }

    [Fact]
    public void Display_PrintsGridCorrectly()
    {
        // Arrange
        var gridData = new List<List<char>>
        {
            new List<char> { 'A', 'B', 'C' },
            new List<char> { 'D', 'E', 'F' },
            new List<char> { 'G', 'H', 'I' }
        };
        var grid = new Grid(gridData);

        // Act
        using (var sw = new System.IO.StringWriter())
        {
            Console.SetOut(sw);
            grid.Display();
            var result = sw.ToString().Trim();

            // Assert
            var expected = "A B C\r\nD E F\r\nG H I";
            Assert.Equal(expected, result);
        }
    }
}

