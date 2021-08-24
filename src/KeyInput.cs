using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TerminalUI
{
    public static class KeyInput
    {
        private static volatile bool _started = false;
        private static Dictionary<Key, Func<Key, Task>> _registeredKeys;
        private static Task<string> _readStringTask = null;
        private static CancellationTokenSource _cts;
        private static volatile bool _listening;
        private static Thread _thread;
        private static List<char> _buffer = new List<char>();
        private static volatile bool _echoChar = false;
        private static volatile bool _bufferChars = false;
        private static Action<string> _stringCallback = (_) => { };

        public static bool Listening => _listening;

        static KeyInput()
            => _registeredKeys = new Dictionary<Key, Func<Key, Task>>();

        public static bool RegisterKey(Key key, Func<Key, Task> callback)
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

        public static void StartLoop()
        {
            if (_started)
                return;

            _cts = new CancellationTokenSource();

            DoListen();

            // _thread = new Thread(DoListen);
            // _thread.Start();

            // return true;
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

                    Terminal.WriteDebugLine($"KeyInput: {key.ToString()}");

                    if (_registeredKeys.ContainsKey(key))
                        _registeredKeys[key](key);
                    else if (_bufferChars == true)
                    {
                        if (keyInfo.Key == ConsoleKey.Enter)
                            _bufferChars = false;
                        else if (keyInfo.Key == ConsoleKey.Backspace)
                            _buffer.RemoveAt(_buffer.Count-1);
                        else
                            _buffer.Add(keyInfo.KeyChar);
                    }
                        
                }
                else
                    Task.Delay(10, _cts.Token);
            }

            _listening = false;
        }

        public async static Task<string> ReadStringAsync(CancellationToken cToken)
        {
            _echoChar = true;
            _bufferChars = true;
            _buffer.Clear();

            string result = await Task.Run(() => 
            {
                while (_bufferChars && cToken.IsCancellationRequested == false)
                    Task.Delay(10, cToken);

                if (cToken.IsCancellationRequested)
                    return null;

                return new string(_buffer.ToArray());
            }, cToken);

            _echoChar = false;
            _bufferChars = false;
            _buffer.Clear();

            return result;
        }

        private static void Pause()
            => _listening = false;

        private static void Resume()
            => _listening = true;
    }
}