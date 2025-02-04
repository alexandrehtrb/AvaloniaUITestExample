using Avalonia.Controls;

namespace AlexandreHtrb.AvaloniaUITest.Example.UITesting.Robots;

public sealed class MainWindowRobot : BaseRobot
{
    public MainWindowRobot(Control rootView) : base(rootView) { }

    internal TextBlock GreetingMsg => GetChildView<TextBlock>("tbGreeting")!;
    internal TextBlock CounterMsg => GetChildView<TextBlock>("tbCounter")!;
    internal Button BtClick => GetChildView<Button>("btClick")!;
    internal Button BtReset => GetChildView<Button>("btReset")!;
}