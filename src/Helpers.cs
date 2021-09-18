using System;
using System.Threading;
using System.Threading.Tasks;

namespace TerminalUI
{
    /// <summary>
    ///     Static class containing miscellaneous helper methods
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        ///     Setup a cancel watchdog. This is used to execute a callback method upon 
        ///     cancellation of the provided cToken
        /// </summary>
        /// <param name="cToken">Token to watch for cancellation</param>
        /// <param name="cancelCallback">Callback to execute if token is cancelled</param>
        /// <returns>
        ///     A CancellationTokenSource object that is used to abort the watchdog once 
        ///     it is no longer needed
        /// </returns>
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

        /// <summary>
        ///     Simple wrapper for Task.Delay that returns a boolean indicating whether 
        ///     the request was canceled or not.
        /// </summary>
        /// <param name="milliseconds">Milliseconds to delay</param>
        /// <param name="cToken">Token that allows for cancellation of the delay</param>
        /// <returns>
        ///     Booleaning indicating cancel status.
        /// 
        ///     false = Delay was canceled prematurely
        ///      true = Delay executed to the end without being canceled
        /// </returns>
        public static async Task<bool> Delay(int milliseconds, CancellationToken cToken)
        {
            try {
                await Task.Delay(milliseconds, cToken);

                return true;
            }
            catch(TaskCanceledException)
            {
                 return false;
            }
        }
    }
}