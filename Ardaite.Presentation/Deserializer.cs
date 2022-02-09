using System.Runtime.Serialization;
using Ardaite.Markup.Parsing;
using Ardaite.Presentation.Controls;
using Ardaite.Presentation.Exceptions;

namespace Ardaite.Presentation;

public class Deserializer
{
    private readonly TagNode source;

    public Deserializer(TagNode source)
    {
        this.source = source;
    }

    public static IControl Run(TagNode source)
        => new Deserializer(source).Run();

    private IControl Run()
        => Deserialize(source);

    private IControl Deserialize(TagNode tagNode)
    {
        switch (tagNode.Identifier)
        {
            case "window":
            {
                var widthProperty = GetProperty(tagNode.Properties, "width", "500");
                var heightProperty = GetProperty(tagNode.Properties, "height", "500");
                var titleProperty = GetProperty(tagNode.Properties, "title", "Ardaite");

                var children = tagNode.Children.Select(Deserialize).ToList();

                if (children.Count is > 1 or 0)
                {
                    throw new DeserializerException("Window control should have only one child");
                }

                return new WindowControl(children[0], (uint) ParseOrThrow(widthProperty),
                    (uint) ParseOrThrow(heightProperty), titleProperty);
            }

            case "stack-panel":
            {
                var spacingProperty = GetProperty(tagNode.Properties, "spacing", "15");
                var children = tagNode.Children.Select(Deserialize).ToList();

                return new StackPanelControl(ParseOrThrow(spacingProperty))
                {
                    Children = children
                };
            }

            case "label":
            {
                var textProperty = GetProperty(tagNode.Properties, "text", "");
                var colorProperty = GetProperty(tagNode.Properties, "color", "black");
                var sizeProperty = GetProperty(tagNode.Properties, "size", "15");

                return new LabelControl(textProperty, colorProperty, ParseOrThrow(sizeProperty));
            }

            default:
            {
                throw new SerializationException($"Unrecognized node {tagNode.Identifier}");
            }
        }
    }

    private string GetProperty(Dictionary<string, StringNode> properties, string name, string defaultValue)
        => properties.GetValueOrDefault(name, new StringNode(defaultValue)).Value;

    private int ParseOrThrow(string value)
    {
        if (!int.TryParse(value, out var number))
        {
            throw new SerializationException($"Incorrect value {value}");
        }

        return number;
    }
}