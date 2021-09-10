/**
 *  TerminalUI - Simple terminal widgets for C#
 * 
 *  Copyright (c) 2021 Steve Cross <flip@foxhollow.cc>
 *
 *  This program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU Lesser General Public License as published by
 *  the Free Software Foundation; either version 2.1 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public License
 *  along with this program; if not, see <http://www.gnu.org/licenses/>.
 */

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
        private static Task _listenTask;
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

        public static Task StartLoop()
        {
            if (_started)
                return Task.CompletedTask;

            _cts = new CancellationTokenSource();

            return (_listenTask = Task.Run(DoListen));
        }

        public static void ClearAllKeys()
            => _registeredKeys.Clear();

        internal static void StopListening()
            => _cts.Cancel();

        internal static void WaitForStop()
            => Task.WaitAll(_listenTask);

        private static async Task DoListen()
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
                        _ = _registeredKeys[key](key);
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
                    await Task.Delay(10, _cts.Token);
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