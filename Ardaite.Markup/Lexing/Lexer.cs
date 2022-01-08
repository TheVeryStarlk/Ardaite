namespace Ardaite.Markup.Lexing;

public class Lexer
{
    private int start;
    private int current;

    private readonly string source;
    private readonly List<Token> tokens;

    public Lexer(string source)
    {
        this.source = source;
        tokens = new List<Token>();
    }

    public List<Token> Run()
    {
        while (!IsAtEnd())
        {
            start = current;
            Scan();
        }

        tokens.Add(new Token(TokenType.EndOfFile, ""));
        return tokens;
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

                var identifier = source[start..current];
                tokens.Add(new Token(TokenType.Identifier, identifier));
                break;
            }

            case '"':
            {
                while (Peek() != '"' && !IsAtEnd())
                {
                    Advance();
                }

                // The closing quote.
                Advance();

                var content = source[(start + 1)..(current - 1)];
                tokens.Add(new Token(TokenType.String, content));
                break;
            }
        }
    }

    private bool Match(char expected)
    {
        if (Peek() == expected)
        {
            current++;
            return true;
        }

        return false;
    }

    private char Peek(int distance = 0)
        => IsAtEnd() ? '\0' : source[current + distance];

    private char Advance()
        => IsAtEnd() ? '\0' : source[current++];

    private bool IsAtEnd()
        => current >= source.Length;
}