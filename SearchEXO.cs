/*
* SearchEXO Class
*
* Class for downloading and parsing Exo Planet database query results
* 
* This class serves as method template for conversions from all 
*  catalog sources
* 
* Author:           Rick McAlister
* Date:             4/23/21
* Current Version:  1.0
* Developed in:     Visual Studio 2019
* Coded in:         C# 8.0
* App Envioronment: Windows 10 Pro, .Net 4.8, TSX 5.0 Build 12978
* 
* Support documents
* 
* Table Access Protocol: https://exoplanetarchive.ipac.caltech.edu/docs/TAP/usingTAP.html
* 
* Change Log:
* 
* 4/23/21 Rev 1.0  Release
* 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TransientSDB
{
    public class SearchEXO
    {
        const string URL_Exo_search = "https://exoplanetarchive.ipac.caltech.edu/TAP/sync?";

        const string TAPSolutionType = "soltype";
        const string TAPPublishedConfirmed = "\'Published Confirmed\'";
        const string TAPName = "pl_name";
        const string TAPRA = "ra";
        const string TAPDec = "dec";
        const string TAPVMag = "sy_vmag";
        const string TAPPeriod = "pl_orbper";
        const string TAPDistance = "sy_dist";
        const string TAPSpecType = "st_spectype";
        const string TAPTransitFlag = "tran_flag";
        const string TAPMaxPeriod = "1000";
        const string TAPMinMag = "14";
        const string TAPTrue = "1";

        const string TAPTransitDuration = "pl_trandur";
        const string TAPTransitDepth = "pl_trandep";

        const string TAPTransitMid = "pl_tranmid";

        public enum PSSearchType
        {
            Confirmed,
            Candidate
        }

        private SDBDesigner sdbDesign;
        private XElement sdbXResults;
        private XDocument sdbXDoc;

        public string SDBIdentifier { get; set; }
        public string SDBDescription { get; set; }

        public PSSearchType SearchType { get; set; }

        public void GetAndSet()
        {
            sdbDesign = new SDBDesigner();
            sdbDesign.SearchPrefix = "Exo";
            switch (SearchType)
            {
                case PSSearchType.Confirmed:
                    { SDBIdentifier = "Confirmed EXO"; break; }
                case PSSearchType.Candidate:
                    { SDBIdentifier = "Candidate EXO"; break; }
            }
            SDBDescription = "Exo Planet Query Listing, IPAC, CalTech";
            sdbDesign.DefaultObjectIndex = 20;
            sdbDesign.DefaultObjectDescription = "Exoplanet Object";

            //Import Exo VOTable query and convert to an SDB XML database
            sdbXResults = ServerQueryToResultsXML();
            if (sdbXResults == null) return;
            //Parse the Exo-specific catalog data for names and widths to be used
            //  for defining columns in the output text data to TSX SDB text file
            //colMap is the generic list of column names and maximum data widths
            sdbDesign.MakeHeaderMap(sdbXResults);
            //Create the TSX SDB Text header by mapping the column map to the 
            //  TSX built-in and user data fields
            sdbXDoc = ResultsXMLtoSDBHeader(sdbXResults);
            return;
        }

        private XElement ServerQueryToResultsXML()
        {
            // url of exo APR
            // 
            string exoResults = "";
            WebClient client = new WebClient();

            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };

            string exoURLquery = URL_Exo_search + MakeSearchQuery();
            int t = exoURLquery.CompareTo(exoURLquery);

            try
            {
                exoResults = client.DownloadString(exoURLquery);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download Error: " + ex.Message);
                return null;
            };
            //Standard VOTable parse
            XElement exoDoc = XElement.Parse(exoResults);
            XNamespace exoNS = XNamespace.Get(exoDoc.Attribute("xmlns").Value);
            XElement resource = exoDoc.Element(exoNS + "RESOURCE");
            XElement table = resource.Element(exoNS + "TABLE");
            XElement xdataTable = table.Element(exoNS + "DATA");
            XElement xData = xdataTable.Element(exoNS + "TABLEDATA");

            XElement headerRecordX = new XElement("SDBDataFields");

            //EXO parse
            List<string> dHeader = new List<string>();
            IEnumerable<XElement> fields = table.Elements(exoNS + "FIELD");
            foreach (XElement xf in fields)
            {
                string fname = xf.Attribute("name").Value.ToString();
                dHeader.Add(fname);
            }
            int[] widths = new int[dHeader.Count];
            IEnumerable<XElement> dRecords = xData.Elements(exoNS + "TR");
            XElement sdbX = new XElement(XMLParser.SDBListX);
            foreach (XElement rec in dRecords)
            {
                IEnumerable<XElement> dResults = rec.Elements(exoNS + "TD");
                XElement xmlItem = new XElement(XMLParser.SDBEntryX);
                int dIndex = 0;
                foreach (XElement dItem in dResults)
                {
                    string dItemValue = dItem.Value.ToString();
                    //Check for converstion of RA from decimal degrees to decimal hours
                    if (dHeader[dIndex] == ("ra"))
                        dItemValue = ((Convert.ToDouble(dItem.Value.ToString())) * (24.0 / 360.0)).ToString();
                    xmlItem.Add(new XElement(dHeader[dIndex], dItemValue));
                    if (dItemValue.Length > widths[dIndex])
                        widths[dIndex] = dItemValue.Length;
                    dIndex++;
                }
                sdbX.Add(xmlItem);
            }
            //pull out the field information
            for (int i = 0; i < widths.Length; i++)
            {
                headerRecordX.Add(new XElement(dHeader[i], widths[i]));
            }
            sdbX.Add(headerRecordX);
            return sdbX;
        }

        private XDocument ResultsXMLtoSDBHeader(XElement xmlDB)
        {
            //Translates EXO Formatted data into TSX readable text header
            //  but still XML formatted
            //
            //Create a TSXSDB formatter to work with
            //  this object will include a standard set of control fields
            //  and empty sets of builtin and user data fields
            //Stick with the standard set of control fields
            // Except for identifier and sdbdescription
            sdbDesign.ControlFields.Single(cf => cf.ControlName == SDBDesigner.IdentifierX).ControlValue = SDBIdentifier;
            sdbDesign.ControlFields.Single(cf => cf.ControlName == SDBDesigner.SDBDescriptionX).ControlValue = SDBDescription;
            //Map the exo fields on to the tsx built-in and user-defined fields
            //  keeping track of the start of the column
            int fieldStart = 1;
            foreach (DataColumn sb in sdbDesign.HeaderMap)
            {
                string fieldName = sb.SourceDataName;
                int fieldWidth = sb.ColumnWidth;
                sb.TSXEntryName = fieldName;
                sb.ColumnStart = fieldStart;
                sb.ColumnWidth = fieldWidth;

                switch (fieldName)
                {
                    case TAPName:
                        sb.TSXEntryName = SDBDesigner.LabelOrSearchX;
                        sb.IsBuiltIn = true;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case TAPRA:
                        sb.TSXEntryName = SDBDesigner.RAHoursX;
                        sb.IsBuiltIn = true;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case TAPDec:
                        sb.TSXEntryName = SDBDesigner.DecDegreesX;
                        sb.IsBuiltIn = true;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case TAPVMag:
                        sb.TSXEntryName = SDBDesigner.MagnitudeX;
                        sb.IsBuiltIn = true;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);

                        DataColumn sbExtra = new DataColumn();
                        sbExtra.SourceDataName = fieldName;
                        sbExtra.TSXEntryName = fieldName;
                        sbExtra.IsBuiltIn = false;
                        sbExtra.ColumnStart = fieldStart;
                        sbExtra.ColumnWidth = fieldWidth;
                        sbExtra.IsPassed = true;
                        sbExtra.IsDuplicate = true;
                        sdbDesign.DataFields.Add(sbExtra);

                        fieldStart += fieldWidth;
                        break;
                    case TAPSolutionType:
                        sb.IsBuiltIn = false;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case TAPPeriod:
                        sb.IsBuiltIn = false;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case TAPDistance:
                        sb.IsBuiltIn = false;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case TAPSpecType:
                        sb.IsBuiltIn = false;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case TAPTransitDuration:
                        sb.IsBuiltIn = false;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case TAPTransitDepth:
                        sb.IsBuiltIn = false;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case TAPTransitMid:
                        sb.IsBuiltIn = false;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    default:
                        sb.IsPassed = false;
                        break;
                }
            }
            //Generate the header xml
            return sdbDesign.HeaderGenerator();
        }

        public void BuildSDBTextFile(string fileName)
        {
            if (sdbXResults != null)
                XMLParser.XMLToSDBText(fileName, sdbDesign, sdbXDoc, sdbXResults);
            return;
        }

        public void BuildSDBClipboard()
        {
            if (sdbXResults != null)
                XMLParser.XMLToSDBClipboard(sdbDesign, sdbXDoc, sdbXResults);
            return;
        }
        private string MakeSearchQuery()
        {
            //Returns a url string for querying the Exo website
            string exoType = "";
            switch (SearchType)
            {
                case PSSearchType.Confirmed:
                    { exoType = ""; break; }
                case PSSearchType.Candidate:
                    { exoType = "+not"; break; }
            }
            string queryString =
                "query=" +
                "select+" + TAPName + "," +
                            TAPSolutionType + "," +
                            TAPRA + "," +
                            TAPDec + "," +
                            TAPVMag + "," +
                            TAPPeriod + "," +
                            TAPDistance + "," +
                            TAPSpecType + "," +
                            TAPTransitDuration + "," +
                            TAPTransitDepth + "," +
                            TAPTransitMid +
                            "+from+ps+" +
                "where+" + "upper(" + TAPSolutionType + ")" + exoType + "+like+\'%CONF%\'" + "+and+" +
                           TAPPeriod + "+<+" + TAPMaxPeriod + "+and+" +
                           TAPVMag + "+<+" + TAPMinMag + "+and+" +
                           TAPTransitFlag + "+=+" + TAPTrue + "+" +
                "&format=votable";
            return queryString;
        }
    }
}
