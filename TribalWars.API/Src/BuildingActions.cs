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
        private string _sessionId, _pageData, _upgReference;
        private int[] _resources;
        
        public BuildingActions(string token)
        {
            _url = new URL(token);
            _sessionId = token;

            GetBuildings();
            _resources = new int[3];
        }

        public int Queue
        {
            get { return _queue; }
        }

        public string GetToken()
        {
            return _sessionId;
        }

        public Buildings[] GetBuildings()
        {
            _action = ENUM.BuildingActions.SetBuildingLevels;

            NavigateThroughTread(_url.GetUrl(ENUM.Screens.Headquarters));

            return _myBuildings;
        }

        public bool UpgradeBuilding(ENUM.Buildings building)
        {
            _upgReference = _myBuildings[(int)building].Reference + (_myBuildings[(int)building].Level + 1);
            _action = ENUM.BuildingActions.UpgLevel;
            NavigateThroughTread(_url.GetUrl(ENUM.Screens.Headquarters));
            return _actionFlag;
        }

        public int[] GetResources()
        {
            _action = ENUM.BuildingActions.GetResources;
            NavigateThroughTread(_url.GetUrl(ENUM.Screens.Headquarters));

            return _resources;
        }

        public int GetResources(ENUM.Resources resource)
        {
            _action = ENUM.BuildingActions.GetResources;
            NavigateThroughTread(_url.GetUrl(ENUM.Screens.Headquarters));

            return _resources[(int)resource];
        }

        private void PageLoaded(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            _actionFlag = false; // reset the action flag

            switch (_action)
            {
                case ENUM.BuildingActions.SetBuildingLevels:
                    if (!_wb.Url.ToString().Equals(_url.GetUrl(ENUM.Screens.Headquarters))) 
                        return; // keep searching the page until the buttons are all loaded 

                    _queue = GetQueueNumber();
                    
                    GetLevels();
                    _actionFlag = true;
                    break;
                case ENUM.BuildingActions.UpgLevel:
                    if (!_wb.Url.ToString().Equals(_url.GetUrl(ENUM.Screens.Headquarters))) 
                        return; // keep searching the page until the buttons are all loaded 

                    _queue = GetQueueNumber();

                    if (_queue == 2)
                    {
                        _actionFlag = false;
                        return;
                    }

                    var button = _wb.Document.GetElementById(_upgReference);
                    Tools.Click(button);
                    _action = ENUM.BuildingActions.ControlUpg;
                    
                    return;

                case ENUM.BuildingActions.ControlUpg:

                    if (!_wb.Url.ToString().Equals(_url.GetUrl(ENUM.Screens.Headquarters)))
                        return; // keep searching the page until the buttons are all loaded 

                    if (GetQueueNumber() != _queue + 1)
                    {
                         _wb.Refresh();
                        return;
                    }

                    _queue++;
                    _actionFlag = true;

                    break;

                case ENUM.BuildingActions.GetResources:
                    if (!_wb.Url.ToString().Equals(_url.GetUrl(ENUM.Screens.Headquarters))) 
                        return; // keep searching the page until the buttons are all loaded 
                    
                    _pageData = Tools.SetPageData(_wb, _wb.DocumentText);

                    _resources[(int)ENUM.Resources.Wood] = int.Parse(Tools.GetBetween(_pageData, "\"wood_float\":", "."));
                    _resources[(int)ENUM.Resources.Clay] = int.Parse(Tools.GetBetween(_pageData, "\"stone_float\":", "."));
                    _resources[(int)ENUM.Resources.Iron] = int.Parse(Tools.GetBetween(_pageData, "\"iron_float\":", "."));

                    _actionFlag = true;
                    break;

                case ENUM.BuildingActions.Idle:
                    // Do nothing
                    return;
            }
            _action = ENUM.BuildingActions.Idle;
            Application.ExitThread();   // Stops the thread
        }

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

        private void GetLevels()
        {
            _myBuildings = new Buildings[16];

            _myBuildings[(int)ENUM.Buildings.HQ] = new Buildings(ENUM.Buildings.HQ.ToString(), "main_buildlink_main_", Get("Anabina"));
            _myBuildings[(int)ENUM.Buildings.RallyPoint] = new Buildings(ENUM.Buildings.RallyPoint.ToString(), "",Get("İçtimaMeydanı"));
            _myBuildings[(int)ENUM.Buildings.Statue] = new Buildings(ENUM.Buildings.Statue.ToString(), "main_buildlink_statue_",Get("Heykel"));
            _myBuildings[(int)ENUM.Buildings.Wood] = new Buildings(ENUM.Buildings.Wood.ToString(), "main_buildlink_wood_", Get("Oduncu"));
            _myBuildings[(int)ENUM.Buildings.Clay] = new Buildings(ENUM.Buildings.Clay.ToString(), "main_buildlink_stone_", Get("Kilocağı"));
            _myBuildings[(int)ENUM.Buildings.Iron] = new Buildings(ENUM.Buildings.Iron.ToString(), "main_buildlink_iron_", Get("Demirmadeni"));
            _myBuildings[(int)ENUM.Buildings.Farm] = new Buildings(ENUM.Buildings.Farm.ToString(), "main_buildlink_farm_", Get("Çiftlik"));
            _myBuildings[(int)ENUM.Buildings.Storage] = new Buildings(ENUM.Buildings.Storage.ToString(), "main_buildlink_storage_", Get("Depo"));
            _myBuildings[(int)ENUM.Buildings.HiddingPlace] = new Buildings(ENUM.Buildings.HiddingPlace.ToString(), "main_buildlink_hide_",Get("Gizlidepo"));

            _myBuildings[(int)ENUM.Buildings.Academy] = new Buildings(ENUM.Buildings.Academy.ToString(), "", 0);
            _myBuildings[(int)ENUM.Buildings.Barracks] = new Buildings(ENUM.Buildings.Barracks.ToString(), "", 0);
            _myBuildings[(int)ENUM.Buildings.MarketPlace] = new Buildings(ENUM.Buildings.MarketPlace.ToString(), "", 0);
            _myBuildings[(int)ENUM.Buildings.Stable] = new Buildings(ENUM.Buildings.Stable.ToString(), "", 0);
            _myBuildings[(int)ENUM.Buildings.Simith] = new Buildings(ENUM.Buildings.Simith.ToString(), "", 0);
            _myBuildings[(int)ENUM.Buildings.Factory] = new Buildings(ENUM.Buildings.Factory.ToString(), "", 0);
            _myBuildings[(int)ENUM.Buildings.Wall] = new Buildings(ENUM.Buildings.Wall.ToString(), "", 0);
        }

        private int Get(string key)
        {
            int level;

            try
            {
                level = int.Parse(Tools.GetBetween(_pageData, string.Format("{0}</A><BR><SPANstyle=\"font-size:0.9em;\">Seviye", key), "</SPAN>"));
            }
            catch (Exception)
            {
                level = 0;
            }

            return level;
        }

        private int GetQueueNumber()
        {
            _pageData = Tools.SetPageData(_wb, _wb.DocumentText);
            return Tools.GetNumSubstringOccurrences(_pageData, "ptalet");
        }
    }
}
