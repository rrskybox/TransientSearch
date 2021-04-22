///ExoManagemet Class
///
/// Class for downloading Exo Planet database query results
/// 
/// This class serves as method template for conversions from all 
///   catalog sources
///

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TransientSDB
{
    public class EXOManagement
    {
        // url of Exo and Exo-sandbox api                                     
        const string URL_Exo_search = "https://exoplanetarchive.ipac.caltech.edu/cgi-bin/nstedAPI/nph-nstedAPI?";

        private SDBDesigner sdbDesign;
        private XElement sdbXResults;
        private XDocument sdbXDoc;

        public string SDBIdentifier { get; set; } = "Exoplanetarchive.ipac.caltech";
        public string SDBDescription { get; set; } = "Realtime Exo Planet Query Listing";

        public int SearchBackDays { get; set; }
        public bool SearchSN { get; set; }
        public bool SearchClassified { get; set; }

        public void GetAndSet()
        {
            sdbDesign = new SDBDesigner();
            sdbDesign.SearchPrefix = "Exo";
            sdbDesign.DefaultObjectIndex = 20;
            sdbDesign.DefaultObjectDescription = "Exoplanet Object";

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
                    //Check for converstion of RA from decimal degrees to decimal hours
                    if (dHeader[dIndex].Contains("ra"))
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

                        DataColumn sbExtra = new DataColumn();
                        sbExtra.SourceDataName = "gaia_gmag";
                        sbExtra.TSXEntryName = "gaia_gmag";
                        sbExtra.IsBuiltIn = false;
                        sbExtra.ColumnStart = fieldStart;
                        sbExtra.ColumnWidth = fieldWidth;
                        sbExtra.IsPassed = true;
                        sbExtra.IsDuplicate = true;

                        sdbDesign.DataFields.Add(sbExtra);
                        fieldStart += fieldWidth;
                        break;
                    case "pl_orbper":
                        sb.SourceDataName = "pl_orbper";
                        sb.TSXEntryName = "pl_orbper";
                        sb.IsBuiltIn = false;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        break;
                    case "st_dist":
                        sb.SourceDataName = "st_dist";
                        sb.TSXEntryName = "st_dist";
                        sb.IsBuiltIn = false;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        break;
                    case "st_optmag":
                        sb.SourceDataName = "st_optmag";
                        sb.TSXEntryName = "st_optmag";
                        sb.IsBuiltIn = false;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
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
            queryString["select"] = "pl_hostname,ra,dec,gaia_gmag,pl_orbper,st_dist,st_optmag";
            queryString["order"] = "dec";
            queryString["format"] = "xml";
            return queryString.ToString(); // Returns "key1=value1&key2=value2", all URL-encoded
        }
    }
}
