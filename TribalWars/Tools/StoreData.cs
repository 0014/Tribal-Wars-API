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
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TribalWars.Tools
{
    public class StoreData
    {
        private string _path;
        private string _columns = "";

        public StoreData(string fileName)
        {
            _path = AppDomain.CurrentDomain.BaseDirectory + fileName + ".csv"; // this is where the info is stored
        }

        public StoreData(string fileName, string columns)
        {
            _path = AppDomain.CurrentDomain.BaseDirectory + fileName + ".csv"; // this is where the info is stored

            if (!File.Exists(_path)) // check if the file exists
                WriteLine(columns); // instantiate the file by defining the columns

            _columns = columns;
        }

        /// <summary>
        /// This function stores the data that is passed to it.
        /// Can be accessed from other Classes.
        /// </summary>
        /// <param name="data"></param>
        public void WriteLine(string data)
        {
            if (!File.Exists(_path)) // check if the file exists
                using (File.CreateText(_path)) { }; // create an empty file if it does not exist

            File.AppendAllText(_path, data + "\r\n"); // store the data as a single line
        }

        /// <summary>
        /// Stores all the lines into a string array.
        /// </summary>
        /// <returns> Returns all the other records which are related to the attachment as input </returns>
        public string[] ReadLines()
        {
            if (!File.Exists(_path)) // check if the file exists
                using (File.CreateText(_path)) { }; // create an empty file if it does not exist

            var records = new List<string>(); // this list will hold the records realated to the attachment

            // Open the file to read from. 
            using (var sr = File.OpenText(_path)) // open the file to read
            {
                string s;

                while ((s = sr.ReadLine()) != null)
                {
                    records.Add(s); // add all the lines as found records
                }
            }

            return records.ToArray(); // return the records as string array
        }

        /// <summary>
        /// This function updates the data that is passed to it.
        /// Can be accessed from other Classes.
        /// </summary>
        /// <param name="lineNumber"> This indicates the value that is going to be updated. </param>
        /// <param name="data"> Store the unique email ID </param>
        public void UpdateLine(int lineNumber, string data)
        {
            var lines = File.ReadAllLines(_path); // get all the lines that is stored in the data

            lines[lineNumber] = data;

            File.WriteAllLines(_path, lines); // after line is updated store it
        }

        /// <summary>
        /// Deletes the specified line from the list
        /// </summary>
        /// <param name="lineNumber"> The line that is wanted to be deleted </param>
        public void DeleteLine(int lineNumber)
        {
            var lines = File.ReadAllLines(_path); // get all the lines that is stored in the data

            lines = lines.Where((source, index) => index != lineNumber).ToArray();

            File.WriteAllLines(_path, lines); // after line is updated store it
        }

        /// <summary>
        /// Deletes the specified line which contains the line data
        /// </summary>
        /// <param name="data"> line data </param>
        public void DeleteLine(string data)
        {
            var lines = File.ReadAllLines(_path); // get all the lines that is stored in the data

            lines = lines.Where((source, index) => !source.Equals(data)).ToArray();

            File.WriteAllLines(_path, lines); // after line is updated store it
        }

        /// <summary>
        /// Clears all the data inside the file
        /// </summary>
        public void Clear()
        {
            File.WriteAllText(_path, String.Empty); // Clear the file

            if (!_columns.Equals("")) // protect the column information
                WriteLine(_columns);
        }
    }
}
