using System.Reflection.Metadata.Ecma335;

namespace Day02;


class Day : BaseDay
{

    public override string Run(int part, string rawData)
    {

        int safe = 0;
        foreach (var line in rawData.Lines())
        {
            var levels = line.ToInts(10, " ").ToArray();
            if (part == 1)
            {
                if (IsReportSafe(levels, part - 1))
                {
                    safe++;
                }
            }
            else
            {

                if (Part2(levels))
                {
                    safe++;
                }
            }
        }

        // part2: 687 wrong
        return $"{safe}";

        static bool ValidRange(ArraySegment<int> range)
        {
            int positive = 0;
            if (range.Count < 1)
            {
                return true;
            }
            else
            {
                for (int i = 1; i < range.Count; i++)
                {
                    int diff = range[i] - range[i - 1];
                    if (diff > 0)
                    {
                        positive++;
                    }
                    else
                    {
                        diff *= -1;
                    }
                    if (diff < 1 || diff > 3)
                    {
                        return false;
                    }
                }
            }
            return positive == 0 || positive == range.Count-1; 
        }

        // not super clever, but it works
        static bool Part2(int[] levels)
        {
            if(ValidRange(levels))
            {
                return true;
            }
            for(int i = 0; i < levels.Length; i++)
            {
                if(ValidRange(levels[0..^(levels.Length-i)].Concat(levels[(i+1)..]).ToArray()))
                {
                    return true;
                }
            }
            return false;
        }

        static bool IsReportSafe(int[] levels, int maxBad)
        {
            int sign = 0;
            int bad = 0;
            for (int i = 1; i < levels.Length; i++)
            {
                int difference = levels[i] - levels[i - 1];

                if (difference > 0 && difference < 4)
                {
                    if (sign == 0)
                    {
                        sign = 1;
                    }
                    else if (sign != 1)
                    {
                        if (bad >= maxBad)
                        {
                            return false;
                        }
                        else
                        {
                            bad++;
                        }
                    }
                }
                else if (difference < 0 && difference > -4)
                {
                    if (sign == 0)
                    {
                        sign = -1;
                    }
                    else if (sign != -1)
                    {
                        if (bad >= maxBad)
                        {
                            return false;
                        }
                        else
                        {
                            bad++;
                        }
                    }
                }
                else
                {
                    if (bad >= maxBad)
                    {
                        return false;
                    }
                    else
                    {
                        bad++;
                    }
                }
            }
            return true;
        }
    }
}
