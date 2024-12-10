using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day4
{
    //Overly complicated, I know, but I just wanted to try it out.
    public void Part1()
    {
        var count = 0;
        var lines = File.ReadAllLines("Input/Day4.txt");

        List<List<char>> grid = lines.Select(line => line.ToCharArray().ToList()).ToList();
        
        //Horizontal
        count += CountXmas(grid);

        //Rotate 90
        var rotated = Rotate90DegreClockWise(grid);

        //Vertical
        count += CountXmas(rotated);

        //Rotate 45
        rotated = Rotate45Degre(grid);

        //Dia 1
        count += CountXmas(rotated);

        //Rotate 45, Otherwise
        rotated = RotateMinus45Degre(grid);

        //Dia 2
        count += CountXmas(rotated);

        Console.WriteLine(count);
    }

    public void Part2()
    {
        var lines = File.ReadAllLines("Input/Day4.txt");
        List<List<char>> grid = lines.Select(line => line.ToCharArray().ToList()).ToList();
        var count = 0;
        
        count+= CountMas(grid);
        
        grid = Rotate90DegreClockWise(grid);
        count+= CountMas(grid);

        grid = Rotate90DegreClockWise(grid);
        count+= CountMas(grid);

        grid = Rotate90DegreClockWise(grid);
        count+= CountMas(grid);

        Console.WriteLine(count);
        
    }

    private int CountMas(List<List<char>> input)
    {
        int count = 0;
        for (int row = 1; row < input.Count-1; row++)
        {
            for (int column = 1; column < input[0].Count-1; column++)
            {
                if (input[row][column] == 'A')
                {
                    if (input[row - 1][column - 1] == 'M' &&
                        input[row + 1][column - 1] == 'M' &&
                        input[row - 1][column + 1] == 'S' &&
                        input[row + 1][column + 1] == 'S')
                    {
                        // Console.WriteLine(
                        //     $"Found a match at {row},{column}");
                        count++;
                    }
                }
            }
        }

        return count;
    }
    
    private void WriteGrid(List<List<char>> input)
    {
        input.ForEach(line => Console.WriteLine(new string(line.ToArray())));
        Console.WriteLine();
    }

    private List<List<char>> Rotate90DegreClockWise(List<List<char>> input)
    {
        var rotated = new List<List<char>>();

        for (int row = 0; row < input.Count; row++)
        {
            for (int column = 0; column < input[row].Count; column++)
            {
                while (rotated.Count < column + 1)
                    rotated.Add(new List<char>());
                rotated[column].Add(input[row][column]);
            }
        }
        return  rotated.Select(line =>
        {
            line.Reverse();
            return line;
        }).ToList();
    }

    

    private List<List<char>> Rotate45Degre(List<List<char>> input)
    {
        var rotated = new List<List<char>>();
        int numberOfRows = input.Count + input[0].Count - 1;
        for (int row = 0; row < numberOfRows; row++)
            rotated.Add(new List<char>());

        for (int row = 0; row < input.Count; row++)
        {
            for (int column = 0; column < input[row].Count; column++)
            {
                rotated[row+column].Add(input[row][column]);
            }
        }

        rotated = rotated.Select(line =>
        {
            line.Reverse();
            return line;
        }).ToList();

        return rotated;
    } 
    
    private List<List<char>> RotateMinus45Degre(List<List<char>> input)
    {
        var rotated = new List<List<char>>();
        int numberOfRows = input.Count + input[0].Count - 1;
        for (int row = 0; row < numberOfRows; row++)
            rotated.Add(new List<char>());

        for (int row = 0; row < input.Count; row++)
        {
            for (int column = 0; column < input[row].Count; column++)
            {
                rotated[(input[0].Count-1) + row - column].Add(input[row][column]);
            }
        }


        return rotated;
    }


    private int CountXmas(List<List<char>> input)
    {
        Regex regex = new Regex(@"(?=(XMAS))|(?=(SAMX))");
        return input.Sum(line => regex.Count(new string(line.ToArray())));
    }
}