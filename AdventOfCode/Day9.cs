namespace AdventOfCode;

public class Day9
{
    /// Tried the following input
    /// 1851882345 (To Low)
    /// 
    public void Part1()
    {
        var files = ReadAndParseInput().ToList();
        // Print(files);

        Console.WriteLine(files.Count(f => f.Lenght == 0));
        
        int lastFileIndexOfCompressed = GetLastFileIndexOfCompressed(files);
        while (lastFileIndexOfCompressed != -1)
        {
            var lastFileInCompressed = files[lastFileIndexOfCompressed];
            var firstFileInUncompressed = files[lastFileIndexOfCompressed + 1];
            var lenghtFreeSpace = firstFileInUncompressed.StartIndex - lastFileInCompressed.EndIndex - 1;
            var lastFile = files.Last();

            if (lastFile.Lenght <= lenghtFreeSpace)
            {
                files.Remove(lastFile);
                lastFile.StartIndex = lastFileInCompressed.EndIndex + 1;
                files.Insert(lastFileIndexOfCompressed + 1, lastFile);
            }
            else
            {
                lastFile.Lenght -= lenghtFreeSpace;
                files.Insert(lastFileIndexOfCompressed + 1, new DiskFile(lastFile.Id, lastFileInCompressed.EndIndex + 1, lenghtFreeSpace));
            }

            lastFileIndexOfCompressed = GetLastFileIndexOfCompressed(files);
        }
        
        Print(files);

        validateOverwrites(files);
        
        
        int checksum = 0;
        int lastIndex = files.Last().EndIndex;
        for (int i = 0; i <= lastIndex; i++)
        {
            var file = files.First(f => i >= f.StartIndex && i <= f.EndIndex);
            checksum += file.Id * i;
        }

        Console.WriteLine(checksum);
    }

    private void validateOverwrites(List<DiskFile> files)
    {
        for (int i = 0; i < files.Count-1; i++)
        {
            if(files[i].EndIndex != files[i + 1].StartIndex - 1)
            {
                Console.WriteLine("Not valid");
            }
        }
    }

    private int GetLastFileIndexOfCompressed(List<DiskFile> files)
    {
        for (int i = 0; i < files.Count - 1; i++)
        {
            if (files[i].EndIndex < files[i + 1].StartIndex - 1)
            {
                return i;
            }
        }

        return -1;
    }

    private void Print(List<DiskFile> files)
    {
        string output = "";
        foreach (var file in files)
        {
            output = output.PadRight(file.StartIndex, '.').PadRight(file.EndIndex + 1, file.Id.ToString().ToCharArray()[0]);
        }

        Console.WriteLine(output);
    }

    public void Part2()
    {
    }

    class DiskFile
    {
        public DiskFile(int id, int startIndex, int lenght)
        {
            Id = id;
            StartIndex = startIndex;
            Lenght = lenght;
        }

        public int Id { get; set; }
        public int StartIndex { get; set; }
        public int Lenght { get; set; }

        public int EndIndex
        {
            get => StartIndex + Lenght - 1;
        }
    }

    private IEnumerable<DiskFile> ReadAndParseInput()
    {
        var inputLines = File.ReadAllText($"Input/Day9.txt");
        int index = 0;
        int id = 0;
        for (int i = 0; i < inputLines.Length; i++)
        {
            int lenght = int.Parse(inputLines[i].ToString());
            if (i % 2 == 0)
            {
                yield return new DiskFile(id, index, lenght);
                id++;
            }

            index += lenght;
        }
    }
}