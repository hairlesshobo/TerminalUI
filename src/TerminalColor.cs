using System;

namespace TerminalUI
{
    public static class TerminalColor
    {
        public static ConsoleColor DefaultBackground { get; set; } = ConsoleColor.Black;
        public static ConsoleColor DefaultForeground { get; set; } = ConsoleColor.White;
        
        // DarkCyan, DarkYellow
        public static ConsoleColor ProgressBarFilled { get; set; } = ConsoleColor.DarkYellow;
        public static ConsoleColor ProgressBarUnfilled { get; set; } = ConsoleColor.DarkGray;

        public static ConsoleColor HeaderLeft { get; set; } = ConsoleColor.Magenta;
        public static ConsoleColor HeaderRight { get; set; } = ConsoleColor.White;
        public static ConsoleColor HeaderBackground { get; set; } = ConsoleColor.Black;

    }
}