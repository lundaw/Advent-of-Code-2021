namespace Day10;

public class Solution
{
	private static readonly char[] OpeningBrackets = { '(', '[', '{', '<' };
	private static readonly char[] ClosingBrackets = { ')', ']', '}', '>' };
	private static readonly ushort[] BracketPoints = { 3, 57, 1197, 25137 };

	private readonly List<string> _input;

	public Solution(List<string> input) => _input = input;

	public long CalculatePart1() => _input.Select(IsCorruptedLine).Sum();

	public long CalculatePart2()
	{
		long[] results = _input.Where(line => IsCorruptedLine(line) == 0)
							   .Select(AutocompleteIncompleteLine)
							   .OrderBy(point => point)
							   .ToArray();
		return results[results.Length / 2];
	}

	private static long IsCorruptedLine(string line)
	{
		var stack = new Stack<char>();
		foreach (char bracket in line)
		{
			if (OpeningBrackets.Contains(bracket))
				stack.Push(bracket);
			else
			{
				if (Array.IndexOf(ClosingBrackets, bracket) != Array.IndexOf(OpeningBrackets, stack.Pop()))
					return BracketPoints[Array.IndexOf(ClosingBrackets, bracket)];
			}
		}

		return 0;
	}

	private static long AutocompleteIncompleteLine(string line)
	{
		var stack = new Stack<char>();
		foreach (char bracket in line)
		{
			// Just assume that everything closes properly and deal with the leftover.
			if (OpeningBrackets.Contains(bracket))
				stack.Push(bracket);
			else
				stack.Pop();
		}

		return stack.Aggregate<char, long>(
			0, (current, leftover) => current * 5 + (Array.IndexOf(OpeningBrackets, leftover) + 1));
	}
}
