namespace AdventOfCode;

public class Day11
{
    public void Part1()
    {
        // List<ulong> row = [6];
        // for(int i = 0; i < 40; i++){
        //     row = OriginalBlink(row).ToList();
        //     Console.WriteLine(row.Count + ",");
        // }        
        // Console.WriteLine("DONE");
        Blinker(25);
    }

    public void Part2()
    {
        Blinker(75);
    }

    private void Blinker(int blinks)
    {
        int preCalculationLenght = 40;
        Dictionary<ulong, List<ulong>> precalculations = [];
        Console.WriteLine("Start precalculations");
        // for (ulong precalculate = 0; precalculate < 10; precalculate++)
        // {
        //     precalculations.Add(precalculate, new List<ulong>());
        //     List<ulong> preCalculateRow = [precalculate];
        //     for(int i = 0; i < preCalculationLenght; i++){
        //         preCalculateRow = OriginalBlink(preCalculateRow).ToList();
        //         precalculations[precalculate].Add(Convert.ToUInt64(preCalculateRow.Count));
        //     }        
        // }
        
        Console.WriteLine("Start blinking");
        var row = ReadAndParseInput();
        ulong extraStones = 0;
        for (int i = 0; i < blinks; i++)
        {
            (row, extraStones) = Blink(row, blinks - i - 1, extraStones, precalculations);
            Console.WriteLine("{3}: {0} stones + {1} extra = {2} stones", row.Count, extraStones, Convert.ToUInt64(row.Count) + extraStones, i+1);
        }
    }


    private (List<ulong> stoneRow, ulong extraStones) Blink(List<ulong> row, int stepsToGo, ulong extraStones, Dictionary<ulong, List<ulong>> preCalculations)
    {
        List<ulong> rowList = new List<ulong>();
        foreach (var stone in row)
        {
            if (stone == 0 && stepsToGo < _preCalculations0.Count)
            {
                extraStones += _preCalculations0[stepsToGo];
                continue;
            }
            
            if (stone == 1 && stepsToGo < _preCalculations1.Count)
            {
                extraStones += _preCalculations1[stepsToGo];
                continue;
            }
            
            if (stone == 2 && stepsToGo < _preCalculations2.Count)
            {
                extraStones += _preCalculations2[stepsToGo];
                continue;
            }

            if (stone == 3 && stepsToGo < _preCalculations3.Count)
            {
                extraStones += _preCalculations3[stepsToGo];
                continue;
            }

            if (stone == 4 && stepsToGo < _preCalculations4.Count)
            {
                extraStones += _preCalculations4[stepsToGo];
                continue;
            }

            if (stone == 5 && stepsToGo < _preCalculations5.Count)
            {
                extraStones += _preCalculations5[stepsToGo];
                continue;
            }

            if (stone == 6 && stepsToGo < _preCalculations6.Count)
            {
                extraStones += _preCalculations6[stepsToGo];
                continue;
            }

            if (stone == 7 && stepsToGo < _preCalculations7.Count)
            {
                extraStones += _preCalculations7[stepsToGo];
                continue;
            }

            if (stone == 8 && stepsToGo < _preCalculations8.Count)
            {
                extraStones += _preCalculations8[stepsToGo];
                continue;
            }

            if (stone == 9 && stepsToGo < _preCalculations9.Count)
            {
                extraStones += _preCalculations9[stepsToGo];
                continue;
            }

            if (stone == 0)
            {
                rowList.Add(1);
                continue;
            }
            
            var stoneString = stone.ToString();
            if (stoneString.Length % 2 == 0)
            {
                rowList.Add(ulong.Parse(stoneString.Substring(0, stoneString.Length / 2)));
                rowList.Add(ulong.Parse(stoneString.Substring(stoneString.Length / 2)));
            }
            else
            {
                rowList.Add(stone * 2024);
            }
        }

        return (rowList, extraStones);
    }


    private List<ulong> ReadAndParseInput()
    {
        return File.ReadAllText("Input/Day11.txt").Split(' ').Select(ulong.Parse).ToList();
    }

    private IEnumerable<ulong> OriginalBlink(List<ulong> row)
    {
        foreach (var stone in row)
        {
            if (stone == 0)
            {
                yield return 1;
                continue;
            }

            var stoneString = stone.ToString();
            if (stoneString.Length % 2 == 0)
            {
                yield return ulong.Parse(stoneString.Substring(0, stoneString.Length / 2));
                yield return ulong.Parse(stoneString.Substring(stoneString.Length / 2));
            }
            else
            {
                yield return stone * 2024;
            }
        }
    }
    
    private readonly List<ulong> _preCalculations6 = [1,
        1,
        2,
        4,
        8,
        8,
        11,
        22,
        32,
        54,
        68,
        103,
        183,
        250,
        401,
        600,
        871,
        1431,
        2033,
        3193,
        4917,
        7052,
        11371,
        16815,
        25469,
        39648,
        57976,
        90871,
        136703,
        205157,
        319620,
        473117,
        727905,
        1110359,
        1661899,
        2567855,
        3849988,
        5866379,
        8978479,
        13464170
    ];
    private readonly List<ulong> _preCalculations7 = [1,
        1,
        2,
        4,
        8,
        8,
        11,
        22,
        32,
        52,
        72,
        106,
        168,
        242,
        413,
        602,
        832,
        1369,
        2065,
        3165,
        4762,
        6994,
        11170,
        16509,
        25071,
        39034,
        57254,
        88672,
        134638,
        203252,
        312940,
        465395,
        716437,
        1092207,
        1637097,
        2519878,
        3794783,
        5771904,
        8814021,
        13273744];
    private readonly List<ulong> _preCalculations8 = [
        1,
        1,
        2,
        4,
        7,
        7,
        11,
        22,
        31,
        48,
        69,
        103,
        161,
        239,
        393,
        578,
        812,
        1322,
        2011,
        3034,
        4580,
        6798,
        10738,
        16018,
        24212,
        37525,
        55534,
        85483,
        130183,
        196389,
        301170,
        450896,
        691214,
        1054217,
        1583522,
        2428413,
        3669747,
        5573490,
        8505207,
        12835708];
    private readonly List<ulong> _preCalculations9 = [
        1,
        1,
        2,
        4,
        8,
        8,
        11,
        22,
        32,
        54,
        70,
        103,
        183,
        262,
        419,
        586,
        854,
        1468,
        2131,
        3216,
        4888,
        7217,
        11617,
        17059,
        25793,
        40124,
        58820,
        92114,
        139174,
        208558,
        322818,
        480178,
        740365,
        1126352,
        1685448,
        2602817,
        3910494,
        5953715,
        9102530,
        13675794];

    
    private readonly List<ulong> _preCalculations3 = [1,
        2,
        4,
        4,
        5,
        10,
        16,
        26,
        35,
        52,
        79,
        114,
        202,
        294,
        401,
        642,
        987,
        1556,
        2281,
        3347,
        5360,
        7914,
        12116,
        18714,
        27569,
        42628,
        64379,
        98160,
        150493,
        223231,
        344595,
        524150,
        788590,
        1210782,
        1821382,
        2779243,
        4230598,
        6382031,
        9778305,
        14761601,
        22417792,
        34225846,
        51690137,
        78827911,
        119542610,
        181315830,
        276460158,
        418258991,
        636641443,
        967436144];
    private readonly List<ulong> _preCalculations4 = [1,
        2,
        4,
        4,
        4,
        8,
        16,
        27,
        30,
        47,
        82,
        115,
        195,
        269,
        390,
        637,
        951,
        1541,
        2182,
        3204,
        5280,
        7721,
        11820,
        17957,
        26669,
        41994,
        62235,
        95252,
        146462,
        216056,
        336192,
        508191,
        766555,
        1178119,
        1761823,
        2709433,
        4110895,
        6188994,
        9515384,
        14316637];
    private readonly List<ulong> _preCalculations5 = [1,
        1,
        2,
        4,
        8,
        8,
        11,
        22,
        32,
        45,
        67,
        109,
        163,
        223,
        383,
        597,
        808,
        1260,
        1976,
        3053,
        4529,
        6675,
        10627,
        15847,
        23822,
        37090,
        55161,
        84208,
        128121,
        194545,
        298191,
        444839,
        681805,
        1042629,
        1565585,
        2396146,
        3626619,
        5509999,
        8396834,
        12678459];

    
    private readonly List<ulong> _preCalculations2 =
    [
        1,
        2,
        4,
        4,
        6,
        12,
        16,
        19,
        30,
        57,
        92,
        111,
        181,
        295,
        414,
        661,
        977,
        1501,
        2270,
        3381,
        5463,
        7921,
        11819,
        18712,
        27842,
        42646,
        64275,
        97328,
        150678,
        223730,
        343711,
        525238,
        784952,
        1208065,
        1824910,
        2774273,
        4230422,
        6365293,
        9763578,
        14777945,
        22365694,
        34205743,
        51643260,
        78678894,
        119550250,
        181040219,
        276213919,
        417940971,
        635526775,
        967190364
    ];

    private readonly List<ulong> _preCalculations1 = [
        1,
        2,
        4,
        4,
        7,
        14,
        16,
        20,
        39,
        62,
        81,
        110,
        200,
        328,
        418,
        667,
        1059,
        1546,
        2377,
        3572,
        5602,
        8268,
        12343,
        19778,
        29165,
        43726,
        67724,
        102131,
        156451,
        234511,
        357632,
        549949,
        819967,
        1258125,
        1916299,
        2886408,
        4414216,
        6669768,
        10174278,
        15458147,
        23333796,
        35712308,
        54046805,
        81997335,
        125001266,
        189148778,
        288114305,
        437102505,
        663251546,
        1010392024
    ];

    private readonly List<ulong> _preCalculations0 =[
        //0
        1, //1
        1, //2024
        2, //20 24
        4, //2 0 2 4
        4, //4048 1 4048 8096
        7, //40 48 2024 40 48 8096
        14, //4 0 4 8 20 24 4 0 4 8 80 96
        16,
        20,
        39,
        62,
        81,
        110,
        200,
        328,
        418,
        667,
        1059,
        1546,
        2377,
        3572,
        5602,
        8268,
        12343,
        19778,
        29165,
        43726,
        67724,
        102131,
        156451,
        234511,
        357632,
        549949,
        819967,
        1258125,
        1916299,
        2886408,
        4414216,
        6669768,
        10174278,
        15458147,
        23333796,
        35712308,
        54046805,
        81997335,
        125001266,
        189148778,
        288114305,
        437102505,
        663251546
    ];
}