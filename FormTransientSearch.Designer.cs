
namespace TransientSDB
{
    partial class FormTransientSearch
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
            this.NCGHostBox = new System.Windows.Forms.CheckBox();
            this.ATSelectButton = new System.Windows.Forms.RadioButton();
            this.SuperNovaSelectButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SuspectsSelectButton = new System.Windows.Forms.RadioButton();
            this.BLLACSelectButton = new System.Windows.Forms.RadioButton();
            this.QSOSelectButton = new System.Windows.Forms.RadioButton();
            this.AGNSelectButton = new System.Windows.Forms.RadioButton();
            this.NovaSelectButton = new System.Windows.Forms.RadioButton();
            this.NEOGroupBox = new System.Windows.Forms.GroupBox();
            this.ScoutButton = new System.Windows.Forms.RadioButton();
            this.NEOButton = new System.Windows.Forms.Button();
            this.NEOCPButton = new System.Windows.Forms.RadioButton();
            this.ExoButton = new System.Windows.Forms.Button();
            this.ExoPlanetGroupBox = new System.Windows.Forms.GroupBox();
            this.ConfirmedButton = new System.Windows.Forms.RadioButton();
            this.CandidateButton = new System.Windows.Forms.RadioButton();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SDBTextFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.OnTopBox = new System.Windows.Forms.CheckBox();
            this.Max50Button = new System.Windows.Forms.RadioButton();
            this.Max500Button = new System.Windows.Forms.RadioButton();
            this.Max100Button = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.OutputGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SearchDaysBox)).BeginInit();
            this.TNSGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.NEOGroupBox.SuspendLayout();
            this.ExoPlanetGroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TNSReaderButton
            // 
            this.TNSReaderButton.Location = new System.Drawing.Point(67, 153);
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
            this.TextFileRadioButton.Location = new System.Drawing.Point(356, 17);
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
            this.ClipboardRadioButton.Location = new System.Drawing.Point(356, 40);
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
            this.OutputGroupBox.Location = new System.Drawing.Point(199, 127);
            this.OutputGroupBox.Name = "OutputGroupBox";
            this.OutputGroupBox.Size = new System.Drawing.Size(431, 73);
            this.OutputGroupBox.TabIndex = 3;
            this.OutputGroupBox.TabStop = false;
            this.OutputGroupBox.Text = "Output";
            // 
            // AAVSOVSXButton
            // 
            this.AAVSOVSXButton.Location = new System.Drawing.Point(103, 66);
            this.AAVSOVSXButton.Name = "AAVSOVSXButton";
            this.AAVSOVSXButton.Size = new System.Drawing.Size(45, 31);
            this.AAVSOVSXButton.TabIndex = 4;
            this.AAVSOVSXButton.Text = "VSX";
            this.AAVSOVSXButton.UseVisualStyleBackColor = true;
            this.AAVSOVSXButton.Click += new System.EventHandler(this.AAVSOVSXButton_Click);
            // 
            // SearchDaysBox
            // 
            this.SearchDaysBox.Location = new System.Drawing.Point(14, 111);
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
            this.SearchDaysBox.Size = new System.Drawing.Size(44, 20);
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
            this.label1.Location = new System.Drawing.Point(22, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Days";
            // 
            // TNSGroupBox
            // 
            this.TNSGroupBox.BackColor = System.Drawing.Color.MediumTurquoise;
            this.TNSGroupBox.Controls.Add(this.groupBox2);
            this.TNSGroupBox.Controls.Add(this.NCGHostBox);
            this.TNSGroupBox.Controls.Add(this.ATSelectButton);
            this.TNSGroupBox.Controls.Add(this.label1);
            this.TNSGroupBox.Controls.Add(this.SuperNovaSelectButton);
            this.TNSGroupBox.Controls.Add(this.TNSReaderButton);
            this.TNSGroupBox.Controls.Add(this.SearchDaysBox);
            this.TNSGroupBox.Location = new System.Drawing.Point(11, 7);
            this.TNSGroupBox.Name = "TNSGroupBox";
            this.TNSGroupBox.Size = new System.Drawing.Size(182, 193);
            this.TNSGroupBox.TabIndex = 4;
            this.TNSGroupBox.TabStop = false;
            this.TNSGroupBox.Text = "Transient Name Server";
            // 
            // NCGHostBox
            // 
            this.NCGHostBox.AutoSize = true;
            this.NCGHostBox.Location = new System.Drawing.Point(14, 69);
            this.NCGHostBox.Name = "NCGHostBox";
            this.NCGHostBox.Size = new System.Drawing.Size(98, 17);
            this.NCGHostBox.TabIndex = 3;
            this.NCGHostBox.Text = "NCG Host Only";
            this.NCGHostBox.UseVisualStyleBackColor = true;
            // 
            // ATSelectButton
            // 
            this.ATSelectButton.AutoSize = true;
            this.ATSelectButton.Location = new System.Drawing.Point(14, 19);
            this.ATSelectButton.Name = "ATSelectButton";
            this.ATSelectButton.Size = new System.Drawing.Size(99, 17);
            this.ATSelectButton.TabIndex = 2;
            this.ATSelectButton.Text = "Unclassified AT";
            this.ATSelectButton.UseVisualStyleBackColor = true;
            // 
            // SuperNovaSelectButton
            // 
            this.SuperNovaSelectButton.AutoSize = true;
            this.SuperNovaSelectButton.Checked = true;
            this.SuperNovaSelectButton.Location = new System.Drawing.Point(14, 42);
            this.SuperNovaSelectButton.Name = "SuperNovaSelectButton";
            this.SuperNovaSelectButton.Size = new System.Drawing.Size(83, 17);
            this.SuperNovaSelectButton.TabIndex = 1;
            this.SuperNovaSelectButton.TabStop = true;
            this.SuperNovaSelectButton.Text = "Supernovae";
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
            this.groupBox1.Location = new System.Drawing.Point(466, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 114);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "AAVSO VSX Server";
            // 
            // SuspectsSelectButton
            // 
            this.SuspectsSelectButton.AutoSize = true;
            this.SuspectsSelectButton.Location = new System.Drawing.Point(89, 29);
            this.SuspectsSelectButton.Name = "SuspectsSelectButton";
            this.SuspectsSelectButton.Size = new System.Drawing.Size(69, 17);
            this.SuspectsSelectButton.TabIndex = 7;
            this.SuspectsSelectButton.Text = "Suspects";
            this.SuspectsSelectButton.UseVisualStyleBackColor = true;
            // 
            // BLLACSelectButton
            // 
            this.BLLACSelectButton.AutoSize = true;
            this.BLLACSelectButton.Location = new System.Drawing.Point(15, 63);
            this.BLLACSelectButton.Name = "BLLACSelectButton";
            this.BLLACSelectButton.Size = new System.Drawing.Size(58, 17);
            this.BLLACSelectButton.TabIndex = 6;
            this.BLLACSelectButton.Text = "BLLAC";
            this.BLLACSelectButton.UseVisualStyleBackColor = true;
            // 
            // QSOSelectButton
            // 
            this.QSOSelectButton.AutoSize = true;
            this.QSOSelectButton.Location = new System.Drawing.Point(15, 86);
            this.QSOSelectButton.Name = "QSOSelectButton";
            this.QSOSelectButton.Size = new System.Drawing.Size(59, 17);
            this.QSOSelectButton.TabIndex = 5;
            this.QSOSelectButton.Text = "Quasar";
            this.QSOSelectButton.UseVisualStyleBackColor = true;
            // 
            // AGNSelectButton
            // 
            this.AGNSelectButton.AutoSize = true;
            this.AGNSelectButton.Location = new System.Drawing.Point(15, 38);
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
            this.NovaSelectButton.Location = new System.Drawing.Point(15, 17);
            this.NovaSelectButton.Name = "NovaSelectButton";
            this.NovaSelectButton.Size = new System.Drawing.Size(57, 17);
            this.NovaSelectButton.TabIndex = 1;
            this.NovaSelectButton.TabStop = true;
            this.NovaSelectButton.Text = "Novae";
            this.NovaSelectButton.UseVisualStyleBackColor = true;
            // 
            // NEOGroupBox
            // 
            this.NEOGroupBox.BackColor = System.Drawing.Color.MediumTurquoise;
            this.NEOGroupBox.Controls.Add(this.ScoutButton);
            this.NEOGroupBox.Controls.Add(this.NEOButton);
            this.NEOGroupBox.Controls.Add(this.NEOCPButton);
            this.NEOGroupBox.Location = new System.Drawing.Point(333, 7);
            this.NEOGroupBox.Name = "NEOGroupBox";
            this.NEOGroupBox.Size = new System.Drawing.Size(128, 114);
            this.NEOGroupBox.TabIndex = 8;
            this.NEOGroupBox.TabStop = false;
            this.NEOGroupBox.Text = "MPC NEO Server";
            // 
            // ScoutButton
            // 
            this.ScoutButton.AutoSize = true;
            this.ScoutButton.Location = new System.Drawing.Point(18, 42);
            this.ScoutButton.Name = "ScoutButton";
            this.ScoutButton.Size = new System.Drawing.Size(53, 17);
            this.ScoutButton.TabIndex = 11;
            this.ScoutButton.Text = "Scout";
            this.ScoutButton.UseVisualStyleBackColor = true;
            // 
            // NEOButton
            // 
            this.NEOButton.Location = new System.Drawing.Point(44, 66);
            this.NEOButton.Name = "NEOButton";
            this.NEOButton.Size = new System.Drawing.Size(45, 31);
            this.NEOButton.TabIndex = 4;
            this.NEOButton.Text = "NEO";
            this.NEOButton.UseVisualStyleBackColor = true;
            this.NEOButton.Click += new System.EventHandler(this.NEOButton_Click);
            // 
            // NEOCPButton
            // 
            this.NEOCPButton.AutoSize = true;
            this.NEOCPButton.Checked = true;
            this.NEOCPButton.Location = new System.Drawing.Point(18, 19);
            this.NEOCPButton.Name = "NEOCPButton";
            this.NEOCPButton.Size = new System.Drawing.Size(62, 17);
            this.NEOCPButton.TabIndex = 10;
            this.NEOCPButton.TabStop = true;
            this.NEOCPButton.Text = "NEOCP";
            this.NEOCPButton.UseVisualStyleBackColor = true;
            // 
            // ExoButton
            // 
            this.ExoButton.Location = new System.Drawing.Point(42, 66);
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
            this.ExoPlanetGroupBox.Controls.Add(this.ConfirmedButton);
            this.ExoPlanetGroupBox.Controls.Add(this.ExoButton);
            this.ExoPlanetGroupBox.Controls.Add(this.CandidateButton);
            this.ExoPlanetGroupBox.Location = new System.Drawing.Point(199, 7);
            this.ExoPlanetGroupBox.Name = "ExoPlanetGroupBox";
            this.ExoPlanetGroupBox.Size = new System.Drawing.Size(128, 114);
            this.ExoPlanetGroupBox.TabIndex = 8;
            this.ExoPlanetGroupBox.TabStop = false;
            this.ExoPlanetGroupBox.Text = "ExoPlanet Server";
            // 
            // ConfirmedButton
            // 
            this.ConfirmedButton.AutoSize = true;
            this.ConfirmedButton.Checked = true;
            this.ConfirmedButton.Location = new System.Drawing.Point(15, 42);
            this.ConfirmedButton.Name = "ConfirmedButton";
            this.ConfirmedButton.Size = new System.Drawing.Size(72, 17);
            this.ConfirmedButton.TabIndex = 9;
            this.ConfirmedButton.TabStop = true;
            this.ConfirmedButton.Text = "Confirmed";
            this.ConfirmedButton.UseVisualStyleBackColor = true;
            // 
            // CandidateButton
            // 
            this.CandidateButton.AutoSize = true;
            this.CandidateButton.Location = new System.Drawing.Point(15, 19);
            this.CandidateButton.Name = "CandidateButton";
            this.CandidateButton.Size = new System.Drawing.Size(73, 17);
            this.CandidateButton.TabIndex = 8;
            this.CandidateButton.Text = "Candidate";
            this.CandidateButton.UseVisualStyleBackColor = true;
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(520, 206);
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
            // OnTopBox
            // 
            this.OnTopBox.AutoSize = true;
            this.OnTopBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.OnTopBox.Location = new System.Drawing.Point(12, 215);
            this.OnTopBox.Name = "OnTopBox";
            this.OnTopBox.Size = new System.Drawing.Size(98, 17);
            this.OnTopBox.TabIndex = 3;
            this.OnTopBox.Text = "Always On Top";
            this.OnTopBox.UseVisualStyleBackColor = true;
            this.OnTopBox.CheckedChanged += new System.EventHandler(this.OnTopBox_CheckedChanged);
            // 
            // Max50Button
            // 
            this.Max50Button.AutoSize = true;
            this.Max50Button.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Max50Button.Checked = true;
            this.Max50Button.Location = new System.Drawing.Point(5, 19);
            this.Max50Button.Name = "Max50Button";
            this.Max50Button.Size = new System.Drawing.Size(23, 30);
            this.Max50Button.TabIndex = 8;
            this.Max50Button.TabStop = true;
            this.Max50Button.Text = "50";
            this.Max50Button.UseVisualStyleBackColor = true;
            // 
            // Max500Button
            // 
            this.Max500Button.AutoSize = true;
            this.Max500Button.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Max500Button.Location = new System.Drawing.Point(69, 19);
            this.Max500Button.Name = "Max500Button";
            this.Max500Button.Size = new System.Drawing.Size(29, 30);
            this.Max500Button.TabIndex = 9;
            this.Max500Button.Text = "500";
            this.Max500Button.UseVisualStyleBackColor = true;
            // 
            // Max100Button
            // 
            this.Max100Button.AutoSize = true;
            this.Max100Button.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Max100Button.Location = new System.Drawing.Point(34, 19);
            this.Max100Button.Name = "Max100Button";
            this.Max100Button.Size = new System.Drawing.Size(29, 30);
            this.Max100Button.TabIndex = 10;
            this.Max100Button.Text = "100";
            this.Max100Button.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Max100Button);
            this.groupBox2.Controls.Add(this.Max500Button);
            this.groupBox2.Controls.Add(this.Max50Button);
            this.groupBox2.Location = new System.Drawing.Point(73, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(103, 55);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Max Count";
            // 
            // FormTransientSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cyan;
            this.ClientSize = new System.Drawing.Size(636, 244);
            this.Controls.Add(this.OnTopBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.NEOGroupBox);
            this.Controls.Add(this.ExoPlanetGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TNSGroupBox);
            this.Controls.Add(this.OutputGroupBox);
            this.ForeColor = System.Drawing.Color.Black;
            this.MaximizeBox = false;
            this.Name = "FormTransientSearch";
            this.Text = "Transient Search";
            this.OutputGroupBox.ResumeLayout(false);
            this.OutputGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SearchDaysBox)).EndInit();
            this.TNSGroupBox.ResumeLayout(false);
            this.TNSGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.NEOGroupBox.ResumeLayout(false);
            this.NEOGroupBox.PerformLayout();
            this.ExoPlanetGroupBox.ResumeLayout(false);
            this.ExoPlanetGroupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.RadioButton SuspectsSelectButton;
        private System.Windows.Forms.RadioButton ConfirmedButton;
        private System.Windows.Forms.RadioButton CandidateButton;
        private System.Windows.Forms.RadioButton ScoutButton;
        private System.Windows.Forms.RadioButton NEOCPButton;
        private System.Windows.Forms.CheckBox OnTopBox;
        private System.Windows.Forms.CheckBox NCGHostBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton Max100Button;
        private System.Windows.Forms.RadioButton Max500Button;
        private System.Windows.Forms.RadioButton Max50Button;
    }
}

