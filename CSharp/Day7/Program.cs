int[] input = Array.ConvertAll(File.ReadLines("day7.txt").First().Split(','), int.Parse);

(int, int) Calculate()
{
	var costs = (Part1: int.MaxValue, Part2: int.MaxValue);
	int min = input.Min();
	int max = input.Max();
	for (int i = min; i <= max; i++)
	{
		int sumFirstPart = input.Sum(crab => Math.Abs(i - crab));
		if (sumFirstPart < costs.Part1)
			costs.Part1 = sumFirstPart;

		int sumSecondPart = input.Select(crab => Math.Abs(i - crab)).Select(n => (n * n + n) / 2).Sum();
		if (sumSecondPart < costs.Part2)
			costs.Part2 = sumSecondPart;
	}

	return costs;
}

(int first, int second) = Calculate();
Console.WriteLine($"[Part 1]: {first}");
Console.WriteLine($"[Part 2]: {second}");