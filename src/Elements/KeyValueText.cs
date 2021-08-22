using System;

namespace TerminalUI.Elements
{
    public class KeyValueText
    {
        private TerminalPoint kvtPoint;
        private TerminalPoint kvtRightPoint;
        private int prevRightWidth = 0;

        public KeyValueText(string keyName, string valueText, int leftWidth = 0)
        {
            kvtPoint = TerminalPoint.GetCurrent();

            if (leftWidth < 0)
                keyName = keyName.PadLeft(leftWidth * -1);
            else if (leftWidth > 0)
                keyName = keyName.PadRight(leftWidth);
            
            Terminal.Write($"{keyName}: ");

            kvtRightPoint = TerminalPoint.GetCurrent();

            this.UpdateValue(valueText);
        }

        public void UpdateValue(string newText)
        {
            TerminalPoint previousPoint = TerminalPoint.GetCurrent();
            kvtRightPoint.MoveTo();

            Console.Write(newText);
            
            if (newText.Length < prevRightWidth)
            {
                int spacesToClear = prevRightWidth - newText.Length;

                for (int i = 0; i < spacesToClear; i++)
                    Console.Write(' ');
            }

            prevRightWidth = newText.Length;

            previousPoint.MoveTo();
        }
    }
}