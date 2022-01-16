using Ardaite.Markup.Lexing;
using Xunit;

namespace Ardaite.Markup.Tests;

public class LexerTests
{
    [Fact]
    public void CustomSource_Outputs_CorrectTokens()
    {
        var source = "(grid (button))";

        var actual = new Lexer(source).Run();

        var expected = new Token[]
        {
            new Token(TokenType.LeftParenthesis, "(", 1),
            new Token(TokenType.Identifier, "grid", 1),
            new Token(TokenType.LeftParenthesis, "(", 1),
            new Token(TokenType.Identifier, "button", 1),
            new Token(TokenType.RightParenthesis, ")", 1),
            new Token(TokenType.RightParenthesis, ")", 1),
            new Token(TokenType.End, "\0", 1)
        };

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PropertiesTagSource_Outputs_CorrectTokens()
    {
        var source = "(button text=\"button\")";

        var actual = new Lexer(source).Run();

        var expected = new Token[]
        {
            new Token(TokenType.LeftParenthesis, "(", 1),
            new Token(TokenType.Identifier, "button", 1),
            new Token(TokenType.Identifier, "text", 1),
            new Token(TokenType.Equal, "=", 1),
            new Token(TokenType.String, "button", 1),
            new Token(TokenType.RightParenthesis, ")", 1),
            new Token(TokenType.End, "\0", 1)
        };
        
        Assert.Equal(expected, actual);
    }
}