using System.Diagnostics;
using AdventOfCode._2024;

Stopwatch watch = new Stopwatch();
var day = new Day11();
watch.Start();
Console.WriteLine("Part 1:");
day.Part1();
Console.WriteLine(watch.ElapsedMilliseconds + "ms");
watch.Restart();
Console.WriteLine("Part 2:");
day.Part2();
Console.WriteLine(watch.ElapsedMilliseconds + "ms");
Console.Write("Done!");
