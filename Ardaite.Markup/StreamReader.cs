namespace Ardaite.Markup;

public class StreamReader<T>
{
    protected int Current { get; set; }

    private readonly T[] source;
    private readonly T discardedValue;

    protected StreamReader(T[] source, T discardedValue)
    {
        this.source = source;
        this.discardedValue = discardedValue;
    }

    protected T Peek(int distance = 0)
        => IsAtEnd() ? discardedValue : source[Current + distance];

    protected T Advance()
        => IsAtEnd() ? discardedValue : source[Current++];

    protected bool IsAtEnd()
        => Current >= source.Length;
}