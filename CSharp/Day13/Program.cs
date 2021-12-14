var input = File.ReadLines("day13.txt");
var dots = input.TakeWhile(row => !string.IsNullOrEmpty(row))
				.Select(d => d.Split(','))
				.Select(d => (X: int.Parse(d[0]), Y: int.Parse(d[1])))
				.ToList();
var foldQueue = new Queue<(char, int)>(input.SkipWhile(row => !string.IsNullOrEmpty(row)).Skip(1)
											.Select(f => f.Split('='))
											.Select(f => (Axis: f[0].Last(), Value: int.Parse(f[1]))));

void Fold()
{
	// Y = fold up, X = fold left
	(char axis, int value) = foldQueue.Dequeue();
	var newDots = new List<(int X, int Y)>();

	if (axis == 'y')
		newDots.AddRange(dots.Select(dot => dot.Y > value ? (dot.X, Y: dot.Y - 2 * (dot.Y - value)) : dot));
	else if (axis == 'x')
		newDots.AddRange(dots.Select(dot => dot.X > value ? (X: dot.X - 2 * (dot.X - value), dot.Y) : dot));

	// var unique = newDots.GroupBy(x => x).Where(x => x.Count() == 1);
	dots = newDots.GroupBy(x => new { x.X, x.Y }).Select(x => x.First()).ToList();
}

Fold();

// Part 1
Console.WriteLine($"[Part 1]: {dots.Count}");

// Part 2
while (foldQueue.Count > 0)
	Fold();

var ordered = dots.OrderBy(d => d.Y).ThenBy(d => d.X).ToList();
for (int i = 0; i <= ordered.Max(d => d.Y); i++)
{
	for (int j = 0; j <= ordered.Max(d => d.X); j++)
	{
		Console.Write(ordered.Contains((j, i)) ? '#' : ' ');
	}

	Console.Write('\n');
}
