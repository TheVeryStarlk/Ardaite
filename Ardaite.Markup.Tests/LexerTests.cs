using Ardaite.Markup.Lexing;
using Xunit;

namespace Ardaite.Markup.Tests;

public sealed class LexerTests
{
    [Fact]
    public void CustomSource_Outputs_CorrectTokens()
    {
        const string source = "(grid (button))";

        var actual = Lexer.Run(source);

        var expected = new Token[]
        {
            new(TokenType.LeftParenthesis, "("),
            new(TokenType.Identifier, "grid"),
            new(TokenType.LeftParenthesis, "("),
            new(TokenType.Identifier, "button"),
            new(TokenType.RightParenthesis, ")"),
            new(TokenType.RightParenthesis, ")"),
            new(TokenType.End, "\0")
        };

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PropertiesTagSource_Outputs_CorrectTokens()
    {
        const string source = "(button text=\"button\")";

        var actual = Lexer.Run(source);

        var expected = new Token[]
        {
            new(TokenType.LeftParenthesis, "("),
            new(TokenType.Identifier, "button"),
            new(TokenType.Identifier, "text"),
            new(TokenType.Equal, "="),
            new(TokenType.String, "button"),
            new(TokenType.RightParenthesis, ")"),
            new(TokenType.End, "\0")
        };

        Assert.Equal(expected, actual);
    }
}