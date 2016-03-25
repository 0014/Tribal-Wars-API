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
    internal static class Parser
    {
        /// <summary>
        /// This function is used to Parse a text inside a Text source.
        /// </summary>
        /// <param name="strSource"> The source text </param>
        /// <param name="strStart"> Initial string inside the source </param>
        /// <param name="strEnd"> Ending string inside the source </param>
        /// <returns> The string between the initial and ending strings, returns null if initial or ending strings
        /// are not within the source text </returns>
        public static string GetBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                var start = strSource.IndexOf(strStart, 0) + strStart.Length; // get the indext of the initial string
                var end = strSource.IndexOf(strEnd, start); // get the indext of the ending string
                return strSource.Substring(start, end - start); // return string in-between
            }

            return null; // returns null if initial or ending string is not in the source
        }

        /// <summary>
        /// Finds the span element whichs inner text contains the input value
        /// </summary>
        /// <param name="browser"> The web browser element which is navigated to a url </param>
        /// <param name="key"> The inner text string of the span </param>
        /// <returns> The span element </returns>
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

        /// <summary>
        /// This function counts the number of strings that is inside a text
        /// </summary>
        /// <param name="source"> The source text </param>
        /// <param name="key"> the string that is inside text </param>
        /// <returns> the amount of the text being repeted inside the source </returns>
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


        /// <summary>
        /// Finds the HtmlElement inside the webbrowser object
        /// </summary>
        /// <param name="browser"> The web browser element which is navigated to a url </param>
        /// <param name="attribute"> The type of element </param>
        /// <param name="key"> The name of the type </param>
        /// <returns> html element with the input attributes </returns>
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


        /// <summary>
        /// Sets the value of an html element within a navigated page
        /// </summary>
        /// <param name="browser"> The web browser element which is navigated to a url </param>
        /// <param name="fieldName"> The id of the element </param>
        /// <param name="value"> The value that is wanted to be inserted </param>
        public static void SetValue(WebBrowser browser, string fieldName, string value)
        {
            browser.Document.GetElementById(fieldName).SetAttribute("value", value);
        }

        /// <summary>
        /// This function demonstrates how to use HTML Agility Pack with linq
        /// </summary>
        /// <param name="wb"> The web browser element which is navigated to a url </param>
        /// <returns> The html node </returns>
        private static string UsageAP(WebBrowser wb)
        {
            // Create the root node by loading the page source
            var html = new HtmlAgilityPack.HtmlDocument();
            html.LoadHtml(wb.DocumentText);
            var root = html.DocumentNode;

            //Start traversing within descendant nodes
            var p = root
                .Descendants()
                .Single(n => n.GetAttributeValue("class", "").Equals("module-profile-recognition"))
                .Descendants("p")
                .Single();
            var content = p.InnerText;
            return content;
        }

        /// <summary>
        /// Finds the node inside a navigated page source
        /// </summary>
        /// <param name="wb"> The web browser element which is navigated to a url </param>
        /// <param name="definition"> The type of the node </param>
        /// <param name="key"> The name of the type </param>
        /// <returns> html node with specified inputs </returns>
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

        /// <summary>
        /// Finds the building node
        /// </summary>
        /// <param name="wb"> The web browser element which is navigated to a url </param>
        /// <param name="key"> The reference of the building </param>
        /// <returns> The building node according to the specified inputs </returns>
        public static HtmlNode FindBuildingNode(WebBrowser wb, string key)
        {
            HtmlNode p = null;

            // Create the root node by loading the page source
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

        /// <summary>
        /// Checks the building page if there is any buildign being built
        /// </summary>
        /// <param name="wb"> The web browser element which is navigated to a url </param>
        /// <returns> The number of buildigns being built </returns>
        public static int QueueNumber(WebBrowser wb)
        {
            int no;

            // Create the root node by loading the page source
            var html = new HtmlAgilityPack.HtmlDocument();
            html.LoadHtml(wb.DocumentText);
            var root = html.DocumentNode;

            try
            {
                // get the number of buildigins being built
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
