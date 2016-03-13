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
using System.Linq;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace TribalWars.API
{
    static class Parser
    {
        public static string GetBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                var start = strSource.IndexOf(strStart, 0) + strStart.Length;
                var end = strSource.IndexOf(strEnd, start);
                return strSource.Substring(start, end - start);
            }

            return null;
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

        private static string UsageAP(WebBrowser wb)
        {
            var html = new HtmlAgilityPack.HtmlDocument();
            html.LoadHtml(wb.DocumentText);
            var root = html.DocumentNode;
            var p = root
                .Descendants()
                .Single(n => n.GetAttributeValue("class", "").Equals("module-profile-recognition"))
                .Descendants("p")
                .Single();
            var content = p.InnerText;
            return content;
        }

        public static HtmlNode FindNode(WebBrowser wb, string definition, string key)
        {
            HtmlNode p = null;

            var html = new HtmlAgilityPack.HtmlDocument();
            html.LoadHtml(wb.DocumentText);
            var root = html.DocumentNode;

            try
            {
                p = root
                    .Descendants()
                    .Single(n => n.GetAttributeValue(definition, "").Equals(key));
            }
            catch
            {
                // no node is not founded
            }

            return p;
        }

        public static HtmlNode FindBuildingNode(WebBrowser wb, string key)
        {
            HtmlNode p = null;

            var html = new HtmlAgilityPack.HtmlDocument();
            html.LoadHtml(wb.DocumentText);
            var root = html.DocumentNode;

            try
            {
                p = root.SelectNodes(String.Format(".//a[@data-building = '{0}']", key))[1];
            }
            catch
            {
                // no node is not founded
            }

            return p;
        }

        public static int QueueNumber(WebBrowser wb)
        {
            
            int no;

            var html = new HtmlAgilityPack.HtmlDocument();
            html.LoadHtml(wb.DocumentText);
            var root = html.DocumentNode;

            try
            {
                no = root
                    .Descendants()
                    .Count(n => n.GetAttributeValue("class", "").Equals("btn btn-cancel"));
            }
            catch
            {
                // no node is not founded
                no = 0;
            }

            return no;
        }
    }
}
