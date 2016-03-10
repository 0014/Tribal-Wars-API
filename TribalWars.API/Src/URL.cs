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
    public class URL
    {
        private const string BaseUrl = "https://tr32.klanlar.org/game.php?village=session&screen=";

        private string _sessionId;
        private string[] _urls;

        public URL()
        {
            SetUrls();
        }

        public URL(string session)
        {
            UpdateSession(session);
        }

        public void UpdateSession(string session)
        {
            _sessionId = session;
            SetUrls();
        }

        public ENUM.Screens GetCurrentScreen(string url)
        {
            if (url.Equals(_urls[(int) ENUM.Screens.LoginScreen]))
                return ENUM.Screens.LoginScreen;
            if (url.Equals(_urls[(int)ENUM.Screens.MainScreen]))
                return ENUM.Screens.MainScreen;
            if (url.Equals(_urls[(int)ENUM.Screens.Headquarters]))
                return ENUM.Screens.Headquarters;
            if (url.Equals(_urls[(int)ENUM.Screens.Barracks]))
                return ENUM.Screens.Barracks;
            if (url.Equals(_urls[(int)ENUM.Screens.RallyPoint]))
                return ENUM.Screens.RallyPoint;
            if (url.Equals(_urls[(int)ENUM.Screens.AttackConfirm]))
                return ENUM.Screens.AttackConfirm;

            return ENUM.Screens.ErrorScreen;
        }

        public string GetUrl(ENUM.Screens screen)
        {
            return _urls[(int)screen];
        }

        private void SetUrls()
        {
            _urls = new string[7];
            _urls[(int)ENUM.Screens.LoginScreen] = "https://www.klanlar.org/";
            _urls[(int)ENUM.Screens.MainScreen] = BaseUrl.Replace("session", _sessionId) + "overview";
            _urls[(int)ENUM.Screens.Headquarters] = BaseUrl.Replace("session", _sessionId) + "main";
            _urls[(int)ENUM.Screens.RallyPoint] = BaseUrl.Replace("session", _sessionId) + "place";
            _urls[(int)ENUM.Screens.Barracks] = BaseUrl.Replace("session", _sessionId) + "barracks";
            _urls[(int)ENUM.Screens.AttackConfirm] = BaseUrl.Replace("session", _sessionId) + "place&try=confirm";
        }
    }
}
