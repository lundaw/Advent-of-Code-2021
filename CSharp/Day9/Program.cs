var input = File.ReadLines("day9.txt").ToArray();
var day9 = new Day9.Day9(input);
(int part1, int part2) = day9.CalculateSolutions();

Console.WriteLine($"[Part 1]: {part1}");
Console.WriteLine($"[Part 2]: {part2}");