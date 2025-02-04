using AlexandreHtrb.AvaloniaUITest.Example.Views;
using Avalonia.Controls;
using Avalonia.Threading;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;

namespace AlexandreHtrb.AvaloniaUITest.Example;

internal static class Dialogs
{
    internal static void ShowDialog(string title, string message, ButtonEnum buttons, Action onButtonOkClicked)
    {
        Task asyncTask()
        {
            Dispatcher.UIThread.Post(onButtonOkClicked);
            return Task.CompletedTask;
        }

        ShowDialog(title, message, buttons, asyncTask);
    }

    internal static void ShowDialog(string title, string message, ButtonEnum buttons, Func<Task>? onButtonOkClicked = null)
    {
        var msgbox = MessageBoxManager.GetMessageBoxStandard(
            new MessageBoxStandardParams()
            {
                ContentTitle = title,
                ContentMessage = message,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                ButtonDefinitions = buttons
            });
        Dispatcher.UIThread.Post(async () =>
        {
            var buttonResult = await msgbox.ShowWindowDialogAsync(MainWindow.Instance!);
            if (buttonResult == ButtonResult.Ok)
            {
                var task = onButtonOkClicked?.Invoke();
                if (task is not null) await task;
            }
        });
    }
}