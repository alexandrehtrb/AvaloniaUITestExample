using Avalonia.Threading;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace AlexandreHtrb.AvaloniaUITest;

public class UITestViewModel : ReactiveObject
{
    private bool includeField;
    public bool Include
    {
        get => this.includeField;
        set => this.RaiseAndSetIfChanged(ref this.includeField, value);
    }

    private string? nameField;
    public string? Name
    {
        get => this.nameField;
        set => this.RaiseAndSetIfChanged(ref this.nameField, value);
    }

    public UITest Test { get; }

    public UITestViewModel(string name, UITest test)
    {
        Include = true;
        Name = name;
        Test = test;
    }
}

public class UITestsPrepareWindowViewModel : ReactiveObject
{
    private readonly Action<string> uiTestsFinishedCallback;

    private int actionsWaitingtimeInMsField;
    public int ActionsWaitingTimeInMs
    {
        get => this.actionsWaitingtimeInMsField;
        set => this.RaiseAndSetIfChanged(ref this.actionsWaitingtimeInMsField, value);
    }

    public ObservableCollection<UITestViewModel> Tests { get; }

    public ReactiveCommand<Unit, Unit> SelectAllTestsCmd { get; }

    public ReactiveCommand<Unit, Unit> DeselectAllTestsCmd { get; }

    public UITestsPrepareWindowViewModel(int defaultActionWaitingTimeInMs, UITest[] uiTests, Action<string> uiTestsFinishedCallback)
    {
        this.uiTestsFinishedCallback = uiTestsFinishedCallback;
        ActionsWaitingTimeInMs = defaultActionWaitingTimeInMs;
        Tests = new(uiTests.Select(t => new UITestViewModel(t.TestName, t)));
        SelectAllTestsCmd = ReactiveCommand.Create(SelectAllTests);
        DeselectAllTestsCmd = ReactiveCommand.Create(DeselectAllTests);
    }

    private void SelectAllTests()
    {
        foreach (var test in Tests)
        {
            test.Include = true;
        }
    }

    private void DeselectAllTests()
    {
        foreach (var test in Tests)
        {
            test.Include = false;
        }
    }

    internal void RunTests() => Dispatcher.UIThread.Post(async () => await RunTestsAsync());

    protected async Task RunTestsAsync()
    {
        var waitingTimeBetweenActions = TimeSpan.FromMilliseconds(ActionsWaitingTimeInMs);
        var tests = Tests.Where(t => t.Include).Select(t => t.Test).ToArray();

        string resultsLog = await UITestsRunner.RunTestsAsync(waitingTimeBetweenActions, tests);

        this.uiTestsFinishedCallback(resultsLog);
    }
}