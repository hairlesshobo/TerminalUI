using System;
using System.Threading;
using System.Threading.Tasks;

namespace TerminalUI
{
    internal static class Helpers
    {
        internal static CancellationTokenSource SetupTaskWatchdog(CancellationToken cToken, Action cancelCallback)
        {
            if (cancelCallback is null)
                throw new ArgumentNullException(nameof(cancelCallback));

            CancellationTokenSource watchdogCts = CancellationTokenSource.CreateLinkedTokenSource(cToken);

            // this is used like a watchdog to sit and wait for a cancel request.
            // if it happens, it'll cancel out of the menu
            Task.Run(async () => {
                try
                {
                    while (!watchdogCts.Token.IsCancellationRequested)
                        await Task.Delay(100000, watchdogCts.Token);
                }
                catch
                {
                    if (cToken.IsCancellationRequested)
                        return true;
                    else
                        return false;
                }

                return false;
            }).ContinueWith((task) => {
                if (task.Result == true)
                    cancelCallback();
            });

            return watchdogCts;
        }
    }
}