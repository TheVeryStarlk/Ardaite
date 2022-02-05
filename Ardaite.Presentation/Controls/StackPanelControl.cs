using SFML.Graphics;
using SFML.System;

namespace Ardaite.Presentation.Controls;

public class StackPanelControl : ILayoutControl
{
    public int Spacing { get; set; }
    public Vector2f Position { get; set; }
    public List<IControl> Children { get; set; }

    public StackPanelControl(int spacing = 15)
    {
        Spacing = spacing;
        Children = new List<IControl>();
    }

    public void Update()
    {
        var currentSpacing = Position.Y;
        foreach (var control in Children)
        {
            control.Position = new Vector2f(control.Position.X, currentSpacing);
            currentSpacing += Spacing;
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