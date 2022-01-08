using Ardaite.Markup.Lexing;
using Xunit;

namespace Ardaite.Markup.Tests;

public class LexerTests
{
    [Fact]
    public void CustomSource_Outputs_CorrectTokens()
    {
        var source = "(grid (button))";

        var expected = new Lexer(source).Run();

        var actual = new Token[]
        {
            new Token(TokenType.LeftParenthesis, "("),
            new Token(TokenType.Identifier, "grid"),
            new Token(TokenType.LeftParenthesis, "("),
            new Token(TokenType.Identifier, "button"),
            new Token(TokenType.RightParenthesis, ")"),
            new Token(TokenType.RightParenthesis, ")"),
            new Token(TokenType.EndOfFile, "")
        };

        Assert.Equal(actual, expected);
    }
}
