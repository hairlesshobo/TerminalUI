using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TerminalUI.Elements
{
    public class StatusBarItem
    {
        public string Name { get; set; }
        public Key[] Keys { get; set; }
        public Func<Key, Task> Task { get; set; }
        public bool ShowKey { get; set; } = true;

        private Action removeCallback = () => { };

        public StatusBarItem()
        {

        }

        public StatusBarItem(string name)
        {
            this.Name = name;
        }

        public StatusBarItem(string name, Func<Key, Task> task, params Key[] keys)
        {
            this.Name = name;
            this.Task = task;
            this.Keys = keys;
        }

        public void Remove()
            => removeCallback();

        internal void AddRemoveCallback(Action callback)
            => removeCallback = callback;
    }

    public class StatusBar : Element
    {
        private List<StatusBarItem> _items = new List<StatusBarItem>();
        private List<StatusBarItem> _prevItems = new List<StatusBarItem>();
        private static StatusBar _instance;

        public StatusBar(params StatusBarItem[] items) => Init(items);
        public StatusBar() => Init(new StatusBarItem[]{ });

        private void Init(StatusBarItem[] items)
        {
            this.Height = 1;
            this.Width = Terminal.Width;
            
            this.TopLeftPoint = new TerminalPoint(0, Terminal.Height-1);
            this.TopRightPoint = new TerminalPoint(Terminal.Width, Terminal.Height-1);
            this.BottomLeftPoint = null;
            this.BottomRightPoint = null;

            ShowItems(items);
        }

        public void ShowItems(params StatusBarItem[] items)
        {
            // stash the current items so that, during redraw, we can remove any key bindings we have
            _prevItems = _items;

            _items = items.ToList();

            Redraw();
        }

        public override void Redraw()
        {
            this.RemovePreviousKeyBindings();
            
            TerminalPoint prevPoint = TerminalPoint.GetCurrent();
            ConsoleColor prevBackgroundColor = Console.BackgroundColor;

            this.TopLeftPoint.MoveTo();

            Console.BackgroundColor = ConsoleColor.DarkBlue;

            for (int i = 0; i < _items.Count; i++)
            {
                StatusBarItem item = _items[i];

                Terminal.Write(' ');

                if (item.Keys != null && item.Task != null)
                {
                    for (int k = 0; k < item.Keys.Length; k++)
                    {
                        Key key = item.Keys[k];

                        // TODO: Convert to task
                        KeyInput.RegisterKey(key, item.Task);

                        if (item.ShowKey)
                            Terminal.WriteColor(ConsoleColor.Magenta, $"{key.ToString()}");

                        if (k < (item.Keys.Length-1))
                            Terminal.WriteColor(ConsoleColor.DarkGray, "/");
                    }

                    if (item.ShowKey)
                        Terminal.Write(' ');
                }

                Terminal.Write(item.Name);
                Terminal.Write(' ');

                item.AddRemoveCallback(() => {
                    if (item.Keys != null)
                    {
                        foreach (Key key in item.Keys)
                            KeyInput.UnregisterKey(key);
                    }

                    _items.Remove(item);

                    this.Redraw();
                });

                if (i < (_items.Count-1))
                    Terminal.Write((char)BoxChars.ThinVertical);
            }

            int charsToBlank = this.Width - Terminal.Left;

            for (int i = 0; i < charsToBlank; i++)
                Terminal.Write(' ');

            Console.BackgroundColor = prevBackgroundColor;
            prevPoint.MoveTo();
        }

        private void RemovePreviousKeyBindings()
        {
            foreach (StatusBarItem prevItem in _prevItems)
            {
                if (prevItem.Keys != null)
                {
                    foreach (Key key in prevItem.Keys)
                        KeyInput.UnregisterKey(key);
                }
            }

            _prevItems.Clear();
        }

        public void RemoveItemByName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            var item = _items.FirstOrDefault(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

            if (item != null)
                item.Remove();
        }

        public static StatusBar GetInstance()
        {
            if (_instance == null)
                _instance = new StatusBar();

            return _instance;
        }
    }
}