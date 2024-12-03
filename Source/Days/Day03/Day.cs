namespace Day03;


class Day : BaseDay {

    public override string Run(int part, string rawData) {
        int total = 0;
        string re = @"mul\(([0-9]{1,3}),([0-9]{1,3})\)";
        rawData = rawData.Replace("\n", "");
        if (part == 2)
        {
            //106414362 too high
            // 93197144 too high
            rawData = Regex.Replace(rawData, @"don't\(\).*?($|do\(\))", "");
        }
        foreach (Match item in Regex.Matches(rawData, re))
        {
            total+= Convert.ToInt32(item.Groups[1].Value) * Convert.ToInt32(item.Groups[2].Value) ;
        }
        return $"{total}";
    }
}
