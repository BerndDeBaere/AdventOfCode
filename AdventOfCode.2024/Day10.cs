namespace AdventOfCode._2024;

public class Day10
{
    public void Part1()
    {
        var input = ReadAndParseInput();

        var heads = input.coordinates.Where(c => c.Height == 0).Select(c => new Tracker { Start = c, End = c }).ToList();

        for (int searchHeight = 1; searchHeight < 10; searchHeight++)
        {
            List<Tracker> newHeads = new List<Tracker>();
            foreach (var head in heads)
            {
                newHeads.AddRange(head.End.GetNeighbours(input.maxRows, input.maxCol).Where(c => c.Height == searchHeight).Select(c => new Tracker { Start = head.Start, End = c }).ToList());
            }

            heads = newHeads;
        }
        Console.WriteLine(heads.DistinctBy(t => new { t.Start, t.End }).Count());
    }
    
    private class Tracker
    {
        public Coordinate Start { get; set; }
        public Coordinate End { get; set; }
    }

    public void Part2()
    {
        var input = ReadAndParseInput();

        var heads = input.coordinates.Where(c => c.Height == 0).Select(c => new Tracker { Start = c, End = c }).ToList();

        for (int searchHeight = 1; searchHeight < 10; searchHeight++)
        {
            List<Tracker> newHeads = new List<Tracker>();
            foreach (var head in heads)
            {
                newHeads.AddRange(head.End.GetNeighbours(input.maxRows, input.maxCol).Where(c => c.Height == searchHeight).Select(c => new Tracker { Start = head.Start, End = c }).ToList());
            }

            heads = newHeads;
        }
        Console.WriteLine(heads.Count());
    }

    private class Coordinate
    {
        public required List<Coordinate> AllCoordinates { get; set; }
        public int Height { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public override string ToString()
        {
            return $"({Row} {Col})";
        }

        public List<Coordinate> GetNeighbours(int maxRow, int maxCol)
        {
            return GetOrthogonalCoordinates(Row, Col, maxRow, maxCol)
                .Select(orthogonalCoordinates => AllCoordinates.FirstOrDefault(ac => ac.Row == orthogonalCoordinates.row && ac.Col == orthogonalCoordinates.col)).Where(ac => ac != null).Cast<Coordinate>()
                .ToList();
        }

        private IEnumerable<(int row, int col)> GetOrthogonalCoordinates(int row, int col, int maxRow, int maxCol)
        {
            if (row > 0)
                yield return (row - 1, col);
            if (col > 0)
                yield return (row, col - 1);
            if (row < maxRow)
                yield return (row + 1, col);
            if (col < maxCol)
                yield return (row, col + 1);
        }
    }

    private (List<Coordinate> coordinates, int maxRows, int maxCol) ReadAndParseInput()
    {
        List<Coordinate> coordinates = new List<Coordinate>();
        var inputLines = File.ReadAllLines("Input/Day10.txt");
        for (int row = 0; row < inputLines.Length; row++)
        {
            for (int col = 0; col < inputLines[row].Length; col++)
            {
                coordinates.Add(new Coordinate
                {
                    AllCoordinates = coordinates,
                    Row = row,
                    Col = col,
                    Height = int.Parse(inputLines[row].Substring(col, 1))
                });
            }
        }

        return (coordinates, inputLines.Length, inputLines[0].Length);
    }
}