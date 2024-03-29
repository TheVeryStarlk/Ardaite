﻿using Ardaite.Markup.Lexing;
using Xunit;

namespace Ardaite.Markup.Tests;

public class LexerTests
{
    [Fact]
    public void CustomSource_Outputs_CorrectTokens()
    {
        const string source = "(grid (button))";

        var actual = Lexer.Run(source);

        var expected = new Token[]
        {
            new Token(TokenType.LeftParenthesis, "("),
            new Token(TokenType.Identifier, "grid"),
            new Token(TokenType.LeftParenthesis, "("),
            new Token(TokenType.Identifier, "button"),
            new Token(TokenType.RightParenthesis, ")"),
            new Token(TokenType.RightParenthesis, ")"),
            new Token(TokenType.End, "\0")
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
            new Token(TokenType.LeftParenthesis, "("),
            new Token(TokenType.Identifier, "button"),
            new Token(TokenType.Identifier, "text"),
            new Token(TokenType.Equal, "="),
            new Token(TokenType.String, "button"),
            new Token(TokenType.RightParenthesis, ")"),
            new Token(TokenType.End, "\0")
        };

        Assert.Equal(expected, actual);
    }
}