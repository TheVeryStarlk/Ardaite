namespace Ardaite.Markup;

public class StreamReader<T>
{
    protected int Current { get; private set; }

    private readonly T[] source;
    private readonly T discardedValue;

    protected StreamReader(T[] source, T discardedValue)
    {
        this.source = source;
        this.discardedValue = discardedValue;
    }

    protected bool Match(T expected)
    {
        var peek = Peek();
        if (peek is not null && peek.Equals(expected))
        {
            Current++;
            return true;
        }

        return false;
    }

    protected T Peek(int distance = 0)
        => IsAtEnd() ? discardedValue : source[Current + distance];

    protected T Advance()
        => IsAtEnd() ? discardedValue : source[Current++];

    protected bool IsAtEnd()
        => Current >= source.Length;
}