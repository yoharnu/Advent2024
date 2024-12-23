using System.Reflection;
using Xunit;

namespace Day07.Tests;

public class EquationTests
{
    // Existing tests...

    [Fact]
    public void Constructor_ValidOperators_InitializesCorrectly()
    {
        // Arrange
        var values = new List<long> { 1, 2, 3 };
        var operators = new string[] { "*", "+" };

        // Act
        var equation = new Equation(values, operators);

        // Assert
        Assert.Equal(values, equation.Values);
        Assert.Equal(new List<string> { "*", "*" }, equation.Slots);
    }

    [Fact]
    public void Constructor_InvalidOperators_ThrowsArgumentException()
    {
        // Arrange
        var values = new List<long> { 1, 2, 3 };
        var invalidOperators = new string[] { "*", "invalid" };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Equation(values, invalidOperators));
    }

    [Fact]
    public void Calculate_ValidValuesAndOperators_ReturnsExpectedResult()
    {
        // Arrange
        var values = new List<long> { 2, 3, 4 };
        var operators = new string[] { "*", "+" };
        var equation = new Equation(values, operators);

        // Act
        var result = equation.Calculate();

        // Assert
        Assert.Equal(24, result); // 2 * 3 * 4 = 24
    }

    [Fact]
    public void HasNext_InitialConfiguration_ReturnsTrue()
    {
        // Arrange
        var values = new List<long> { 2, 3, 4 };
        var operators = new string[] { "*", "+" };
        var equation = new Equation(values, operators);

        // Act
        var result = equation.HasNext();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Next_AdvancesToNextOperatorSet()
    {
        // Arrange
        var values = new List<long> { 2, 3, 4 };
        var operators = new string[] { "*", "+" };
        var equation = new Equation(values, operators);

        // Act
        equation.Next();

        // Assert
        Assert.Equal(new List<string> { "+", "*" }, equation.Slots);
    }

    [Fact]
    public void ToString_ReturnsCorrectStringRepresentation()
    {
        // Arrange
        var values = new List<long> { 2, 3, 4 };
        var operators = new string[] { "*", "+" };
        var equation = new Equation(values, operators);

        // Act
        var result = equation.ToString();

        // Assert
        Assert.Equal("2 * 3 * 4", result);
    }

    [Theory]
    [InlineData(2, 3, "*", 6)]
    [InlineData(5, 7, "+", 12)]
    [InlineData(10, 20, "||", 1020)]
    public void DoOperation_ValidOperators_ReturnsExpectedResult(long v1, long v2, string op, long expected)
    {
        // Act
        var result = typeof(Equation).GetMethod("DoOperation", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                                     .Invoke(null, new object[] { v1, v2, op });

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(2, 3, "-")]
    [InlineData(5, 7, "/")]
    [InlineData(10, 20, "&")]
    public void DoOperation_InvalidOperators_ThrowsInvalidOperationException(long v1, long v2, string op)
    {
        // Act & Assert
        Assert.Throws<TargetInvocationException>(() =>
            typeof(Equation).GetMethod("DoOperation", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                            .Invoke(null, new object[] { v1, v2, op }));
    }

}
