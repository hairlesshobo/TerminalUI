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
                builder.Append(Modifiers.Value.ToString().Replace(", ", "+") + "+");

            builder.Append(GetRootKeyCharacter());

            return builder.ToString();
        }

        public string GetRootKeyCharacter()
        {
            switch ((int)this.RootKey)
            {
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