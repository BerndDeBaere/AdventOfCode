using System.Text.RegularExpressions;

namespace AdventOfCode._2024;

public class Day13
{
    public void Part1()
    {
        long totalCost = 0;
        foreach (var game in ReadAndParseInput())
        {
            long? costOfGame = null;

            for (long pressA = 0; pressA <= game.Price.x; pressA++)
            {
                long pressB = (game.Price.x - (pressA * game.ButtonA.x)) / game.ButtonB.x;
                if (Check(game, pressA, pressB) && (costOfGame is null || costOfGame > pressA * 3 + pressB))
                {
                    costOfGame = pressA * 3 + pressB;
                }
            }

            totalCost += costOfGame ?? 0;
        }

        Console.WriteLine(totalCost);
    }

    public void Part2()
    {
        long totalCost = 0;
        foreach (var gameWithIndex in ReadAndParseInput(10000000000000).Index())
        {
            var game = gameWithIndex.Item;

            var price = game.Price.x;
            var ax = game.ButtonA.x;
            var bx = game.ButtonB.x;

            if (price % GCD(ax, bx) != 0)
            {
                //No solution possible
                continue;
            }
            
            
                
            
        }

        Console.WriteLine(totalCost);
    }

    private bool Check(Game game, long pressA, long pressB)
    {
        return game.Price.x == pressA * game.ButtonA.x + pressB * game.ButtonB.x &&
               game.Price.y == pressA * game.ButtonA.y + pressB * game.ButtonB.y;
    }

    private (long pressLargeX, long pressSmallX)? GetPress(long x, long largeX, long smallX)
    {
        for (long pressLarge = x / largeX + 1; pressLarge >= 0; pressLarge--)
        {
            long pressSmall = (x - pressLarge * largeX) / smallX;
            if (pressLarge * largeX + pressSmall * smallX == x)
            {
                return (pressLarge, pressSmall);
            }
        }

        return null;
    }

    class Game
    {
        public (long x, long y) ButtonA { get; set; }
        public (long x, long y) ButtonB { get; set; }
        public (long x, long y) Price { get; set; }
    }

    private IEnumerable<Game> ReadAndParseInput(long priceOffset = 0)
    {
        var lines = File.ReadLines("Input/Day13.txt").ToList();

        Regex regex = new Regex(@"(\d+)");

        for (int lineIndex = 0; lineIndex < lines.Count; lineIndex++)
        {
            if (lines[lineIndex].StartsWith("Button A:"))
            {
                Game game = new Game();

                var numberStrings = regex.Matches(lines[lineIndex]);
                game.ButtonA = (long.Parse(numberStrings[0].Value), long.Parse(numberStrings[1].Value));
                numberStrings = regex.Matches(lines[lineIndex + 1]);
                game.ButtonB = (long.Parse(numberStrings[0].Value), long.Parse(numberStrings[1].Value));
                numberStrings = regex.Matches(lines[lineIndex + 2]);
                game.Price = (long.Parse(numberStrings[0].Value) + priceOffset, long.Parse(numberStrings[1].Value));
                yield return game;
            }
        }
    }
    
    private long GCD(long a, long b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a | b;
    }
}