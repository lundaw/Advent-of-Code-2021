var input = Array.ConvertAll(File.ReadLines("day6.txt").First().Split(','), int.Parse).ToList();
var lanternfish = new long[9];
foreach (int fish in input) lanternfish[fish]++;

long CalculateForDays(int days)
{
	for (int day = 0; day < days; day++)
	{
		long parents = lanternfish[0];
		for (int n = 1; n < lanternfish.Length; n++)
			lanternfish[n - 1] = lanternfish[n];

		lanternfish[6] += parents;
		lanternfish[8] = parents;
	}

	return lanternfish.Sum();
}

Console.WriteLine($"[Part 1]: {CalculateForDays(80)}");
Console.WriteLine($"[Part 2]: {CalculateForDays(256)}");
