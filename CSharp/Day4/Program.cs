using Day4.Entity;

var input = File.ReadLines("day4.txt")
				.Where(l => !string.IsNullOrEmpty(l))
				.ToList();
var marks = Array.ConvertAll(input[0].Split(','), int.Parse).ToList();
var boards = input.Skip(1)
				  .Chunk(5)
				  .Select(b => b.Select(r => r.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToList())
				  .ToList();
var bingo = new Bingo(marks, boards);

Console.WriteLine($"[Part 1]: {bingo.SimulatePartOne()}");
Console.WriteLine($"[Part 2]: {bingo.SimulatePartTwo()}");
