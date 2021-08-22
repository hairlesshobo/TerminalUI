using System;

namespace TerminalUI.Elements
{
    public class KeyValueText : Element
    {
        private TerminalPoint kvtRightPoint;
        private int prevRightWidth = 0;
        private string rightText = String.Empty;

        public KeyValueText(string keyName, string valueText, int leftWidth = 0)
        {
            this.TopLeftPoint = TerminalPoint.GetCurrent();

            if (leftWidth < 0)
                keyName = keyName.PadLeft(leftWidth * -1);
            else if (leftWidth > 0)
                keyName = keyName.PadRight(leftWidth);
            
            Terminal.Write($"{keyName}: ");

            kvtRightPoint = TerminalPoint.GetCurrent();

            this.UpdateValue(valueText);
        }

        public override void Redraw()
        {
            TerminalPoint previousPoint = TerminalPoint.GetCurrent();
            kvtRightPoint.MoveTo();

            if (this.rightText == null)
                this.rightText = String.Empty;

            Console.Write(this.rightText);
            
            if (this.rightText.Length < prevRightWidth)
            {
                int spacesToClear = prevRightWidth - this.rightText.Length;

                for (int i = 0; i < spacesToClear; i++)
                    Console.Write(' ');
            }

            prevRightWidth = this.rightText.Length;

            this.TopRightPoint = TerminalPoint.GetCurrent();

            previousPoint.MoveTo();
        }

        public void UpdateValue(string newText)
        {
            this.rightText = newText;

            this.Redraw();
        }
    }
}