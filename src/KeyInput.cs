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
        private static List<char> _buffer = new List<char>();
        private static volatile bool _echoChar = false;
        private static volatile bool _bufferChars = false;
        private static Action<string> _stringCallback = (_) => { };

        public static bool Listening => _listening;

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
            // if (!_listening)
            //     return;

            // Action cancelAction = () => _cts.Cancel();
            _cts.Cancel();

            _thread.Join();
        }

        private static void DoListen()
        {
            _listening = true;

            while (!_cts.Token.IsCancellationRequested)
            {
                if (Console.KeyAvailable && _listening)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(!_echoChar);
                    Key key = Key.FromConsoleKeyInfo(keyInfo);

                    if (_registeredKeys.ContainsKey(key))
                        _registeredKeys[key](key);
                    else if (_bufferChars == true)
                    {
                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            _echoChar = false;
                            _bufferChars = false;
                            _stringCallback(new string(_buffer.ToArray()));
                            _buffer.Clear();
                        }
                        else if (keyInfo.Key == ConsoleKey.Backspace)
                            _buffer.RemoveAt(_buffer.Count-1);
                        else
                            _buffer.Add(keyInfo.KeyChar);
                    }
                        
                }
                else
                    Thread.Sleep(10);
            }

            _listening = false;
        }

        public static void ReadString(Action<string> callback)
        {
            _echoChar = true;
            _bufferChars = true;
            _buffer.Clear();
            _stringCallback = callback;
        }

        private static void Pause()
            => _listening = false;

        private static void Resume()
            => _listening = true;
    }
}