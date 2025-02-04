using Avalonia.Controls;

namespace AlexandreHtrb.AvaloniaUITest;

public abstract class BaseRobot
{
    protected readonly Control rootView;

    protected BaseRobot(Control rootView) => this.rootView = rootView;

    protected A? GetChildView<A>(string name) where A : Control =>
        this.rootView.FindControl<A>(name);
}