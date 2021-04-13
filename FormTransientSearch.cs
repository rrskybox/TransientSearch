using System;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Deployment.Application;
using System.Drawing;


namespace TransientSDB
{
    public partial class FormTransientServer : Form
    {
        public FormTransientServer()
        {
            InitializeComponent();
            string version;
            try
            { version = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(); }
            catch
            {
                //probably in debug mode
                version = "  **in Debug**";
            }
            this.Text = "Transient Search V"+version;
            Utility.ButtonGreen(TNSReaderButton);
            Utility.ButtonGreen(AAVSOVSXButton);
            OutputGroupBox.ForeColor = Color.Black;
        }

        private void TNSReaderButton_Click(object sender, EventArgs e)
        {
            //Import TNS-specific catalog data
            //sbXTNS is the TNS-specific xml db that has caught all the TNS header and data
            Utility.ButtonRed(TNSReaderButton);
            TNSManagement tnsAcquisition = new TNSManagement();
            tnsAcquisition.GetAndSet();
            if (TextFileRadioButton.Checked)
            {
                DialogResult odr = SDBTextFileDialog.ShowDialog();
                if (odr == DialogResult.OK)
                {
                    string textFileName = SDBTextFileDialog.FileName;
                    if (TextFileRadioButton.Checked)
                        tnsAcquisition.BuildSDBTextFile(textFileName);
                }
            }
            else
                tnsAcquisition.BuildSDBClipboard();
            Utility.ButtonGreen(TNSReaderButton);
        }

        private void AAVSOVSXButton_Click(object sender, EventArgs e)
        {
            //Import TSX-specific catalog data
            //sbXTNS is the TNS-specific xml db that has caught all the TNS header and data
            Utility.ButtonRed(AAVSOVSXButton);
            VSXManagement tsxAcquisition = new VSXManagement();
            tsxAcquisition.GetAndSet();
            if (TextFileRadioButton.Checked)
            {
                DialogResult odr = SDBTextFileDialog.ShowDialog();
                if (odr == DialogResult.OK)
                {
                    string textFileName = SDBTextFileDialog.FileName;
                    if (TextFileRadioButton.Checked)
                        tsxAcquisition.BuildSDBTextFile(textFileName);
                }
            }
            else
                tsxAcquisition.BuildSDBClipboard();
            Utility.ButtonGreen(AAVSOVSXButton);
        }

    }
}

