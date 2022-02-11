using System.Collections.Generic;
using Ardaite.Markup.Lexing;
using Ardaite.Markup.Parsing;
using Ardaite.Presentation.Controls;
using Xunit;

namespace Ardaite.Presentation.Tests;

public class DeserializerTests
{
    [Fact]
    public void PropertiesSource_Outputs_CorrectControlHierarchy()
    {
        const string source = "(stack-panel spacing=\"5\" (label text=\"Label\"))";

        var actual = Deserializer.Run(Parser.Run(Lexer.Run(source)));

        var expected = new StackPanelControl(5)
        {
            Children = new List<IControl>()
            {
                new LabelControl("Label")
            }
        };

        var actualStackPanel = ((StackPanelControl) actual).Children[0];
        var actualLabel = ((LabelControl) actualStackPanel);

        var expectedLabel = (LabelControl) expected.Children[0];

        Assert.Equal(expectedLabel.Text, actualLabel.Text);
    }
}