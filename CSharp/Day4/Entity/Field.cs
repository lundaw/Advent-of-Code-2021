namespace Day4.Entity;

public class Field
{
	public int Value { get; }
	public bool Marked { get; set; } = false;
	
	public Field(int value) => Value = value;
}
