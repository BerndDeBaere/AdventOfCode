using System.Data;
using System.Runtime.InteropServices;
using System.Threading.Tasks.Dataflow;

namespace AdventOfCode;

public class Day6
{
    public void Part1()
    {
        var input = ReadAndParseInput();
        bool isFree = false;
        do
        {
            (input.row, input.col, isFree) = StepToNewLocation(input.grid, input.row, input.col);
            // Print(input.grid);
        } while (!isFree);

        int count = 1; // OFFSET BY 1. Last Step hasn't been taken yet
        foreach (var character in input.grid)
        {
            if (character == 'X') count++;
        }

        Console.WriteLine(count);
    }


    public void Part2()
    {
        var input = ReadAndParseInput();
        int count = 0;
        bool isFree = false;
        do
        {
            (input.row, input.col, isFree) = StepToNewLocation(input.grid, input.row, input.col, false);

            if (CheckIfNextObstacleCouldCauseLoop(input.grid, input.row, input.col))
            {
                count++;
            }
        } while (!isFree);

        Console.WriteLine(count);
    }

    private bool CheckIfNextObstacleCouldCauseLoop(char[,] grid, int row, int col)
    {
        if (InFrontIsOutside(grid, row, col)) return false;



        //SETUP SIMULATION
        var sim = (char[,])grid.Clone();
        var simRow = row;
        var simCol = col;
        var nextSimLocation = GetCharacterLocationInFront(sim, simRow, simCol);

        sim[nextSimLocation.row, nextSimLocation.col] = 'O';

        bool isFree;
        HashSet<(int simRow, int simCol, char simChar)> visited = new();
        do
        {
            if (InFrontIsOutside(sim, simRow, simCol)) return false;
            (simRow, simCol, isFree) = StepToNewLocation(sim, simRow, simCol, false);
            if (!visited.Add((simRow, simCol, sim[simRow, simCol])))
            {
                return true;
            }
        } while (!isFree);

        return false;
    }

    private char GetCharacterInFront(char[,] grid, int row, int col)
    {
        var nextLocation = GetCharacterLocationInFront(grid, row, col);
        return grid[nextLocation.row, nextLocation.col];
    }

    private (int row, int col) GetCharacterLocationInFront(char[,] grid, int row, int col)
    {
        switch (grid[row, col])
        {
            case '^':
                return (row - 1, col);
            case 'v':
                return (row + 1, col);
            case '<':
                return (row, col - 1);
            case '>':
                return (row, col + 1);
            default:
                throw new Exception("No direction known");
        }
    }

    private bool InFrontIsBlocked(char[,] grid, int row, int col)
    {
        return GetCharacterInFront(grid, row, col) == '#' ||
               GetCharacterInFront(grid, row, col) == 'O';
    }

    private bool InFrontIsOutside(char[,] grid, int row, int col)
    {
        var nextLocation = GetCharacterLocationInFront(grid, row, col);
        return nextLocation.row < 0 ||
               nextLocation.row >= grid.GetLength(0) ||
               nextLocation.col < 0 ||
               nextLocation.col >= grid.GetLength(1);
    }

    public (int row, int col, bool isFree) StepToNewLocation(char[,] grid, int row, int col, bool leaveTrail = true)
    {
        if (InFrontIsOutside(grid, row, col))
        {
            return (row, col, true);
        }

        if (InFrontIsBlocked(grid, row, col))
        {
            RotateArrow(grid, row, col);
            return (row, col, false);
        }

        var nextLocation = GetCharacterLocationInFront(grid, row, col);
        grid[nextLocation.row, nextLocation.col] = grid[row, col];
        if (leaveTrail)
            grid[row, col] = 'X';
        return (nextLocation.row, nextLocation.col, false);
    }

    private void RotateArrow(char[,] grid, int row, int col)
    {
        char[] direction = ['<', '^', '>', 'v'];
        var index = (direction.Index().First(x => x.Item == grid[row, col]).Index + 1) % 4;
        grid[row, col] = direction[index];
    }

    private (char[,] grid, int row, int col) ReadAndParseInput()
    {
        char[] direction = ['<', '^', '>', 'v'];
        int row = -1, col = -1;
        var inputLines = File.ReadAllLines("Input/Day6.txt");
        var input = new char[inputLines.Length, inputLines[0].Length];
        for (var i = 0; i < inputLines.Length; i++)
        {
            for (var j = 0; j < inputLines[i].Length; j++)
            {
                input[i, j] = inputLines[i][j];
                if (!direction.Contains(inputLines[i][j])) continue;
                row = i;
                col = j;
            }
        }

        return (input, row, col);
    }

    private void Print(char[,] toPrint)
    {
        Console.SetCursorPosition(0, 0);
        for (int i = 0; i < toPrint.GetLength(0); i++)
        {
            for (int j = 0; j < toPrint.GetLength(1); j++)
                Console.Write(toPrint[i, j]);
            Console.WriteLine();
        }

        Thread.Sleep(100);
    }
}