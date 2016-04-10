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

namespace TribalWars.API
{
    public class ArmyBuilder
    {
        private int _spearman;
        private int _swordsman;
        private int _axeman;

        private int _scout;
        private int _lightCavalry;
        private int _heavyCavalry;

        private int _ram;
        private int _catapult;

        private int _knight;
        private int _nobleman;

        public string Name { get; set; }
        public int[] Army { get; private set; }
        
        internal string[] ArmyFields;
        internal int[] ArmySpeed;

        public ArmyBuilder()
        {
            // initialize the army size as empty
            Army = new int[10];
            for (var i = 0; i < Army.Length; i++)
            {
                Army[i] = 0;
            }

            Name = "Army:"; // default army name
            SetArmyProperties();
        }

        public ArmyBuilder(string name)
        {
            // initialize the army size as empty
            Army = new int[10];
            for (var i = 0; i < Army.Length; i++)
            {
                Army[i] = 0;
            }

            Name = name;
            SetArmyProperties();
        }

        public ArmyBuilder(int[] army)
        {
            Army = army;
            Name = "Army:"; // default army name
        }

        public ArmyBuilder(string name, int[] army)
        {
            Army = army;
            Name = name;
        }

        public int Spearman
        {
            get { return _spearman; }
            set
            {
                _spearman = value;
                Army[(int)ENUM.Army.Spearman] = value;
            }
        }

        public int Swordsman
        {
            get { return _swordsman; }
            set
            {
                _swordsman = value;
                Army[(int)ENUM.Army.Swordsman] = value;
            }
        }

        public int Axeman
        {
            get { return _axeman; }
            set
            {
                _axeman = value;
                Army[(int)ENUM.Army.Axeman] = value;
            }
        }

        public int Scout
        {
            get { return _scout; }
            set
            {
                _scout = value;
                Army[(int)ENUM.Army.Scout] = value;
            }
        }

        public int LightCavalry
        {
            get { return _lightCavalry; }
            set
            {
                _lightCavalry = value;
                Army[(int)ENUM.Army.LightCavalary] = value;
            }
        }

        public int HeavyCavalary
        {
            get { return _heavyCavalry; }
            set
            {
                _heavyCavalry = value;
                Army[(int)ENUM.Army.HeavyCavalary] = value;
            }
        }

        public int Ram
        {
            get { return _ram; }
            set
            {
                _ram = value;
                Army[(int)ENUM.Army.Ram] = value;
            }
        }

        public int Catapult
        {
            get { return _catapult; }
            set
            {
                _catapult = value;
                Army[(int)ENUM.Army.Catapult] = value;
            }
        }

        public int Knight
        {
            get { return _knight; }
            set
            {
                _knight = value;
                Army[(int)ENUM.Army.Knight] = value;
            }
        }

        public int Nobleman
        {
            get { return _nobleman; }
            set
            {
                _nobleman = value;
                Army[(int)ENUM.Army.Nobleman] = value;
            }
        }

        private void SetArmyProperties()
        {
            SetArmyReference();
            SetArmySpeed();
        }

        /// <summary>
        /// Sets each soldiers reference that is in page source
        /// </summary>
        private void SetArmyReference()
        {
            ArmyFields = new string[10];

            ArmyFields[(int) ENUM.Army.Spearman] = "unit_input_spear";
            ArmyFields[(int) ENUM.Army.Swordsman] = "unit_input_sword";
            ArmyFields[(int) ENUM.Army.Axeman] = "unit_input_axe";
            ArmyFields[(int) ENUM.Army.Scout] = "unit_input_spy";
            ArmyFields[(int) ENUM.Army.LightCavalary] = "unit_input_light";
            ArmyFields[(int) ENUM.Army.HeavyCavalary] = "unit_input_heavy";
            ArmyFields[(int) ENUM.Army.Ram] = "unit_input_ram";
            ArmyFields[(int) ENUM.Army.Catapult] = "unit_input_catapult";
            ArmyFields[(int) ENUM.Army.Knight] = "unit_input_knight";
            ArmyFields[(int) ENUM.Army.Nobleman] = "unit_input_snob";
        }

        private void SetArmySpeed()
        {
            ArmySpeed = new int[10];

            ArmySpeed[(int)ENUM.Army.Spearman] = 18;
            ArmySpeed[(int)ENUM.Army.Swordsman] = 22;
            ArmySpeed[(int)ENUM.Army.Axeman] = 18;
            ArmySpeed[(int)ENUM.Army.Scout] = 9;
            ArmySpeed[(int)ENUM.Army.LightCavalary] = 10;
            ArmySpeed[(int)ENUM.Army.HeavyCavalary] = 11;
            ArmySpeed[(int)ENUM.Army.Ram] = 30;
            ArmySpeed[(int)ENUM.Army.Catapult] = 30;
            ArmySpeed[(int)ENUM.Army.Knight] = 10;
            ArmySpeed[(int)ENUM.Army.Nobleman] = 35;

        }

        /// <summary>
        /// Overrides to string to print a detailed army information
        /// </summary>
        /// <returns> The string of army detail </returns>
        public override string ToString()
        {
            var armyString = "Army: ";

            if (_spearman > 0)
                armyString += " Sp = " + _spearman;
            if (_swordsman > 0)
                armyString += " Sw = " + _swordsman;
            if (_axeman > 0)
                armyString += " Axe = " + _axeman;
            if (_lightCavalry > 0)
                armyString += " LC = " + _lightCavalry;
            if (_heavyCavalry > 0)
                armyString += " HC= " + _heavyCavalry;
            if (_scout > 0)
                armyString += " Sc = " + _scout;
            if (_ram > 0)
                armyString += " Ram = " + _ram;
            if (_catapult > 0)
                armyString += " Cat = " + _catapult;
            if (_knight > 0)
                armyString += " Knig = " + _knight;
            if (_nobleman > 0)
                armyString += " Nobl = " + _nobleman;

            return armyString;
        }

        /// <summary>
        /// This is another implementation of printing army info
        /// </summary>
        /// <param name="name"> According to the paramter the printing method will change </param>
        /// <returns> The string of army detail </returns>
        public string ToString(string name)
        {
            switch (name.ToUpperInvariant())
            {
                case "Name":
                    return string.Format("{0}", Name);
                
                default:
                    var msg = string.Format("'{0}' is an invalid format string", name);
                    
                    throw new ArgumentException(msg);
            }
        }

        /// <summary>
        /// This function is used to get the size of a created army
        /// </summary>
        /// <returns> The amount of units the army contains </returns>
        public int GetSize()
        {
            return _spearman + _swordsman + _axeman
                   + _scout + _lightCavalry + _heavyCavalry
                   + _ram + _catapult + _knight + _nobleman;
        }

        /// <summary>
        /// This function will find out the slowest unit within the Army
        /// </summary>
        /// <returns> The speed of the slowest unit </returns>
        public int GetSlowestUnitSpeed()
        {
            var slowest = 0;

            for (var i = 0; i < ArmySpeed.Length; i ++)
            {
                if (Army[i] == 0) continue;

                if (slowest < ArmySpeed[i]) slowest = ArmySpeed[i];
            }

            return slowest;
        }
    }
}
