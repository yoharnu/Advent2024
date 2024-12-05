namespace Business;

public class Helper
{
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
