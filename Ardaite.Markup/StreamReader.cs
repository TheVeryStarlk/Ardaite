namespace Ardaite.Markup;

public abstract class StreamReader<T>(T[] source, T discardedValue)
{
    protected int Current { get; set; }
    protected int Line { get; set; } = 1;

    protected T Peek(int distance = 0)
        => IsAtEnd() ? discardedValue : source[Current + distance];

    protected T Advance()
        => IsAtEnd() ? discardedValue : source[Current++];

    protected bool IsAtEnd()
        => Current >= source.Length;
}