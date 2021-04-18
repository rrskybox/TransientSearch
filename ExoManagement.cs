///ExoManagemet Class
///
/// Class for downloading Exo Planet database query results
/// 
/// This class serves as method template for conversions from all 
///   catalog sources
///

using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TransientSDB
{
    public class ExoManagement
    {
        // url of Exo and Exo-sandbox api                                     
        const string URL_Exo_search = "https://exoplanetarchive.ipac.caltech.edu/cgi-bin/nstedAPI/nph-nstedAPI?";

        const string ExoName = "https://exoplanetarchive.ipac.caltech.edu";
        const string ExoDescription = "Realtime Exo Planet Query Listing";

        private SDBDesigner sdbDesign;
        private XElement sdbXResults;
        private XDocument sdbXDoc;

        public int SearchBackDays { get; set; }
        public bool SearchSN { get; set; }
        public bool SearchClassified { get; set; }

        public void GetAndSet()
        {
            sdbDesign = new SDBDesigner();
            sdbDesign.SearchPrefix = "Exo";

            //Import Exo VOTable query and convert to an SDB XML database
            sdbXResults = ServerQueryToResultsXML();
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
            //string exoURLquery1 = URL_Exo_search + "table=exoplanets&select=pl_hostname,ra,dec,gaia_gmag&order=dec&format=xml";
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
            //Translates TNS Formatted data into TSX readable text header
            //  but still XML formatted
            //
            //Create a TSXSDB formatter to work with
            //  this object will include a standard set of control fields
            //  and empty sets of builtin and user data fields
            //Stick with the standard set of control fields
            // Except for identifier and sdbdescription
            sdbDesign.ControlFields.Single(cf => cf.ControlName == SDBDesigner.IdentifierX).ControlValue = ExoName;
            sdbDesign.ControlFields.Single(cf => cf.ControlName == SDBDesigner.SDBDescriptionX).ControlValue = ExoDescription;
            //Map the tns fields on to the tsx built-in and user-defined fields
            //  keeping track of the start of the column
            int fieldStart = 1;
            foreach (DataColumn sb in sdbDesign.HeaderMap)
            //for (int i = 0; i < sbdDesign .HeaderMap.Count; i++)
            //sbdDesign.DataFields.Single(f => f.SDBColumnName == SDBDesigner.LabelOrSearchX).TNSColumnName = "Name";
            {
                string fieldName = sb.SourceDataName;
                int fieldWidth = sb.ColumnWidth;
                switch (fieldName)
                {
                    case "pl_hostname":
                        sb.SourceDataName = "pl_hostname";
                        sb.TSXEntryName = SDBDesigner.LabelOrSearchX;
                        sb.IsBuiltIn = true;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "ra":
                        sb.SourceDataName = "ra";
                        sb.TSXEntryName = SDBDesigner.RAHoursX;
                        sb.IsBuiltIn = true;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "dec":
                        sb.SourceDataName = "dec";
                        sb.TSXEntryName = SDBDesigner.DecDegreesX;
                        sb.IsBuiltIn = true;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "gaia_gmag":
                        sb.SourceDataName = "gaia_gmag";
                        sb.TSXEntryName = SDBDesigner.MagnitudeX;
                        sb.IsBuiltIn = true;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "const":
                        break;
                    case "maxPass":
                        break;
                    case "minMag":
                        break;
                    case "MinPass":
                        break;
                    case "epoch":
                        break;
                    case "novaYr":
                        break;
                    case "period":
                        break;
                    case "riseDur":
                        break;
                    case "specType":
                        break;
                    case "disc":
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
        private string MakeSearchQuery()
        {
            //Returns a url string for querying the Exo website
            //table=exoplanets&select=pl_hostname,ra,dec,gaia_gmag&order=dec&format=xml
            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            queryString["table"] = "exoplanets";
            queryString["select"] = "pl_hostname,ra,dec,gaia_gmag";
            queryString["order"] = "dec";
            queryString["format"] = "xml";
            return queryString.ToString(); // Returns "key1=value1&key2=value2", all URL-encoded
        }
    }
}
