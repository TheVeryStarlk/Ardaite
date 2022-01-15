using Ardaite.Markup.Exceptions;

namespace Ardaite.Markup.Lexing;

public class Lexer : StreamReader<char>
{
    private int start;

    private readonly string source;
    private readonly List<Token> tokens;

    public Lexer(string source) : base(source.ToCharArray(), '\0')
    {
        this.source = source;
        tokens = new List<Token>();
    }

    public static Token[] Run(string source)
        => new Lexer(source).Run();

    public Token[] Run()
    {
        while (!IsAtEnd())
        {
            start = Current;
            Scan();
        }

        AddToken(TokenType.End, "");
        return tokens.ToArray();
    }

    private void Scan()
    {
        var character = Advance();

        switch (character)
        {
            case ' ' or '\t':
            {
                break;
            }

            case '\r':
            {
                if (Peek(1) is '\n')
                {
                    Line++;
                    Advance();
                }
                break;
            }

            case '\n':
            {
                Line++;
                break;
            }

            case '=':
            {
                AddToken(TokenType.Equal, character.ToString());
                break;
            }

            case '(' or ')':
            {
                AddToken(character is '('
                    ? TokenType.LeftParenthesis
                    : TokenType.RightParenthesis, character.ToString());
                break;
            }

            case >= 'a' and <= 'z' or '_':
            {
                while (char.IsDigit(Peek()) || char.IsLetter(Peek()) || Match('-'))
                {
                    Advance();
                }

                var identifier = source[start..Current];
                AddToken(TokenType.Identifier, identifier);
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
                    ThrowError("Unterminated string");
                }

                // The closing quote.
                Advance();

                var content = source[(start + 1)..(Current - 1)];
                AddToken(TokenType.String, content);
                break;
            }

            default:
            {
                ThrowError($"Unexpected character '{character}'");
                break;
            }
        }
    }

    private void AddToken(TokenType tokenType, string value)
        => tokens.Add(new Token(tokenType, value, Line));

    private void ThrowError(string message)
        => throw new LexerException(message, Line);

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