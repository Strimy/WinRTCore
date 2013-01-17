using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace WinRTCore.Helpers
{
    public static class ColorHelper
    {
        public static Color FromString(string strColor)
        {
            byte r, g, b, a;
            a = 255;
            if (strColor.Length == 6)
            {
                r = byte.Parse(strColor.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                g = byte.Parse(strColor.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                b = byte.Parse(strColor.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            }
            else if (strColor.Length == 7 && strColor[0] == '#')
            {
                r = byte.Parse(strColor.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
                g = byte.Parse(strColor.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
                b = byte.Parse(strColor.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);
            }
            else if (strColor.Length == 8)
            {
                a = byte.Parse(strColor.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                r = byte.Parse(strColor.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                g = byte.Parse(strColor.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                b = byte.Parse(strColor.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
            }
            else if (strColor.Length == 9 && strColor[0] == '#')
            {
                a = byte.Parse(strColor.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
                r = byte.Parse(strColor.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
                g = byte.Parse(strColor.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);
                b = byte.Parse(strColor.Substring(7, 2), System.Globalization.NumberStyles.HexNumber);
            }
            else
            {
                throw new ArgumentException("Cannot parse the string");
            }
            Color c = new Color();
            c.A = a;
            c.B = b;
            c.R = r;
            c.G = g;

            return c;
        }
    }
}
