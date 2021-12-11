using Day11;

int[][] input = File.ReadLines("day11.txt")
					.Select(row => row.Select(c => c - '0').ToArray())
					.ToArray();
var solution = new Solution(input);
(int part1, int part2) = solution.Calculate();

Console.WriteLine($"[Part 1]: {part1}");
Console.WriteLine($"[Part 2]: {part2}");