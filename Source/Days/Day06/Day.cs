namespace Day06;

using System.Drawing;
using Map = char[][];
class Day : BaseDay
{

    const int UP = 0;
    const int RIGHT = 1;
    const int DOWN = 2;
    const int LEFT = 3;

    readonly Size[] Dirs = new Size[] {
        new Size( 0, -1 )
        ,new Size(  1,0 )
        ,new Size( 0, 1 )
        ,new Size( -1,0 )
    };
    void Draw(Map m)
    {
        Trace.WriteLine("");
        foreach (var line in m)
        {
            Trace.WriteLine(new string(line)
                .Replace("A", "↑")
                .Replace('B', '→')
                .Replace('D', '↓')
                .Replace('H', '←')
                .Replace('.', ' ')
                .Replace('L', '+')
                .Replace('C', '+')
                .Replace('I', '+')
                .Replace('F', '+')
                );
        }
    }
    string GuardFacing = "^>v<";

    const char WALL = '#';
    const char EMPTY = '.';
    const char STEP = '*';

    static bool OutOfBounds(Map map, Point newpos)
    {
        return newpos.X < 0 || newpos.Y < 0 || newpos.X >= map[0].Length || newpos.Y >= map.Length;
    }

    bool IsLoop(Point pos, int dir, Map map)
    {
        DefaultDictionary<Point, int> steps = new();
        while (true)
        {
            int dirMask = (1 << dir);
            steps[pos] |= (char)(dirMask);

            var newpos = pos + Dirs[dir];
            if (OutOfBounds(map, newpos))
            {
                return false;
            }

            char next = map[newpos.Y][newpos.X];
            if (next == WALL)
            {
                dir = (dir + 1) % 4;
                continue;
            }
            else if ((steps[newpos] & dirMask) == dirMask)
            {
                return true;
            }
            else
            {
                pos = newpos;
            }

        }
    }

    int Walk(Point pos, int dir, Map map, int part)
    {
        HashSet<Point> blockers = new();
        int steps = 1;

        while (true)
        {
            if (map[pos.Y][pos.X] == EMPTY)
            {
                map[pos.Y][pos.X] = (char)(64 + (1 << dir));
            }
            else
            {
                map[pos.Y][pos.X] |= (char)((1 << dir));

            }
            //Draw(map);
            var newpos = pos + Dirs[dir];
            if (OutOfBounds(map, newpos))
            {
                break;
            }


            char next = map[newpos.Y][newpos.X];
            if (next == WALL)
            {
                dir = (dir + 1) % 4;
                continue;
            }
            else
            {
                if (next == EMPTY)
                {
                    steps++;

                    if(part == 2)
                    {
                      //  var rightDir = Dirs[(dir + 1) % 4];
                      //  var mask = (1 << ((dir + 1) % 4));
                      //  var rightpos = pos + rightDir;
                      //  while (!OutOfBounds(map, rightpos))
                      //  {
                      //      var right = map[rightpos.Y][rightpos.X];
                      //
                      //      if ((right >= 64 && mask == (right & mask)))
                      //      {
                      //          map[newpos.Y][newpos.X] = '0';
                      //          //      Draw(map);
                      //          blockers.Add(newpos);
                      //          goto found;
                      //      }
                      //      rightpos += rightDir;
                      //  }

                        map[newpos.Y][newpos.X]=WALL;
                        if(IsLoop(pos, dir, map))
                        {
                            map[newpos.Y][newpos.X] = '0';
                            //    Draw(map);
                            blockers.Add(newpos);
                        }
                 //   found: pos = newpos;
                    }
                }

                pos = newpos;
                map[pos.Y][pos.X] = next;
            }

        }
      //  Draw(map);
        if (part == 1)
        {
            return steps;
        }
        else
        {
            //253 too low    
            //684 too low
            //685 too low
            // 2094 wrong;
            return blockers.Count;
        }


    }
    public override string Run(int part, string rawData)
    {
        Map map = rawData.Lines().Select(line => line.ToCharArray()).ToArray();
        int pos = rawData.IndexOfAny(GuardFacing.ToCharArray());
        int width = (map[0].Length);
        int y = pos / (width + 1);
        int x = pos - (y * (width + 1));

        int dir = GuardFacing.IndexOf(map[y][x]);
        map[y][x] = EMPTY;
        return $"{Walk(new Point(x, y), dir, map, part)}";
    }
}
