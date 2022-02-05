using SFML.System;

namespace Ardaite.Presentation.Controls;

public interface IGraphicalControl : IControl
{
    public Vector2f Size { get; set; }
}