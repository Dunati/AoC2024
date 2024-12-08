using System.Drawing;
namespace Day08;

class Day : BaseDay
{

    public override string Run(int part, string rawData)
    {
        int y = 0;
        HashSet<Point> antinodes = new();
        Dictionary<Point, char> antennas = new();

        DefaultDictionary<char, List<Point>> nodes = new() { Default = () => new List<Point>(), InsertOnDefaultReference = true };


        int height = rawData.Lines().Count();
        int width = 0;
        foreach (var line in rawData.Lines())
        {
            width = line.Length;
            for (int x = 0; x < line.Length; x++)
            {
                char node = line[x];
                if (node != '.')
                {
                    var antenna = new Point(x, y);
                    antennas[antenna] = node;

                    Draw(antinodes, antennas, height, width);
                    var this_node = nodes[node];
                    foreach (Point point in this_node)
                    {
                        Size offset = new Size(x - point.X, y - point.Y);
                        var anti = point;
                        while (true)
                        {
                            anti = Point.Subtract(anti, offset);
                            if (!(anti.X < 0 || anti.Y < 0 || anti.X >= width || anti.Y >= height))
                            {
                                antinodes.Add(anti);
                                Draw(antinodes, antennas, height, width);
                                if (part == 1)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }

                        }
                        anti = antenna;
                        while (true)
                        {
                            anti = Point.Add(anti, offset);
                            if (!(anti.X < 0 || anti.Y < 0 || anti.X >= width || anti.Y >= height))
                            {
                                antinodes.Add(anti);
                                Draw(antinodes, antennas, height, width);
                                if (part == 1)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }

                        }
                    }
                    this_node.Add(new Point(x, y));
                }
            }
            y++;
        }

        if (part == 2)
        {
            foreach (var ant in antennas.Keys)
            {
                antinodes.Add(ant);
            }
        }
        // 2: 1119 too low
        return antinodes.Count.ToString();

    }

    [Conditional("DRAW")]
    private static void Draw(HashSet<Point> antinodes, Dictionary<Point, char> antennas, int height, int width)
    {
        Trace.WriteLine("");
        int y;
        for (y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                bool anti = antinodes.Contains(new Point(x, y));
                if (antennas.TryGetValue(new Point(x, y), out var ant))
                {
                    if (anti)
                    {
                        Trace.Write("!");
                    }
                    else
                    {
                        Trace.Write(ant);
                    }
                }
                else if (anti)
                {
                    Trace.Write("#");
                }
                else
                {
                    Trace.Write(".");
                }
            }
            Trace.WriteLine("");
        }

    }
}
