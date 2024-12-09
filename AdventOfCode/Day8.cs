using System.Data;
using System.Runtime.InteropServices;
using System.Text.Unicode;
using System.Threading.Tasks.Dataflow;
using Microsoft.Win32.SafeHandles;

namespace AdventOfCode;

public class Day8
{
    public void Part1()
    {
        (List<Frequency> frequencies, int width, int height) input = ReadAndParseInput();
        var antinodes = input.frequencies
            .SelectMany(f => f.GetAntiNodes())
            .Where(an =>
                an.row >= 0 &&
                an.row < input.height &&
                an.column >= 0 &&
                an.column < input.width)
            .Distinct()
            .ToList();
        foreach (var valueTuple in antinodes)
        {
            Console.WriteLine(valueTuple);
        }
        Console.WriteLine(antinodes.Count);
        
    }


    public void Part2()
    {
        (List<Frequency> frequencies, int width, int height) input = ReadAndParseInput();
        var antinodes = input.frequencies
            .SelectMany(f => f.GetAntiNodesWithHarmonics(input.height, input.width))
            .Distinct()
            .ToList();
        foreach (var valueTuple in antinodes)
        {
            Console.WriteLine(valueTuple);
        }
        Console.WriteLine(antinodes.Count);
    }

    class Frequency
    {
        public Frequency(char type)
        {
            Type = type;
            Locations = new List<(int row, int col)>();
        }

        public char Type { get; set; }
        public List<(int row, int col)> Locations { get; set; }


        public IEnumerable<(int row, int column)> GetAntiNodes()
        {
            for (int i = 0; i < Locations.Count - 1; i++)
            {
                for (int j = i + 1; j < Locations.Count; j++)
                {
                    var loc1 = Locations[i];
                    var loc2 = Locations[j];
                    var rowOffset = loc2.row - loc1.row;
                    var colOffset = loc2.col - loc1.col;
                    yield return (loc1.row - rowOffset, loc1.col - colOffset);
                    yield return (loc2.row + rowOffset, loc2.col + colOffset);
                }
            }
        }
        
        public IEnumerable<(int row, int column)> GetAntiNodesWithHarmonics(int height, int width)
        {
            for (int i = 0; i < Locations.Count - 1; i++)
            {
                for (int j = i + 1; j < Locations.Count; j++)
                {
                    var loc1 = Locations[i];
                    var loc2 = Locations[j];
                    var rowOffset = loc2.row - loc1.row;
                    var colOffset = loc2.col - loc1.col;
                    
                    
                    while (rowOffset%2 == 0 && colOffset%2 == 0)
                    {
                        rowOffset/=2;
                        colOffset/=2;
                    }

                    var row1 = loc1.row;
                    var col1 = loc1.col;
                    while(row1 < height && col1 < width && row1 >= 0 && col1 >= 0)
                    {
                        yield return (row1, col1);
                        row1 += rowOffset;
                        col1 += colOffset;
                    }
                    row1 = loc1.row - rowOffset;
                    col1 = loc1.col - colOffset;
                    while(row1 < height && col1 < width && row1 >= 0 && col1 >= 0)
                    {
                        yield return (row1, col1);
                        row1 -= rowOffset;
                        col1 -= colOffset;
                    }
                }
            }
        }
    }

    private (List<Frequency>, int width, int height) ReadAndParseInput()
    {
        List<Frequency> frequencies = new List<Frequency>();
        var inputLines = File.ReadAllLines("Input/Day8.txt");
        for (int row = 0; row < inputLines.Length; row++)
        {
            for (int col = 0; col < inputLines[row].Length; col++)
            {
                if (inputLines[row][col] != '.' && inputLines[row][col] != '#')
                {
                    var frequency = frequencies.FirstOrDefault(f => f.Type == inputLines[row][col]);
                    if (frequency == null)
                    {
                        frequency = new Frequency(inputLines[row][col]);
                        frequencies.Add(frequency);
                    }

                    frequency.Locations.Add((row, col));
                }
            }
        }

        return (frequencies, inputLines[0].Length, inputLines.Length);
    }
}