namespace AdventOfCode._2024;

public class Day11
{

    private Dictionary<ulong, List<ulong>> _precalculations = [];

    public Day11()
    {
        int preCalculationLenght = 40;
        for (ulong precalculate = 0; precalculate < 10; precalculate++)
        {
            Console.WriteLine("Start precalculation of {0}", precalculate);
            _precalculations.Add(precalculate, new List<ulong>());
            List<ulong> preCalculateRow = [precalculate];
            for(int i = 0; i < preCalculationLenght; i++){
                preCalculateRow = OriginalBlink(preCalculateRow).ToList();
                _precalculations[precalculate].Add(Convert.ToUInt64(preCalculateRow.Count));
            }        
        }
    }
    
    public void Part1()
    {
        Blinker(25);
    }

    public void Part2()
    {
        Blinker(75);
    }

    private void Blinker(int blinks)
    {
        Console.WriteLine("Start blinking");
        var row = ReadAndParseInput();
        ulong extraStones = 0;
        for (int i = 0; i < blinks; i++)
        {
            (row, extraStones) = Blink(row, blinks - i - 1, extraStones);
        }
        Console.WriteLine("{0} stones", Convert.ToUInt64(row.Count) + extraStones);
    }


    private (List<ulong> stoneRow, ulong extraStones) Blink(List<ulong> row, int stepsToGo, ulong extraStones)
    {
        List<ulong> rowList = new List<ulong>();
        foreach (var stone in row)
        {
            if (_precalculations.ContainsKey(stone) && stepsToGo < _precalculations[stone].Count)
            {
                extraStones += _precalculations[stone][stepsToGo];
                continue;
            }

            if (stone == 0)
            {
                rowList.Add(1);
                continue;
            }
            
            var stoneString = stone.ToString();
            if (stoneString.Length % 2 == 0)
            {
                rowList.Add(ulong.Parse(stoneString.Substring(0, stoneString.Length / 2)));
                rowList.Add(ulong.Parse(stoneString.Substring(stoneString.Length / 2)));
            }
            else
            {
                rowList.Add(stone * 2024);
            }
        }

        return (rowList, extraStones);
    }


    private List<ulong> ReadAndParseInput()
    {
        return File.ReadAllText("Input/Day11.txt").Split(' ').Select(ulong.Parse).ToList();
    }

    private IEnumerable<ulong> OriginalBlink(List<ulong> row)
    {
        foreach (var stone in row)
        {
            if (stone == 0)
            {
                yield return 1;
                continue;
            }

            var stoneString = stone.ToString();
            if (stoneString.Length % 2 == 0)
            {
                yield return ulong.Parse(stoneString.Substring(0, stoneString.Length / 2));
                yield return ulong.Parse(stoneString.Substring(stoneString.Length / 2));
            }
            else
            {
                yield return stone * 2024;
            }
        }
    }
}