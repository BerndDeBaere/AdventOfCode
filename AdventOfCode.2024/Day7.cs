namespace AdventOfCode._2024;

public class Day7
{
    public void Part1()
    {
        long totalCount = 0;
        foreach (var calibration in ReadAndParseInput())
        {
            bool isPossible = false;
            foreach (var operators in GetAllPossibleOperatorsCombinations(calibration.Values.Count - 1, ['+', '*']))
            {
                string calculation = calibration.CreateCalculationString(operators);
                if (CheckIfCorrect(calibration.Result, calculation, ['+', '*']))
                {
                    isPossible = true;
                    break;
                }
            }

            if (isPossible)
            {
                totalCount += calibration.Result;
            }
        }

        Console.WriteLine(totalCount);
    }
    
    public void Part2()
    {
        long totalCount = 0;
        foreach (var calibration in ReadAndParseInput())
        {
            bool isPossible = false;
            foreach (var operators in GetAllPossibleOperatorsCombinations(calibration.Values.Count - 1, ['+', '*', '|']))
            {
                string calculation = calibration.CreateCalculationString(operators);
                if (CheckIfCorrect(calibration.Result, calculation, ['+', '*', '|']))
                {
                    isPossible = true;
                    break;
                }
            }

            if (isPossible)
            {
                totalCount += calibration.Result;
            }
        }

        Console.WriteLine(totalCount);
    }

    public bool CheckIfCorrect(long result, string calculation, char[] operators)
    {
        if (result.ToString() == calculation)
            return true;

        int index = 0;
        int operationIndex = calculation.IndexOfAny(operators, index);
        if (operationIndex == -1) return false;

        string numberString = calculation.Substring(index, operationIndex);
        long value = long.Parse(numberString);
        index = operationIndex;

        while (operationIndex != -1)
        {
            char operation = calculation[operationIndex];
            operationIndex = calculation.IndexOfAny(operators, operationIndex+1);
            numberString = operationIndex == -1 ? calculation.Substring(index+1) : calculation.Substring(index+1, operationIndex-index-1);
            index = operationIndex;
            switch (operation)
            {
                case '+':
                    value += long.Parse(numberString);
                    break;
                case '*':
                    value *= long.Parse(numberString);
                    break;
                case '|':
                    value = long.Parse(string.Concat(value.ToString(), numberString));
                    break;
            }
        }

        return value == result;
    }


    public string NumberToBaseXString(long value, char[] baseChars)
    {
        string result = string.Empty;
        int targetBase = baseChars.Length;
        do
        {
            result = baseChars[value % targetBase] + result;
            value /= targetBase;
        } while (value > 0);

        return result;
    }

    public IEnumerable<char[]> GetAllPossibleOperatorsCombinations(int lenght, char[] operators)
    {
        var totalValue = Math.Pow(operators.Length, lenght);
        for (var i = 0; i < totalValue; i++)
        {
            var toBaseString = NumberToBaseXString(i, operators).PadLeft(lenght, operators[0]);
            yield return toBaseString.ToCharArray();
        }
    }


    private class Calibration
    {
        public long Result { get; set; }
        public List<long> Values { get; set; } = [];

        public string CreateCalculationString(char[] operators)
        {
            string output = Values[0].ToString();
            for (int i = 0; i < operators.Length; i++)
            {
                output += $"{operators[i]}{Values[i + 1]}";
            }
            return output;
        }
    }

    private IEnumerable<Calibration> ReadAndParseInput()
    {
        var inputLines = File.ReadAllLines("Input/Day7.txt");
        foreach (var inputLine in inputLines)
        {
            var splitInputData = inputLine.Split(":");
            var r = long.Parse(splitInputData[0].Trim());
            var v = splitInputData[1].Trim().Split(" ").Select(long.Parse).ToList();
            yield return new Calibration
            {
                Values = v,
                Result = r
            };
        }
    }
}