using System.Text;

namespace Day07;

public class Equation
{
    public List<long> Values;
    public List<char> Slots = [];
    private static readonly char[] operators = ['*', '+'];

    public Equation(List<long> values)
    {
        Values = values;
        for (int i = 0; i < values.Count - 1; i++)
        {
            Slots.Add(operators[0]);
        }
    }

    private static long DoOperation(long v1, long v2, char op)
    {
        return op switch
        {
            '*' => v1 * v2,
            '+' => v1 + v2,
            _ => throw new InvalidOperationException(),
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
        NextOperatorSet(Slots);
    }

    public static void NextOperatorSet(List<char> slots)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            int opIndex = Array.IndexOf(operators, slots[i]);
            if (opIndex == operators.Length - 1)
            {
                slots[i] = operators[0];
            }
            else
            {
                slots[i] = operators[opIndex + 1];
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
