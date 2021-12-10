using Day10;

List<string> input = File.ReadLines("day10.txt").ToList();
var solution = new Solution(input);

Console.WriteLine($"[Part 1]: {solution.CalculatePart1()}");
Console.WriteLine($"[Part 2]: {solution.CalculatePart2()}");