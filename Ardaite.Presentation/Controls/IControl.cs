using SFML.Graphics;
using SFML.System;

namespace Ardaite.Presentation.Controls;

public interface IControl
{
    public Vector2f Position { get; set; }
    
    public float Height { get; }

    public void Update();

    public void Render(RenderTarget renderTarget);
}