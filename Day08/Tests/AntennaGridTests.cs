using Xunit;
using Day08;
using Business;
using System.Collections.Generic;

public class AntennaGridTests
{
    [Fact]
    public void FindAntiNodes_SameLocation_ReturnsEmptyList()
    {
        // Arrange
        var grid = new List<string> { "abc", "def", "ghi" };
        var antennaGrid = new AntennaGrid(grid);
        var location = new Grid.Location(1, 1, 'e');

        // Act
        var result = antennaGrid.FindAntiNodes(location, location);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void FindAntiNodes_DifferentLocations_ReturnsAntinodes()
    {
        // Arrange
        var grid = new List<string> { "abc", "def", "ghi" };
        var antennaGrid = new AntennaGrid(grid);
        var locationA = new Grid.Location(0, 0, 'a');
        var locationB = new Grid.Location(1, 1, 'e');

        // Act
        var result = antennaGrid.FindAntiNodes(locationA, locationB);

        // Assert
        Assert.Equal(1, result.Count);
        Assert.Contains(result, loc => loc.X == 2 && loc.Y == 2);
    }

    [Fact]
    public void FindAntiNodesLong_SameLocation_ReturnsEmptyList()
    {
        // Arrange
        var grid = new List<string> { "abc", "def", "ghi" };
        var antennaGrid = new AntennaGrid(grid);
        var location = new Grid.Location(1, 1, 'e');

        // Act
        var result = antennaGrid.FindAntiNodesLong(location, location);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void FindAntiNodesLong_DifferentLocations_ReturnsAntinodes()
    {
        // Arrange
        var grid = new List<string> { "abcde", "fghij", "klmno", "pqrst", "uvwxy" };
        var antennaGrid = new AntennaGrid(grid);
        var locationA = new Grid.Location(1, 1, 'g');
        var locationB = new Grid.Location(2, 2, 'm');

        // Act
        var result = antennaGrid.FindAntiNodesLong(locationA, locationB);

        // Assert
        Assert.NotEmpty(result);
        Assert.DoesNotContain(result, loc => loc.X == 2 && loc.Y == 2);
        Assert.DoesNotContain(result, loc => loc.X == 1 && loc.Y == 1);
        Assert.Contains(result, loc => loc.X == 0 && loc.Y == 0);
        Assert.Contains(result, loc => loc.X == 3 && loc.Y == 3);
        Assert.Contains(result, loc => loc.X == 4 && loc.Y == 4);
    }
}

