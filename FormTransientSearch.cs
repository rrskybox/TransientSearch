﻿/*
* Form TransientSearch is the control form for Transient Search
* 
* Author:           Rick McAlister
* Date:             4/23/21
* Current Version:  1.0
* Developed in:     Visual Studio 2019
* Coded in:         C# 8.0
* App Envioronment: Windows 10 Pro, .Net 4.8, TSX 5.0 Build 12978
* 
* Change Log:
* 
* 4/23/21 Rev 1.0  Release
* 
*/
//This research has made use of data and/or services provided by the International Astronomical Union's Minor Planet Center.

using System;
using System.Deployment.Application;
using System.Drawing;
using System.Windows.Forms;

namespace TransientSDB
{
    public partial class FormTransientSearch : Form
    {
        public FormTransientSearch()
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
            //turn the buttons green
            Utility.ButtonGreen(TNSReaderButton);
            Utility.ButtonGreen(AAVSOVSXButton);
            Utility.ButtonGreen(ExoButton);
            Utility.ButtonGreen(NEOButton);
            Utility.ButtonGreen(CloseButton);
            OutputGroupBox.ForeColor = Color.Black;
        }

        private void TNSReaderButton_Click(object sender, EventArgs e)
        {
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

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}

