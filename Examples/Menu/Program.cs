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
using System.Threading;
using System.Threading.Tasks;
using FoxHollow.TerminalUI;
using FoxHollow.TerminalUI.Elements;
using FoxHollow.TerminalUI.Types;

namespace Examples.MenuExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Terminal.Run("Menu Example", "TerminalUI", RunExample);
        }

        static async Task RunExample(CancellationTokenSource cts)
        {
            List<MenuEntry> entries = new List<MenuEntry>()
            {
                new MenuEntry()
                {
                    Name = "Header entry",
                    Header = true
                },
                new MenuEntry()
                {
                    Name = "Standard entry",
                    Task = async (cToken) => await DoCountdown("You selected the standard entry", cToken)
                },
                new MenuEntry()
                {
                    Name = "Disabled entry",
                    Task = (ctoken) => Task.CompletedTask,
                    Disabled = true
                },
                new MenuEntry()
                {
                    Name = "Dangerous entry",
                    ForegroundColor = ConsoleColor.Red,
                    Task = async (cToken) => await DoCountdown("You seem to enjoy living dangeously...", cToken)
                },
            };

            Menu menu = new Menu(entries);
            menu.QuitCallback = () => {
                cts.Cancel();
                return Task.CompletedTask;
            };

            while (!cts.IsCancellationRequested)
                await menu.ShowAsync(cts.Token);
        }

        private static async Task DoCountdown(string message, CancellationToken ctoken)
        {
            Text text = new Text(message, show: true);
            Terminal.NextLine();
            Terminal.NextLine();
            Text countdown = new Text(show: true);

            for (int i = 3; i > 0; i--)
            {
                countdown.UpdateValue($"{i}...");

                if (!await TaskHelpers.Delay(1000, ctoken))
                    return;
            }
        }
    }
}
