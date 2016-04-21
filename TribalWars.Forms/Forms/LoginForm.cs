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
using System.Windows.Forms;
using TribalWars.API;
using TribalWars.Forms;

namespace TribalWars
{
    public partial class FrmLogin : Form
    {
        private LoginActions _command;

        public FrmLogin()
        {
            InitializeComponent();

            _command = new LoginActions();
            var userName = _command.GetLoginStatus();

            if (userName == null) return;

            // if the user is already logged in dont show the username and password area
            HideUI(userName);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Visible)
            {
                var userId = _command.EnterCredentials(txtUserName.Text, txtPassword.Text);

                if (userId == null)
                {
                    MessageBox.Show(@"Entered wrong username\password.");
                    return;
                }

                HideUI(userId);
            }
            else
            {
                var token = _command.Login();

                Hide();
                var form = new Buildings(token);
                var form2 = new Attack(token);
                form2.Show();
                form.Show();
            }
        }

        private void HideUI(string userName)
        {
            lblUser.Text = String.Format("Welcome {0}", userName);
            lblUser.Visible = true;

            lblUserName.Visible = false;
            lblPassword.Visible = false;
            txtUserName.Visible = false;
            txtPassword.Visible = false;
        }
    }
}
