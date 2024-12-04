namespace Day04;


class Day : BaseDay
{

    public override string Run(int part, string rawData)
    {
        int linelength = rawData.IndexOf('\n') + 1;

        if (part == 1)
            return Part1(rawData, linelength);

        int count = 0;
        int diagonal = linelength - 1;
        for (int i = linelength + 1; i < rawData.Length - linelength - 1; i++)
        {
            if ((rawData[i]>='0' && rawData[i]<='9')|| rawData[i] == 'A')
            {
                string X = $"{rawData[i - (diagonal + 2)]}{rawData[i - diagonal]}{rawData[i + diagonal]}{rawData[i + (diagonal + 2)]}";
                count += X switch
                {
                    "MMSS" => 1,
                    "MSMS" => 1,
                    "SSMM" => 1,
                    "SMSM" => 1,
                    _ => 0 
                };
            }
        }


        return $"{count}";
    }

    private static string Part1(string rawData, int linelength)
    {
        string token = "MAS";
        char first = 'X';
        int padding = token.Length * (linelength);
        rawData = "".PadLeft(padding, ' ') + rawData + "".PadLeft(padding, ' ');
        int[] offsets = [1, -1, -(linelength), -(linelength + 1), -(linelength - 1), (linelength), (linelength + 1), (linelength - 1)];
        int count = 0;


        for (int i = padding; i < rawData.Length - padding; i++)
        {
            if (rawData[i] == first)
            {
                foreach (int offset in offsets)
                {
                    int index = i + offset;
                    foreach (char c in token)
                    {
                        if (rawData[index] != c)
                        {
                            goto fail;
                        }
                        index += offset;
                    }
                    count++;

                fail: continue;
                }
            }
        }

        return $"{count}";
    }
}
