namespace Day9;

public class Day9
{
	// The whole grid which is the layout
	private readonly int[][] _data;

	// To track if the field was visited. Needed for the flood fill algorithm.
	private readonly bool[,] _visited;

	// Grid dimensions to avoid repeating length queries.
	private readonly int _width;
	private readonly int _height;

	public Day9(string[] input)
	{
		_data = input.Select(l => l.ToArray().Select(d => d - '0').ToArray()).ToArray();
		_height = _data.Length;
		_width = _data[0].Length;
		_visited = new bool[_width, _height];
	}

	public (int, int) CalculateSolutions()
	{
		// Part 1
		int riskLevel = GetBasins().Sum(field => _data[field.Y][field.X] + 1);

		// Part 2
		// Find all the lowest points as basins and map them to their size. Then take the top 3 after ordering and
		// multiply their size together.
		int largestBasins = GetBasins().Select(GetBasinSize)
									   .OrderByDescending(basinSize => basinSize)
									   .Take(3)
									   .Aggregate(1, (basinSize, accumulator) => accumulator * basinSize);

		// Give solution
		return (riskLevel, largestBasins);
	}

	private List<Field> GetNeighbours(Field field)
	{
		// Create a temporary array and instantly filter it for the valid tiles like not being out of bounds.
		(int x, int y) = field;
		return new[]
			{
				new Field(x, y - 1),
				new Field(x, y + 1),
				new Field(x + 1, y),
				new Field(x - 1, y)
			}.Where(f => f.X >= 0 && f.X < _width && f.Y >= 0 && f.Y < _height)
			 .ToList();
	}

	private List<Field> GetBasins()
	{
		return Enumerable.Range(0, _width)
						 .SelectMany(x => Enumerable.Range(0, _height)
													.Select(y => (x, y)))
						 .Select(f => new Field(f.x, f.y))
						 .Where(f => GetNeighbours(f).All(n => _data[f.Y][f.X] < _data[n.Y][n.X]))
						 .ToList();
	}

	private int GetBasinSize(Field field)
	{
		_visited[field.X, field.Y] = true;
		int currentDepth = _data[field.Y][field.X];

		// - Start with one extra for the current field.
		// - Check if the neighbour is 9, because that does not count according to the rules.
		return 1 + GetNeighbours(field)
				   .Where(neighbour => !_visited[neighbour.X, neighbour.Y] &&
									   _data[neighbour.Y][neighbour.X] >= currentDepth &&
									   _data[neighbour.Y][neighbour.X] < 9)
				   .Sum(GetBasinSize);
	}
}
