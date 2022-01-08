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

    public Token[] Run()
    {
        while (!IsAtEnd())
        {
            start = Current;
            Scan();
        }

        tokens.Add(new Token(TokenType.EndOfFile, ""));
        return tokens.ToArray();
    }

    private void Scan()
    {
        var character = Advance();

        switch (character)
        {
            case '(' or ')':
            {
                tokens.Add(character is '('
                    ? new Token(TokenType.LeftParenthesis, character.ToString())
                    : new Token(TokenType.RightParenthesis, character.ToString()));
                break;
            }

            case >= 'a' and <= 'z' or '_':
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
                    throw new LexerException("Unterminated string");
                }

                // The closing quote.
                Advance();

                var content = source[(start + 1)..(Current - 1)];
                tokens.Add(new Token(TokenType.String, content));
                break;
            }

            default:
            {
                throw new LexerException($"Unexpected character '{character}'");
            }
        }
    }

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