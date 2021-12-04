namespace Day4.Entity;

public class Board
{
	public Field[][] Fields { get; }

	public Board()
	{
		Fields = new Field[5][];
		for (int i = 0; i < 5; i++) Fields[i] = new Field[5];
	}

	public void AddField(Field field, int x, int y) => Fields[x][y] = field;

	public void Mark(int number)
	{
		for (int i = 0; i < 5; i++)
			for (int j = 0; j < 5; j++)
				if (Fields[i][j].Value == number)
					Fields[i][j].Marked = true;
	}

	public bool CheckWin()
	{
		for (var i = 0; i < 5; i++)
		{
			if (GetRow(i).All(f => f.Marked) || GetColumn(i).All(f => f.Marked))
				return true;
		}

		return false;
	}

	private Field[] GetColumn(int columnNumber)
	{
		return Enumerable.Range(0, 5)
						 .Select(x => Fields[x][columnNumber])
						 .ToArray();
	}

	private Field[] GetRow(int rowNumber)
	{
		return Enumerable.Range(0, 5)
						 .Select(x => Fields[rowNumber][x])
						 .ToArray();
	}
}
