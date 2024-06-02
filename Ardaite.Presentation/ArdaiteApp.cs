using Ardaite.Presentation.Controls;
using Ardaite.Presentation.Exceptions;
using SFML.Graphics;

namespace Ardaite.Presentation;

public sealed class ArdaiteApp
{
    private readonly IControl control;

    internal ArdaiteApp(IControl control, Font font)
    {
        this.control = control;
        ResourceProvider.Font = font;
    }

    public void Run()
    {
        if (control is WindowControl window)
        {
            while (window.RenderWindow.IsOpen)
            {
                window.Update();
                window.Render(window.RenderWindow);
            }
        }
        else
        {
            throw new ArdaiteAppException("The parent control should be a window");
        }
    }
}