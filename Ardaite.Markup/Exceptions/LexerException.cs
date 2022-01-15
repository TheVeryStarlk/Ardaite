namespace Ardaite.Markup.Exceptions;

public class LexerException : Exception
{
    public LexerException(string message, int line, int column) : base($"{message}. @ {line}:{column}\n")
    {
    }
}