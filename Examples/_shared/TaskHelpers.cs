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
using System.Threading;
using System.Threading.Tasks;

namespace Examples
{
    public static class TaskHelpers
    {
        /// <summary>
        ///     Simple wrapper for Task.Delay that returns a boolean indicating whether 
        ///     the request was canceled or not.
        /// </summary>
        /// <param name="milliseconds">Milliseconds to delay</param>
        /// <param name="cToken">Token that allows for cancellation of the delay</param>
        /// <returns>
        ///     Booleaning indicating cancel status.
        /// 
        ///     false = Delay was canceled prematurely
        ///      true = Delay executed to the end without being canceled
        /// </returns>
        public static async Task<bool> Delay(int milliseconds, CancellationToken cToken)
        {
            try {
                await Task.Delay(milliseconds, cToken);

                return true;
            }
            catch(TaskCanceledException)
            {
                 return false;
            }
        }
    }
}