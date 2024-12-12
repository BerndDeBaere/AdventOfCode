namespace AdventOfCode.Helpers.Grid;

public class Grid<T> where T : class
{
    public GridCell<T>? FirstCell { get; set; }
    public Dictionary<(int row, int col), GridCell<T>> GridCells { get; set; } = new();

    public IEnumerable<GridCell<T>> CellsInInputOrder()
    {
        var cursor = FirstCell;
        while (cursor != null)
        {
            yield return cursor;
            cursor = cursor.NextInInputOrder;
        }
    }
}