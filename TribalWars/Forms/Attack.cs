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
using System.Drawing;
using System.Windows.Forms;
using TribalWars.API;

namespace TribalWars.Forms
{
    public partial class Attack : Form
    {
        private FarmActions _command;
        private ScheduleTimer _tickTimer;

        private delegate void RemoveItemDelegate();

        private bool _isOn;

        public Attack(string token)
        {
            // Set the form location so that there wont be any overlap with the building form
            Left = 310;
            Top = 25;
            InitializeComponent();
            
            // Army list will display according to the added items name property
            ArmyList.DisplayMember = "Name";

            // Create an instance for farm actions
            _command = new FarmActions(token);

            _isOn = false; // set the sate as off

            // set the tray icon
            var trayIcon = new NotifyIcon();
            trayIcon.Text = "My application";
            trayIcon.Icon = new Icon("Attack.ico");

            // Add menu to tray icon and show it.
            var cm = new ContextMenu();
            cm.MenuItems.Add("Show", OnShow);
            cm.MenuItems.Add("Exit", OnExit);

            trayIcon.ContextMenu = cm;

            trayIcon.Visible = true;
        }

        private void ArmyAdd_Click(object sender, EventArgs e)
        {
            // Instantiate army builder
            var army = new ArmyBuilder(); 
            
            try
            {
                // Get the army name
                if (!txtTemplateName.Text.Equals("")) army.Name = txtTemplateName.Text;

                // Get army values
                if (!txtSpear.Text.Equals("")) army.Spearman = int.Parse(txtSpear.Text);
                if (!txtSwords.Text.Equals("")) army.Swordsman = int.Parse(txtSwords.Text);
                if (!txtAxe.Text.Equals("")) army.Axeman = int.Parse(txtAxe.Text);
                if (!txtLC.Text.Equals("")) army.LightCavalry = int.Parse(txtLC.Text);
                if (!txtHC.Text.Equals("")) army.HeavyCavalary = int.Parse(txtHC.Text);
                if (!txtScout.Text.Equals("")) army.Scout = int.Parse(txtScout.Text);
                if (!txtRam.Text.Equals("")) army.Ram = int.Parse(txtRam.Text);
                if (!txtCat.Text.Equals("")) army.Catapult = int.Parse(txtCat.Text);
                if (!txtKnight.Text.Equals("")) army.Knight = int.Parse(txtKnight.Text);
                if (!txtNoble.Text.Equals("")) army.Nobleman = int.Parse(txtNoble.Text);

                // add the created army into the army list
                if(army.GetSize() != 0) ArmyList.Items.Add(army);
            }
            catch (Exception)
            {
                // enters here if there is something wrong while parsing string to integer
                MessageBox.Show("Only numbers are allowed");
            }
        }

        private void btnArmyRemove_Click(object sender, EventArgs e)
        {
            // Return if nothing is selected
            if (ArmyList.SelectedIndex == -1)
                return;

            // Remove the seleceted item from the list
            ArmyList.Items.RemoveAt(ArmyList.SelectedIndex);
            
        }

        private void btnFarmAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a location according to the coordinate values inside the text boxes
                var village = new Village(int.Parse(txtCoordinateX.Text), int.Parse(txtCoordinateY.Text));

                // Add the created location into the list 
                FarmingList.Items.Add(village);
            }
            catch (Exception)
            {
                // enters here if there is something wrong while parsing string to integer
                MessageBox.Show("Please enter both coordinates.");
            }
        }

        private void btnFarmRemove_Click(object sender, EventArgs e)
        {
            // Return if nothing is selected
            if (FarmingList.SelectedIndex == -1)
                return;

            // Remove the seleceted item from the list
            FarmingList.Items.RemoveAt(FarmingList.SelectedIndex);
        }

        private void btnPickDate_Click(object sender, EventArgs e)
        {
            if (ArmyList.SelectedIndex == -1 || FarmingList.SelectedIndex == -1)
                return;

            if (_isOn)
            {
                MessageBox.Show("To add new events, first turn off the scheduler.");
                return;
            }

            var scheduleItem = new AttackScheduler((Village)FarmingList.SelectedItem, (ArmyBuilder)ArmyList.SelectedItem, dateTimePicker.Value);

            if (ScheduleList.SelectedIndex == -1)
                ScheduleList.Items.Add(scheduleItem);
            else
            {
                ScheduleList.Items.RemoveAt(ScheduleList.SelectedIndex);
                ScheduleList.Items.Add(scheduleItem);
            }
            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_isOn)
            {
                // Update UI
                lblState.Text = "OFF";
                lblState.ForeColor = Color.Red;
                btnStart.Text = "Start Schedule";

                // Reset the scheduler
                _tickTimer.Stop();
                _tickTimer.Dispose();
            }
            else
            {
                // Instantiate the scheduler
                _tickTimer = new ScheduleTimer();
                _tickTimer.Elapsed += TickTimer_Elapsed;

                // Update UI
                lblState.Text = "ON";
                lblState.ForeColor = Color.Green;
                btnStart.Text = "Stop Schedule";

                for (var i = ScheduleList.Items.Count - 1; i >= 0; i--)
                {
                    var date = Convert.ToDateTime(ScheduleList.Items[i].ToString().Split('|')[0]);

                    if (date.CompareTo(DateTime.Now) <= 0)
                    {
                        ScheduleList.Items.RemoveAt(i);
                        continue;
                    }

                    _tickTimer.AddEvent(new SingleEvent(date));
                }

                // start the timer
                _tickTimer.Start();

                Hide(); // Hide form window.
                ShowInTaskbar = false; // Remove from taskbar.
            }

            _isOn ^= true;
        }

        private void ScheduleList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ScheduleList.Items.RemoveAt(ScheduleList.SelectedIndex);
        }

        private void TickTimer_Elapsed(object sender, ScheduledEventArgs scheduledEventArgs)
        {
            //Parse the building name from the list-box
            var item = (AttackScheduler)ScheduleList.Items[0];

            _command.Attack(item.Location.X, item.Location.Y, item.Army);

            // building is upgraded, remove it from the list-box
            RemoveItemAfterUpg();
        }

        private void RemoveItemAfterUpg()
        {
            if (ScheduleList.InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new RemoveItemDelegate(RemoveItemAfterUpg), new object[] { });
                return;
            }

            // Must be on the UI thread if we've got this far
            ScheduleList.Items.RemoveAt(0);
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnShow(object sender, EventArgs e)
        {
            Show();
        }

        private void Attack_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

    }
}
