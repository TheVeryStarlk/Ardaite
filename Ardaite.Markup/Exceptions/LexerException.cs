namespace Ardaite.Markup.Exceptions;

public class LexerException : Exception
{
    public LexerException(string message, int line) : base($"{message}\nAt line {line}\n")
    {
    }
}