/***************************************************************************
 * Copyright 2016 Arif Gencosmanoglu
 * 
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.

 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.

 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 **************************************************************************/

using System;
using System.Windows.Forms;

namespace TribalWars.API
{
    static class Tools
    {
        public static string GetBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                var Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                var End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }

        public static string SetPageData(WebBrowser browser, string pageData)
        {
            pageData = browser.DocumentText;
            pageData = pageData.Replace(" ", "");
            pageData = pageData.Replace("\n", "");
            pageData = pageData.Replace("\r", "");

            return pageData;
        }

        public static int GetNumSubstringOccurrences(string source, string key)
        {
            var num = 0;
            var pos = 0;

            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(key)) return num;

            while ((pos = source.IndexOf(key, pos)) > -1)
            {
                num ++;
                pos += key.Length;
            }
            return num;
        }

        public static HtmlElement FindSpanContains(WebBrowser browser, string key)
        {
            // Get all the elements of the current url
            var elements = browser.Document.GetElementsByTagName("span");

            // within each elements find the element that we are looking for
            foreach (HtmlElement element in elements)
                if (element.InnerHtml != null && element.InnerHtml.Contains(key))
                    return element;

            return null; // if not found return null
        }

        public static HtmlElement FindElement(WebBrowser browser, string attribute ,string key)
        {
            foreach (HtmlElement element in browser.Document.Body.All)
            {
                if (element.GetAttribute(attribute) == key)
                {
                    return element;
                }
            }
            return null;
        }

        public static void SetValue(WebBrowser browser, string fieldName, string value)
        {
            browser.Document.GetElementById(fieldName).SetAttribute("value", value);
        }

        public static void Click(HtmlElement element)
        {
            element.InvokeMember("click");
        }
    }
}
