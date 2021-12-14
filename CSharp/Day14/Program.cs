using MoreLinq;

var input = File.ReadLines("day14.txt").ToList();
string template = input.First();
var substitutions = input.Skip(2)
						 .Select(s => s.Split("->", StringSplitOptions.TrimEntries))
						 .ToDictionary(s => s[0], s => s[1][0]);

Console.WriteLine($"[Part 1]: {CalculatePolymer(10)}");
Console.WriteLine($"[Part 2]: {CalculatePolymer(40)}");

long CalculatePolymer(int steps)
{
	var previousPairCounter = new Dictionary<string, long>();
	foreach (var pair in template.Window(2))
	{
		string pairStr = string.Join("", pair);
		if (previousPairCounter.ContainsKey(pairStr))
			previousPairCounter[pairStr]++;
		else
			previousPairCounter.Add(pairStr, 1);
	}

	for (var i = 0; i < steps; i++)
	{
		var nextPairCounter = new Dictionary<string, long>();
		foreach (string pair in previousPairCounter.Keys)
		{
			if (!substitutions.ContainsKey(pair))
				continue;

			long count = previousPairCounter[pair];

			string firstPart = string.Join("", pair[0], substitutions[pair]);
			if (nextPairCounter.ContainsKey(firstPart))
				nextPairCounter[firstPart] += count;
			else
				nextPairCounter.Add(firstPart, count);

			string secondPart = string.Join("", substitutions[pair], pair[1]);
			if (nextPairCounter.ContainsKey(secondPart))
				nextPairCounter[secondPart] += count;
			else
				nextPairCounter.Add(secondPart, count);
		}

		previousPairCounter = nextPairCounter;
	}

	// Only go for the first characters of the pairs, since the next pair
	// will duplicate it. The last character will be missing, but since that
	// is unchanged, it can be retrieved from the original, starting template.
	var elementCount = new Dictionary<char, long>();
	foreach ((string pair, long count) in previousPairCounter)
	{
		if (elementCount.ContainsKey(pair[0]))
			elementCount[pair[0]] += count;
		else
			elementCount.Add(pair[0], count);
	}

	elementCount[template[^1]]++;

	var ordered = elementCount.OrderByDescending(e => e.Value)
							  .Select(e => e.Value)
							  .ToList();
	return ordered.First() - ordered.Last();
}
