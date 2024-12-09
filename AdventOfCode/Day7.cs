using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Unicode;
using System.Threading.Tasks.Dataflow;
using Microsoft.Win32.SafeHandles;

namespace AdventOfCode;

public class Day7
{
    public void Part1()
    {
        long totalCount = 0;
        foreach (var calibration in ReadAndParseInput())
        {
            bool isPossible = false;
            foreach (var operators in GetAllPossibleOperatorsCombinations(calibration.Values.Count - 1))
            {
                if (!calibration.CheckOperators(operators)) continue;
                isPossible = true;
                Console.Write($"{calibration.Result} = {calibration.Values[0]}");
                for (int i = 0; i < operators.Length; i++)
                {
                    Console.Write($"{operators[i]}{calibration.Values[i + 1]}");
                }
                Console.WriteLine();
                break;
            }
            if (isPossible)
            {
                totalCount += calibration.Result;
            }
        }
        Console.WriteLine(totalCount);
    }

    public IEnumerable<char[]> GetAllPossibleOperatorsCombinations(int lenght)
    {
        long totalValue = Convert.ToInt64(Math.Pow(2, lenght));
        for (long i = 0; i < totalValue; i++)
        {
            var toBaseString = Convert.ToString(i, 2).PadLeft(lenght, '0');
            for (int replace = 0; replace < 2; replace++)
            {
                toBaseString = toBaseString.Replace("0", "+").Replace("1", "*");
            }

            yield return toBaseString.ToCharArray();
        }
    }


    public void Part2()
    {
    }


    private class Calibration
    {
        public long Result { get; set; }
        public List<long> Values { get; set; }

        public bool CheckOperators(char[] operators)
        {
            if (operators.Length != Values.Count - 1)
                throw new Exception("Operator count does not match value count");

            long result = Values[0];
            for (int i = 0; i < Values.Count - 1; i++)
            {
                result = Calculate(result, Values[i + 1], operators[i]);
            }

            return result == Result;
        }


        public long Calculate(long a, long b, char operation)
        {
            return operation switch
            {
                '+' => a + b,
                '*' => a * b,
                _ => throw new Exception("Unknown operation")
            };
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