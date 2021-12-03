var input = File.ReadLines("day3.txt").ToList();

int part1 = CalculatePart1(input);
Console.WriteLine($"[Part 1]: {part1}");

int part2 = CalculatePart2(input);
Console.WriteLine($"[Part 2]: {part2}");

#region Part 1

int CalculatePart1(List<string> data)
{
	// Flatten every single bit character into a single list of tuples with their corresponding original index
	var positions = data.SelectMany(item => item.Select((d, p) => (Digit: d, Position: p))).ToList();
	
	// Build a binary string from the most common bits from each position
	var buffer = string.Empty;
	for (var i = 0; i < data[0].Length; i++)
	{
		buffer += positions.Where(x => x.Position == i)
						   .GroupBy(x => x.Digit)
						   .OrderByDescending(x => x.Count())
						   .First()
						   .Key;
	}

	// Get the integer values. Calculate least common by inverting the bits.
	int mostCommon = Convert.ToInt32(buffer, 2);
	int leastCommon = ~mostCommon & 0xFFF;

	return mostCommon * leastCommon;
}

#endregion

#region Part 2

int CalculatePart2(List<string> data)
{
	var oxygen = data;
	var scrubber = data;

	for (var i = 0; i < data[0].Length; i++)
	{
		oxygen = FilterByDigit(oxygen, i, '1');
		scrubber = FilterByDigit(scrubber, i, '0');
	}

	int mostCommon = Convert.ToInt32(oxygen[0], 2);
	int leastCommon = Convert.ToInt32(scrubber[0], 2);

	return mostCommon * leastCommon;
}

#endregion

#region Helpers

List<string> FilterByDigit(List<string> data, int position, char digit)
{
	// Flatten the list into tuples of bits and their bit position
	var digitPositions = data.SelectMany(x => x.Select((d, p) => (Digit: d, Position: p))).ToList();
	
	// Group the collection by the digits and order by the group size based on the digit
	var digits = digitPositions.Where(x => x.Position == position).GroupBy(x => x.Digit);
	var sortedDigits = (digit == '0'
			? digits.OrderBy(x => x.Count())
			: digits.OrderByDescending(x => x.Count()))
		.ToArray();

	// Get the filter bit value
	char filterCriteria = digits.Count() switch
	{
		1 => sortedDigits[0].Key,
		_ => sortedDigits[0].Count() == sortedDigits[1].Count() ? digit : sortedDigits[0].Key
	};

	// Get the values that match the bit on the given position
	return data.Where(x => x[position] == filterCriteria).ToList();
}

#endregion
