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
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using ThreadState = System.Threading.ThreadState;

namespace TribalWars.API
{
    public class FarmActions
    {
        private const int X = 0;
        private const int Y = 1;

        private URL _url;

        public FarmActions(string token)
        {
            // set the urls using the token
            _url = new URL(token);
        }

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
            var command = new Navigator(army, x, y, _url);

            if (command.ErrorFlag)
            {
                command.Dispose();
                return -1;
            }
            
            var xLenght = Math.Abs(command.OwnCoordinates[X] - x);
            var yLenght = Math.Abs(command.OwnCoordinates[Y] - y);

            command.Dispose();

            var distance = Math.Sqrt(Math.Pow(xLenght, 2) + Math.Pow(yLenght, 2));

            return (int)Math.Ceiling(distance * army.GetSlowestUnitSpeed());
        }
    }

    internal class Navigator : ApplicationContext
    {
        private const int X = 0;
        private const int Y = 1;

        public readonly int[] OwnCoordinates;
        public bool ErrorFlag;

        private ENUM.FarmActions _action;
        private WebBrowser _wb;
        private ArmyBuilder _army;
        private Thread _th;
        private URL _url;

        private int[] _enemyCoordinates;
        private bool _attackFlag;

        public Navigator(ArmyBuilder army, int x, int y, URL url)
        {
            // instantiate the coordinates
            OwnCoordinates = new int[2];
            _enemyCoordinates = new int[2];

            // instruct to attack the village
            _action = ENUM.FarmActions.Attack;

            //get the army and coordinates
            _army = army;

            _enemyCoordinates[X] = x;
            _enemyCoordinates[Y] = y;

            //Place the attack command
            ErrorFlag = true; // the action is not complated, the flag will set as false once action is complete
            _attackFlag = false; // attack is not made yet

            _url = url;

            Console.WriteLine("-------------------------");
            Console.WriteLine("Journey starts");

            NavigateThroughTread(_url.GetUrl(ENUM.Screens.RallyPoint));
        }

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
                    if (_army.Spearman != 0)
                        Parser.SetValue(_wb, _army.ArmyFields[(int) ENUM.Army.Spearman], _army.Spearman.ToString());
                    if (_army.Swordsman != 0)
                        Parser.SetValue(_wb, _army.ArmyFields[(int) ENUM.Army.Swordsman], _army.Swordsman.ToString());
                    if (_army.Axeman != 0)
                        Parser.SetValue(_wb, _army.ArmyFields[(int) ENUM.Army.Axeman], _army.Axeman.ToString());
                    if (_army.Scout != 0)
                        Parser.SetValue(_wb, _army.ArmyFields[(int) ENUM.Army.Scout], _army.Scout.ToString());
                    if (_army.LightCavalry != 0)
                        Parser.SetValue(_wb, _army.ArmyFields[(int) ENUM.Army.LightCavalary],
                            _army.LightCavalry.ToString());
                    if (_army.HeavyCavalary != 0)
                        Parser.SetValue(_wb, _army.ArmyFields[(int) ENUM.Army.HeavyCavalary],
                            _army.HeavyCavalary.ToString());
                    if (_army.Ram != 0)
                        Parser.SetValue(_wb, _army.ArmyFields[(int) ENUM.Army.Ram], _army.Ram.ToString());
                    if (_army.Catapult != 0)
                        Parser.SetValue(_wb, _army.ArmyFields[(int) ENUM.Army.Catapult], _army.Catapult.ToString());
                    if (_army.Nobleman != 0)
                        Parser.SetValue(_wb, _army.ArmyFields[(int) ENUM.Army.Nobleman], _army.Nobleman.ToString());
                    //Parser.SetValue(_wb, _army.ArmyFields[(int)ENUM.Army.Knight], _army.Knight.ToString()); //Enable this line if the server allows the Knight
                    Console.WriteLine("Soldier values entered");

                    var attackerCoords = Parser.GetCoordinates(_wb);
                    Console.WriteLine("Got the attacker coordinates");
                    OwnCoordinates[X] = int.Parse(attackerCoords.Split('|')[X]);
                    OwnCoordinates[Y] = int.Parse(attackerCoords.Split('|')[Y]);

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
                        _wb.DocumentCompleted -= PageLoaded;
                        _wb.Dispose();
                        Application.ExitThread(); // Stops the thread
                        return;
                    }

                    Console.WriteLine("Confirmation found.");
                    confirm.InvokeMember("click");
                    Console.WriteLine("Confirmation clicked");
                    _attackFlag = true;
                    ErrorFlag = false;

                    _action = ENUM.FarmActions.Idle;

                    return;
                case ENUM.FarmActions.Idle:
                    _wb.Navigate(new Uri("about:blank"));
                    _action = ENUM.FarmActions.Exit;

                    return;
                case ENUM.FarmActions.Exit:
                    Console.WriteLine("Disposing wb...");
                    _wb.Stop();
                    _wb.DocumentCompleted -= PageLoaded;
                    _wb.Dispose();

                    break;
            }

            Console.WriteLine("Navigation completed.");
            Application.ExitThread(); // Stops the thread
        }

        /// <summary>
        /// This function help the web browser to perform actions in a synchronous way.
        /// </summary>
        /// <param name="url"> Navigates the browser object to the input url </param>
        private void NavigateThroughTread(string url)
        {
            Console.WriteLine("Defining thread...");

            _th = new Thread(() =>
            {
                try
                {
                    _wb = new WebBrowser();
                    _wb.DocumentCompleted += PageLoaded;
                    _wb.Visible = true;
                    _wb.AllowNavigation = true;
                    _wb.ScriptErrorsSuppressed = true;
                    _wb.Navigate(url);
                    Console.WriteLine("Web browser navigated.");
                    Application.Run();
                }
                catch (ThreadInterruptedException exception)
                {
                    /* Clean up. */
                    Console.WriteLine("Enters exception.");
                }
            });
            Console.WriteLine("Thread defined.");

            _th.SetApartmentState(ApartmentState.STA);

            Console.WriteLine("Before thread start...");
            _th.Start();
            Console.WriteLine("Thread started.");

            var sw = new Stopwatch();
            sw.Start();

            while (_th.IsAlive)
            {
                if (sw.Elapsed > TimeSpan.FromMilliseconds(10000))
                {
                    Console.WriteLine("Infinite loop detected!!!");
                    MessageBox.Show("Program disconnected. Reopen your program.");
                    ErrorFlag = true;
                    break;
                }
            }

            Console.WriteLine("Journey ends.");
            Console.WriteLine("*************************");
        }
    }
}
