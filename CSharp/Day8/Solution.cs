using System.Diagnostics.CodeAnalysis;

namespace Day8;

public class Solution
{
	// As given by the task description.
	private static readonly Dictionary<string, int> SegmentMasks = new()
	{
		{ "abcefg", 0 },
		{ "cf", 1 },
		{ "acdeg", 2 },
		{ "acdfg", 3 },
		{ "bcdf", 4 },
		{ "abdfg", 5 },
		{ "abdefg", 6 },
		{ "acf", 7 },
		{ "abcdefg", 8 },
		{ "abcdfg", 9 }
	};

	private readonly List<(string[], string[], List<int>)> _input;

	public Solution(string[] input)
	{
		_input = input.Select(line => (
								  Digits: ParseInputRow(line),
								  Output: ParseInputRow(line, false),
								  Decoded: new List<int>()
							  ))
					  .ToList();
	}

	public int CalculatePart1() => _input.Sum(line => line.Item2.Count(o => o.Length is 2 or 4 or 3 or 7));

	public int CalculatePart2()
	{
		foreach ((string[] signal, string[] output, var decoded) in _input)
		{
			var decoder = DecodeSignalToMap(signal);
			var plaintextSolution = output.Select(s => new string(s.Select(c => decoder[c]).OrderBy(c => c).ToArray()));
			decoded.AddRange(plaintextSolution.Select(mask => SegmentMasks[mask]));
		}

		return _input.Select(x => int.Parse(string.Join("", x.Item3.Select(d => d.ToString())))).Sum();
	}

	[SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
	private static Dictionary<char, char> DecodeSignalToMap(string[] input)
	{
		var map = new Dictionary<char, char>();

		// Prepare all 7 segments of the display
		char[] chars = "abcdefg".ToArray();

		// Get the data for the known, unique values, such as 1 (which is 2 segments), 4 (which is 4) and 7 (which is 3).
		string d1 = input.First(i => i.Length == 2);
		string d4 = input.First(i => i.Length == 4);
		string d7 = input.First(i => i.Length == 3);

		// The 'a' segment is the one which is on the upside. This means that we need to find the one element, that is
		// in '7', but not in '1'.
		var a = d7.Except(d1);
		map[a.First()] = 'a';

		// The 'b' segment is on the left side, which is found in 6 different numbers: 0, 4, 5, 6, 8, 9. Filter out the
		// character which appears 6 times in the signal. That is the value for 'b' segment.
		var b = chars.Where(c => input.Count(i => i.Contains(c)) == 6);
		map[b.First()] = 'b';

		// The 'c' segment is on the right side, found in 8 different numbers: 0, 2, 3, 5, 6, 7, 8, 9. But the 'a'
		// segment also appears 8 times, therefore remove those from the result set to get 'c'.
		map[chars.Where(c => input.Count(i => i.Contains(c)) == 8).Except(a).First()] = 'c';

		// 'd' segment can be filtered out from the unique 4, since we know the right and left hand side already
		// ('b', 'c' and 'f' segments).
		var d = d4.Except(d1).Except(b);
		map[d.First()] = 'd';

		// 'e' segment is unique as it appears 4 times: 0, 2, 6, 8.
		map[chars.First(c => input.Count(i => i.Contains(c)) == 4)] = 'e';

		// The 'f' segment can be deduced from the uniqueness of it as well, as it is the only one that appears 9 times.
		// Which is all numbers but 2.
		map[chars.First(c => input.Count(i => i.Contains(c)) == 9)] = 'f';

		// The last one is the 'g' segment, which appears 7 times, but 'd' does too - which is already known. Therefore
		// filter out from the results the 'd' segment and we got it.
		map[chars.Where(c => input.Count(i => i.Contains(c)) == 7).Except(d).First()] = 'g';

		return map;
	}

	private static string[] ParseInputRow(string row, bool signal = true) =>
		row.Split('|', StringSplitOptions.TrimEntries)
		   .ElementAt(signal ? 0 : 1)
		   .Split(' ')
		   .Select(element => new string(element.OrderBy(c => c).ToArray()))
		   .ToArray();
}
