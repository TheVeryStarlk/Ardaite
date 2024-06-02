using Ardaite.Markup.Exceptions;

namespace Ardaite.Markup.Lexing;

public sealed class Lexer(string source) : StreamReader<char>(source.ToCharArray(), '\0')
{
    private int start;

    private readonly List<Token> tokens = new();

    public static Token[] Run(string source)
        => new Lexer(source).Run();

    private Token[] Run()
    {
        while (!IsAtEnd())
        {
            start = Current;
            Scan();
        }

        tokens.Add(new Token(TokenType.End, "\0"));
        return tokens.ToArray();
    }

    private void Scan()
    {
        var character = Advance();

        switch (character)
        {
            case ' ' or '\t' or '\r':
            {
                break;
            }

            case '\n':
            {
                Line++;
                break;
            }

            case '=':
            {
                tokens.Add(new Token(TokenType.Equal, "="));
                break;
            }

            case '(' or ')':
            {
                tokens.Add(new Token(character is '('
                    ? TokenType.LeftParenthesis
                    : TokenType.RightParenthesis, character.ToString()));
                break;
            }

            case >= 'a' and <= 'z' or '-':
            {
                while (char.IsDigit(Peek()) || char.IsLetter(Peek()) || Match('-'))
                {
                    Advance();
                }

                var identifier = source[start..Current];
                tokens.Add(new Token(TokenType.Identifier, identifier));
                break;
            }

            case '"':
            {
                while (Peek() != '"' && !IsAtEnd())
                {
                    Advance();
                }

                if (IsAtEnd())
                {
                    ThrowLexerException("Unterminated string");
                }

                // The closing quote.
                Advance();

                var content = source[(start + 1)..(Current - 1)];
                tokens.Add(new Token(TokenType.String, content));
                break;
            }

            default:
            {
                ThrowLexerException($"Unexpected character '{character}'");
                break;
            }
        }
    }

    private void ThrowLexerException(string message)
        => throw new LexerException(message, Line, Current);

    private bool Match(char expected)
    {
        if (Peek() == expected)
        {
            Current++;
            return true;
        }

        return false;
    }
}