var input = Array.ConvertAll(File.ReadLines("day6.txt").First().Split(','), int.Parse).ToList();
var lanternfish = new long[9];
foreach (int fish in input) lanternfish[fish]++;

long CalculateForDays(int days)
{
	var copyLanternfish = (lanternfish.Clone() as long[])!;
	for (int day = 0; day < days; day++)
	{
		long parents = copyLanternfish[0];
		for (int n = 1; n < copyLanternfish.Length; n++)
			copyLanternfish[n - 1] = copyLanternfish[n];

		copyLanternfish[6] += parents;
		copyLanternfish[8] = parents;
	}

	return copyLanternfish.Sum();
}

Console.WriteLine($"[Part 1]: {CalculateForDays(80)}");
Console.WriteLine($"[Part 2]: {CalculateForDays(256)}");
