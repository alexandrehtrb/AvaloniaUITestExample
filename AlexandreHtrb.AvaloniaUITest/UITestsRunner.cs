using System.Text;

namespace AlexandreHtrb.AvaloniaUITest;

public static class UITestsRunner
{
    public static async Task<string> RunTestsAsync(TimeSpan waitingTimeBetweenActions, params UITest[] tests)
    {
        UITestActions.WaitingTimeAfterActions = waitingTimeBetweenActions;

        static TimeSpan SumTotalTime(IEnumerable<UITest> ts)
        {
            var totalTime = TimeSpan.Zero;
            foreach (var t in ts)
            {
                totalTime += t.ElapsedTime;
            }
            return totalTime;
        }

        StringBuilder allTestsLogsAppender = new();
        foreach (var test in tests)
        {
            await RunTestAsync(allTestsLogsAppender, test);
        }
        var totalTime = SumTotalTime(tests);
        string fmtTime = @"hh'h'mm'm'ss's'";
        allTestsLogsAppender.AppendLine("TOTAL TIME: " + totalTime.ToString(fmtTime));
        return allTestsLogsAppender.ToString();
    }

    private static async Task RunTestAsync(StringBuilder allTestsLogsAppender, UITest test)
    {
        Exception? possibleException = null;
        try
        {
            test.Start();
            await test.RunAsync();
        }
        catch (Exception ex)
        {
            test.Successful = false;
            possibleException = ex;
            
        }
        finally
        {
            test.Finish();
            if (!string.IsNullOrWhiteSpace(test.Log))
            {
                allTestsLogsAppender.Append(test.Log);
            }
            if (possibleException != null)
            {
                allTestsLogsAppender.AppendLine(possibleException.ToString());
            }
            allTestsLogsAppender.AppendLine($"{test.TestName}: {(test.Successful == true ? "SUCCESS" : "FAILED")} {test.TotalElapsedSeconds}s");
        }
    }
}