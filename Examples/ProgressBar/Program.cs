﻿/*
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

using System.Threading;
using System.Threading.Tasks;
using FoxHollow.TerminalUI;
using FoxHollow.TerminalUI.Elements;

namespace Examples.ProgressBarExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Terminal.Run("ProgressBar Example", "TerminalUI", RunExample);
        }

        static async Task RunExample(CancellationTokenSource cts)
        {
            Text text = new Text("Loading the matrix ...", show: true);
            Terminal.NextLine();
            
            ProgressBar progress = new ProgressBar(show: true);

            for (int i = 0; i <= 10; i++)
            {
                if (cts.IsCancellationRequested)
                    return;

                progress.UpdateProgress((double)i / 10.0);
                await TaskHelpers.Delay(400, cts.Token);
            }
        }
    }
}
