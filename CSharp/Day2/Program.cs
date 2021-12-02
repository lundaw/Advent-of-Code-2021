var input = File.ReadLines("day2.txt")
				.Select(l => l.Split(' '))
				.Select(l => (l[0], int.Parse(l[1])))
				.ToList();
int partOne = CalculatePartOne(input);
Console.WriteLine($"[Part 1]: {partOne}");

int partTwo = CalculatePartTwo(input);
Console.WriteLine($"[Part 2]: {partTwo}");

#region Calculator methods

int CalculatePartOne(IEnumerable<(string, int)> data)
{
	int position = 0;
	int depth = 0;

	foreach ((string command, int units) in data)
	{
		if (command == "forward")
			position += units;
		else if (command == "down")
			depth += units;
		else if (command == "up")
			depth -= units;
	}

	return position * depth;
}

int CalculatePartTwo(IEnumerable<(string, int)> data)
{
	int position = 0;
	int depth = 0;
	int aim = 0;

	foreach ((string command, int units) in data)
	{
		if (command == "forward")
		{
			position += units;
			depth += aim * units;
		}
		else if (command == "down")
			aim += units;
		else if (command == "up")
			aim -= units;
	}

	return position * depth;
}

#endregion
