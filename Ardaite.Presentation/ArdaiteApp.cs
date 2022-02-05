using Ardaite.Presentation.Controls;
using SFML.Graphics;

namespace Ardaite.Presentation;

public class ArdaiteApp
{
    private readonly IControl control;

    internal ArdaiteApp(IControl control, Font font)
    {
        this.control = control;
        ResourceProvider.Font = font;
    }

    public void Run()
    {
        var windowControl = new WindowControl(control);

        while (windowControl.RenderWindow.IsOpen)
        {
            windowControl.Update();
            windowControl.Render(windowControl.RenderWindow);
        }
    }
}