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
using System.Threading;
using System.Windows.Forms;

namespace TribalWars.API
{
    public class BuildingActions
    {
        private URL _url;
        private ENUM.BuildingActions _action;
        private WebBrowser _wb;
        private Buildings[] _myBuildings; 

        private bool _actionFlag;
        private int _queue;
        private string _sessionId, _upgReference;
        private int[] _resources;
        
        public BuildingActions(string token)
        {
            // set the urls using the token
            _url = new URL(token); 
            _sessionId = token;

            // get thebuilding levels
            GetBuildings();
            
            // initialize resources array
            _resources = new int[3]; //wood, clay, iron
        }

        #region Public Functions

        /// <summary>
        /// This parameter returns the token that is used to initialize this object
        /// </summary>
        public string Token
        {
            get { return _sessionId; }
        }

        /// <summary>
        /// This function is used to get overall information about the buildings 
        /// </summary>
        /// <returns> A list of buildings </returns>
        public Buildings[] GetBuildings()
        {
            // instruct to get building information
            _action = ENUM.BuildingActions.SetBuildingLevels;

            NavigateThroughTread(_url.GetUrl(ENUM.Screens.Headquarters));

            return _myBuildings;
        }

        /// <summary>
        /// This function is used to upgrade a specific building
        /// </summary>
        /// <param name="building"> The building that is to be built </param>
        /// <returns> True if successfull, else false </returns>
        public bool UpgradeBuilding(ENUM.Buildings building)
        {
            // get the key word that is on DOM (depended on the level of the building)
            _upgReference = _myBuildings[(int)building].Reference + (_myBuildings[(int)building].Level + 1);
            
            // instruct to upgrade the building
            _action = ENUM.BuildingActions.UpgLevel;

            NavigateThroughTread(_url.GetUrl(ENUM.Screens.Headquarters));
            
            return _actionFlag;
        }

        /// <summary>
        /// Stores the current resources into resource array
        /// </summary>
        /// <returns> The resources array </returns>
        public int[] GetResources()
        {
            // Instruct to get the current resources
            _action = ENUM.BuildingActions.GetResources;

            NavigateThroughTread(_url.GetUrl(ENUM.Screens.Headquarters));

            return _resources;
        }

        /// <summary>
        /// Overloads the function to get a specific kind of resource info
        /// </summary>
        /// <param name="resource"> The resource that is wanted fetch </param>
        /// <returns> The value of specified resource </returns>
        public int GetResources(ENUM.Resources resource)
        {
            // Instruct to get the current resources
            _action = ENUM.BuildingActions.GetResources;

            NavigateThroughTread(_url.GetUrl(ENUM.Screens.Headquarters));

            return _resources[(int)resource];
        }

        #endregion

        #region Private Functions

        private void GetLevels()
        {
            _myBuildings = new Buildings[16];

            _myBuildings[(int)ENUM.Buildings.HQ] = new Buildings(ENUM.Buildings.HQ.ToString(), "main_buildlink_main_", Get("main"));
            _myBuildings[(int)ENUM.Buildings.RallyPoint] = new Buildings(ENUM.Buildings.RallyPoint.ToString(), "", 1);
            _myBuildings[(int)ENUM.Buildings.Statue] = new Buildings(ENUM.Buildings.Statue.ToString(), "main_buildlink_statue_", Get("statue"));
            _myBuildings[(int)ENUM.Buildings.Wood] = new Buildings(ENUM.Buildings.Wood.ToString(), "main_buildlink_wood_", Get("wood"));
            _myBuildings[(int)ENUM.Buildings.Clay] = new Buildings(ENUM.Buildings.Clay.ToString(), "main_buildlink_stone_", Get("stone"));
            _myBuildings[(int)ENUM.Buildings.Iron] = new Buildings(ENUM.Buildings.Iron.ToString(), "main_buildlink_iron_", Get("iron"));
            _myBuildings[(int)ENUM.Buildings.Farm] = new Buildings(ENUM.Buildings.Farm.ToString(), "main_buildlink_farm_", Get("farm"));
            _myBuildings[(int)ENUM.Buildings.Storage] = new Buildings(ENUM.Buildings.Storage.ToString(), "main_buildlink_storage_", Get("storage"));
            _myBuildings[(int)ENUM.Buildings.HiddingPlace] = new Buildings(ENUM.Buildings.HiddingPlace.ToString(), "main_buildlink_hide_", Get("hide"));

            _myBuildings[(int)ENUM.Buildings.Barracks] = new Buildings(ENUM.Buildings.Barracks.ToString(), "main_buildlink_barracks_", Get("barracks"));
            _myBuildings[(int)ENUM.Buildings.Wall] = new Buildings(ENUM.Buildings.Wall.ToString(), "main_buildlink_wall_", Get("wall"));
            _myBuildings[(int)ENUM.Buildings.Academy] = new Buildings(ENUM.Buildings.Academy.ToString(), "", 0);
            _myBuildings[(int)ENUM.Buildings.MarketPlace] = new Buildings(ENUM.Buildings.MarketPlace.ToString(), "", 0);
            _myBuildings[(int)ENUM.Buildings.Stable] = new Buildings(ENUM.Buildings.Stable.ToString(), "", 0);
            _myBuildings[(int)ENUM.Buildings.Simith] = new Buildings(ENUM.Buildings.Simith.ToString(), "", 0);
            _myBuildings[(int)ENUM.Buildings.Factory] = new Buildings(ENUM.Buildings.Factory.ToString(), "", 0);
            
        }

        private int Get(string key)
        {
            int level;

            try
            {
                var buildingNode = Parser.FindBuildingNode(_wb, key);

                level = int.Parse(buildingNode.InnerText.Replace("Seviye ", "")) - 1;
            }

            catch (Exception)
            {
                level = 0;
            }

            return level;
        }

        #endregion

        #region Helper Functions

        /// <summary>
        /// This event fires when the navigation inside theread is complete. The main actions are performed
        /// in this function.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageLoaded(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            _actionFlag = false; // reset the action flag

            switch (_action)
            {
                case ENUM.BuildingActions.SetBuildingLevels:
                    if (!_wb.Url.ToString().Equals(_url.GetUrl(ENUM.Screens.Headquarters))) 
                        return; // keep searching the page until the buttons are all loaded 

                    _queue = Parser.QueueNumber(_wb);
                    
                    GetLevels();
                    _actionFlag = true;
                    break;
                case ENUM.BuildingActions.UpgLevel:
                    if (!_wb.Url.ToString().Equals(_url.GetUrl(ENUM.Screens.Headquarters))) 
                        return; // keep searching the page until the buttons are all loaded 

                    _queue = Parser.QueueNumber(_wb);

                    if (_wb.Document != null && _queue != 2)
                    {
                        var button = _wb.Document.GetElementById(_upgReference);
                        if (button != null) button.InvokeMember("click");
                    }
                    else
                    {
                        _actionFlag = false;
                        return;
                    }

                    _action = ENUM.BuildingActions.ControlUpg;
                    
                    return;

                case ENUM.BuildingActions.ControlUpg:

                    if (!_wb.Url.ToString().Equals(_url.GetUrl(ENUM.Screens.Headquarters)))
                        return; // keep searching the page until the buttons are all loaded 

                    if (Parser.QueueNumber(_wb) != _queue + 1)
                    {
                         _wb.Refresh();
                        return;
                    }

                    _queue ++;
                    _actionFlag = true;

                    break;

                case ENUM.BuildingActions.GetResources:
                    if (!_wb.Url.ToString().Equals(_url.GetUrl(ENUM.Screens.Headquarters))) 
                        return; // keep searching the page until the buttons are all loaded 

                    _resources[(int)ENUM.Resources.Wood] = int.Parse(Parser.FindNode(_wb, "id", "wood").InnerText);
                    _resources[(int)ENUM.Resources.Clay] = int.Parse(Parser.FindNode(_wb, "id", "stone").InnerText);
                    _resources[(int)ENUM.Resources.Iron] = int.Parse(Parser.FindNode(_wb, "id", "iron").InnerText);

                    _actionFlag = true;
                    break;

                case ENUM.BuildingActions.Idle:
                    // Do nothing
                    return;
            }
            _action = ENUM.BuildingActions.Idle;
            Application.ExitThread();   // Stops the thread
        }

        /// <summary>
        /// This function help the web browser to perform actions in a synchronous way.
        /// </summary>
        /// <param name="url"> Navigates the browser object to the input url </param>
        private void NavigateThroughTread(string url)
        {
            var th = new Thread(() =>
            {
                _wb = new WebBrowser();
                _wb.DocumentCompleted += PageLoaded;
                _wb.Navigate(url);
                Application.Run();
            });
            th.SetApartmentState(ApartmentState.STA);
            
            th.Start();
            while (th.IsAlive) { }
        }

        #endregion
        
    }
}
