using System;

namespace TerminalUI
{
    public static class Terminal
    {
        public static int Width => Console.WindowWidth;
        public static int Height => Console.WindowHeight;
        public static int Left => Console.CursorLeft;
        public static int Top => Console.CursorTop;

        public static ConsoleColor DefaultBackgroundColor { get; private set; } = Console.BackgroundColor;
        public static ConsoleColor DefaultForegroundColor { get; private set; } = Console.ForegroundColor;

        public static ConsoleColor BackgroundColor
        {
            get => Console.BackgroundColor;
            set => Console.BackgroundColor = value;
        }

        public static ConsoleColor ForegroundColor
        {
            get => Console.ForegroundColor;
            set => Console.ForegroundColor = value;
        }

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

        public static void WriteLine() 
            => Terminal.NextLine();

        public static void WriteLine(string inputString)
        {
            Console.Write(inputString);
            Terminal.NextLine();
        }

        public static void WriteLineColor(ConsoleColor color, char inputChar)
            => WriteLineColor(color, new string(new char[] { inputChar }));

        public static void WriteLineColor(ConsoleColor color, string inputString)
        {
            WriteColor(color, inputString);
            Terminal.NextLine();
        }


        public static void NextLine()
        {
            Console.CursorLeft = 0;
            Console.CursorTop += 1;
        }

        public static void SetDefaultBackgroundColor(ConsoleColor color)
            => DefaultBackgroundColor = color;

        public static void SetDefaultForegroundColor(ConsoleColor color)
            => DefaultForegroundColor = color;

        public static void ResetColor()
        {
            Console.BackgroundColor = DefaultBackgroundColor;
            Console.ForegroundColor = DefaultForegroundColor;
        }

        public static void Clear()
            => Console.Clear();
    }
}
