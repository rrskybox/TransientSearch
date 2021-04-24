
namespace TransientSDB
{
    partial class FormTransientServer
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
            this.TNSReaderButton = new System.Windows.Forms.Button();
            this.TextFileRadioButton = new System.Windows.Forms.RadioButton();
            this.ClipboardRadioButton = new System.Windows.Forms.RadioButton();
            this.OutputGroupBox = new System.Windows.Forms.GroupBox();
            this.AAVSOVSXButton = new System.Windows.Forms.Button();
            this.SearchDaysBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.TNSGroupBox = new System.Windows.Forms.GroupBox();
            this.ATSelectButton = new System.Windows.Forms.RadioButton();
            this.SuperNovaSelectButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SuspectsSelectButton = new System.Windows.Forms.RadioButton();
            this.BLLACSelectButton = new System.Windows.Forms.RadioButton();
            this.QSOSelectButton = new System.Windows.Forms.RadioButton();
            this.AGNSelectButton = new System.Windows.Forms.RadioButton();
            this.NovaSelectButton = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.RedDwarfSelectButton = new System.Windows.Forms.RadioButton();
            this.WhiteDwarfSelectButton = new System.Windows.Forms.RadioButton();
            this.NEOGroupBox = new System.Windows.Forms.GroupBox();
            this.NEOButton = new System.Windows.Forms.Button();
            this.ExoButton = new System.Windows.Forms.Button();
            this.ExoPlanetGroupBox = new System.Windows.Forms.GroupBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SDBTextFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.VZRGroupBox = new System.Windows.Forms.GroupBox();
            this.VZRButton = new System.Windows.Forms.Button();
            this.OutputGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SearchDaysBox)).BeginInit();
            this.TNSGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.NEOGroupBox.SuspendLayout();
            this.ExoPlanetGroupBox.SuspendLayout();
            this.VZRGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // TNSReaderButton
            // 
            this.TNSReaderButton.Location = new System.Drawing.Point(74, 45);
            this.TNSReaderButton.Name = "TNSReaderButton";
            this.TNSReaderButton.Size = new System.Drawing.Size(46, 31);
            this.TNSReaderButton.TabIndex = 0;
            this.TNSReaderButton.Text = "TNS";
            this.TNSReaderButton.UseVisualStyleBackColor = true;
            this.TNSReaderButton.Click += new System.EventHandler(this.TNSReaderButton_Click);
            // 
            // TextFileRadioButton
            // 
            this.TextFileRadioButton.AutoSize = true;
            this.TextFileRadioButton.Location = new System.Drawing.Point(624, 19);
            this.TextFileRadioButton.Name = "TextFileRadioButton";
            this.TextFileRadioButton.Size = new System.Drawing.Size(65, 17);
            this.TextFileRadioButton.TabIndex = 1;
            this.TextFileRadioButton.Text = "Text File";
            this.TextFileRadioButton.UseVisualStyleBackColor = true;
            // 
            // ClipboardRadioButton
            // 
            this.ClipboardRadioButton.AutoSize = true;
            this.ClipboardRadioButton.Checked = true;
            this.ClipboardRadioButton.Location = new System.Drawing.Point(624, 42);
            this.ClipboardRadioButton.Name = "ClipboardRadioButton";
            this.ClipboardRadioButton.Size = new System.Drawing.Size(69, 17);
            this.ClipboardRadioButton.TabIndex = 2;
            this.ClipboardRadioButton.TabStop = true;
            this.ClipboardRadioButton.Text = "Clipboard";
            this.ClipboardRadioButton.UseVisualStyleBackColor = true;
            // 
            // OutputGroupBox
            // 
            this.OutputGroupBox.BackColor = System.Drawing.Color.MediumTurquoise;
            this.OutputGroupBox.Controls.Add(this.ClipboardRadioButton);
            this.OutputGroupBox.Controls.Add(this.TextFileRadioButton);
            this.OutputGroupBox.Location = new System.Drawing.Point(12, 127);
            this.OutputGroupBox.Name = "OutputGroupBox";
            this.OutputGroupBox.Size = new System.Drawing.Size(708, 73);
            this.OutputGroupBox.TabIndex = 3;
            this.OutputGroupBox.TabStop = false;
            this.OutputGroupBox.Text = "Output";
            // 
            // AAVSOVSXButton
            // 
            this.AAVSOVSXButton.Location = new System.Drawing.Point(87, 70);
            this.AAVSOVSXButton.Name = "AAVSOVSXButton";
            this.AAVSOVSXButton.Size = new System.Drawing.Size(45, 31);
            this.AAVSOVSXButton.TabIndex = 4;
            this.AAVSOVSXButton.Text = "VSX";
            this.AAVSOVSXButton.UseVisualStyleBackColor = true;
            this.AAVSOVSXButton.Click += new System.EventHandler(this.AAVSOVSXButton_Click);
            // 
            // SearchDaysBox
            // 
            this.SearchDaysBox.Location = new System.Drawing.Point(11, 81);
            this.SearchDaysBox.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.SearchDaysBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SearchDaysBox.Name = "SearchDaysBox";
            this.SearchDaysBox.Size = new System.Drawing.Size(42, 20);
            this.SearchDaysBox.TabIndex = 5;
            this.SearchDaysBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.SearchDaysBox.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Days";
            // 
            // TNSGroupBox
            // 
            this.TNSGroupBox.BackColor = System.Drawing.Color.MediumTurquoise;
            this.TNSGroupBox.Controls.Add(this.ATSelectButton);
            this.TNSGroupBox.Controls.Add(this.label1);
            this.TNSGroupBox.Controls.Add(this.SuperNovaSelectButton);
            this.TNSGroupBox.Controls.Add(this.TNSReaderButton);
            this.TNSGroupBox.Controls.Add(this.SearchDaysBox);
            this.TNSGroupBox.Location = new System.Drawing.Point(11, 7);
            this.TNSGroupBox.Name = "TNSGroupBox";
            this.TNSGroupBox.Size = new System.Drawing.Size(137, 114);
            this.TNSGroupBox.TabIndex = 4;
            this.TNSGroupBox.TabStop = false;
            this.TNSGroupBox.Text = "Transient Name Server";
            // 
            // ATSelectButton
            // 
            this.ATSelectButton.AutoSize = true;
            this.ATSelectButton.Location = new System.Drawing.Point(13, 38);
            this.ATSelectButton.Name = "ATSelectButton";
            this.ATSelectButton.Size = new System.Drawing.Size(39, 17);
            this.ATSelectButton.TabIndex = 2;
            this.ATSelectButton.Text = "AT";
            this.ATSelectButton.UseVisualStyleBackColor = true;
            // 
            // SuperNovaSelectButton
            // 
            this.SuperNovaSelectButton.AutoSize = true;
            this.SuperNovaSelectButton.Checked = true;
            this.SuperNovaSelectButton.Location = new System.Drawing.Point(13, 15);
            this.SuperNovaSelectButton.Name = "SuperNovaSelectButton";
            this.SuperNovaSelectButton.Size = new System.Drawing.Size(40, 17);
            this.SuperNovaSelectButton.TabIndex = 1;
            this.SuperNovaSelectButton.TabStop = true;
            this.SuperNovaSelectButton.Text = "SN";
            this.SuperNovaSelectButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.MediumTurquoise;
            this.groupBox1.Controls.Add(this.SuspectsSelectButton);
            this.groupBox1.Controls.Add(this.BLLACSelectButton);
            this.groupBox1.Controls.Add(this.QSOSelectButton);
            this.groupBox1.Controls.Add(this.AGNSelectButton);
            this.groupBox1.Controls.Add(this.AAVSOVSXButton);
            this.groupBox1.Controls.Add(this.NovaSelectButton);
            this.groupBox1.Location = new System.Drawing.Point(398, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 114);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "AAVSO VSX Server";
            // 
            // SuspectsSelectButton
            // 
            this.SuspectsSelectButton.AutoSize = true;
            this.SuspectsSelectButton.Location = new System.Drawing.Point(87, 15);
            this.SuspectsSelectButton.Name = "SuspectsSelectButton";
            this.SuspectsSelectButton.Size = new System.Drawing.Size(69, 17);
            this.SuspectsSelectButton.TabIndex = 7;
            this.SuspectsSelectButton.Text = "Suspects";
            this.SuspectsSelectButton.UseVisualStyleBackColor = true;
            // 
            // BLLACSelectButton
            // 
            this.BLLACSelectButton.AutoSize = true;
            this.BLLACSelectButton.Location = new System.Drawing.Point(87, 38);
            this.BLLACSelectButton.Name = "BLLACSelectButton";
            this.BLLACSelectButton.Size = new System.Drawing.Size(58, 17);
            this.BLLACSelectButton.TabIndex = 6;
            this.BLLACSelectButton.Text = "BLLAC";
            this.BLLACSelectButton.UseVisualStyleBackColor = true;
            // 
            // QSOSelectButton
            // 
            this.QSOSelectButton.AutoSize = true;
            this.QSOSelectButton.Location = new System.Drawing.Point(15, 59);
            this.QSOSelectButton.Name = "QSOSelectButton";
            this.QSOSelectButton.Size = new System.Drawing.Size(59, 17);
            this.QSOSelectButton.TabIndex = 5;
            this.QSOSelectButton.Text = "Quasar";
            this.QSOSelectButton.UseVisualStyleBackColor = true;
            // 
            // AGNSelectButton
            // 
            this.AGNSelectButton.AutoSize = true;
            this.AGNSelectButton.Location = new System.Drawing.Point(15, 36);
            this.AGNSelectButton.Name = "AGNSelectButton";
            this.AGNSelectButton.Size = new System.Drawing.Size(48, 17);
            this.AGNSelectButton.TabIndex = 2;
            this.AGNSelectButton.Text = "AGN";
            this.AGNSelectButton.UseVisualStyleBackColor = true;
            // 
            // NovaSelectButton
            // 
            this.NovaSelectButton.AutoSize = true;
            this.NovaSelectButton.Checked = true;
            this.NovaSelectButton.Location = new System.Drawing.Point(15, 15);
            this.NovaSelectButton.Name = "NovaSelectButton";
            this.NovaSelectButton.Size = new System.Drawing.Size(51, 17);
            this.NovaSelectButton.TabIndex = 1;
            this.NovaSelectButton.TabStop = true;
            this.NovaSelectButton.Text = "Nova";
            this.NovaSelectButton.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Enabled = false;
            this.radioButton4.Location = new System.Drawing.Point(6, 67);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(47, 17);
            this.radioButton4.TabIndex = 10;
            this.radioButton4.Text = "TBD";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // RedDwarfSelectButton
            // 
            this.RedDwarfSelectButton.AutoSize = true;
            this.RedDwarfSelectButton.Location = new System.Drawing.Point(6, 45);
            this.RedDwarfSelectButton.Name = "RedDwarfSelectButton";
            this.RedDwarfSelectButton.Size = new System.Drawing.Size(76, 17);
            this.RedDwarfSelectButton.TabIndex = 9;
            this.RedDwarfSelectButton.Text = "Red Dwarf";
            this.RedDwarfSelectButton.UseVisualStyleBackColor = true;
            // 
            // WhiteDwarfSelectButton
            // 
            this.WhiteDwarfSelectButton.AutoSize = true;
            this.WhiteDwarfSelectButton.Checked = true;
            this.WhiteDwarfSelectButton.Location = new System.Drawing.Point(6, 22);
            this.WhiteDwarfSelectButton.Name = "WhiteDwarfSelectButton";
            this.WhiteDwarfSelectButton.Size = new System.Drawing.Size(84, 17);
            this.WhiteDwarfSelectButton.TabIndex = 8;
            this.WhiteDwarfSelectButton.TabStop = true;
            this.WhiteDwarfSelectButton.Text = "White Dwarf";
            this.WhiteDwarfSelectButton.UseVisualStyleBackColor = true;
            // 
            // NEOGroupBox
            // 
            this.NEOGroupBox.BackColor = System.Drawing.Color.MediumTurquoise;
            this.NEOGroupBox.Controls.Add(this.NEOButton);
            this.NEOGroupBox.Location = new System.Drawing.Point(265, 7);
            this.NEOGroupBox.Name = "NEOGroupBox";
            this.NEOGroupBox.Size = new System.Drawing.Size(127, 114);
            this.NEOGroupBox.TabIndex = 8;
            this.NEOGroupBox.TabStop = false;
            this.NEOGroupBox.Text = "ESA NEOScan Server";
            // 
            // NEOButton
            // 
            this.NEOButton.Location = new System.Drawing.Point(41, 45);
            this.NEOButton.Name = "NEOButton";
            this.NEOButton.Size = new System.Drawing.Size(45, 31);
            this.NEOButton.TabIndex = 4;
            this.NEOButton.Text = "NEO";
            this.NEOButton.UseVisualStyleBackColor = true;
            this.NEOButton.Click += new System.EventHandler(this.NEOButton_Click);
            // 
            // ExoButton
            // 
            this.ExoButton.Location = new System.Drawing.Point(33, 45);
            this.ExoButton.Name = "ExoButton";
            this.ExoButton.Size = new System.Drawing.Size(45, 31);
            this.ExoButton.TabIndex = 4;
            this.ExoButton.Text = "Exo";
            this.ExoButton.UseVisualStyleBackColor = true;
            this.ExoButton.Click += new System.EventHandler(this.ExoButton_Click);
            // 
            // ExoPlanetGroupBox
            // 
            this.ExoPlanetGroupBox.BackColor = System.Drawing.Color.MediumTurquoise;
            this.ExoPlanetGroupBox.Controls.Add(this.ExoButton);
            this.ExoPlanetGroupBox.Location = new System.Drawing.Point(154, 7);
            this.ExoPlanetGroupBox.Name = "ExoPlanetGroupBox";
            this.ExoPlanetGroupBox.Size = new System.Drawing.Size(105, 114);
            this.ExoPlanetGroupBox.TabIndex = 8;
            this.ExoPlanetGroupBox.TabStop = false;
            this.ExoPlanetGroupBox.Text = "ExoPlanet Server";
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(653, 206);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(67, 32);
            this.CloseButton.TabIndex = 9;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // SDBTextFileDialog
            // 
            this.SDBTextFileDialog.DefaultExt = "*.txt";
            // 
            // VZRGroupBox
            // 
            this.VZRGroupBox.BackColor = System.Drawing.Color.MediumTurquoise;
            this.VZRGroupBox.Controls.Add(this.radioButton4);
            this.VZRGroupBox.Controls.Add(this.VZRButton);
            this.VZRGroupBox.Controls.Add(this.RedDwarfSelectButton);
            this.VZRGroupBox.Controls.Add(this.WhiteDwarfSelectButton);
            this.VZRGroupBox.Location = new System.Drawing.Point(568, 7);
            this.VZRGroupBox.Name = "VZRGroupBox";
            this.VZRGroupBox.Size = new System.Drawing.Size(152, 114);
            this.VZRGroupBox.TabIndex = 9;
            this.VZRGroupBox.TabStop = false;
            this.VZRGroupBox.Text = "VIZIER Server";
            // 
            // VZRButton
            // 
            this.VZRButton.Location = new System.Drawing.Point(101, 45);
            this.VZRButton.Name = "VZRButton";
            this.VZRButton.Size = new System.Drawing.Size(45, 31);
            this.VZRButton.TabIndex = 4;
            this.VZRButton.Text = "VZR";
            this.VZRButton.UseVisualStyleBackColor = true;
            this.VZRButton.Click += new System.EventHandler(this.VZRButton_Click);
            // 
            // FormTransientServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cyan;
            this.ClientSize = new System.Drawing.Size(729, 244);
            this.Controls.Add(this.VZRGroupBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.NEOGroupBox);
            this.Controls.Add(this.ExoPlanetGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TNSGroupBox);
            this.Controls.Add(this.OutputGroupBox);
            this.ForeColor = System.Drawing.Color.Black;
            this.MaximizeBox = false;
            this.Name = "FormTransientServer";
            this.Text = "Transient Server";
            this.OutputGroupBox.ResumeLayout(false);
            this.OutputGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SearchDaysBox)).EndInit();
            this.TNSGroupBox.ResumeLayout(false);
            this.TNSGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.NEOGroupBox.ResumeLayout(false);
            this.ExoPlanetGroupBox.ResumeLayout(false);
            this.VZRGroupBox.ResumeLayout(false);
            this.VZRGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TNSReaderButton;
        private System.Windows.Forms.RadioButton TextFileRadioButton;
        private System.Windows.Forms.RadioButton ClipboardRadioButton;
        private System.Windows.Forms.GroupBox OutputGroupBox;
        private System.Windows.Forms.Button AAVSOVSXButton;
        private System.Windows.Forms.NumericUpDown SearchDaysBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox TNSGroupBox;
        private System.Windows.Forms.RadioButton ATSelectButton;
        private System.Windows.Forms.RadioButton SuperNovaSelectButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton AGNSelectButton;
        private System.Windows.Forms.RadioButton NovaSelectButton;
        private System.Windows.Forms.GroupBox NEOGroupBox;
        private System.Windows.Forms.Button NEOButton;
        private System.Windows.Forms.Button ExoButton;
        private System.Windows.Forms.GroupBox ExoPlanetGroupBox;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.RadioButton BLLACSelectButton;
        private System.Windows.Forms.RadioButton QSOSelectButton;
        private System.Windows.Forms.SaveFileDialog SDBTextFileDialog;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton RedDwarfSelectButton;
        private System.Windows.Forms.RadioButton WhiteDwarfSelectButton;
        private System.Windows.Forms.RadioButton SuspectsSelectButton;
        private System.Windows.Forms.GroupBox VZRGroupBox;
        private System.Windows.Forms.Button VZRButton;
    }
}

