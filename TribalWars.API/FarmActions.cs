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

using System.Threading;
using System.Windows.Forms;

namespace TribalWars.API
{
    public class FarmActions
    {
        private const int X = 0;
        private const int Y = 1;

        private URL _url;
        private WebBrowser _wb;
        private ENUM.FarmActions _action;
        private ArmyBuilder _army;

        private bool _actionFlag;
        private int[] _coordinates;

        public FarmActions(string token)
        {
            _url = new URL(token);

            _coordinates = new int[2];
        }

        public bool Attack(int x, int y, ArmyBuilder army)
        {
            _action = ENUM.FarmActions.Attack;
            _army = army;

            _coordinates[X] = x;
            _coordinates[Y] = y;

            NavigateThroughTread(_url.GetUrl(ENUM.Screens.RallyPoint));

            return _actionFlag;
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

        private void PageLoaded(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            _actionFlag = false; // reset the action flag

            switch (_action)
            {
                case ENUM.FarmActions.Attack:
                    if (!_wb.Url.ToString().Equals(_url.GetUrl(ENUM.Screens.RallyPoint))) 
                        return; // keep searching the page until the buttons are all loaded 

                    Tools.SetValue(_wb, _army.armyFields[(int)ENUM.Army.Spearman], _army.Spearman.ToString());
                    Tools.SetValue(_wb, _army.armyFields[(int)ENUM.Army.Swordsman], _army.Swordsman.ToString());
                    Tools.SetValue(_wb, _army.armyFields[(int)ENUM.Army.Axeman], _army.Axeman.ToString());
                    Tools.SetValue(_wb, _army.armyFields[(int)ENUM.Army.Scout], _army.Scout.ToString());
                    Tools.SetValue(_wb, _army.armyFields[(int)ENUM.Army.LightCavalary], _army.LightCavalry.ToString());
                    Tools.SetValue(_wb, _army.armyFields[(int)ENUM.Army.HeavyCavalary], _army.HeavyCavalary.ToString());
                    Tools.SetValue(_wb, _army.armyFields[(int)ENUM.Army.Ram], _army.Ram.ToString());
                    Tools.SetValue(_wb, _army.armyFields[(int)ENUM.Army.Catapult], _army.Catapult.ToString());
                    Tools.SetValue(_wb, _army.armyFields[(int)ENUM.Army.Knight], _army.Knight.ToString());
                    Tools.SetValue(_wb, _army.armyFields[(int)ENUM.Army.Nobleman], _army.Nobleman.ToString());

                    var element = Tools.FindElement(_wb, "name", "input");
                    element.SetAttribute("value", _coordinates[X] + "|" + _coordinates[Y]);
                    _action = ENUM.FarmActions.AttackConfirm;
                    var attack = _wb.Document.GetElementById("target_attack");
                    Tools.Click(attack);

                    return;
                case ENUM.FarmActions.AttackConfirm:
                    if (!_wb.Url.ToString().Equals(_url.GetUrl(ENUM.Screens.AttackConfirm))) 
                        return; // keep searching the page until the buttons are all loaded 

                    var confirm = _wb.Document.GetElementById("troop_confirm_go");

                    if (confirm == null)
                        return;
                    
                    Tools.Click(confirm);

                    break;

                case ENUM.FarmActions.Idle:
                    // Do nothing
                    return;
            }

            _action = ENUM.FarmActions.Idle;
            Application.ExitThread();   // Stops the thread
        }

        public static System.Web.UI.Control FindControlR(System.Web.UI.Control root, string id)
        {
            if (root == null) return null;

            var controlFound = root.FindControl(id);

            if (controlFound != null)
            {
                return controlFound;
            }

            foreach (System.Web.UI.Control c in root.Controls)
            {
                controlFound = c.FindControl(id);

                if (controlFound != null)
                {
                    return controlFound;
                }
            }

            return null;
        }
    }
}
