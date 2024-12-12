namespace AdventOfCode.Helpers.Grid;

public class GridCell<T> where T : class
{
    public required int Row { get; set; }
    public required int Column { get; set; }
    public required T Value { get; set; }

    public IEnumerable<GridCell<T>?> OrthogonalCells(bool allowNull = false)
    {
        if (Up != null || allowNull)
            yield return Up;
        if (Down != null|| allowNull)
            yield return Down;
        if (Left != null|| allowNull)
            yield return Left;
        if (Right != null|| allowNull)
            yield return Right;
    }

    public GridCell<T>? Up { get; set; }
    public GridCell<T>? Down { get; set; }
    public GridCell<T>? Left { get; set; }
    public GridCell<T>? Right { get; set; }

    public GridCell<T>? NextInInputOrder { get; set; }
}