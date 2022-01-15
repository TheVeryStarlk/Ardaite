using Ardaite.Markup.Exceptions;
using Ardaite.Markup.Lexing;

namespace Ardaite.Markup.Parsing;

public class Parser : StreamReader<Token>
{
    public Parser(Token[] source) : base(source, source.Last())
    {
    }

    public static TagNode Run(Token[] source)
        => new Parser(source).Run();

    public TagNode Run()
    {
        return ParseExpression();
    }

    private TagNode ParseExpression()
    {
        Consume(TokenType.LeftParenthesis);
        var name = Consume(TokenType.Identifier).Value;
        var properties = new Dictionary<string, StringNode>();
        var children = new List<TagNode>();

        while (!IsAtEnd())
        {
            if (Peek().TokenType is TokenType.RightParenthesis)
            {
                return new TagNode(name, properties, children);
            }
            else if (Peek().TokenType is TokenType.LeftParenthesis)
            {
                children.Add(ParseExpression());
            }
            else
            {
                var property = Consume(TokenType.Identifier).Value;
                Consume(TokenType.Equal);

                if (Peek().TokenType is TokenType.String)
                {
                    properties.Add(property, new StringNode(Peek().Value));
                }
                else
                {
                    ThrowError(TokenType.String, Peek().TokenType);
                }
            }

            Advance();
        }

        return new TagNode(name, properties, children);
    }

    private void ThrowError(TokenType expected, TokenType actual)
        => throw new ParserException(expected, actual, Line, Current);

    private Token Consume(TokenType expected)
    {
        var tokenType = Peek().TokenType;
        if (tokenType == expected)
        {
            return Advance();
        }

        throw new ParserException(expected, tokenType, Line, Current);
    }
}