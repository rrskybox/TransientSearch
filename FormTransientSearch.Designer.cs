
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
            this.SDBTextFileDialog = new System.Windows.Forms.OpenFileDialog();
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
            this.AGNSelectButton = new System.Windows.Forms.RadioButton();
            this.NovaSelectButton = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.ExoButton = new System.Windows.Forms.Button();
            this.ExoPlanetGroupBox = new System.Windows.Forms.GroupBox();
            this.OutputGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SearchDaysBox)).BeginInit();
            this.TNSGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.ExoPlanetGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // TNSReaderButton
            // 
            this.TNSReaderButton.Location = new System.Drawing.Point(43, 64);
            this.TNSReaderButton.Name = "TNSReaderButton";
            this.TNSReaderButton.Size = new System.Drawing.Size(46, 31);
            this.TNSReaderButton.TabIndex = 0;
            this.TNSReaderButton.Text = "TNS";
            this.TNSReaderButton.UseVisualStyleBackColor = true;
            this.TNSReaderButton.Click += new System.EventHandler(this.TNSReaderButton_Click);
            // 
            // SDBTextFileDialog
            // 
            this.SDBTextFileDialog.CheckFileExists = false;
            this.SDBTextFileDialog.DefaultExt = "txt";
            this.SDBTextFileDialog.FileName = "TNSdatabase.txt";
            // 
            // TextFileRadioButton
            // 
            this.TextFileRadioButton.AutoSize = true;
            this.TextFileRadioButton.Location = new System.Drawing.Point(9, 29);
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
            this.ClipboardRadioButton.Location = new System.Drawing.Point(9, 52);
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
            this.OutputGroupBox.Location = new System.Drawing.Point(161, 186);
            this.OutputGroupBox.Name = "OutputGroupBox";
            this.OutputGroupBox.Size = new System.Drawing.Size(87, 104);
            this.OutputGroupBox.TabIndex = 3;
            this.OutputGroupBox.TabStop = false;
            this.OutputGroupBox.Text = "Output";
            // 
            // AAVSOVSXButton
            // 
            this.AAVSOVSXButton.Location = new System.Drawing.Point(44, 64);
            this.AAVSOVSXButton.Name = "AAVSOVSXButton";
            this.AAVSOVSXButton.Size = new System.Drawing.Size(45, 31);
            this.AAVSOVSXButton.TabIndex = 4;
            this.AAVSOVSXButton.Text = "VSX";
            this.AAVSOVSXButton.UseVisualStyleBackColor = true;
            this.AAVSOVSXButton.Click += new System.EventHandler(this.AAVSOVSXButton_Click);
            // 
            // SearchDaysBox
            // 
            this.SearchDaysBox.Location = new System.Drawing.Point(43, 38);
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
            this.SearchDaysBox.Size = new System.Drawing.Size(46, 20);
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
            this.label1.Location = new System.Drawing.Point(6, 40);
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
            this.TNSGroupBox.Location = new System.Drawing.Point(7, 12);
            this.TNSGroupBox.Name = "TNSGroupBox";
            this.TNSGroupBox.Size = new System.Drawing.Size(137, 104);
            this.TNSGroupBox.TabIndex = 4;
            this.TNSGroupBox.TabStop = false;
            this.TNSGroupBox.Text = "Transient Name Server";
            // 
            // ATSelectButton
            // 
            this.ATSelectButton.AutoSize = true;
            this.ATSelectButton.Location = new System.Drawing.Point(54, 15);
            this.ATSelectButton.Name = "ATSelectButton";
            this.ATSelectButton.Size = new System.Drawing.Size(83, 17);
            this.ATSelectButton.TabIndex = 2;
            this.ATSelectButton.Text = "Un-class AT";
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
            this.groupBox1.Controls.Add(this.AGNSelectButton);
            this.groupBox1.Controls.Add(this.AAVSOVSXButton);
            this.groupBox1.Controls.Add(this.NovaSelectButton);
            this.groupBox1.Location = new System.Drawing.Point(7, 122);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(137, 104);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "AAVSO VSX Server";
            // 
            // AGNSelectButton
            // 
            this.AGNSelectButton.AutoSize = true;
            this.AGNSelectButton.Enabled = false;
            this.AGNSelectButton.Location = new System.Drawing.Point(72, 15);
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
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.MediumTurquoise;
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Location = new System.Drawing.Point(7, 342);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(137, 104);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Minor Planet Server";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(44, 64);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(45, 31);
            this.button2.TabIndex = 4;
            this.button2.Text = "TBD";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // ExoButton
            // 
            this.ExoButton.Location = new System.Drawing.Point(44, 64);
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
            this.ExoPlanetGroupBox.Location = new System.Drawing.Point(7, 232);
            this.ExoPlanetGroupBox.Name = "ExoPlanetGroupBox";
            this.ExoPlanetGroupBox.Size = new System.Drawing.Size(137, 104);
            this.ExoPlanetGroupBox.TabIndex = 8;
            this.ExoPlanetGroupBox.TabStop = false;
            this.ExoPlanetGroupBox.Text = "ExoPlanet Server";
            // 
            // FormTransientServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cyan;
            this.ClientSize = new System.Drawing.Size(254, 455);
            this.Controls.Add(this.groupBox3);
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
            this.groupBox3.ResumeLayout(false);
            this.ExoPlanetGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TNSReaderButton;
        private System.Windows.Forms.OpenFileDialog SDBTextFileDialog;
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
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button ExoButton;
        private System.Windows.Forms.GroupBox ExoPlanetGroupBox;
    }
}

