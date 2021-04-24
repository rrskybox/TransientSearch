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
            Utility.ButtonGreen(VZRButton);
            Utility.ButtonGreen(CloseButton);
            OutputGroupBox.ForeColor = Color.Black;
        }

        private void TNSReaderButton_Click(object sender, EventArgs e)
        {
            //Import TNS-specific catalog data
            //sbXTNS is the TNS-specific xml db that has caught all the TNS header and data
            Utility.ButtonRed(TNSReaderButton);
            SearchTNS tnsAcquisition = new SearchTNS();
            tnsAcquisition.SearchBackDays = (int)SearchDaysBox.Value;
            tnsAcquisition.SearchClassified = ATSelectButton.Checked;
            tnsAcquisition.SearchSN = SuperNovaSelectButton.Checked;

            tnsAcquisition.GetAndSet();
            if (TextFileRadioButton.Checked)
            {
                SDBTextFileDialog.FileName = tnsAcquisition.SDBIdentifier + " Database.txt";
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
            if (NovaSelectButton.Checked)
            {
                vsxAcquisition.SearchType = VSXManagement.NOVA_VTYPE;
                vsxAcquisition.SDBSuspect = "0,1,2";
            }
            if (AGNSelectButton.Checked)
            {
                vsxAcquisition.SearchType = VSXManagement.AGN_VTYPE;
                vsxAcquisition.SDBSuspect = "0,1,2";
            }
            if (QSOSelectButton.Checked)
            {
                vsxAcquisition.SearchType = VSXManagement.QSO_VTYPE;
                vsxAcquisition.SDBSuspect = "0,1,2";
            }
            if (BLLACSelectButton.Checked)
            {
                vsxAcquisition.SearchType = VSXManagement.BLLAC_VTYPE;
                vsxAcquisition.SDBSuspect = "0,1,2";
            }
            if (SuspectsSelectButton.Checked)
            {
                vsxAcquisition.SearchType = VSXManagement.SUSPECTS_VTYPE;
                vsxAcquisition.SDBSuspect = "1";
            }

            vsxAcquisition.GetAndSet();
            if (TextFileRadioButton.Checked)
            {
                SDBTextFileDialog.FileName = vsxAcquisition.SDBIdentifier + " Database.txt";
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
            SearchEXO exoAcquisition = new SearchEXO();
            exoAcquisition.SearchType = "EXO";

            exoAcquisition.GetAndSet();
            if (TextFileRadioButton.Checked)
            {
                SDBTextFileDialog.FileName = exoAcquisition.SDBIdentifier + " Database.txt";
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
            SearchNEO neoAcquisition = new SearchNEO();
            neoAcquisition.SearchType = "NEO";

            neoAcquisition.GetAndSet();
            if (TextFileRadioButton.Checked)
            {
                SDBTextFileDialog.FileName = neoAcquisition.SDBIdentifier + " Database.txt";
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

        private void VZRButton_Click(object sender, EventArgs e)
        {
            //Import ESA NEO-specific catalog data
            //sbXTNS is the TNS-specific xml db that has caught all the TNS header and data
            Utility.ButtonRed(VZRButton);
            SearchVZR vzrAcquisition = new SearchVZR();
            if (WhiteDwarfSelectButton.Checked)
            {
                vzrAcquisition.SearchType = SearchVZR.WhiteDwarfCatalog;
            }
            if (RedDwarfSelectButton.Checked)
            {
                vzrAcquisition.SearchType = SearchVZR.RedDwarfCatalog;
            }

            vzrAcquisition.GetAndSet();
            if (TextFileRadioButton.Checked)
            {
                SDBTextFileDialog.FileName = vzrAcquisition.SDBIdentifier + " Database.txt";
                DialogResult odr = SDBTextFileDialog.ShowDialog();
                if (odr == DialogResult.OK)
                {
                    string textFileName = SDBTextFileDialog.FileName;
                    if (TextFileRadioButton.Checked)
                        vzrAcquisition.BuildSDBTextFile(textFileName);
                }
            }
            else
                vzrAcquisition.BuildSDBClipboard();
            Utility.ButtonGreen(VZRButton);

        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}

