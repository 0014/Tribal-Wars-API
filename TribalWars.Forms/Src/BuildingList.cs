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

namespace TribalWars
{
    class BuildingList
    {
        public string BuildingName { get; set; }

        public int UpgradedLevel { get; set; }

        public DateTime BuildDate { get; set; }

        public BuildingList(string name, int level)
        {
            BuildingName = name;
            UpgradedLevel = level;
        }

        public BuildingList(string name, int level, DateTime date)
        {
            BuildingName = name;
            UpgradedLevel = level;
            BuildDate = date;
        }
    }
}
