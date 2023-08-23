using System.Text;
using Avalonia.Controls;
using AvaloniaUITestExample.UITesting.Robots;
using AvaloniaUITestExample.Views;

namespace AvaloniaUITestExample.UITesting.Tests;

public sealed class MainWindowUITest : UITest
{
    private MainWindowRobot Robot { get; }

    public MainWindowUITest()
    {
        var content = MainWindow.Instance!.Content;
        Robot = new(this, (Control)content!);
    }

    public override async Task RunAsync()
    {
        Robot.GreetingMsg.AssertIsVisible(this);
        Robot.CounterMsg.AssertIsVisible(this);
        Robot.BtClick.AssertIsVisible(this);
        Robot.BtReset.AssertIsVisible(this);

        await Robot.BtReset.ClickOn();

        Robot.GreetingMsg.AssertHasText(this, "Press F7 to run UI tests");
        Robot.CounterMsg.AssertHasText(this, "Clicked 0 times");
        Robot.BtClick.AssertHasText(this, "Click me");

        await Robot.BtClick.ClickOn();
        Robot.CounterMsg.AssertHasText(this, "Clicked 1 times");

        await Robot.BtClick.ClickOn();
        Robot.CounterMsg.AssertHasText(this, "Clicked 2 times");
        
        await Robot.BtReset.ClickOn();
        await Wait(1);
        Robot.CounterMsg.AssertHasText(this, "Clicked 0 times");
    }
}