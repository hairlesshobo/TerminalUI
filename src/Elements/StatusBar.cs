using System;
using System.Collections.Generic;
using System.Linq;

namespace TerminalUI.Elements
{
    public class StatusBarItem
    {
        public string Text { get; set; }
        public ConsoleKey? Key { get; set; }
        public Action Callback { get; set; }
        public bool ShowKey { get; set; } = true;
    }

    public class StatusBar : Element
    {
        public StatusBar(params StatusBarItem[] items) => Init(items.ToList());
        public StatusBar() => Init(new List<StatusBarItem>());

        private void Init(List<StatusBarItem> items)
        {
            this.Height = 1;
            this.Width = Terminal.Width;
            
            this.TopLeftPoint = new TerminalPoint(0, Terminal.Height);
            this.TopRightPoint = new TerminalPoint(Terminal.Width, Terminal.Height);
            this.BottomLeftPoint = null;
            this.BottomRightPoint = null;

            TerminalPoint prevPoint = TerminalPoint.GetCurrent();

            this.TopLeftPoint.MoveTo();

            ConsoleColor prevBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            foreach (StatusBarItem item in items)
            {
                Terminal.Write(' ');
                if (item.Key != null && item.Callback != null && item.ShowKey)
                {
                    ShortcutKeyHelper.RegisterKey(item.Key.Value, item.Callback);

                    Terminal.WriteColor(ConsoleColor.Magenta, item.Key.Value.ToString());
                    Terminal.Write(' ');
                }

                Terminal.Write(item.Text);
                Terminal.Write(' ');
            }

            int charsToBlank = this.Width - Terminal.Left;

            for (int i = 0; i < charsToBlank; i++)
                Terminal.Write(' ');

            Console.BackgroundColor = prevBackgroundColor;
        }

        public override void Redraw()
        {
            throw new NotImplementedException();
        }
    }
}