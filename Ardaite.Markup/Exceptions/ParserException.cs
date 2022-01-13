using Ardaite.Markup.Lexing;

namespace Ardaite.Markup.Exceptions;

public class ParserException : Exception
{
    public ParserException(string message) : base(message)
    {
    }

    public ParserException(TokenType expected, TokenType actual) : base(
        $"Expected '{expected}' but got '{actual}' instead")
    {
    }
}