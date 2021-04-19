//This research has made use of data and/or services provided by the International Astronomical Union's Minor Planet Center.

using System;
using System.Deployment.Application;
using System.Drawing;
using System.Windows.Forms;


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
            this.Text = "Transient Search V" + version;
            Utility.ButtonGreen(TNSReaderButton);
            Utility.ButtonGreen(AAVSOVSXButton);
            Utility.ButtonGreen(ExoButton);
            Utility.ButtonGreen(NEOButton);
            Utility.ButtonGreen(CloseButton);
            OutputGroupBox.ForeColor = Color.Black;
        }

        private void TNSReaderButton_Click(object sender, EventArgs e)
        {
            //Import TNS-specific catalog data
            //sbXTNS is the TNS-specific xml db that has caught all the TNS header and data
            Utility.ButtonRed(TNSReaderButton);
            TNSManagement tnsAcquisition = new TNSManagement();
            tnsAcquisition.SearchBackDays = (int)SearchDaysBox.Value;
            tnsAcquisition.SearchClassified = ATSelectButton.Checked;
            tnsAcquisition.SearchSN = SuperNovaSelectButton.Checked;

            tnsAcquisition.GetAndSet();
            if (TextFileRadioButton.Checked)
            {
                SDBTextFileDialog.FileName = "TNS Database.txt";
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
            VSXManagement vsxAcquisition = new VSXManagement();
            vsxAcquisition.SearchNova = NovaSelectButton.Checked;
            vsxAcquisition.SearchAGN = AGNSelectButton.Checked;

            vsxAcquisition.GetAndSet();
            if (TextFileRadioButton.Checked)
            {
                SDBTextFileDialog.FileName = "VSX Database.txt";
                DialogResult odr = SDBTextFileDialog.ShowDialog();
                if (odr == DialogResult.OK)
                {
                    string textFileName = SDBTextFileDialog.FileName;
                    if (TextFileRadioButton.Checked)
                        vsxAcquisition.BuildSDBTextFile(textFileName);
                }
            }
            else
                vsxAcquisition.BuildSDBClipboard();
            Utility.ButtonGreen(AAVSOVSXButton);
        }

        private void ExoButton_Click(object sender, EventArgs e)
        {
            //Import TSX-specific catalog data
            //sbXTNS is the TNS-specific xml db that has caught all the TNS header and data
            Utility.ButtonRed(ExoButton);
            EXOManagement exoAcquisition = new EXOManagement();
            //exoAcquisition.SearchNova = NovaSelectButton.Checked;
            //exoAcquisition.SearchAGN = AGNSelectButton.Checked;

            exoAcquisition.GetAndSet();
            if (TextFileRadioButton.Checked)
            {
                SDBTextFileDialog.FileName = "EXO Database.txt";
                DialogResult odr = SDBTextFileDialog.ShowDialog();
                if (odr == DialogResult.OK)
                {
                    string textFileName = SDBTextFileDialog.FileName;
                    if (TextFileRadioButton.Checked)
                        exoAcquisition.BuildSDBTextFile(textFileName);
                }
            }
            else
                exoAcquisition.BuildSDBClipboard();
            Utility.ButtonGreen(ExoButton);

        }

        private void NEOButton_Click(object sender, EventArgs e)
        {
            //Import ESA NEO-specific catalog data
            //sbXTNS is the TNS-specific xml db that has caught all the TNS header and data
            Utility.ButtonRed(NEOButton);
            NEOManagement neoAcquisition = new NEOManagement();

            neoAcquisition.GetAndSet();
            if (TextFileRadioButton.Checked)
            {
                SDBTextFileDialog.FileName = "NEO Database.txt";
                DialogResult odr = SDBTextFileDialog.ShowDialog();
                if (odr == DialogResult.OK)
                {
                    string textFileName = SDBTextFileDialog.FileName;
                    if (TextFileRadioButton.Checked)
                        neoAcquisition.BuildSDBTextFile(textFileName);
                }
            }
            else
                neoAcquisition.BuildSDBClipboard();
            Utility.ButtonGreen(NEOButton);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

