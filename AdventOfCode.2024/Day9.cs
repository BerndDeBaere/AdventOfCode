namespace AdventOfCode._2024;

public class Day9
{
    public void Part1()
    {
        var files = ReadAndParseInput().ToList();
        // Print(files);


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

        // Print(files);

        long checksum = 0;
        foreach (var file in files)
        {
            for (int i = file.StartIndex; i <= file.EndIndex; i++)
            {
                checksum += file.Id * i;
            }
        }
        Console.WriteLine(checksum);
    }


    /// 7311068665635 (To High)
    /// 6636608910639 (To High
    /// 5841321518474 (To Low)
    /// 6636608781232
    public void Part2()
    {
        var files = ReadAndParseInput().ToList();

        // Print(files);

        for (int i = files.Max(f => f.Id); i >= 0; i--)
        {
            var fileToMove = files.First(x => x.Id == i);
            var fileToMoveIndex = files.IndexOf(fileToMove);

            var afterFileIndex = GetFirstFileIndexBeforeMinimumEmptySpace(files, fileToMove.Lenght, fileToMoveIndex);
            if (afterFileIndex == -1)
            {
                // Console.WriteLine("No place found");
                continue;
            }
            
            var afterFile = files[afterFileIndex];
            fileToMove.StartIndex = afterFile.EndIndex + 1;
            files.Remove(fileToMove);
            files.Insert(afterFileIndex + 1, fileToMove);
            // Print(files);
        }

        long checksum = 0;

        foreach (var file in files)
        {
            for (int i = file.StartIndex; i <= file.EndIndex; i++)
            {
                checksum += file.Id * i;
            }
        }
        Console.WriteLine(checksum);
    }

    private int GetFirstFileIndexBeforeMinimumEmptySpace(List<DiskFile> files, int lenght, int maxIndex)
    {
        for (int i = 0; i <= maxIndex - 1; i++)
        {
            if (files[i].EndIndex < files[i + 1].StartIndex - lenght)
            {
                return i;
            }
        }

        return -1;
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