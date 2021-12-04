namespace Day4.Entity;

public class Bingo
{
	private List<int> _marks;
	private readonly List<Board> _boards = new();

	public Bingo(List<int> marks, List<List<string[]>> boards)
	{
		_marks = marks;
		foreach (var board in boards)
		{
			var boardObj = new Board();
			for (var i = 0; i < 5; i++)
			{
				for (var j = 0; j < 5; j++)
				{
					boardObj.AddField(new Field(int.Parse(board[i][j])), i, j);
				}
			}

			_boards.Add(boardObj);
		}
	}

	public int SimulatePartOne()
	{
		foreach (int mark in _marks)
		{
			foreach (Board board in _boards)
			{
				board.Mark(mark);
				if (!board.CheckWin()) continue;

				int sum = board.Fields.Sum(row => row.Sum(field => field.Marked ? 0 : field.Value));
				ResetMarks();

				return sum * mark;
			}
		}

		return -1;
	}

	public int SimulatePartTwo()
	{
		foreach (int mark in _marks)
		{
			foreach (var board in _boards)
			{
				board.Mark(mark);
				if (_boards.All(b => b.CheckWin()))
					return board.Fields.Sum((row => row.Sum((field => field.Marked ? 0 : field.Value)))) * mark;
			}
		}

		return -1;
	}

	private void ResetMarks()
	{
		foreach (var board in _boards)
			foreach (var row in board.Fields)
				foreach (var field in row)
					field.Marked = false;
	}
}
