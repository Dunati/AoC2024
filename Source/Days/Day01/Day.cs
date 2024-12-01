namespace Day01;


class Day : BaseDay
{

    public override string Run(int part, string rawData)
    {
        if (part == 1)
        {
            return Part1(rawData);
        }
        string[] lines = rawData.Lines().ToArray();

        var left = new int[100000];
        var right = new int[100000];

        for (int index = 0; index < lines.Length; index++)
            {
            var num = lines[index].ToInts(10, "   ").ToArray();

            left[index]=num[0];
            right[num[1]]++;
        }

        int similarity = 0;
        for (int i = 0; i < left.Length; i++)
        {
            similarity += left[i] * right[left[i]];
        }

        //55489665 wrong
        return similarity.ToString();
    }

    private static string Part1(string rawData)
    {
        string[] lines = rawData.Lines().ToArray();
        int[] left = new int[lines.Length];
        int[] right = new int [lines.Length];

        for (int index=0; index<lines.Length; index++)
        {
            var num = lines[index].ToInts(10, "   ").ToArray();
            left[index]=(num[0]);
            right[index]=(num[1]);
        }

        Array.Sort(left);
        Array.Sort(right);

        int distance = 0;
        for (int i = 0; i < left.Length; i++)
        {
            distance += left[i] > right[i] ? left[i] - right[i] : right[i] - left[i]
;
        }
        return distance.ToString();
    }
}
