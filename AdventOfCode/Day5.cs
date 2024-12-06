namespace AdventOfCode;

public class Day5
{
    public void Part1()
    {
        var input = ReadAndParseInput();
        var sumValid = 0;
        foreach (var update in input.Updates)
        {
            var pages = update.Split(",").Select(int.Parse).ToList();
            if (GetErrorLocation(pages, input.Rules) == -1)
                sumValid += pages[(pages.Count - 1) / 2];
        }

        Console.WriteLine(sumValid);
    }

    public void Part2()
    {
        var input = ReadAndParseInput();
        var sumValid = 0;
        foreach (var update in input.Updates)
        {
            var pages = update.Split(",").Select(int.Parse).ToList();

            var errorIndex = GetErrorLocation(pages, input.Rules);
            if (errorIndex == -1)
                continue;

            while (GetErrorLocation(pages, input.Rules) != -1)
            {
                var wrongPage = pages[errorIndex];
                pages.Remove(wrongPage);

                var firstPage = pages.First(p => input.Rules[wrongPage].Contains(p));
                pages.Insert(pages.IndexOf(firstPage), wrongPage);

                errorIndex = GetErrorLocation(pages, input.Rules);
            }

            sumValid += pages[(pages.Count - 1) / 2];
        }

        Console.WriteLine(sumValid);
    }

    private int GetErrorLocation(List<int> pages, Dictionary<int, List<int>> rules)
    {
        var passedPages = new HashSet<int>();
        for (var index = 0; index < pages.Count; index++)
        {
            var page = pages[index];
            var rule = rules.TryGetValue(page, out var inputRule) ? inputRule : [];
            if (rule.Any(r => passedPages.Contains(r)))
            {
                return index;
            }

            passedPages.Add(page);
        }

        return -1;
    }

    private Input ReadAndParseInput()
    {
        var input = new Input();
        var inputLines = File.ReadAllLines("Input/Day5.txt");
        foreach (var inputLine in inputLines)
        {
            if (inputLine.Contains('|'))
            {
                var splitInputLine = inputLine.Split("|");
                if (input.Rules.ContainsKey(int.Parse(splitInputLine[0])))
                {
                    input.Rules[int.Parse(splitInputLine[0])].Add(int.Parse(splitInputLine[1]));
                }
                else
                {
                    input.Rules.Add(int.Parse(splitInputLine[0]), [int.Parse(splitInputLine[1])]);
                }
            }

            else if (!string.IsNullOrWhiteSpace(inputLine))
            {
                input.Updates.Add(inputLine);
            }
        }

        return input;
    }

    private class Input
    {
        public Dictionary<int, List<int>> Rules { get; set; } = new();
        public List<string> Updates { get; set; } = [];
    }
}