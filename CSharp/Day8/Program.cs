using Day8;

string[] input = File.ReadLines("day8.txt").ToArray();
var solution = new Solution(input);
Console.WriteLine($"[Part 1]: {solution.CalculatePart1()}");
Console.WriteLine($"[Part 2]: {solution.CalculatePart2()}");