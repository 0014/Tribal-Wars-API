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
        private static string _userId;

        private URL _url;
        private static WebBrowser _wb;
        private static ENUM.LoginActions _action;

        private string _userName, _password;
        
        public LoginActions()
        {
            _url = new URL();

            _sessionId = null;
        }

        public string GetLoginStatus()
        {
            _action = ENUM.LoginActions.LoginStatus;
            NavigateThroughTread(_url.GetUrl(ENUM.Screens.LoginScreen));
            return _userId;
        }

        public string EnterCredentials(string userName, string password)
        {
            _action = ENUM.LoginActions.EnterCredentials;

            _userName = userName;
            _password = password;

            NavigateThroughTread(_url.GetUrl(ENUM.Screens.LoginScreen));

            return _userId;
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
            switch (_action)
            {
                case ENUM.LoginActions.LoginStatus:
                    var worldLoginButton = Tools.FindSpanContains(_wb, "32."); // check if the button exists

                    // if button exists, user is already logged in, else is not
                    if (worldLoginButton != null)
                    {
                        // Get the user id
                        var pageData1 = Tools.SetPageData(_wb, _wb.DocumentText);
                        _userId = Tools.GetBetween(pageData1, "Merhaba", "!</H2>");
                    }
                    else
                        _userId = null;
                    
                    break;

                case ENUM.LoginActions.EnterCredentials:
                    var mainLoginButton = Tools.FindSpanContains(_wb, "Giri");

                    Tools.SetValue(_wb, "User", _userName); // set user name
                    Tools.SetValue(_wb, "Password", _password); // set password

                    Tools.Click(mainLoginButton);

                    // Get the user id
                    var pageData2 = Tools.SetPageData(_wb, _wb.DocumentText);
                    _userId = Tools.GetBetween(pageData2, "Merhaba", "!</H2>");

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
