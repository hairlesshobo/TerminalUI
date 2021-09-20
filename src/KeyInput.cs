/*
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
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using FoxHollow.TerminalUI.Types;

namespace FoxHollow.TerminalUI
{
    /// <summary>
    ///     Class used for reading key input from the terminal
    /// </summary>
    public static class KeyInput
    {
        private static volatile bool _started = false;
        private static Dictionary<Key, Func<Key, Task>> _registeredKeys;
        private static CancellationTokenSource _cts;
        private static volatile bool _listening;
        private static Task _listenTask;
        private static List<char> _buffer = new List<char>();
        private static volatile bool _bufferChars = false;
        private static TerminalPoint _bufferStartPoint = null;
        private static Action<string> _stringCallback = (_) => { };

        /// <summary>
        ///     If true, the class is currently listening for key input from the terminal
        /// </summary>
        public static bool Listening => _listening;

        /// <summary>
        ///     Static constructor
        /// </summary>
        static KeyInput()
            => _registeredKeys = new Dictionary<Key, Func<Key, Task>>();

        /// <summary>
        ///     Register a callback to a specific console key
        /// </summary>
        /// <param name="key">Key to listen for</param>
        /// <param name="callback">Callback to execute if the key is received</param>
        /// <returns>true if the key wa ssuccessfully registerde, false otherwise</returns>
        public static bool RegisterKey(Key key, Func<Key, Task> callback)
        {
            if (callback == null)
                return false;

            if (_registeredKeys.ContainsKey(key))
                return false;

            _registeredKeys.Add(key, callback);

            return true;
        }

        /// <summary>
        ///     Unregister any callback that may exist for the specified key
        /// </summary>
        /// <param name="key">Key to unregister</param>
        /// <returns>true if the key previously existed and was successfully unregistered, false otherwise</returns>
        public static bool UnregisterKey(Key key)
        {
            if (!_registeredKeys.ContainsKey(key))
                return false;

            _registeredKeys.Remove(key);

            return true;
        }

        /// <summary>
        ///     Start the main loop that listens for keyboard input
        /// </summary>
        /// <param name="cts">CancellationTokenSource used to cancel listening</param>
        /// <returns>Task</returns>
        public static Task StartLoop(CancellationTokenSource cts = null)
        {
            if (_started)
                return Task.CompletedTask;

            _cts = cts ?? new CancellationTokenSource();

            return (_listenTask = Task.Run(MainLoopAsync));
        }

        /// <summary>
        ///     Clear all registered keys
        /// </summary>
        public static void ClearAllKeys()
            => _registeredKeys.Clear();

        /// <summary>
        ///     Stop listening for key input
        /// </summary>
        internal static void StopListening()
            => _cts.Cancel();

        /// <summary>
        ///     Synchronously wait until the listener stops before returning to the caller
        /// </summary>
        internal static void WaitForStop()
            => Task.WaitAll(_listenTask);

        // TODO: In the future, we may want to add the ability to specify additional code to execute during the main loop
        /// <summary>
        ///     Main loop that listens for key input as well as terminal resize events
        /// </summary>
        /// <returns>Task</returns>
        private static async Task MainLoopAsync()
        {
            _listening = true;
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;
            Stopwatch terminalResizeSw = new Stopwatch();

            while (!_cts.Token.IsCancellationRequested)
            {
                if (height != Console.WindowHeight || width != Console.WindowWidth)
                {
                    width = Console.WindowWidth;
                    height = Console.WindowHeight;

                    terminalResizeSw.Restart();
                }

                if (terminalResizeSw.IsRunning && terminalResizeSw.ElapsedMilliseconds > 150)
                {
                    Terminal.TerminalSizeChanged();
                    terminalResizeSw.Stop();
                }

                if (Console.KeyAvailable && _listening)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    Key key = Key.FromConsoleKeyInfo(keyInfo);

                    if (_registeredKeys.ContainsKey(key))
                        _ = _registeredKeys[key](key);
                    else if (_bufferChars == true)
                    {
                        if (keyInfo.Key == ConsoleKey.Enter)
                            _bufferChars = false;
                        else if (keyInfo.Key == ConsoleKey.Backspace)
                        {
                            int bufferLength = _buffer.Count;

                            if (bufferLength > 0)
                            {
                                Console.CursorLeft -= 1;
                                Console.Write(' ');
                                Console.CursorLeft -= 1;
                                _buffer.RemoveAt(bufferLength-1);
                            }
                        }
                        else
                        {
                            _buffer.Add(keyInfo.KeyChar);
                            Console.Write(keyInfo.KeyChar);
                        }
                    }
                        
                }
                else
                    await Task.Delay(10, _cts.Token);
            }

            _listening = false;
        }

        /// <summary>
        ///     Asynchronously read an string from the terminal
        /// </summary>
        /// <param name="cToken">Token used to cancel the key input</param>
        /// <returns>Task that provides a string. Null if the input was canceled</returns>
        public async static Task<string> ReadStringAsync(CancellationToken cToken)
        {
            _bufferChars = true;
            _bufferStartPoint = TerminalPoint.GetCurrent();
            _buffer.Clear();

            string result = await Task.Run(() => 
            {
                while (_bufferChars && cToken.IsCancellationRequested == false)
                    Task.Delay(10, cToken);

                if (cToken.IsCancellationRequested)
                    return null;

                return new string(_buffer.ToArray());
            }, cToken);

            _bufferChars = false;
            _buffer.Clear();

            return result;
        }
    }
}