int[] input = File.ReadLines("input.txt").Select(int.Parse).ToArray();

#region Part 1

uint increaseCountPart1 = 0;
for (var i = 1; i < input.Length; i++)
{
	if (input[i - 1] < input[i])
	{
		increaseCountPart1++;
	}
}
Console.WriteLine($"[Part 1]: Increasing measurements: {increaseCountPart1}");

#endregion

#region Part 2

uint increaseCountPart2 = 0;
for (var i = 0; i < input.Length - 3; i++)
{
	if (input.Skip(i).Take(3).Sum() < input.Skip(i + 1).Take(3).Sum())
	{
		increaseCountPart2++;
	}
}
Console.WriteLine($"[Part 2]: Increasing measurements: {increaseCountPart2}");

#endregion