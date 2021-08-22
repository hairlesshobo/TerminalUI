using System;
using System.Collections.Generic;
using System.Threading;

namespace TerminalUI
{
    public static class ShortcutKeyHelper
    {
        private static Dictionary<ConsoleKey, Action> _registeredKeys;
        private static CancellationTokenSource _cts;
        private static volatile bool _listening;
        private static Thread _thread;

        static ShortcutKeyHelper()
        {
            _registeredKeys = new Dictionary<ConsoleKey, Action>();
        }

        // TODO: Add support for modifiers

        public static bool RegisterKey(ConsoleKey key, Action callback)
        {
            if (callback == null)
                return false;

            if (_registeredKeys.ContainsKey(key))
                return false;

            _registeredKeys.Add(key, callback);

            return true;
        }

        public static bool UnregisterKey(ConsoleKey key)
        {
            if (!_registeredKeys.ContainsKey(key))
                return false;

            _registeredKeys.Remove(key);

            return true;
        }

        public static bool ListenForKeys()
        {
            _cts = new CancellationTokenSource();

            if (_listening)
                return false;

            _thread = new Thread(DoListen);
            _thread.Start();

            return true;
        }

        public static void StopListening()
        {
            if (!_listening)
                return;

            Action cancelAction = () => _cts.Cancel();

            _thread.Join();
        }

        private static void DoListen()
        {
            _listening = true;

            while (!_cts.Token.IsCancellationRequested)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();

                    if (_registeredKeys.ContainsKey(keyInfo.Key))
                        _registeredKeys[keyInfo.Key]();
                }
            }

            _listening = false;
        }
    }
}