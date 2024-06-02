using SFML.Graphics;
using SFML.System;

namespace Ardaite.Presentation.Controls;

public sealed class StackPanelControl(int spacing = 15) : ILayoutControl
{
    public int Spacing { get; set; } = spacing;
    public Vector2f Position { get; set; }
    public List<IControl> Children { get; set; } = new();
    public float Height => Children.Sum(child => child.Height);

    public void Update()
    {
        var currentSpacing = Position.Y;
        foreach (var control in Children)
        {
            control.Position = new Vector2f(control.Position.X, currentSpacing);
            currentSpacing += Spacing + control.Height;
            control.Update();
        }
    }

    public void Render(RenderTarget renderTarget)
    {
        foreach (var control in Children)
        {
            control.Render(renderTarget);
        }
    }
}