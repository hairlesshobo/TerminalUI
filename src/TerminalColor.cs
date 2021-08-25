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

        public static ConsoleColor CliMenuCursorBackground { get; set; } = ConsoleColor.DarkGray;
        public static ConsoleColor CliMenuCursorForeground { get; set; } = ConsoleColor.DarkGreen;
        public static ConsoleColor CliMenuCursorArrow { get; set; } = ConsoleColor.Green;
        public static ConsoleColor CliMenuDisabledForeground { get; set; } = ConsoleColor.DarkGray;
        public static ConsoleColor CliMenuHeaderForeground { get; set; } = ConsoleColor.Cyan;


    }
}