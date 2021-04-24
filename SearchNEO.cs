/*
* SearchNEO Class
*
* Class for downloading and parsing MPC NEOCP database query results
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
* Change Log:
* 
* 4/23/21 Rev 1.0  Release
* 
*/

using System;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TransientSDB
{
    class SearchNEO
    {
        const string URL_NEO_search = "https://minorplanetcenter.net/iau/NEO/neocp.txt";

        private SDBDesigner sdbDesign;
        private XElement sdbXResults;
        private XDocument sdbXDoc;

        public string SDBIdentifier { get; set; }
        public string SDBDescription { get; set; }
        public string SearchType { get; set; }

        public void GetAndSet()
        {
            sdbDesign = new SDBDesigner();
            sdbDesign.SearchPrefix = "NEO";
            sdbDesign.DefaultObjectIndex = 37;
            sdbDesign.DefaultObjectDescription = "Near Earth Object";
            SDBIdentifier = "MPC NEOCP";
            SDBDescription = "Minor Planet Center Confirmed NEO";

            //Import TNS CSV text query and convert to an XML database
            sdbXResults = ServerQueryToResultsXML();
            //Parse the TNS-specific catalog data for names and widths to be used
            //  for defining columns in the output text data to TSX SDB text file
            //colMap is the generic list of column names and maximum data widths
            sdbDesign.MakeHeaderMap(sdbXResults);
            //Create the TSX SDB Text header by mapping the column map to the 
            //  TSX built-in and user data fields
            sdbXDoc = ResultsXMLtoSDBHeader(sdbXResults);
            return;
        }

        /// <summary>
        /// Method to import TNS server database and convert to internal XML db
        /// </summary>
        /// <returns></returns>
        private XElement ServerQueryToResultsXML()
        {
            Tuple<int, int> nNameCol = new Tuple<int, int>(0, 8);
            Tuple<int, int> nScoreCol = new Tuple<int, int>(9, 12);
            Tuple<int, int> nDiscoveryCol = new Tuple<int, int>(13, 25);
            Tuple<int, int> nRACol = new Tuple<int, int>(26, 34);
            Tuple<int, int> nDecCol = new Tuple<int, int>(35, 42);
            Tuple<int, int> nVCol = new Tuple<int, int>(43, 48);
            Tuple<int, int> nUpdatedCol = new Tuple<int, int>(49, 70);
            Tuple<int, int> nObservationsCol = new Tuple<int, int>(80, 85);
            Tuple<int, int> nHCol = new Tuple<int, int>(86, 90);
            Tuple<int, int> nArcCol = new Tuple<int, int>(91, 95);
            Tuple<int, int> nNotSeenCol = new Tuple<int, int>(96, 101);

            string neoResultText;
            WebClient client = new WebClient();
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            try
            {
                // string urlSearch = url_NEO_search + MakeSearchQuery();
                string urlSearch = URL_NEO_search;
                neoResultText = client.DownloadString(urlSearch);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download Error: " + ex.Message);
                return null;
            };

            string[] headers = new string[] { "Name", "RA", "DEC", "Magnitude", "NotSeen" };

            string[] entries = new string[headers.Length];
            int[] widths = new int[headers.Length];

            //create an xml working database
            XElement xml = new XElement(XMLParser.SDBListX);
            //Load DB based on columns of text
            string[] neoResultLines = neoResultText.Split('\n');
            for (int line = 0; line < neoResultLines.Count(); line++)
            {
                if (neoResultLines[line].Length < 100) break;
                entries[0] = neoResultLines[line].Substring(nNameCol.Item1, QuickWidth(nNameCol));
                entries[1] = neoResultLines[line].Substring(nRACol.Item1, QuickWidth(nRACol));
                entries[2] = neoResultLines[line].Substring(nDecCol.Item1, QuickWidth(nDecCol));
                entries[3] = neoResultLines[line].Substring(nVCol.Item1, QuickWidth(nVCol));
                entries[4] = neoResultLines[line].Substring(nNotSeenCol.Item1, QuickWidth(nNotSeenCol));

                XElement xmlItem = new XElement(XMLParser.SDBEntryX);
                for (int i = 0; i < headers.Length; i++)
                {
                    xmlItem.Add(new XElement(headers[i], entries[i]));
                    if (entries[i].Length > widths[i]) widths[i] = entries[i].Length;
                }
                xml.Add(xmlItem);
            }
            XElement headerRecordX = new XElement("SDBDataFields");
            for (int i = 0; i < widths.Length; i++)
            {
                XElement colRecordX = new XElement(headers[i], widths[i]);
                headerRecordX.Add(colRecordX);
            }
            xml.Add(headerRecordX);
            return xml;
        }

        private XDocument ResultsXMLtoSDBHeader(XElement xmlDB)
        {
            sdbDesign.ControlFields.Single(cf => cf.ControlName == SDBDesigner.IdentifierX).ControlValue = SDBIdentifier;
            sdbDesign.ControlFields.Single(cf => cf.ControlName == SDBDesigner.SDBDescriptionX).ControlValue = SDBDescription;
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
                    case "Name":
                        sb.TSXEntryName = SDBDesigner.LabelOrSearchX;
                        sb.IsBuiltIn = true;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "RA":
                        sb.TSXEntryName = SDBDesigner.RAHoursX;
                        sb.IsBuiltIn = true;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "DEC":
                        sb.TSXEntryName = SDBDesigner.DecDegreesX;
                        sb.IsBuiltIn = true;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "Magnitude":
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
                    case "NotSeen":
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
            XMLParser.XMLToSDBText(fileName, sdbDesign, sdbXDoc, sdbXResults);
            return;
        }

        public void BuildSDBClipboard()
        {
            XMLParser.XMLToSDBClipboard(sdbDesign, sdbXDoc, sdbXResults);
            return;
        }

        private int QuickWidth(Tuple<int, int> t)
        {
            return t.Item2 - t.Item1;
        }

    }
}

