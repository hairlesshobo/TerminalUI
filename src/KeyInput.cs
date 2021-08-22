using System;
using System.Collections.Generic;
using System.Threading;

namespace TerminalUI
{
    public static class KeyInput
    {
        private static Dictionary<Key, Action<Key>> _registeredKeys;
        private static CancellationTokenSource _cts;
        private static volatile bool _listening;
        private static Thread _thread;

        static KeyInput()
        {
            _registeredKeys = new Dictionary<Key, Action<Key>>();
        }

        public static bool RegisterKey(Key key, Action<Key> callback)
        {
            if (callback == null)
                return false;

            if (_registeredKeys.ContainsKey(key))
                return false;

            _registeredKeys.Add(key, callback);

            return true;
        }

        public static bool UnregisterKey(Key key)
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

        public static void ClearAllKeys()
            => _registeredKeys.Clear();

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
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    Key key = Key.FromConsoleKeyInfo(keyInfo);

                    if (_registeredKeys.ContainsKey(key))
                        _registeredKeys[key](key);
                }
            }

            _listening = false;
        }
    }
}