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
    public class LoginActions
    {
        private static string _sessionId;
        private static bool _actionFlag = false;

        private URL _url;
        private static WebBrowser _wb;
        private static ENUM.LoginActions _action;

        private string _userName, _password;
        
        public LoginActions()
        {
            _url = new URL();

            _sessionId = null;
        }

        public bool GetLoginStatus()
        {
            _action = ENUM.LoginActions.LoginStatus;
            NavigateThroughTread(_url.GetUrl(ENUM.Screens.LoginScreen));
            return _actionFlag;
        }

        public bool EnterCredentials(string userName, string password)
        {
            _action = ENUM.LoginActions.EnterCredentials;

            _userName = userName;
            _password = password;

            NavigateThroughTread(_url.GetUrl(ENUM.Screens.LoginScreen));

            return true;
        }

        public string Login()
        {
            // Logining into game
            _action = ENUM.LoginActions.Login;
            NavigateThroughTread(_url.GetUrl(ENUM.Screens.LoginScreen));
            return _sessionId;
        }

        private void PageLoaded(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            _actionFlag = false; // reset the action flag

            switch (_action)
            {
                case ENUM.LoginActions.LoginStatus:
                    var worldLoginButton = Tools.FindSpanContains(_wb, "32."); // check if the button exists
                    _actionFlag = worldLoginButton != null; // if button exists, user is already logged in, else is not
                    break;

                case ENUM.LoginActions.EnterCredentials:
                    var mainLoginButton = Tools.FindSpanContains(_wb, "Giri");

                    Tools.SetValue(_wb, "User", _userName); // set user name
                    Tools.SetValue(_wb, "Password", _password); // set password

                    Tools.Click(mainLoginButton);
                    break;

                case ENUM.LoginActions.Login:
                    
                    var loginButton = Tools.FindSpanContains(_wb, "32.");
                    _action = ENUM.LoginActions.EnterGame;
                    Tools.Click(loginButton);
                    
                    return;

                case ENUM.LoginActions.EnterGame:
                    if (!_wb.Url.ToString().Contains("overview"))
                        return;
                    _action = ENUM.LoginActions.GetSessionId;
                    if (_wb.Document != null)
                    {
                        var elementById = _wb.Document.GetElementById("map_main");
                        if (elementById != null)
                            Tools.Click(elementById); // if you gett an error here check bot protection !
                    }
                    return;

                case ENUM.LoginActions.GetSessionId:
                    if (!_wb.Url.ToString().Contains("main"))
                        return;
                    _sessionId = Tools.GetBetween(_wb.Url.ToString(), "village=", "&screen");
                    _actionFlag = true;
                    break;

                case ENUM.LoginActions.Idle:
                    // Do nothing
                    return;
            }

            _action = ENUM.LoginActions.Idle;
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
            while (th.IsAlive){ }
        }
    }
}
