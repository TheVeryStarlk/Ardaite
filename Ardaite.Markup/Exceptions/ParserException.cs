using Ardaite.Markup.Lexing;

namespace Ardaite.Markup.Exceptions;

public class ParserException : Exception
{
    public ParserException(TokenType expected, TokenType actual, int line, int column) :
        base($"Expected '{expected}' but got '{actual}' instead. @ {line}:{column}\n")
    {
    }
}