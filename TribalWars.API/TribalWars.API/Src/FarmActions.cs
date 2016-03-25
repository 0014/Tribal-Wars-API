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
            // set the urls using the token
            _url = new URL(token);

            // instantiate the coordinates
            _coordinates = new int[2];
        }

        #region Public Functions

        /// <summary>
        /// This function performs an attack on village with the specified coordinates,
        /// with the specified army
        /// </summary>
        /// <param name="x"> X coordinate of the village to attack </param>
        /// <param name="y"> Y coordinate of the village to attack </param>
        /// <param name="army"> the army in that is about to attack </param>
        /// <returns> True if successfull, else false </returns>
        public bool Attack(int x, int y, ArmyBuilder army)
        {
            // instruct to attack the village
            _action = ENUM.FarmActions.Attack;

            //get the army and coordinates
            _army = army;
            _coordinates[X] = x;
            _coordinates[Y] = y;

            NavigateThroughTread(_url.GetUrl(ENUM.Screens.RallyPoint));

            return _actionFlag;
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
                case ENUM.FarmActions.Attack:
                    if (!_wb.Url.ToString().Equals(_url.GetUrl(ENUM.Screens.RallyPoint))) 
                        return; // keep searching the page until the buttons are all loaded 

                    Parser.SetValue(_wb, _army.ArmyFields[(int)ENUM.Army.Spearman], _army.Spearman.ToString());
                    Parser.SetValue(_wb, _army.ArmyFields[(int)ENUM.Army.Swordsman], _army.Swordsman.ToString());
                    Parser.SetValue(_wb, _army.ArmyFields[(int)ENUM.Army.Axeman], _army.Axeman.ToString());
                    Parser.SetValue(_wb, _army.ArmyFields[(int)ENUM.Army.Scout], _army.Scout.ToString());
                    Parser.SetValue(_wb, _army.ArmyFields[(int)ENUM.Army.LightCavalary], _army.LightCavalry.ToString());
                    Parser.SetValue(_wb, _army.ArmyFields[(int)ENUM.Army.HeavyCavalary], _army.HeavyCavalary.ToString());
                    Parser.SetValue(_wb, _army.ArmyFields[(int)ENUM.Army.Ram], _army.Ram.ToString());
                    Parser.SetValue(_wb, _army.ArmyFields[(int)ENUM.Army.Catapult], _army.Catapult.ToString());
                    Parser.SetValue(_wb, _army.ArmyFields[(int)ENUM.Army.Knight], _army.Knight.ToString());
                    Parser.SetValue(_wb, _army.ArmyFields[(int)ENUM.Army.Nobleman], _army.Nobleman.ToString());

                    var element = Parser.FindElement(_wb, "name", "input");
                    element.SetAttribute("value", _coordinates[X] + "|" + _coordinates[Y]);
                    _action = ENUM.FarmActions.AttackConfirm;
                    var attack = _wb.Document.GetElementById("target_attack");
                    attack.InvokeMember("click");

                    return;
                case ENUM.FarmActions.AttackConfirm:
                    if (!_wb.Url.ToString().Equals(_url.GetUrl(ENUM.Screens.AttackConfirm))) 
                        return; // keep searching the page until the buttons are all loaded 

                    var confirm = _wb.Document.GetElementById("troop_confirm_go");

                    if (confirm == null)
                        return;

                    confirm.InvokeMember("click");

                    break;

                case ENUM.FarmActions.Idle:
                    // Do nothing
                    return;
            }

            _action = ENUM.FarmActions.Idle;
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
