/*
* TennisNet is a Transient Name Server client for assembling supernova data
* 
* Author:           Rick McAlister
* Date:             12/21/18
* Current Version:  0.1
* Developed in:     Visual Studio 2017
* Coded in:         C# 7.0
* App Envioronment: Windows 10 Pro (V1809)
* 
* Change Log:
* 
* 12/22/18 Rev 1.0  Release
* 03/22/21 Rev 1.1  Release
* 
*/

using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TransientSDB

{

    public partial class XMLParser
    {

        public const string SDBListX = "SDBDataList";
        public const string SDBEntryX = "SDBEntry";

        public static void XMLToSDBText(string exportFileName, SDBDesigner sdbDesign, XDocument sdbXhdr, XElement sdbXML)
        {
            //Open text file
            //Add the header
            //Build and add text lines according to the header
            StreamWriter tFile = File.CreateText(exportFileName);
            //Write the XDocument to the text file
            sdbXhdr.Save(tFile);
            tFile.Close();
            File.AppendAllText(exportFileName, "\n");
            //Write the sdb text according to the column map
            foreach (XElement dRec in sdbXML.Elements(SDBEntryX))
            {
                string line = XMLtoTextLine(dRec, sdbDesign.DataFields);
                File.AppendAllText(exportFileName, line);
            }
            return;
        }

        public static void XMLToSDBClipboard(SDBDesigner sdbDesign, XDocument sdbXhdr, XElement sdbXML)
        {
            //Open text file
            //Add the header
            //Build and add text lines according to the header
            //Write the XDocument to the text file
            string test = sdbXhdr.ToString();
            Clipboard.SetText(test);
            test += "\n";
            //Write the sdb text according to the column map
            foreach (XElement dRec in sdbXML.Elements(SDBEntryX))
            {
                string line = XMLtoTextLine(dRec, sdbDesign.DataFields);
                test += line;
            }
            Clipboard.SetText(test);
            return;
        }

        private static string XMLtoTextLine(XElement recX, List<DataColumn> fieldSet)
        {
            string outString = "";
            foreach (DataColumn fc in fieldSet)
            {
                if (fc.IsPassed)
                {
                    string recString = recX.Element(fc.SourceDataName).Value.ToString();
                    outString += recString.PadRight(fc.ColumnWidth);
                }
            }
            outString += "\n";
            return outString;
        }
    }
}


