namespace AdventOfCode;

public class Day2
{
    public void Part1()
    {
        var safeLevels = 0;
        foreach (var levels in ReadAndParseInput())
        {
            int errorLocation = CheckIfFails(levels);
            safeLevels = errorLocation == -1 ? safeLevels + 1 : safeLevels;
        }

        Console.WriteLine(safeLevels);
    }

    public void Part2()
    {
        var safeLevels = 0;
        foreach (var levels in ReadAndParseInput())
        {
            int index = CheckIfFails(levels);
            if (index != -1)
            {
                for (int i = 0; i < levels.Count; i++)
                {
                    var levelsCopy = new List<int>(levels);
                    levelsCopy.RemoveAt(i);
                    index = CheckIfFails(levelsCopy);
                    if (index == -1)
                        break;
                }
            }

            safeLevels = index == -1 ? safeLevels + 1 : safeLevels;
        }

        Console.WriteLine(safeLevels);
    }

    private int CheckIfFails(List<int> levels)
    {
        var index = -1;
        var errorIndex = -1;
        bool? direction = null;
        levels.Aggregate((x, y) =>
        {
            if (errorIndex != -1)
                return y;
            index++;
            if (!direction.HasValue)
            {
                direction = x < y;
            }
            else if (direction.Value && x > y)
            {
                errorIndex = index;
            }
            else if (!direction.Value && y > x)
            {
                errorIndex = index;
            }

            if (Math.Abs(x - y) == 0 || Math.Abs(x - y) > 3)
            {
                errorIndex = index;
            }

            return y;
        });
        return errorIndex;
    }

    private IEnumerable<List<int>> ReadAndParseInput()
    {
        var inputLines = File.ReadAllLines("Input/Day2.txt");
        foreach (var inputLine in inputLines)
        {
            yield return inputLine!.Split(" ")
                .Select(int.Parse).ToList();
        }
    }
}