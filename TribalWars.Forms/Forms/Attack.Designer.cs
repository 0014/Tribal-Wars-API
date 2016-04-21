namespace TribalWars.Forms
{
    partial class Attack
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button = new System.Windows.Forms.Button();
            this.ArmyList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnArmyAdd = new System.Windows.Forms.Button();
            this.btnArmyRemove = new System.Windows.Forms.Button();
            this.txtTemplateName = new System.Windows.Forms.TextBox();
            this.txtCoordinateX = new System.Windows.Forms.TextBox();
            this.btnFarmRemove = new System.Windows.Forms.Button();
            this.btnFarmAdd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.FarmingList = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCoordinateY = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSpear = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSwords = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAxe = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtLC = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtHC = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtScout = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtRam = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCat = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtKnight = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtNoble = new System.Windows.Forms.TextBox();
            this.btnPickDate = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblState = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.ScheduleList = new System.Windows.Forms.ListBox();
            this.lblErrorCounter = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(212, 504);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(75, 23);
            this.button.TabIndex = 0;
            this.button.Text = "Test Button";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // ArmyList
            // 
            this.ArmyList.FormattingEnabled = true;
            this.ArmyList.Location = new System.Drawing.Point(296, 46);
            this.ArmyList.Name = "ArmyList";
            this.ArmyList.Size = new System.Drawing.Size(135, 160);
            this.ArmyList.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(307, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Army Templates:";
            // 
            // btnArmyAdd
            // 
            this.btnArmyAdd.Location = new System.Drawing.Point(296, 215);
            this.btnArmyAdd.Name = "btnArmyAdd";
            this.btnArmyAdd.Size = new System.Drawing.Size(49, 23);
            this.btnArmyAdd.TabIndex = 3;
            this.btnArmyAdd.Text = "Add";
            this.btnArmyAdd.UseVisualStyleBackColor = true;
            this.btnArmyAdd.Click += new System.EventHandler(this.ArmyAdd_Click);
            // 
            // btnArmyRemove
            // 
            this.btnArmyRemove.Location = new System.Drawing.Point(374, 215);
            this.btnArmyRemove.Name = "btnArmyRemove";
            this.btnArmyRemove.Size = new System.Drawing.Size(57, 23);
            this.btnArmyRemove.TabIndex = 4;
            this.btnArmyRemove.Text = "Remove";
            this.btnArmyRemove.UseVisualStyleBackColor = true;
            this.btnArmyRemove.Click += new System.EventHandler(this.btnArmyRemove_Click);
            // 
            // txtTemplateName
            // 
            this.txtTemplateName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtTemplateName.Location = new System.Drawing.Point(12, 38);
            this.txtTemplateName.Name = "txtTemplateName";
            this.txtTemplateName.Size = new System.Drawing.Size(82, 20);
            this.txtTemplateName.TabIndex = 5;
            // 
            // txtCoordinateX
            // 
            this.txtCoordinateX.Location = new System.Drawing.Point(213, 316);
            this.txtCoordinateX.Name = "txtCoordinateX";
            this.txtCoordinateX.Size = new System.Drawing.Size(71, 20);
            this.txtCoordinateX.TabIndex = 10;
            // 
            // btnFarmRemove
            // 
            this.btnFarmRemove.Location = new System.Drawing.Point(374, 555);
            this.btnFarmRemove.Name = "btnFarmRemove";
            this.btnFarmRemove.Size = new System.Drawing.Size(57, 23);
            this.btnFarmRemove.TabIndex = 9;
            this.btnFarmRemove.Text = "Remove";
            this.btnFarmRemove.UseVisualStyleBackColor = true;
            this.btnFarmRemove.Click += new System.EventHandler(this.btnFarmRemove_Click);
            // 
            // btnFarmAdd
            // 
            this.btnFarmAdd.Location = new System.Drawing.Point(294, 555);
            this.btnFarmAdd.Name = "btnFarmAdd";
            this.btnFarmAdd.Size = new System.Drawing.Size(49, 23);
            this.btnFarmAdd.TabIndex = 8;
            this.btnFarmAdd.Text = "Add";
            this.btnFarmAdd.UseVisualStyleBackColor = true;
            this.btnFarmAdd.Click += new System.EventHandler(this.btnFarmAdd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(290, 250);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Farming Locations:";
            // 
            // FarmingList
            // 
            this.FarmingList.FormattingEnabled = true;
            this.FarmingList.Location = new System.Drawing.Point(296, 277);
            this.FarmingList.Name = "FarmingList";
            this.FarmingList.Size = new System.Drawing.Size(135, 264);
            this.FarmingList.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Template Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(210, 296);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Coordinate X:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(210, 344);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Coordinate Y:";
            // 
            // txtCoordinateY
            // 
            this.txtCoordinateY.Location = new System.Drawing.Point(213, 365);
            this.txtCoordinateY.Name = "txtCoordinateY";
            this.txtCoordinateY.Size = new System.Drawing.Size(71, 20);
            this.txtCoordinateY.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Spearmen:";
            // 
            // txtSpear
            // 
            this.txtSpear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtSpear.Location = new System.Drawing.Point(12, 90);
            this.txtSpear.Name = "txtSpear";
            this.txtSpear.Size = new System.Drawing.Size(71, 20);
            this.txtSpear.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(9, 113);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Swordsmen:";
            // 
            // txtSwords
            // 
            this.txtSwords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtSwords.Location = new System.Drawing.Point(12, 129);
            this.txtSwords.Name = "txtSwords";
            this.txtSwords.Size = new System.Drawing.Size(71, 20);
            this.txtSwords.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(9, 152);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Axemen:";
            // 
            // txtAxe
            // 
            this.txtAxe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtAxe.Location = new System.Drawing.Point(12, 168);
            this.txtAxe.Name = "txtAxe";
            this.txtAxe.Size = new System.Drawing.Size(71, 20);
            this.txtAxe.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(112, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Light Cavalier:";
            // 
            // txtLC
            // 
            this.txtLC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtLC.Location = new System.Drawing.Point(115, 90);
            this.txtLC.Name = "txtLC";
            this.txtLC.Size = new System.Drawing.Size(71, 20);
            this.txtLC.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(112, 113);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Heavy Cavalier:";
            // 
            // txtHC
            // 
            this.txtHC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtHC.Location = new System.Drawing.Point(115, 129);
            this.txtHC.Name = "txtHC";
            this.txtHC.Size = new System.Drawing.Size(71, 20);
            this.txtHC.TabIndex = 23;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(112, 152);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Scout:";
            // 
            // txtScout
            // 
            this.txtScout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtScout.Location = new System.Drawing.Point(115, 168);
            this.txtScout.Name = "txtScout";
            this.txtScout.Size = new System.Drawing.Size(71, 20);
            this.txtScout.TabIndex = 25;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(210, 74);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "Ram:";
            // 
            // txtRam
            // 
            this.txtRam.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtRam.Location = new System.Drawing.Point(213, 90);
            this.txtRam.Name = "txtRam";
            this.txtRam.Size = new System.Drawing.Size(71, 20);
            this.txtRam.TabIndex = 27;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(210, 113);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(49, 13);
            this.label13.TabIndex = 30;
            this.label13.Text = "Catapult:";
            // 
            // txtCat
            // 
            this.txtCat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtCat.Location = new System.Drawing.Point(213, 129);
            this.txtCat.Name = "txtCat";
            this.txtCat.Size = new System.Drawing.Size(71, 20);
            this.txtCat.TabIndex = 29;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(210, 152);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(40, 13);
            this.label14.TabIndex = 32;
            this.label14.Text = "Knight:";
            // 
            // txtKnight
            // 
            this.txtKnight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtKnight.Location = new System.Drawing.Point(213, 168);
            this.txtKnight.Name = "txtKnight";
            this.txtKnight.Size = new System.Drawing.Size(71, 20);
            this.txtKnight.TabIndex = 31;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(210, 189);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 13);
            this.label15.TabIndex = 34;
            this.label15.Text = "Nobleman:";
            // 
            // txtNoble
            // 
            this.txtNoble.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtNoble.Location = new System.Drawing.Point(213, 205);
            this.txtNoble.Name = "txtNoble";
            this.txtNoble.Size = new System.Drawing.Size(71, 20);
            this.txtNoble.TabIndex = 33;
            // 
            // btnPickDate
            // 
            this.btnPickDate.Location = new System.Drawing.Point(651, 467);
            this.btnPickDate.Name = "btnPickDate";
            this.btnPickDate.Size = new System.Drawing.Size(63, 20);
            this.btnPickDate.TabIndex = 45;
            this.btnPickDate.Text = "Pick";
            this.btnPickDate.UseVisualStyleBackColor = true;
            this.btnPickDate.Click += new System.EventHandler(this.btnPickDate_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label16.Location = new System.Drawing.Point(525, 447);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(189, 15);
            this.label16.TabIndex = 44;
            this.label16.Text = "Pick a date for selected item";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker.Location = new System.Drawing.Point(528, 467);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(117, 20);
            this.dateTimePicker.TabIndex = 43;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(528, 501);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(190, 34);
            this.btnStart.TabIndex = 42;
            this.btnStart.Text = "Start Schedule";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.ForeColor = System.Drawing.Color.Red;
            this.lblState.Location = new System.Drawing.Point(15, 316);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(135, 63);
            this.lblState.TabIndex = 46;
            this.lblState.Text = "OFF";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label17.Location = new System.Drawing.Point(449, 7);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(80, 20);
            this.label17.TabIndex = 41;
            this.label17.Text = "Schedule:";
            // 
            // ScheduleList
            // 
            this.ScheduleList.FormattingEnabled = true;
            this.ScheduleList.Location = new System.Drawing.Point(453, 36);
            this.ScheduleList.Name = "ScheduleList";
            this.ScheduleList.Size = new System.Drawing.Size(309, 407);
            this.ScheduleList.Sorted = true;
            this.ScheduleList.TabIndex = 47;
            this.ScheduleList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ScheduleList_MouseDoubleClick);
            // 
            // lblErrorCounter
            // 
            this.lblErrorCounter.AutoSize = true;
            this.lblErrorCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F);
            this.lblErrorCounter.ForeColor = System.Drawing.Color.Maroon;
            this.lblErrorCounter.Location = new System.Drawing.Point(31, 425);
            this.lblErrorCounter.Name = "lblErrorCounter";
            this.lblErrorCounter.Size = new System.Drawing.Size(0, 44);
            this.lblErrorCounter.TabIndex = 48;
            // 
            // Attack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 614);
            this.Controls.Add(this.lblErrorCounter);
            this.Controls.Add(this.ScheduleList);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.btnPickDate);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtNoble);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtKnight);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtCat);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtRam);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtScout);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtHC);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtLC);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtAxe);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtSwords);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtSpear);
            this.Controls.Add(this.txtCoordinateY);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCoordinateX);
            this.Controls.Add(this.btnFarmRemove);
            this.Controls.Add(this.btnFarmAdd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FarmingList);
            this.Controls.Add(this.txtTemplateName);
            this.Controls.Add(this.btnArmyRemove);
            this.Controls.Add(this.btnArmyAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ArmyList);
            this.Controls.Add(this.button);
            this.Name = "Attack";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Farming Screen";
            this.Resize += new System.EventHandler(this.Attack_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button;
        private System.Windows.Forms.ListBox ArmyList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnArmyAdd;
        private System.Windows.Forms.Button btnArmyRemove;
        private System.Windows.Forms.TextBox txtTemplateName;
        private System.Windows.Forms.TextBox txtCoordinateX;
        private System.Windows.Forms.Button btnFarmRemove;
        private System.Windows.Forms.Button btnFarmAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox FarmingList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCoordinateY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSpear;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSwords;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAxe;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtLC;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtHC;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtScout;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtRam;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtCat;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtKnight;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtNoble;
        private System.Windows.Forms.Button btnPickDate;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ListBox ScheduleList;
        private System.Windows.Forms.Label lblErrorCounter;
    }
}
