namespace Day05;


class Day : BaseDay
{

    public override string Run(int part, string rawData)
    {
        if (part == 1)
        {
            return Part1(rawData);
        }

        List<string> empty = new();
        DefaultDictionary<string, List<string>> order = new() { Default= () => empty };
        int valid = 0;

        foreach (var line in rawData.Lines())
        {
            if (line[2] == '|')
            {
                var before = order.Get(line[0..2], () => new List<string>());
                before.Add(line[3..]);
            }
            else if (line != "")
            {
                var numbers = line.Split(',');
                bool corrected = false;
                List<string> arranged = new();
                arranged.Add(numbers[0]);
                for (int i = 1; i < numbers.Length; i++)
                {
                    var num = numbers[i];
                    int j = 0;
                    for (; j < arranged.Count; j++)
                    {
                        if (order[num].Contains(arranged[j]))
                        {
                            corrected = true;
                            break;
                        }
                    }
                    arranged.Insert(j, num);
                }

                if (corrected)
                {
                    valid += int.Parse(arranged[arranged.Count / 2]);
                }
            }
        }
        return $"{valid}";
    }

    private static string Part1(string rawData)
    {
        DefaultDictionary<string, List<string>> order = new();
        HashSet<string> read = new HashSet<string>();
        int valid = 0;
        foreach (var line in rawData.Lines())
        {
            if (line[2] == '|')
            {
                var before = order.Get(line[0..2], () => new List<string>());
                before.Add(line[3..]);
            }
            else if (line != "")
            {
                read.Clear();
                var numbers = line.Split(',');
                foreach (var num in numbers)
                {
                    var prev = order.Get(num, () => new List<string>());
                    foreach (var pred in prev)
                    {
                        if (read.Contains(pred))
                        {
                            goto fail;
                        }
                    }
                    read.Add(num);
                }
                valid += int.Parse(numbers[numbers.Length / 2]);
            fail: continue;
            }
        }
        return $"{valid}";
    }
}
