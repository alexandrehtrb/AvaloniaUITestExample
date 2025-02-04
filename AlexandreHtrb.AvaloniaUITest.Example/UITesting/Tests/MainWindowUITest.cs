using AlexandreHtrb.AvaloniaUITest.Example.UITesting.Robots;
using AlexandreHtrb.AvaloniaUITest.Example.Views;
using Avalonia.Controls;

namespace AlexandreHtrb.AvaloniaUITest.Example.UITesting.Tests;

public sealed class MainWindowUITest : UITest
{
    private MainWindowRobot Robot { get; }

    public MainWindowUITest()
    {
        var content = MainWindow.Instance!.Content;
        Robot = new((Control)content!);
    }

    public override async Task RunAsync()
    {
        AppendToLog("Starting my test!");

        Robot.GreetingMsg.AssertIsVisible();
        Robot.CounterMsg.AssertIsVisible();
        Robot.BtClick.AssertIsVisible();
        Robot.BtReset.AssertIsVisible();
        await Robot.BtReset.ClickOn();

        Robot.GreetingMsg.AssertHasText("Press F7 to run UI tests");
        Robot.CounterMsg.AssertHasText("Clicked 0 times");
        Robot.BtClick.AssertHasText("Click me");

        await Robot.BtClick.ClickOn();
        Robot.CounterMsg.AssertHasText("Clicked 1 times");

        await Robot.BtClick.ClickOn();
        Robot.CounterMsg.AssertHasText("Clicked 2 times");

        await Robot.BtReset.ClickOn();
        await Wait(1);
        Robot.CounterMsg.AssertHasText("Clicked 0 times");
    }
}