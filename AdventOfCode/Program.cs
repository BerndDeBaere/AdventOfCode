using System.Diagnostics;
using AdventOfCode;

Stopwatch watch = new Stopwatch();
var day = new Day9();
watch.Start();
Console.WriteLine("Part 1:");
day.Part1();
Console.WriteLine(watch.ElapsedMilliseconds + "ms");
watch.Restart();
Console.WriteLine("Part 2:");
day.Part2();
Console.WriteLine(watch.ElapsedMilliseconds + "ms");
Console.Write("Done!");
