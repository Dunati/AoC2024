namespace Day07;


public static class DXT
{

    public static long[] ToLongs(this ReadOnlySpan<char> str, int @base = 10, string separator = "\r\n")
    {
        List<Range> items = new();
        foreach (Range item in str.Split(' '))
        {
            items.Add(item);
        }

        long[] result = new long[items.Count];
        for (int i = 0; i < result.Length; i++)
        {
            var item = items[i];
            result[i] = long.Parse(str.Slice(item.Start.GetOffset(0), item.End.GetOffset(0) - item.Start.GetOffset(0)));
        }
        return result;
    }
}


class Day : BaseDay
{

    bool HasSolution(Span<long> operands, long value, long result, int part)
    {
        if (operands.Length == 0)
        {
            return result == value;
        }

        if (value > result)
        {
            return false;
        }
        var rest = operands[1..];
        if (HasSolution(rest, value + operands[0], result, part)
          || HasSolution(rest, value * operands[0], result, part))
        {
            return true;
        }

        if (part == 1)
        {
            return false;
        }
        return HasSolution(rest, long.Parse(value.ToString() + operands[0].ToString()), result, part);
    }
    public override string Run(int part, string rawData)
    {
        decimal count = 0;
        foreach (var line in rawData.Lines())
        {
            int split = line.IndexOf(':');
            long result = long.Parse(line[..split]);

            long[] operands = line[(split + 2)..].AsSpan().ToLongs(separator: " ");

            if (HasSolution(operands, 0, result, part))
            {
                count += result;
            }
        }
        return $"{count}";
    }
}
