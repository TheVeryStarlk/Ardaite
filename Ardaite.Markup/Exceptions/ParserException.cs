using Ardaite.Markup.Lexing;

namespace Ardaite.Markup.Exceptions;

public class ParserException : Exception
{
    public ParserException(TokenType expected, TokenType actual, int line) :
        base($"Expected '{expected}' but got '{actual}' instead\nAt line {line}\n")
    {
    }
}