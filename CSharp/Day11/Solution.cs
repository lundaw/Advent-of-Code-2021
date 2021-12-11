namespace Day11;

public class Solution
{
	private readonly int[][] _input;

	public Solution(int[][] input) => _input = input;

	public (int, int) Calculate()
	{
		var flashCount = 0;
		var i = 0;
		while (true)
		{
			int flashed = Tick();

			if (i++ < 100)
				flashCount += flashed;

			// Part 2
			// If everyone flashes in sync, it means that the flash count is
			// 100. Therefore we only need to loop until the return val is 100.
			if (flashed == 100)
				break;
		}

		return (flashCount, i);
	}

	private int Tick()
	{
		var fieldsStack = new Stack<(int, int)>();
		var flashes = new HashSet<(int, int)>();
		var iterateField = (int x, int y) =>
		{
			_input[x][y]++;
			if (_input[x][y] > 9)
			{
				var field = (x, y);
				flashes.Add(field);
				fieldsStack.Push(field);
			}
		};

		for (int i = 0; i < _input.Length; i++)
			for (int j = 0; j < _input[i].Length; j++)
				iterateField(i, j);

		while (fieldsStack.Count > 0)
		{
			(int x, int y) = fieldsStack.Pop();
			foreach (var neighbour in GetNeighbours(x, y))
				if (!flashes.Contains(neighbour))
					iterateField(neighbour.Item1, neighbour.Item2);
		}

		foreach ((int x, int y) in flashes)
			_input[x][y] = 0;

		return flashes.Count;
	}

	private List<(int, int)> GetNeighbours(int x, int y)
	{
		return new[]
			{
				(x - 1, y - 1), (x - 1, y), (x - 1, y + 1),
				(x, y - 1), (x, y + 1),
				(x + 1, y - 1), (x + 1, y), (x + 1, y + 1)
			}.Where(f => f.Item1 >= 0 && f.Item1 < _input[0].Length && f.Item2 >= 0 && f.Item2 < _input.Length)
			 .ToList();
	}
}
