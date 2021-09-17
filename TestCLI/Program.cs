using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TerminalUI;
using TerminalUI.Elements;

namespace TestCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Terminal.Run("TestCLI", "TerminalUI", Entry);
        }

        private static Text _text;
        private static KeyValueText _kvt;
        private static SplitLine _split;

        static async Task Entry(CancellationTokenSource cts)
        {
            _text = new Text("Starting text", show: true);
            Terminal.NextLine();

            _kvt = new KeyValueText("test", "meow", -10, show: true);
            Terminal.NextLine();

            _split = new SplitLine("meow", "hey", show: true);
            Terminal.NextLine();

            await Loop(cts);
        }

        static async Task Loop(CancellationTokenSource cts)
        {
            int i = 0;

            while (!cts.IsCancellationRequested)
            {
                try {
                    await Task.Delay(1000, cts.Token);
                }
                catch (TaskCanceledException) { }

                i++;

                _text.UpdateValue($"interval {i}");
                _kvt.UpdateValue($"interval {i}");
                
                // Terminal.WriteLine("meow");
            }

        }
    }
}
