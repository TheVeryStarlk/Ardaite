using SFML.Graphics;
using SFML.System;

namespace Ardaite.Presentation.Controls;

public class LabelControl : IGraphicalControl
{
    public string Content { get; set; }
    public string Color { get; set; }
    public Vector2f Position { get; set; }
    public Vector2f Size { get; set; }

    public LabelControl(string content, string color = "black", int size = 15)
    {
        Content = content;
        Size = new Vector2f(size, size);
        Color = color.ToLower();
    }

    public void Update()
    {
    }

    public void Render(RenderTarget renderTarget)
    {
        var font = ResourceProvider.Font;
        if (font is null)
        {
            throw new InvalidOperationException($"{font} was null");
        }

        new Text(Content, font)
        {
            CharacterSize = (uint) Size.X,
            Position = Position,
            FillColor = Color switch
            {
                "black" => SFML.Graphics.Color.Black,
                "white" => SFML.Graphics.Color.White,
                "red" => SFML.Graphics.Color.Red,
                "green" => SFML.Graphics.Color.Green,
                "blue" => SFML.Graphics.Color.Blue,
                "yellow" => SFML.Graphics.Color.Yellow,
                "magenta" => SFML.Graphics.Color.Magenta,
                "cyan" => SFML.Graphics.Color.Cyan,
                "transparent" => SFML.Graphics.Color.Transparent,
                _ => SFML.Graphics.Color.Black
            }
        }.Draw(renderTarget, RenderStates.Default);
    }
}