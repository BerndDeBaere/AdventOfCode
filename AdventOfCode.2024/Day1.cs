namespace AdventOfCode._2024;

public class Day1
{
    public void Part1()
    {
        ReadAndParseInput(out List<int> locationsLeft, out List<int> locationsRight);
        locationsLeft = locationsLeft.OrderBy(x => x).ToList();
        locationsRight = locationsRight.OrderBy(x => x).ToList();
        var totalDistance = locationsLeft.Select((left, index) => Math.Abs(left - locationsRight[index])).Sum();
        Console.WriteLine(totalDistance);
    }

    public void Part2()
    {
        ReadAndParseInput(out List<int> locationsLeft, out List<int> locationsRight);
        var similarityScore = 0;
        locationsLeft.ForEach(left => { similarityScore += left * locationsRight.Count(right => left == right); });
        Console.WriteLine(similarityScore);
    }

    private void ReadAndParseInput(out List<int> locationsLeft, out List<int> locationsRight)
    {
        var inputLines = File.ReadAllLines("Input/Day1.txt");

        locationsLeft = [];
        locationsRight = [];

        foreach (var inputLine in inputLines)
        {
            var splitInputData = inputLine!.Split("   ");
            locationsLeft.Add(int.Parse(splitInputData[0]));
            locationsRight.Add(int.Parse(splitInputData[1]));
        }
    }
}