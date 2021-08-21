using System;

namespace TerminalUI
{
    public static class Terminal
    {
        /// <summary>
        ///     Proxy method for <see cref="Console.Write(char)" />. Reserved for future use
        /// </summary>
        /// <param name="input">Character to write to the terminal</param>
        public static void Write(char input) => Console.Write(input);

        /// <summary>
        ///     Proxy method for <see cref="Console.Write(string)" />. Reserved for future use
        /// </summary>
        /// <param name="input">String to write to the terminal</param>
        public static void Write(string input) => Console.Write(input);

        /// <summary>
        ///     Parses a string for color control codes and then writes the string to the terminal
        /// </summary>
        /// <param name="input">String to parse and write to the terminal</param>
        public static void WriteParsed(string input) => Console.Write(input);
        
        public static void WriteColor(ConsoleColor color, char inputChar)
            => WriteColor(color, new string(new char[] { inputChar }));

        public static void WriteColor(ConsoleColor color, string inputString)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Terminal.Write(inputString);
            Console.ForegroundColor = originalColor;
        }

        // public static void WriteLineC(ConsoleColor color, char inputChar)
        //     => Write(color, new string(new char[] { inputChar }));

        // public static void WriteLineC(ConsoleColor color, string inputString)
        // {
        //     ConsoleColor originalColor = Console.ForegroundColor;
        //     Console.ForegroundColor = color;
        //     Console.WriteLine(inputString);
        //     Console.ForegroundColor = originalColor;
        // }


        public static void NextLine()
        {
            Console.CursorLeft = 0;
            Console.CursorTop += 1;
        }
    }
}
