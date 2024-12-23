namespace Business;

/// <summary>
/// Provides helper methods for various operations.
/// </summary>
public class Helper
{
    /// <summary>
    /// Reads all lines from the provided <see cref="StreamReader"/> and returns them as a list of strings.
    /// </summary>
    /// <param name="reader">The <see cref="StreamReader"/> to read from.</param>
    /// <returns>A list of strings containing all lines read from the <see cref="StreamReader"/>.</returns>
    public static List<string> ReadAllLines(StreamReader reader)
    {
        var lines = new List<string>();
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            lines.Add(line);
        }
        return lines;
    }
}
