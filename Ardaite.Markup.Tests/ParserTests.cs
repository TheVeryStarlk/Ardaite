using System.Collections.Generic;
using System.Linq;
using Ardaite.Markup.Lexing;
using Ardaite.Markup.Parsing;
using Xunit;

namespace Ardaite.Markup.Tests;

public sealed class ParserTests
{
    [Fact]
    public void IdentifierSource_Outputs_CorrectIdentifier()
    {
        var source = Lexer.Run("(button)");

        var actual = Parser.Run(source);

        var expected = new TagNode("button", new Dictionary<string, StringNode>(), new List<TagNode>());

        Assert.Equal(expected.Identifier, actual.Identifier);
    }

    [Fact]
    public void PropertiesSource_Outputs_CorrectProperties()
    {
        var source = Lexer.Run("(button text=\"button\")");

        var actual = Parser.Run(source);

        var expected = new TagNode("button", new Dictionary<string, StringNode>()
        {
            { "text", new StringNode("button") }
        }, new List<TagNode>());

        Assert.Equal(expected.Properties, actual.Properties);
    }

    [Fact]
    public void ChildrenSource_Outputs_CorrectChildren()
    {
        var source = Lexer.Run("(grid (button))");

        var actual = Parser.Run(source);

        var expected = new TagNode("grid", new Dictionary<string, StringNode>(), new List<TagNode>()
        {
            new("button", new Dictionary<string, StringNode>(), new List<TagNode>())
        });

        Assert.Equal(expected.Children.FirstOrDefault()?.Identifier, actual.Children.FirstOrDefault()?.Identifier);
    }
}