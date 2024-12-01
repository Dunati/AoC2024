namespace Day01;


class Day : BaseDay
{

    public override string Run(int part, string rawData)
    {
        if (part == 1)
        {
            return Part1(rawData);
        }

        var left = new int[100000];
        var right = new int[100000];

        int index = 0;
        foreach (var line in rawData.Lines())
        {
            var num = line.ToInts(10, "   ").ToArray();

            left[index++]=num[0];
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
        List<int> left = new List<int>();
        List<int> right = new List<int>();

        foreach (var line in rawData.Lines())
        {
            var num = line.ToInts(10, "   ").ToArray();
            left.Add(num[0]);
            right.Add(num[1]);
        }

        left.Sort();
        right.Sort();

        int distance = 0;
        for (int i = 0; i < left.Count; i++)
        {
            distance += left[i] > right[i] ? left[i] - right[i] : right[i] - left[i]
;
        }
        return distance.ToString();
    }
}
