namespace Day5;

public class Solution
{
	private readonly List<Vent> _vents;
	private int[,] _field;

	public Solution()
	{
		_vents = File.ReadLines("day5.txt")
					 .SelectMany(row => row.Split("->", StringSplitOptions.TrimEntries).Select(i => i.Split(','))
										   .ToList())
					 .Select(row => new Vent(Convert.ToInt32(row[0]), Y: Convert.ToInt32(row[1])))
					 .ToList();
		_field = new int[_vents.Max(t => t.X) + 1, _vents.Max(t => t.Y) + 1];
	}

	public void Solve()
	{
		SolvePart1();
		int resultPart1 = _field.Cast<int>().Count(field => field >= 2);
		Console.WriteLine($"[Part 1]: {resultPart1}");

		SolvePart2();
		int resultPart2 = _field.Cast<int>().Count(field => field >= 2);
		Console.WriteLine($"[Part 2]: {resultPart2}");
	}

	private void SolvePart1()
	{
		foreach (var vent in _vents.Chunk(2))
		{
			if (vent[0].X == vent[1].X)
			{
				if (vent[0].Y < vent[1].Y)
					for (int i = vent[0].Y; i <= vent[1].Y; i++)
						_field[vent[0].X, i]++;
				else
					for (int i = vent[1].Y; i <= vent[0].Y; i++)
						_field[vent[0].X, i]++;
			}
			else if (vent[0].Y == vent[1].Y) // Horizontal
			{
				if (vent[0].X < vent[1].X)
					for (int i = vent[0].X; i <= vent[1].X; i++)
						_field[i, vent[0].Y]++;
				else
					for (int i = vent[1].X; i <= vent[0].X; i++)
						_field[i, vent[0].Y]++;
			}
		}
	}

	private void SolvePart2()
	{
		foreach (var vent in _vents.Chunk(2))
		{
			if (vent[0].X == vent[1].X || vent[0].Y == vent[1].Y)
				continue;

			int m = (vent[0].Y - vent[1].Y) / (vent[0].X - vent[1].X);
			Vent pointLeft, pointRight;

			if (m == 1)
			{
				pointLeft = vent[0].X < vent[1].X ? vent[0] : vent[1];
				pointRight = vent[0].X < vent[1].X ? vent[1] : vent[0];

				for (int i = pointLeft.X, j = pointLeft.Y; i <= pointRight.X; i++, j++)
					_field[i, j]++;
			}
			else
			{
				pointLeft = vent[0].X > vent[1].X ? vent[0] : vent[1];
				pointRight = vent[0].X > vent[1].X ? vent[1] : vent[0];

				for (int i = pointLeft.X, j = pointLeft.Y; i >= pointRight.X; i--, j++)
					_field[i, j]++;
			}
		}
	}
}
