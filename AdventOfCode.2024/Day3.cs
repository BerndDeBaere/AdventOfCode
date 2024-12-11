using System.Text.RegularExpressions;

namespace AdventOfCode._2024;

public class Day3
{
    public void Part1()
    {
        int totalValue = 0;
        var line = File.ReadAllText("Input/Day3.txt");
        var matches = Regex.Matches(line, @"mul\((\d{1,3}),(\d{1,3})\)");
        foreach (Match match in matches.ToList())
        {
            totalValue += (int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));
        }

        Console.WriteLine(totalValue);
    }

    public void Part2()
    {
        int totalValue = 0;
        var line = File.ReadAllText("Input/Day3.txt");
        var matches = Regex.Matches(line, @"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)");
        bool enabled = true;
        foreach (Match match in matches.ToList())
        {
            if (match.Value == "do()")
                enabled = true;
            else if (match.Value == "don't()")
                enabled = false;
            else if (enabled)
                totalValue += (int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));
        }

        Console.WriteLine(totalValue);
    }
}