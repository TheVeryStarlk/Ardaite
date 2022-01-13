namespace Ardaite.Markup.Parsing;

public record TagNode(string Identifier, Dictionary<string, StringNode> Properties, List<TagNode> Children);