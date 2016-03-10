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

        public int[] Army { get; private set; }

        internal string[] armyFields;

        public ArmyBuilder()
        {
            // initialize the army size as empty
            Army = new int[10];
            for (int i = 0; i < Army.Length; i++)
            {
                Army[i] = 0;
            }

            SetArmyReference();
        }

        public ArmyBuilder(int[] army)
        {
            Army = army;
        }

        private void SetArmyReference()
        {
            armyFields = new string[10];

            armyFields[(int) ENUM.Army.Spearman] = "unit_input_spear";
            armyFields[(int) ENUM.Army.Swordsman] = "unit_input_sword";
            armyFields[(int) ENUM.Army.Axeman] = "unit_input_axe";
            armyFields[(int) ENUM.Army.Scout] = "unit_input_spy";
            armyFields[(int) ENUM.Army.LightCavalary] = "unit_input_light";
            armyFields[(int) ENUM.Army.HeavyCavalary] = "unit_input_heavy";
            armyFields[(int) ENUM.Army.Ram] = "unit_input_ram";
            armyFields[(int) ENUM.Army.Catapult] = "unit_input_catapult";
            armyFields[(int) ENUM.Army.Knight] = "unit_input_knight";
            armyFields[(int) ENUM.Army.Nobleman] = "unit_input_snob";
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
    }
}
