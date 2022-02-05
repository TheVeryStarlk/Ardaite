namespace Ardaite.Presentation.Controls;

public interface ILayoutControl : IControl
{
    public List<IControl> Children { get; set; }
}