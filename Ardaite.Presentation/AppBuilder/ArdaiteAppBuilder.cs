using Ardaite.Markup.Lexing;
using Ardaite.Markup.Parsing;
using Ardaite.Presentation.Controls;
using SFML.Graphics;

namespace Ardaite.Presentation.AppBuilder;

public class ArdaiteAppBuilder : IFontSelectionStage, IMarkupSelectionStage, IAppBuildingStage
{
    private Font? font;
    private IControl? control;

    private ArdaiteAppBuilder()
    {
    }

    public static IFontSelectionStage CreateBuilder()
        => new ArdaiteAppBuilder();

    public IMarkupSelectionStage UseFont(string path)
    {
        font = new Font(path);
        return this;
    }

    public IAppBuildingStage UseMarkup(string path)
    {
        var tokens = Lexer.Run(File.ReadAllText(path));
        var tagNode = Parser.Run(tokens);
        control = Deserializer.Run(tagNode);

        return this;
    }

    public ArdaiteApp Build()
    {
        if (font is null || control is null)
        {
            throw new InvalidOperationException($"{nameof(font)} or {nameof(control)} was null");
        }

        return new ArdaiteApp(control, font);
    }
}