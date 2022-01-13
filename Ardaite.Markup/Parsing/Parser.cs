using Ardaite.Markup.Lexing;

namespace Ardaite.Markup.Parsing;

public class Parser : StreamReader<Token>
{
    private readonly Token[] source;

    public Parser(Token[] source) : base(source, source.Last())
    {
        this.source = source;
    }

    public TagNode Run()
    {
        return ParseExpression();
    }

    private TagNode ParseExpression()
    {
        var name = "";
        var properties = new Dictionary<string, StringNode>();
        var children = new List<TagNode>();

        while (!IsAtEnd())
        {
            if (Peek().TokenType is TokenType.LeftParenthesis)
            {
                Advance();

                if (Peek().TokenType is TokenType.Identifier)
                {
                    name = Peek().Value;
                    Advance();

                    while (true)
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
                            if (Peek().TokenType is TokenType.Identifier)
                            {
                                var property = Peek().Value;
                                Advance();

                                if (Peek().TokenType is TokenType.Equal)
                                {
                                    Advance();

                                    if (Peek().TokenType is TokenType.String)
                                    {
                                        properties.Add(property, new StringNode(Peek().Value));
                                    }
                                }
                            }
                        }

                        Advance();
                    }
                }
            }

            Advance();
        }

        return new TagNode(name, properties, children);
    }
}