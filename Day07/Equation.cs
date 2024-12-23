using System.Text;

namespace Day07;

public class Equation
{
    public List<long> Values;
    public List<string> Slots = new();
    private readonly string[] operators;

    public Equation(List<long> values, string[] operators)
    {
        if (operators == null || operators.Length == 0)
        {
            throw new ArgumentException("Operators array must contain at least one operator.");
        }

        foreach (var op in operators)
        {
            if (string.IsNullOrWhiteSpace(op) || (op != "*" && op != "+" && op != "||"))
            {
                throw new ArgumentException("Operators must be '*', '+', or '||'.");
            }
        }

        Values = values;
        this.operators = operators;
        for (int i = 0; i < values.Count - 1; i++)
        {
            Slots.Add(operators[0]);
        }
    }

    private static long DoOperation(long v1, long v2, string op)
    {
        return op switch
        {
            "*" => v1 * v2,
            "+" => v1 + v2,
            "||" => long.Parse(v1.ToString() + v2),
            _ => throw new InvalidOperationException($"Unsupported operator: {op}"),
        };
    }

    public long Calculate()
    {
        if (Values.Count < 2)
        {
            throw new ArgumentException("Values list must contain at least two elements.");
        }

        long result = Values[0];
        for (int i = 1; i < Values.Count; i++)
        {
            result = DoOperation(result, Values[i], Slots[i - 1]);
        }

        return result;
    }

    public bool HasNext()
    {
        return Slots.Any(slot => slot != operators.Last());
    }

    public void Next()
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            int opIndex = Array.IndexOf(operators, Slots[i]);
            if (opIndex == operators.Length - 1)
            {
                Slots[i] = operators[0];
            }
            else
            {
                Slots[i] = operators[opIndex + 1];
                break;
            }
        }
    }

    public override string ToString()
    {
        var equation = new StringBuilder();
        for (int i = 0; i < Values.Count; i++)
        {
            equation.Append(Values[i]);
            if (i < Slots.Count)
            {
                equation.Append($" {Slots[i]} ");
            }
        }
        return equation.ToString();
    }
}
