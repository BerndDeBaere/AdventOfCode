using AdventOfCode.Helpers.Grid;

namespace AdventOfCode._2024;

public class Day12
{
    public void Part1()
    {
        var grid = ReadAndParseInput();
        int plotNumber = 0;
        foreach (var cell in grid.CellsInInputOrder())
        {
            AssignPlotNumbers(cell, plotNumber++);
            cell.Value.PlotNumber = cell.OrthogonalCells().FirstOrDefault(c => c?.Value.GardenType == cell.Value.GardenType && c.Value.PlotNumber.HasValue)?.Value.PlotNumber ?? plotNumber++;
            cell.Value.FencesNeeded = cell.OrthogonalCells(true).Count(c => c?.Value.GardenType != cell.Value.GardenType);
        }

        var cost = grid.CellsInInputOrder()
            .GroupBy(c => c.Value.PlotNumber)
            .Sum(plot =>
            {
                return plot.Count() * plot.Sum(c => c.Value.FencesNeeded);
            });
        Console.WriteLine(cost);
    }

    public void Part2()
    {
        var grid = ReadAndParseInput();
        int plotNumber = 0;
        foreach (var cell in grid.CellsInInputOrder())
        {
            AssignPlotNumbers(cell, plotNumber++);
            cell.Value.PlotNumber = cell.OrthogonalCells().FirstOrDefault(c => c?.Value.GardenType == cell.Value.GardenType && c.Value.PlotNumber.HasValue)?.Value.PlotNumber ?? plotNumber++;
        }


        var cost = grid.CellsInInputOrder()
            .GroupBy(c => c.Value.PlotNumber)
            .Sum(plot =>
            {
                var corners = CalculteCorners(plot.ToList());
                return plot.Count() * corners;
            });
        Console.WriteLine(cost);
    }

    private int CalculteCorners(List<GridCell<GardenPlot>> cells)
    {
        int cornerCount = 0;
        var plotNumber = cells.First().Value.PlotNumber!.Value;

        //Count inner corners
        foreach (var cell in cells)
        {
            var knownCells = cells.Where(c => cell.OrthogonalCells().Contains(c)).ToList();

            if (knownCells.Count == 0)
                cornerCount += 4;
            else if (knownCells.Count == 1)
                cornerCount += 2;
            else if (knownCells.Count == 2 &&
                     knownCells[0].Row != knownCells[1].Row &&
                     knownCells[0].Column != knownCells[1].Column)
                cornerCount += 1;
        }
        // Count outer corners

        var neighbours = cells
            .SelectMany(c => c.OrthogonalCells().Where(n => n.Value.PlotNumber != plotNumber)).ToList();
        neighbours = neighbours.Distinct().ToList();

        HashSet<(int row, int col)> outerCorners = new();
        foreach (var neighbour in neighbours.Cast<GridCell<GardenPlot>>())
        {
            if (neighbour.Right != null && neighbour.Right.Value.PlotNumber == plotNumber &&
                neighbour.Down != null && neighbour.Down.Value.PlotNumber == plotNumber &&
                neighbour.Right.Down != null && neighbour.Right.Down.Value.PlotNumber == plotNumber)
            {
                outerCorners.Add((neighbour.Row, neighbour.Column));
            }


            if (neighbour.Left != null && neighbour.Left.Value.PlotNumber == plotNumber &&
                neighbour.Down != null && neighbour.Down.Value.PlotNumber == plotNumber &&
                neighbour.Left.Down != null && neighbour.Left.Down.Value.PlotNumber == plotNumber)
            {
                outerCorners.Add((neighbour.Row, neighbour.Column - 1));
            }


            if (neighbour.Right != null && neighbour.Right.Value.PlotNumber == plotNumber &&
                neighbour.Up != null && neighbour.Up.Value.PlotNumber == plotNumber &&
                neighbour.Right.Up != null && neighbour.Right.Up.Value.PlotNumber == plotNumber)
            {
                outerCorners.Add((neighbour.Row - 1, neighbour.Column));
            }


            if (neighbour.Left != null && neighbour.Left.Value.PlotNumber == plotNumber &&
                neighbour.Up != null && neighbour.Up.Value.PlotNumber == plotNumber &&
                neighbour.Left.Up != null && neighbour.Left.Up.Value.PlotNumber == plotNumber)
            {
                outerCorners.Add((neighbour.Row - 1, neighbour.Column - 1));
            }
        }
        return cornerCount + outerCorners.Count;
    }

    private void AssignPlotNumbers(GridCell<GardenPlot> start, int plotNumber)
    {
        if (start.Value.PlotNumber.HasValue) return;
        start.Value.PlotNumber = plotNumber;
        foreach (GridCell<GardenPlot> gridCell in start.OrthogonalCells().Where(c => c!.Value.GardenType == start.Value.GardenType))
        {
            AssignPlotNumbers(gridCell, plotNumber);
        }
    }

    class GardenPlot
    {
        public GardenPlot(char gardenType)
        {
            GardenType = gardenType;
        }

        public char GardenType { get; set; }
        public int? PlotNumber { get; set; }
        public int FencesNeeded { get; set; }
    }

    private Grid<GardenPlot> ReadAndParseInput()
    {
        var lines = File.ReadLines("Input/Day12.txt").ToList();
        return lines.CreateGridFromStringArray<GardenPlot>(x => new GardenPlot(x));
    }
}