/*
* SearchTargetTool Class
*
* Class for downloading and parsing AAVSO Target Tool database query results
* 
* This class serves as method template for conversions from all 
*  catalog sources
* 
* Author:           Rick McAlister
* Date:             9/15/23
* Current Version:  1.0
* Developed in:     Visual Studio 2022
* Coded in:         C# 8.0
* App Envioronment: Windows 10 Pro, .Net 4.8, TSX 5.0 Build 12978+
* 
* Change Log:
* 
* 
*/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using System.Xml.Linq;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace TransientSDB

{
    class TargetToolManagement
    {
        public const string TARGET_TOOL_DATABASE_DESCRIPTION = "AAVSO Target Tool" + "\r\n;\r\n" +
            "We acknowledge with thanks the variable star observations from the " +
            "AAVSO International Database contributed by observers worldwide and" +
            " used in this research." + "\r\n;\r\n" +
            "https://www.aavso.org/vsx/";


        public string SDBIdentifier { get; set; } = "AAVSO Target Tool";
        public string SDBDescription { get; set; } = TARGET_TOOL_DATABASE_DESCRIPTION;

        private SDBDesigner sdbDesign;
        private XElement sdbXResults;
        private XDocument sdbXDoc;

        public bool GetAndSet(string ttFilePath)
        {
            sdbDesign = new SDBDesigner();  //Variable Star
            sdbDesign.DefaultObjectIndex = 1;
            sdbDesign.DefaultObjectDescription = "AAVSO Target Tool Variable";
            sdbDesign.DefaultMaximumFOV = "100.0000";
            sdbDesign.SearchPrefix = "AAVSO-TT";

            //Import TNS CSV text query and convert to an XML database
            sdbXResults = TargetToolFileToResultsXML(ttFilePath);
            if (sdbXResults == null)
                return false;
            //Parse the TNS-specific catalog data for names and widths to be used
            //  for defining columns in the output text data to TSX SDB text file
            //colMap is the generic list of column names and maximum data widths
            sdbDesign.MakeHeaderMap(sdbXResults);
            //Create the TSX SDB Text header by mapping the column map to the 
            //  TSX built-in and user data fields
            sdbXDoc = ResultsXMLtoSDBHeader(sdbXResults);
            return true;
        }

        public static XElement TargetToolFileToResultsXML(string textFilePath)
        {
            //Generages the XML target file from the filePath .txt file
            //The assumed format for the AGN text file is |<name>|<ra>|<dec>|<magnitude>
            char[] remChar = { 'h', 'm', 's', 'd' };

            XElement sdbX = new XElement(XMLParser.SDBListX);

            string textLine;
            string[] lineElements;
            TextReader tgtTextFile;
            try { tgtTextFile = File.OpenText(textFilePath); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return sdbX;
            }

            textLine = tgtTextFile.ReadLine();  //Header line 
            textLine = textLine.Replace(" ", "");
            textLine = textLine.Replace("\"", "");
            textLine = textLine.Replace("(", "");
            textLine = textLine.Replace(")", "");
            textLine = textLine.Replace("/", "");

            string[] hList = textLine.Split(',');
            int[] widths = new int[hList.Length];
            XElement headerRecordX = new XElement("SDBDataFields");

            while (tgtTextFile.Peek() != -1) //skip non entry lines
            {
                //next entry
                textLine = tgtTextFile.ReadLine();
                lineElements = textLine.Split(',');
                //Calculate RA and Dec -> decimal values
                if (lineElements.Length >= 3)
                {
                    string nameString = Utility.ParseNameString(lineElements[0].Replace(":", " "));
                    double? raDouble = Utility.ParseRADecString(lineElements[1]);
                    double? decDouble = Utility.ParseRADecString(lineElements[2]);
                    //Load up xelement
                    if (raDouble != null && decDouble != null)
                    {
                        XElement varTarget = new XElement(XMLParser.SDBEntryX);
                        varTarget.Add(new XElement(hList[0], nameString));
                        if (widths[0] < nameString.Length)
                            widths[0] = nameString.Length;
                        varTarget.Add(new XElement(hList[1], raDouble.ToString()));
                        if (widths[1] < raDouble.ToString().Length)
                            widths[1] = raDouble.ToString().Length;
                        varTarget.Add(new XElement(hList[2], decDouble.ToString()));
                        if (widths[2] < decDouble.ToString().Length)
                            widths[2] = decDouble.ToString().Length;
                        for (int i = 3; i < hList.Length; i++)
                        {
                            string entry = lineElements[i];
                            varTarget.Add(new XElement(hList[i], entry));
                            if (entry.Length > widths[i])
                                widths[i] = entry.Length;
                        }
                        sdbX.Add(varTarget);
                    }
                }
            }
            //pull out the field information
            for (int i = 0; i < widths.Length; i++)
                headerRecordX.Add(new XElement(hList[i], widths[i]));
            sdbX.Add(headerRecordX);
            return sdbX;
        }

        private XDocument ResultsXMLtoSDBHeader(XElement xmlDB)
        {
            //Translates TNS Formatted data into TSX readable text header
            //  but still XML formatted
            //
            //Create a TSXSDB formatter to work with
            //  this object will include a standard set of control fields
            //  and empty sets of builtin and user data fields
            //Stick with the standard set of control fields
            // Except for identifier and sdbdescription
            sdbDesign.ControlFields.Single(cf => cf.ControlName == SDBDesigner.IdentifierX).ControlValue = SDBIdentifier;
            sdbDesign.ControlFields.Single(cf => cf.ControlName == SDBDesigner.SDBDescriptionX).ControlValue = SDBDescription;
            //Map the tns fields on to the tsx built-in and user-defined fields
            //  keeping track of the start of the column
            int fieldStart = 1;
            foreach (DataColumn sb in sdbDesign.HeaderMap)
            //for (int i = 0; i < sbdDesign .HeaderMap.Count; i++)
            //sbdDesign.DataFields.Single(f => f.SDBColumnName == SDBDesigner.LabelOrSearchX).TNSColumnName = "Name";
            {
                string fieldName = sb.SourceDataName;
                int fieldWidth = sb.ColumnWidth;
                sb.TSXEntryName = fieldName;
                sb.ColumnStart = fieldStart;
                sb.ColumnWidth = fieldWidth;

                switch (fieldName)
                {
                    case "StarName":
                        sb.TSXEntryName = SDBDesigner.LabelOrSearchX;
                        sb.IsBuiltIn = true;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "RAJ2000.0":
                        sb.TSXEntryName = SDBDesigner.RAHoursX;
                        sb.IsBuiltIn = true;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "DecJ2000.0":
                        sb.TSXEntryName = SDBDesigner.DecDegreesX;
                        sb.IsBuiltIn = true;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                        
                    //case "maxMag":
                    // Do not do magnitude -- string has filter component letter
                    //    sb.TSXEntryName = SDBDesigner.MagnitudeX;
                    //    sb.IsBuiltIn = true;
                    //    sb.IsPassed = true;
                    //    sdbDesign.DataFields.Add(sb);
                    //    break;

                    case "Notes":
                        sb.TSXEntryName = fieldName;
                        sb.IsBuiltIn = false;
                        sb.IsPassed = false;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    default:
                        sb.TSXEntryName = fieldName;
                        sb.IsBuiltIn = false;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                }
            }
            //Generate the header xml
            return sdbDesign.HeaderGenerator();
        }

        public void BuildSDBTextFile(string fileName)
        {
            XMLParser.XMLToSDBText(fileName, sdbDesign, sdbXDoc, sdbXResults);
            return;
        }

        public void BuildSDBClipboard()
        {
            XMLParser.XMLToSDBClipboard(sdbDesign, sdbXDoc, sdbXResults);
            return;
        }

    }

}

