
namespace TransientSDB
{
    partial class Form1
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
            this.OutputGroupBos = new System.Windows.Forms.GroupBox();
            this.AAVSOTSXButton = new System.Windows.Forms.Button();
            this.OutputGroupBos.SuspendLayout();
            this.SuspendLayout();
            // 
            // TNSReaderButton
            // 
            this.TNSReaderButton.Location = new System.Drawing.Point(12, 95);
            this.TNSReaderButton.Name = "TNSReaderButton";
            this.TNSReaderButton.Size = new System.Drawing.Size(101, 54);
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
            // OutputGroupBos
            // 
            this.OutputGroupBos.Controls.Add(this.ClipboardRadioButton);
            this.OutputGroupBos.Controls.Add(this.TextFileRadioButton);
            this.OutputGroupBos.Location = new System.Drawing.Point(12, 12);
            this.OutputGroupBos.Name = "OutputGroupBos";
            this.OutputGroupBos.Size = new System.Drawing.Size(101, 65);
            this.OutputGroupBos.TabIndex = 3;
            this.OutputGroupBos.TabStop = false;
            this.OutputGroupBos.Text = "Output";
            // 
            // AAVSOTSXButton
            // 
            this.AAVSOTSXButton.Location = new System.Drawing.Point(140, 95);
            this.AAVSOTSXButton.Name = "AAVSOTSXButton";
            this.AAVSOTSXButton.Size = new System.Drawing.Size(101, 54);
            this.AAVSOTSXButton.TabIndex = 4;
            this.AAVSOTSXButton.Text = "AAVSO VSX Server";
            this.AAVSOTSXButton.UseVisualStyleBackColor = true;
            this.AAVSOTSXButton.Click += new System.EventHandler(this.AAVSOVSXButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 176);
            this.Controls.Add(this.AAVSOTSXButton);
            this.Controls.Add(this.OutputGroupBos);
            this.Controls.Add(this.TNSReaderButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.OutputGroupBos.ResumeLayout(false);
            this.OutputGroupBos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TNSReaderButton;
        private System.Windows.Forms.OpenFileDialog SDBTextFileDialog;
        private System.Windows.Forms.RadioButton TextFileRadioButton;
        private System.Windows.Forms.RadioButton ClipboardRadioButton;
        private System.Windows.Forms.GroupBox OutputGroupBos;
        private System.Windows.Forms.Button AAVSOTSXButton;
    }
}

