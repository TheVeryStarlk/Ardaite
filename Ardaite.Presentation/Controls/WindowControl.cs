using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Ardaite.Presentation.Controls;

public sealed class WindowControl : IControl
{
    public Vector2f Position { get; set; }
    public RenderWindow RenderWindow { get; }
    public float Height { get; }

    private readonly IControl control;

    public WindowControl(IControl control, uint width = 500, uint height = 500, string title = "Ardaite")
    {
        this.control = control;

        RenderWindow = new RenderWindow(new VideoMode(width, height), title);
        RenderWindow.Closed += (_, _) => RenderWindow.Close();
        Height = height;
    }

    public void Update()
    {
        RenderWindow.DispatchEvents();
        control.Update();
    }

    public void Render(RenderTarget renderTarget)
    {
        RenderWindow.Clear(Color.White);
        control.Render(RenderWindow);
        RenderWindow.Display();
    }
}