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
    public class FarmActions
    {
        private const int X = 0;
        private const int Y = 1;

        private URL _url;
        private WebBrowser _wb;
        private ENUM.FarmActions _action;
        private ArmyBuilder _army;

        private int[] _ownCoordinates, _enemyCoordinates;
        private bool _errorFlag, _attackFlag;

        public FarmActions(string token)
        {
            // set the urls using the token
            _url = new URL(token);

            // instantiate the coordinates
            _enemyCoordinates = new int[2];
            _ownCoordinates = new int[2];
        }

        #region Public Functions

        /// <summary>
        /// This function performs an attack on village with the specified coordinates,
        /// with the specified army
        /// </summary>
        /// <param name="x"> X coordinate of the village to attack </param>
        /// <param name="y"> Y coordinate of the village to attack </param>
        /// <param name="army"> the army in that is about to attack </param>
        /// <returns> Returns the time in terms of minutes, indicating when the army will reach destination </returns>
        public int Attack(int x, int y, ArmyBuilder army)
        {
            // instruct to attack the village
            _action = ENUM.FarmActions.Attack;

            //get the army and coordinates
            _army = army;
            _enemyCoordinates[X] = x;
            _enemyCoordinates[Y] = y;
            
            //Place the attack command
            _errorFlag = true; // the action is not complated, the flag will set as false once action is complete
            _attackFlag = false; // attack is not made yet
            Console.WriteLine("Journey starts");

            NavigateThroughTread(_url.GetUrl(ENUM.Screens.RallyPoint));

            return _errorFlag ? -1 : CalculateDistance();
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
            Console.WriteLine("Pages loads...");
            switch (_action)
            {
                case ENUM.FarmActions.Attack:
                    Console.WriteLine("Before rally point URL comparison...");
                    if (!_wb.Url.ToString().Equals(_url.GetUrl(ENUM.Screens.RallyPoint))) 
                        return; // keep searching the page until the buttons are all loaded 

                    Console.WriteLine("Rally point URL correct.");
                    if (_army.Spearman != 0) Parser.SetValue(_wb, _army.ArmyFields[(int)ENUM.Army.Spearman], _army.Spearman.ToString());
                    if (_army.Swordsman != 0) Parser.SetValue(_wb, _army.ArmyFields[(int)ENUM.Army.Swordsman], _army.Swordsman.ToString());
                    if (_army.Axeman != 0) Parser.SetValue(_wb, _army.ArmyFields[(int)ENUM.Army.Axeman], _army.Axeman.ToString());
                    if (_army.Scout != 0) Parser.SetValue(_wb, _army.ArmyFields[(int)ENUM.Army.Scout], _army.Scout.ToString());
                    if (_army.LightCavalry != 0) Parser.SetValue(_wb, _army.ArmyFields[(int)ENUM.Army.LightCavalary], _army.LightCavalry.ToString());
                    if (_army.HeavyCavalary != 0) Parser.SetValue(_wb, _army.ArmyFields[(int)ENUM.Army.HeavyCavalary], _army.HeavyCavalary.ToString());
                    if (_army.Ram != 0) Parser.SetValue(_wb, _army.ArmyFields[(int)ENUM.Army.Ram], _army.Ram.ToString());
                    if (_army.Catapult != 0) Parser.SetValue(_wb, _army.ArmyFields[(int)ENUM.Army.Catapult], _army.Catapult.ToString());
                    if (_army.Nobleman != 0) Parser.SetValue(_wb, _army.ArmyFields[(int)ENUM.Army.Nobleman], _army.Nobleman.ToString());
                    //Parser.SetValue(_wb, _army.ArmyFields[(int)ENUM.Army.Knight], _army.Knight.ToString()); //Enable this line if the server allows the Knight
                    Console.WriteLine("Soldier values entered");

                    var attackerCoords = Parser.GetCoordinates(_wb);
                    Console.WriteLine("Got the attacker coordinates");
                    _ownCoordinates[X] = int.Parse(attackerCoords.Split('|')[X]);
                    _ownCoordinates[Y] = int.Parse(attackerCoords.Split('|')[Y]);

                    Console.WriteLine("Before finding coordinate input...");
                    var element = Parser.FindElement(_wb, "name", "input");
                    element.SetAttribute("value", _enemyCoordinates[X] + "|" + _enemyCoordinates[Y]);
                    Console.WriteLine("Coordinates placed in input field.");

                    _action = ENUM.FarmActions.AttackConfirm;
                    Console.WriteLine("Search for attack command button...");
                    var attack = _wb.Document.GetElementById("target_attack");

                    if (attack != null)
                    {
                        Console.WriteLine("Attack command found.");
                        attack.InvokeMember("click");
                    }

                    else Console.WriteLine("Attack command not found!!!");

                    return;
                case ENUM.FarmActions.AttackConfirm:
                    Console.WriteLine("Before attack confirm URL comparison...");
                    if (!_wb.Url.ToString().Equals(_url.GetUrl(ENUM.Screens.AttackConfirm))) 
                        return; // keep searching the page until the buttons are all loaded 
                    if (_attackFlag)
                    {
                        Console.WriteLine("Attack is already made, returned.");
                        return;
                    }

                    Console.WriteLine("Attack confirm URL correct.");
                    Console.WriteLine("Searching for troop_confirmation...");
                    var confirm = _wb.Document.GetElementById("troop_confirm_go");

                    if (confirm == null)
                    {
                        Console.WriteLine("confirm is null, thread ends.");
                        _attackFlag = true; // do not enter here more than once
                        _wb.Stop();
                        _wb.DocumentCompleted -= PageLoaded;
                        _wb.Dispose();
                        Application.ExitThread();   // Stops the thread
                        return;
                    }

                    Console.WriteLine("Confirmation found.");
                    confirm.InvokeMember("click");
                    Console.WriteLine("Confirmation clicked");
                    _attackFlag = true;
                    _errorFlag = false;

                    _action = ENUM.FarmActions.Idle;

                    return;
                case ENUM.FarmActions.Idle:
                    Console.WriteLine("Disposing wb...");
                    _wb.Stop();
                    _wb.DocumentCompleted -= PageLoaded;
                    _wb.Dispose();

                    break;
            }

            Console.WriteLine("Navigation completed.");
            Application.ExitThread();   // Stops the thread
        }

        private void TestNavigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            Console.WriteLine("Navigating is fine, URL: " + e.Url);
        }

        private void TestNavigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            Console.WriteLine("Navigated is fine, URL: " + e.Url);
        }

        /// <summary>
        /// This function help the web browser to perform actions in a synchronous way.
        /// </summary>
        /// <param name="url"> Navigates the browser object to the input url </param>
        private void NavigateThroughTread(string url)
        {
            Console.WriteLine("Defining thread...");

            var th = new Thread(() =>
            {
                _wb = new WebBrowser();
                _wb.DocumentCompleted += PageLoaded;
                _wb.Navigating += TestNavigating;
                _wb.Navigated += TestNavigated;
                _wb.Visible = true;
                _wb.AllowNavigation = true;
                _wb.ScriptErrorsSuppressed = true;
                _wb.Navigate(url);
                Console.WriteLine("Web browser navigated.");
                Application.Run();
            });
            Console.WriteLine("Thread defined.");

            th.SetApartmentState(ApartmentState.STA);

            Console.WriteLine("Before thread start...");
            th.Start();
            Console.WriteLine("Thread started.");

            while (th.IsAlive) { }
            Console.WriteLine("Journey ends.");
        }

        /// <summary>
        /// Calculates how many minute it takes to attack the enemy coordinates
        /// </summary>
        /// <returns> The minutes for the distance </returns>
        private int CalculateDistance()
        {
            var xLenght = Math.Abs(_ownCoordinates[X] - _enemyCoordinates[X]);
            var yLenght = Math.Abs(_ownCoordinates[Y] - _enemyCoordinates[Y]);

            var distance = Math.Sqrt(Math.Pow(xLenght, 2) + Math.Pow(yLenght, 2));

            return (int)Math.Ceiling(distance *_army.GetSlowestUnitSpeed()); 
        }

        #endregion
    }
}
