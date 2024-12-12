namespace AdventOfCode.Helpers.Grid;

public static class GridHelper
{
    public static Grid<T> CreateGridFromStringArray<T>(this List<string> rows, Func<char, T> createCell) where T : class
    {
        var grid = new Grid<T>();
        GridCell<T> previousCell = null;

        for (int row = 0; row < rows.Count; row++)
        {
            for (int col = 0; col < rows[row].Length; col++)
            {
                var value = createCell(rows[row][col]);
                GridCell<T> cell = new GridCell<T>
                {
                    Row = row,
                    Column = col,
                    Value = value,
                };
                if (grid.FirstCell == null)
                    grid.FirstCell = cell;
                if (previousCell != null)
                    previousCell.NextInInputOrder = cell;
                previousCell = cell;
                grid.GridCells.Add((row, col), cell);
            }
        }

        grid.CalculateNeighbours();
        return grid;
    }

    private static void CalculateNeighbours<T>(this Grid<T> grid) where T : class
    {
        foreach ((int row, int col) loc in grid.GridCells.Keys)
        {
            var cell = grid.GridCells[loc];
            cell.Up = grid.GridCells.GetValueOrDefault((loc.row - 1, loc.col));
            cell.Down = grid.GridCells.GetValueOrDefault((loc.row + 1, loc.col));
            cell.Left = grid.GridCells.GetValueOrDefault((loc.row, loc.col - 1));
            cell.Right = grid.GridCells.GetValueOrDefault((loc.row, loc.col + 1));
        }
    }
}