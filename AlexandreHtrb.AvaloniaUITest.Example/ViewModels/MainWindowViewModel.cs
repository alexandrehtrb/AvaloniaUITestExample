using AlexandreHtrb.AvaloniaUITest.Example.UITesting.Tests;
using AlexandreHtrb.AvaloniaUITest.Example.Views;
using MsBox.Avalonia.Enums;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive;

namespace AlexandreHtrb.AvaloniaUITest.Example.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    [Reactive]
    public string Greeting { get; set; }

    private int clickCounter = 0;

    [Reactive]
    public string ClickedCounterMessage { get; set; }

    public ReactiveCommand<Unit, Unit> ClickCmd { get; }

    public ReactiveCommand<Unit, Unit> ResetCmd { get; }

    public ReactiveCommand<Unit, Unit> RunUITestsCmd { get; }

    public MainWindowViewModel()
    {
#if DEBUG || UI_TESTS_ENABLED
        Greeting = "Press F7 to run UI tests";
#else
        Greeting = "Welcome to Avalonia!";
#endif
        ClickedCounterMessage = "Clicked 0 times";
        ClickCmd = ReactiveCommand.Create(Click);
        ResetCmd = ReactiveCommand.Create(Reset);
        RunUITestsCmd = ReactiveCommand.CreateFromTask(RunUITestsAsync);
    }

    private void Click()
    {
        this.clickCounter++;
        ClickedCounterMessage = $"Clicked {this.clickCounter} times";
    }

    private void Reset()
    {
        this.clickCounter = 0;
        ClickedCounterMessage = $"Clicked {this.clickCounter} times";
    }

#if DEBUG || UI_TESTS_ENABLED
    private Task RunUITestsAsync()
    {
        UITestsPrepareWindowViewModel vm = new(
            defaultActionWaitingTimeInMs: 20,
            uiTests: [
                new MainWindowUITest()
            ],
            uiTestsFinishedCallback: (resultsLog) =>
            {
                Dialogs.ShowDialog(
                    title: "UI tests results",
                    message: resultsLog,
                    buttons: ButtonEnum.Ok);
            });
        UITestsPrepareWindow uiTestsPrepareWindow = new(vm);
        uiTestsPrepareWindow.Show(MainWindow.Instance!);
        return Task.CompletedTask;
    }
#else
    private Task RunUITestsAsync() => Task.CompletedTask;
#endif

}
