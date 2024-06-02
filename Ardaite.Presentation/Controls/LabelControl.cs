using SFML.Graphics;
using SFML.System;

namespace Ardaite.Presentation.Controls;

public sealed class LabelControl(string text = "", string color = "black", int size = 15) : IGraphicalControl
{
    public string Text { get; set; } = text;
    public string Color { get; set; } = color.ToLower();
    public Vector2f Position { get; set; }
    public Vector2f Size { get; set; } = new(size, size);
    public float Height { get; private set; }

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

        var text = new Text(Text, font)
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
        };
        
        text.Draw(renderTarget, RenderStates.Default);

        Height = text.GetLocalBounds().Height;
    }
}