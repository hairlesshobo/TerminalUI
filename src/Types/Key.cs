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
using System.Text;

namespace TerminalUI
{
    public class Key
    {
        // OK.. so.. I'm gonna go ahead and say right up from that this is ugly. Basically,
        // we only want a max of one object for each key combination.. so in other words,
        // if one caller registers Ctrl+C, and then later a different caller registers Ctrl+C
        // we want to make sure that both of those generate Key objects are referring to the same
        // object in memory. This is to ensure that dictionary lookups work. For now, we generate the 
        // key object, convert to string and look it up in the cache dictionary. 
        //
        // This may be not necessary, but it ensures that the dictionary lookup will always match
        private static Dictionary<string, Key> keyCache = new Dictionary<string, Key>();

        public ConsoleKey RootKey { get; private set; }
        public ConsoleModifiers? Modifiers { get; private set; } = null;
        public string Character => GetRootKeyCharacter();

        private Key() { }

        public static Key MakeKey(ConsoleKey rootKey, ConsoleModifiers? modifiers = null)
        {
            Key key = new Key();

            key.RootKey = rootKey;

            if (modifiers != null && modifiers.Value != 0)
                key.Modifiers = modifiers;

            if (keyCache.ContainsKey(key.ToString()))
                return keyCache[key.ToString()];
                
            keyCache.Add(key.ToString(), key);

            return key;
        }

        public static Key FromConsoleKeyInfo(ConsoleKeyInfo keyInfo)
        {
            Key key = Key.MakeKey(keyInfo.Key, keyInfo.Modifiers);

            if (keyCache.ContainsKey(key.ToString()))
                return keyCache[key.ToString()];
                
            keyCache.Add(key.ToString(), key);

            return key;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            
            if (Modifiers != null)
                builder.Append(Modifiers.Value.ToString().Replace("Control", "Ctrl").Replace(", ", "+") + "+");

            builder.Append(GetRootKeyCharacter());

            return builder.ToString();
        }

        public string GetRootKeyCharacter()
        {
            switch ((int)this.RootKey)
            {
                case 32: // Space
                    return "Space";

                case 33: // PageUp
                    return "PgUp";
                
                case 34: // PageDown
                    return "PgDn";
                
                case 35: // End
                    return ((char)ArrowChars.End).ToString();
                
                case 36: // Home
                    return ((char)ArrowChars.Home).ToString();
                
                case 37: // LeftArrow
                    return ((char)ArrowChars.Left).ToString();
                    
                case 38: // UpArrow
                    return ((char)ArrowChars.Up).ToString();
                    
                case 39: // RightArrow
                    return ((char)ArrowChars.Right).ToString();
                    
                case 40: // DownArrow
                    return ((char)ArrowChars.Down).ToString();
                    
            }

            return this.RootKey.ToString();
        }
    }
}