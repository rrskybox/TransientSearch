
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
            this.OutputGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // TNSReaderButton
            // 
            this.TNSReaderButton.Location = new System.Drawing.Point(130, 13);
            this.TNSReaderButton.Name = "TNSReaderButton";
            this.TNSReaderButton.Size = new System.Drawing.Size(101, 64);
            this.TNSReaderButton.TabIndex = 0;
            this.TNSReaderButton.Text = "Transient Name Server";
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
            this.TextFileRadioButton.Checked = true;
            this.TextFileRadioButton.Location = new System.Drawing.Point(15, 15);
            this.TextFileRadioButton.Name = "TextFileRadioButton";
            this.TextFileRadioButton.Size = new System.Drawing.Size(65, 17);
            this.TextFileRadioButton.TabIndex = 1;
            this.TextFileRadioButton.TabStop = true;
            this.TextFileRadioButton.Text = "Text File";
            this.TextFileRadioButton.UseVisualStyleBackColor = true;
            // 
            // ClipboardRadioButton
            // 
            this.ClipboardRadioButton.AutoSize = true;
            this.ClipboardRadioButton.Location = new System.Drawing.Point(15, 38);
            this.ClipboardRadioButton.Name = "ClipboardRadioButton";
            this.ClipboardRadioButton.Size = new System.Drawing.Size(69, 17);
            this.ClipboardRadioButton.TabIndex = 2;
            this.ClipboardRadioButton.Text = "Clipboard";
            this.ClipboardRadioButton.UseVisualStyleBackColor = true;
            // 
            // OutputGroupBox
            // 
            this.OutputGroupBox.BackColor = System.Drawing.Color.PaleTurquoise;
            this.OutputGroupBox.Controls.Add(this.ClipboardRadioButton);
            this.OutputGroupBox.Controls.Add(this.TextFileRadioButton);
            this.OutputGroupBox.Location = new System.Drawing.Point(12, 12);
            this.OutputGroupBox.Name = "OutputGroupBox";
            this.OutputGroupBox.Size = new System.Drawing.Size(101, 65);
            this.OutputGroupBox.TabIndex = 3;
            this.OutputGroupBox.TabStop = false;
            this.OutputGroupBox.Text = "Output";
            // 
            // AAVSOVSXButton
            // 
            this.AAVSOVSXButton.Location = new System.Drawing.Point(248, 13);
            this.AAVSOVSXButton.Name = "AAVSOVSXButton";
            this.AAVSOVSXButton.Size = new System.Drawing.Size(101, 64);
            this.AAVSOVSXButton.TabIndex = 4;
            this.AAVSOVSXButton.Text = "AAVSO VSX Server";
            this.AAVSOVSXButton.UseVisualStyleBackColor = true;
            this.AAVSOVSXButton.Click += new System.EventHandler(this.AAVSOVSXButton_Click);
            // 
            // FormTransientServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cyan;
            this.ClientSize = new System.Drawing.Size(364, 93);
            this.Controls.Add(this.AAVSOVSXButton);
            this.Controls.Add(this.OutputGroupBox);
            this.Controls.Add(this.TNSReaderButton);
            this.ForeColor = System.Drawing.Color.Black;
            this.MaximizeBox = false;
            this.Name = "FormTransientServer";
            this.Text = "Transient Server";
            this.OutputGroupBox.ResumeLayout(false);
            this.OutputGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TNSReaderButton;
        private System.Windows.Forms.OpenFileDialog SDBTextFileDialog;
        private System.Windows.Forms.RadioButton TextFileRadioButton;
        private System.Windows.Forms.RadioButton ClipboardRadioButton;
        private System.Windows.Forms.GroupBox OutputGroupBox;
        private System.Windows.Forms.Button AAVSOVSXButton;
    }
}

