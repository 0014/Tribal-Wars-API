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
    public class ENUM
    {
        #region Public Enumarations

        public enum Buildings
        {
            HQ,
            RallyPoint,
            Statue,
            Wood,
            Clay,
            Iron,
            Farm,
            Storage,
            HiddingPlace,
            Barracks,
            Stable,
            Simith,
            Factory,
            Academy,
            MarketPlace,
            Wall
        }

        public enum Resources
        {
            Wood,
            Clay,
            Iron
        }

        public enum Army
        {
            Spearman,
            Swordsman,
            Axeman,
            Scout,
            LightCavalary,
            HeavyCavalary,
            Ram,
            Catapult,
            Knight,
            Nobleman
        }
        #endregion
        
        #region Internal Enumarations
        internal enum Screens
        {
            ErrorScreen,
            LoginScreen,
            MainScreen,
            Headquarters,
            RallyPoint,
            Barracks,
            AttackConfirm
        }

        internal enum FarmActions
        {
            Idle,
            Attack,
            AttackConfirm
        }

        internal enum LoginActions
        {
            Idle,
            LoginStatus,
            Login,
            GetSessionId,
            EnterCredentials,
            EnterGame
        }

        internal enum BuildingActions
        {
            Idle,
            UpgLevel,
            ControlUpg,
            SetBuildingLevels,
            GetResources
        }
        #endregion
    }
}
