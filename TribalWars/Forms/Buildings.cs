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
using System.Linq;
using System.Windows.Forms;
using TribalWars.API;
using TribalWars.Tools;

namespace TribalWars
{
    public partial class Buildings : Form
    {
        private bool _isOn = false;

        private BuildingActions _command;
        private BuildingList[] _bList;
        private ScheduleTimer _tickTimer;
        private StoreData _storage;

        private delegate void RemoveItemDelegate();

        public Buildings(string token)
        {
            Left = 610;
            Top = 25;
            InitializeComponent();

            // set the storage and load the building list
            const string storageColumns = "Date,Building";
            _storage = new StoreData("Buildings", storageColumns);
            var items = _storage.ReadLines();
            for(var i = 1; i < items.Length; i ++) // do not add the column names to list
                scheduleList.Items.Add(items[i].Replace(",", "|"));

            // set tribal wars api object
            _command = new BuildingActions(token);

            // set all the current building levels
            SetBuildignLevels();

            // set the tray icon
            var trayIcon = new NotifyIcon();
            trayIcon.Text = "My application";
            trayIcon.Icon = new Icon("Building.ico");

            // Add menu to tray icon and show it.
            var cm = new ContextMenu();
            cm.MenuItems.Add("Show", OnShow);
            cm.MenuItems.Add("Exit", OnExit);

            trayIcon.ContextMenu = cm;

            trayIcon.Visible = true;
        }

        private void SetBuildignLevels()
        {
            var b = _command.GetBuildings();
            _bList = new BuildingList[b.Length];

            lvlHQ.Text = string.Format("(level {0})", b[(int)ENUM.Buildings.HQ].Level);
            lvlAcademy.Text = string.Format("(level {0})", b[(int)ENUM.Buildings.Academy].Level);
            lvlBarracks.Text = string.Format("(level {0})", b[(int)ENUM.Buildings.Barracks].Level);
            lvlClay.Text = string.Format("(level {0})", b[(int)ENUM.Buildings.Clay].Level);
            lvlFactory.Text = string.Format("(level {0})", b[(int)ENUM.Buildings.Factory].Level);
            lvlFarm.Text = string.Format("(level {0})", b[(int)ENUM.Buildings.Farm].Level);
            lvlHiddingPlace.Text = string.Format("(level {0})", b[(int)ENUM.Buildings.HiddingPlace].Level);
            lvlIron.Text = string.Format("(level {0})", b[(int)ENUM.Buildings.Iron].Level);
            lvlMarketPlace.Text = string.Format("(level {0})", b[(int)ENUM.Buildings.MarketPlace].Level);
            lvlRallyPoint.Text = string.Format("(level {0})", b[(int)ENUM.Buildings.RallyPoint].Level);
            lvlSimith.Text = string.Format("(level {0})", b[(int)ENUM.Buildings.Simith].Level);
            lvlStable.Text = string.Format("(level {0})", b[(int)ENUM.Buildings.Stable].Level);
            lvlStatue.Text = string.Format("(level {0})", b[(int)ENUM.Buildings.Statue].Level);
            lvlStorage.Text = string.Format("(level {0})", b[(int)ENUM.Buildings.Storage].Level);
            lvlWall.Text = string.Format("(level {0})", b[(int)ENUM.Buildings.Wall].Level);
            lvlWood.Text = string.Format("(level {0})", b[(int)ENUM.Buildings.Wood].Level);

            // This will instantiate the list where I`ll use to add the buildigns to the list-box
            foreach (ENUM.Buildings index in Enum.GetValues(typeof(ENUM.Buildings)))
            {
                _bList[(int)index] = new BuildingList(index.ToString(), b[(int)index].Level);
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

                // Clear the data before wrintg the elements in it
                _storage.Clear();

                for (var i = scheduleList.Items.Count - 1; i >= 0; i--)
                {
                    var date = Convert.ToDateTime(scheduleList.Items[i].ToString().Split('|')[0]);

                    if (date.CompareTo(DateTime.Now) <= 0)
                    {
                        scheduleList.Items.RemoveAt(i);
                        continue;
                    }

                    _storage.WriteLine(scheduleList.Items[i].ToString().Replace("|",",")); // store the items in a file

                    _tickTimer.AddEvent(new SingleEvent(date));
                }

                _tickTimer.Start();
                
                Visible       = false; // Hide form window.
                ShowInTaskbar = false; // Remove from taskbar.
            }

            _isOn ^= true; // toggle the on-off state

        }

        private void btnPickDate_Click(object sender, EventArgs e)
        {
            if (scheduleList.SelectedIndex == -1)
                return;

            if (_isOn)
            {
                MessageBox.Show("To add new events, first turn off the scheduler.");
                return;
            }

            foreach (var b in _bList.Where(b => scheduleList.SelectedItem.ToString().Contains(b.BuildingName)))
            {
                b.BuildDate = dateTimePicker1.Value;

                var date = b.BuildDate.ToString("yyyy-MM-dd HH:mm:ss");

                scheduleList.Items.RemoveAt(scheduleList.SelectedIndex);
                scheduleList.Items.Add(date.PadRight(25 - date.Length) + "|" + b.BuildingName);
                break;
            }
        }

        private void scheduleList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (scheduleList.SelectedIndex == -1)
                return;

            scheduleList.Items.RemoveAt(scheduleList.SelectedIndex);
        }

        private void LabelClick(object sender, EventArgs e)
        {
            foreach (var b in _bList)
            {
                if (!((Label) sender).Text.Equals(b.BuildingName)) continue;

                var date = b.BuildDate.ToString("yyyy-MM-dd HH:mm:ss");

                scheduleList.Items.Add(date.PadRight(25 - date.Length) + "|" + b.BuildingName);

                break;
            }
        }

        private void TickTimer_Elapsed(object sender, ScheduledEventArgs scheduledEventArgs)
        {
            //Parse the building name from the list-box
            var bName = scheduleList.Items[0].ToString().Split('|')[1];

            // Find the actual building using the parsed value, and upgrade it
            foreach (var index in Enum.GetValues(typeof(ENUM.Buildings)).Cast<ENUM.Buildings>().Where(index => bName.Equals(index.ToString())))
                _command.UpgradeBuilding(index);

            // remove from the file
            _storage.DeleteLine(scheduleList.Items[0].ToString().Replace("|", ","));

            // building is upgraded, remove it from the list-box
            RemoveItemAfterUpg();
        }

        private void RemoveItemAfterUpg()
        {
            if (scheduleList.InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new RemoveItemDelegate(RemoveItemAfterUpg), new object[] { });
                return;
            }
            
            // Must be on the UI thread if we've got this far
            scheduleList.Items.RemoveAt(0);
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnShow(object sender, EventArgs e)
        {
            Show();
        }

        private void Buildings_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }
    }
}
