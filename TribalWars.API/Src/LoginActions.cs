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

        #region Public Functions
        public LoginActions()
        {
            _url = new URL();

            _sessionId = null;
        }

        /// <summary>
        /// This function is used to determine whether the user is already logged in or not.
        /// </summary>
        /// <returns> If user is already logged-in returns the user-name, else returns null</returns>
        public string GetLoginStatus()
        {
            _action = ENUM.LoginActions.LoginStatus;

            NavigateThroughTread(_url.GetUrl(ENUM.Screens.LoginScreen));
            
            return _userId;
        }

        /// <summary>
        /// This function performs logging-in using the user name and password information 
        /// </summary>
        /// <param name="userName"> User name account </param>
        /// <param name="password"> password of account </param>
        /// <returns> Returns the username if logging-in is successfull, else it returns null </returns>
        public string EnterCredentials(string userName, string password)
        {
            _action = ENUM.LoginActions.EnterCredentials;

            // set username and password
            _userName = userName;
            _password = password;

            NavigateThroughTread(_url.GetUrl(ENUM.Screens.LoginScreen));

            return _userId;
        }

        /// <summary>
        /// This function is used to login to the game, It
        /// MUST be used after the credetials are entered.
        /// </summary>
        /// <returns> Returns the Token that instantiates other objects. 
        /// Returns null if login-in fails</returns>
        public string Login()
        {
            // Logining into game
            _action = ENUM.LoginActions.Login;
            
            NavigateThroughTread(_url.GetUrl(ENUM.Screens.LoginScreen));

            return _sessionId; // return token
        }
        #endregion

        #region Helper Functions
        private void PageLoaded(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            switch (_action)
            {
                case ENUM.LoginActions.LoginStatus:
                    
                    var worldLoginButton = Tools.FindNode(_wb, "Class", "world_button_active");// check if the button exists

                    // if button exists, user is already logged in, else is not
                    _userId = worldLoginButton != null ? 
                        Tools.FindNode(_wb, "name", "user").GetAttributeValue("value", "") : null;
                    
                    break;

                case ENUM.LoginActions.EnterCredentials:
                    var mainLoginButton = Tools.FindSpanContains(_wb, "Giri");

                    Tools.SetValue(_wb, "User", _userName); // set user name
                    Tools.SetValue(_wb, "Password", _password); // set password

                    Tools.Click(mainLoginButton);

                    // Get the user id
                    _userId = Tools.FindNode(_wb, "name", "user").GetAttributeValue("value", "");

                    break;

                case ENUM.LoginActions.Login:
                    
                    //Find the button to log-in
                    var loginButton = Tools.FindSpanContains(_wb, "32.");

                    // after loging in, continue to the next stage to enter the game
                    _action = ENUM.LoginActions.EnterGame;
                    
                    Tools.Click(loginButton);
                    
                    return;

                case ENUM.LoginActions.EnterGame:
                    // check if loaded page is the correct page
                    if (!_wb.Url.ToString().Contains("overview"))
                        return;

                    // after entering game, the next stage is to get the session id
                    _action = ENUM.LoginActions.GetSessionId;
                    
                    if (_wb.Document != null)
                    {
                        var elementById = _wb.Document.GetElementById("map_main");

                        if (elementById != null)
                            Tools.Click(elementById); // if you gett an error here check bot protection !
                    }
                    return;

                case ENUM.LoginActions.GetSessionId:
                    // check if loaded page is the correct page
                    if (!_wb.Url.ToString().Contains("main"))
                        return;

                    // get the session id which is located in the url
                    _sessionId = Tools.GetBetween(_wb.Url.ToString(), "village=", "&screen"); 
                    
                    break;

                case ENUM.LoginActions.Idle:
                    // Do nothing
                    return;
            }

            _action = ENUM.LoginActions.Idle; // before exiting the thread make sure the action is set back to idle

            Application.ExitThread();  // Stops the navigating thread
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
            
            while (th.IsAlive){ }
        }
        #endregion
    }
}
