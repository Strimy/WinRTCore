using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinRTCore.Helpers
{
    public static class StringHelpers
    {
        static private Dictionary<string, string> m_htmlCharToString = new Dictionary<string, string>();

        static StringHelpers()
        {
            m_htmlCharToString.Add("&#130;", "'");
            m_htmlCharToString.Add("&#160;", " ");
            m_htmlCharToString.Add("&#8217;", "'");
            m_htmlCharToString.Add("&#232;", "è");
            m_htmlCharToString.Add("&#233;", "é");
            m_htmlCharToString.Add("&#224;", "à");
            m_htmlCharToString.Add("&#244;", "ô");
            m_htmlCharToString.Add("&#234;", "ê");
            m_htmlCharToString.Add("&#231;", "ç");
            m_htmlCharToString.Add("&#201;", "É");
            m_htmlCharToString.Add("&#200;", "È");
            m_htmlCharToString.Add("&#8364;", "€");
            m_htmlCharToString.Add("&#171;", "«");
            m_htmlCharToString.Add("&#187;", "»");
            m_htmlCharToString.Add("&#226;", "â");
            m_htmlCharToString.Add("&eacute;", "é");
            m_htmlCharToString.Add("&egrave;", "è");
            m_htmlCharToString.Add("&rsquo;", "'");
            m_htmlCharToString.Add("&ccedil;", "ç");
            m_htmlCharToString.Add("&ecirc;", "ê");
            m_htmlCharToString.Add("&agrave;", "à");
            m_htmlCharToString.Add("&euml;", "ë");
            m_htmlCharToString.Add("&ugrave;", "û");
            m_htmlCharToString.Add("&ocirc;", "ô");
            m_htmlCharToString.Add("&nbsp;", " ");
            m_htmlCharToString.Add("&ucirc;", "û");
            m_htmlCharToString.Add("&icirc;", "î");
            m_htmlCharToString.Add("&laquo;", "«");
            m_htmlCharToString.Add("&raquo;", "»");
            m_htmlCharToString.Add("&amp;", "&");
            m_htmlCharToString.Add("&oelig;", "œ");
        }

        public static string ParseHtmlSpecialCharacters(this String str)
        {
            foreach (var item in m_htmlCharToString)
            {
                str = str.Replace(item.Key, item.Value);
            }
            return str;
        }
    }
}
