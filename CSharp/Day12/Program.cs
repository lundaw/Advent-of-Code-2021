var input = File.ReadLines("day12.txt").Select(p => p.Split('-')).ToArray();
var outgoingEdges = new Dictionary<string, List<string>>();
// var paths = new HashSet<List<string>>();
HashSet<List<string>> paths;

foreach (var edge in input)
{
	string a = edge[0];
	string b = edge[1];
	if (a != "start")
	{
		if (outgoingEdges.ContainsKey(b))
			outgoingEdges[b].Add(a);
		else
			outgoingEdges.Add(b, new List<string> { a });
	}

	if (b != "start")
	{
		if (outgoingEdges.ContainsKey(a))
			outgoingEdges[a].Add(b);
		else
			outgoingEdges.Add(a, new List<string> { b });
	}
}

bool SelectorPart1(string next, ICollection<string> path) => next.All(char.IsUpper) || !path.Contains(next);

bool SelectorPart2(string next, ICollection<string> path)
{
	return next.All(char.IsUpper) || !path.Contains(next) ||
		   path.Where(p => p.All(char.IsLower)).GroupBy(p => p).All(count => count.Count() <= 1);
}

void DFS(string node, List<string> path, Func<string, ICollection<string>, bool> selector)
{
	if (node.Equals("end"))
	{
		paths.Add(path);
		return;
	}

	var targets = outgoingEdges[node].Where(target => selector(target, path)).ToList();
	foreach (string target in targets)
		DFS(target, new List<string>(path) { target }, selector);
}

// Part 1
paths = new HashSet<List<string>>();
DFS("start", new List<string> { "start" }, SelectorPart1);
Console.WriteLine($"[Part 1]: {paths.Count}");

// Part 2
paths = new HashSet<List<string>>();
DFS("start", new List<string>{ "start"}, SelectorPart2);
Console.WriteLine($"[Part 2]: {paths.Count}");