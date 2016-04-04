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
using TribalWars.API;

namespace TribalWars
{
    public class AttackScheduler
    {
        public Village Location { get; set; }
        public ArmyBuilder Army { get; set; }
        public DateTime Date { get; set; }

        public AttackScheduler(Village loc, ArmyBuilder army, DateTime date)
        {
            Location = loc;
            Army = army;
            Date = date;
        }

        public override string ToString()
        {
            return String.Format("{0} | {2} | {1}", Date, Army, Location);
        }
    }
}
