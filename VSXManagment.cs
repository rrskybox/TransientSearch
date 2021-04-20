/*
* AAVSO Reader is a VSX client for assembling nova data
* 
* Author:           Rick McAlister
* Date:             03/21/2021
* Current Version:  0.1
* Developed in:     Visual Studio 2019
* Coded in:         C# 8.0
* App Envioronment: Windows 10 Pro x32 and x64 (DB12978)
* 
* Change Log:
* 
* 
* 
*/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TransientSDB

{
    class VSXManagement
    {
        const string VSXGETURL = "http://www.aavso.org/vsx/index.php?view=query.votable&";
        const string vsxIdentifier = "VSX";
        const string vsxDescription = "AAVSO VSX Database";

        public const string NOVA_VTYPE = "N%";
        public const string BLLAC_VTYPE = "BLLAC";
        public const string AGN_VTYPE = "AGN";
        public const string QSO_VTYPE = "QSO";

        public string SearchType { get; set; } = NOVA_VTYPE; //default

        private SDBDesigner sdbDesign;
        private XElement sdbXResults;
        private XDocument sdbXDoc;

        public void GetAndSet()
        {
            sdbDesign = new SDBDesigner();
            sdbDesign.SearchPrefix = "VSX";
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

        private XElement ServerQueryToResultsXML()
        {
            // url of vsx and vsx-sandbox api
            // 
            string startYear = (DateTime.Now.Year - 21).ToString("0");
            string endYear = (DateTime.Now.Year).ToString("0"); ;
            string url_vsx_search = VSXGETURL;
            string vsxResults = "";
            WebClient client = new WebClient();
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };

            string vsxURLquery1 = url_vsx_search + MakeSearchQuery(SearchType, startYear, endYear);

            try
            {
                vsxResults = client.DownloadString(vsxURLquery1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download Error: " + ex.Message);
                return null;
            };

            XElement xmlDoc1 = XElement.Parse(vsxResults);
            XElement resource = xmlDoc1.Element("RESOURCE");
            string sdbDescription = resource.Element("DESCRIPTION").Value.ToString();
            XElement table = resource.Element("TABLE");
            XElement xdataTable = table.Element("DATA");
            XElement xData = xdataTable.Element("TABLEDATA");

            XElement headerRecordX = new XElement("SDBDataFields");

            List<string> dHeader = new List<string>();
            IEnumerable<XElement> fields = table.Elements("FIELD");
            foreach (XElement xf in fields)
            {
                string fid = xf.Attribute("id").Value.ToString();
                string fname = xf.Attribute("name").Value.ToString();
                dHeader.Add(fid);
            }
            int[] widths = new int[dHeader.Count];
            IEnumerable<XElement> dRecords = xData.Elements("TR");
            XElement sdbX = new XElement(XMLParser.SDBListX);
            foreach (XElement rec in dRecords)
            {
                IEnumerable<XElement> dResults = rec.Elements("TD");
                XElement xmlItem = new XElement(XMLParser.SDBEntryX);
                int dIndex = 0;
                foreach (XElement dItem in dResults)
                {
                    string dItemValue = dItem.Value.ToString();
                    if (dHeader[dIndex] == "radec2000")
                    {
                        double dItemRAdegrees = Convert.ToDouble(dItemValue.Split(',')[0]);
                        double dItemDecdegrees = Convert.ToDouble(dItemValue.Split(',')[1]);
                        //Convert RA Degrees to RA Hours as VSX delivers it in degrees and TSX wants it in hours
                        double dItemRAHours = dItemRAdegrees * 24.0 / 360.0;
                        string dItemRA = dItemRAHours.ToString();
                        string dItemDec = dItemDecdegrees.ToString();
                        xmlItem.Add(new XElement("RA2000", dItemRA));
                        xmlItem.Add(new XElement("Dec2000", dItemDecdegrees));
                        if (dItemRA.Length > widths[dIndex])
                            widths[dIndex] = dItemRA.Length;
                        if (dItemDec.Length > widths[dIndex])
                            widths[dIndex] = dItemDec.Length;
                    }
                    else
                    {
                        xmlItem.Add(new XElement(dHeader[dIndex], dItemValue));
                        if (dItemValue.Length > widths[dIndex])
                            widths[dIndex] = dItemValue.Length;
                    }
                    dIndex++;
                }
                sdbX.Add(xmlItem);
            }
            //pull out the field information
            for (int i = 0; i < widths.Length; i++)
            {
                if (dHeader[i] == "radec2000")
                {
                    headerRecordX.Add(new XElement("RA2000", widths[i]));
                    headerRecordX.Add(new XElement("Dec2000", widths[i]));
                }
                else
                {
                    headerRecordX.Add(new XElement(dHeader[i], widths[i]));
                }
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
            sdbDesign.ControlFields.Single(cf => cf.ControlName == SDBDesigner.IdentifierX).ControlValue = vsxIdentifier;
            sdbDesign.ControlFields.Single(cf => cf.ControlName == SDBDesigner.SDBDescriptionX).ControlValue = vsxDescription;
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
                    case "auid":
                        sb.SourceDataName = "auid";
                        sb.TSXEntryName = "ID";
                        sb.IsBuiltIn = false;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "name":
                        sb.SourceDataName = "name";
                        sb.TSXEntryName = SDBDesigner.LabelOrSearchX;
                        sb.IsBuiltIn = true;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "const":
                        sb.SourceDataName = "const";
                        sb.TSXEntryName = "Constellation";
                        sb.IsBuiltIn = false;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "RA2000":
                        sb.SourceDataName = "RA2000";
                        sb.TSXEntryName = SDBDesigner.RAHoursX;
                        sb.IsBuiltIn = true;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "Dec2000":
                        sb.SourceDataName = "Dec2000";
                        sb.TSXEntryName = SDBDesigner.DecDegreesX;
                        sb.IsBuiltIn = true;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "varType":
                        sb.SourceDataName = "varType";
                        sb.TSXEntryName = "Variable Type";
                        sb.IsBuiltIn = false;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "maxMag":
                        sb.SourceDataName = "maxMag";
                        sb.TSXEntryName = SDBDesigner.MagnitudeX;
                        sb.IsBuiltIn = true;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);

                        DataColumn sbExtra = new DataColumn();
                        sbExtra.SourceDataName = "maxMag";
                        sbExtra.TSXEntryName = "Magnitude";
                        sbExtra.IsBuiltIn = false;
                        sbExtra.ColumnStart = fieldStart;
                        sbExtra.ColumnWidth = fieldWidth;
                        sbExtra.IsPassed = true;
                        sbExtra.IsDuplicate = true;
                        sdbDesign.DataFields.Add(sbExtra);
                        fieldStart += fieldWidth;

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
                        sb.SourceDataName = "novaYr";
                        sb.TSXEntryName = "Discovery_Date_UT";
                        sb.IsBuiltIn = false;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "period":
                        sb.SourceDataName = "period";
                        sb.TSXEntryName = "Period";
                        sb.IsBuiltIn = false;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
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

        private string MakeSearchQuery(string vType, string startYear, string endYear)
        {
            //Returns a url string for querying the vsx website

            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            //queryString[ "coords"]= "";
            //queryString[  "ident"] = "";
            //queryString[ "constid"] = "";
            queryString["format"] = "d";
            //queryString[  "geom"] = "";
            //queryString[  "size"] = "";
            //queryString[  "unit"] = "";
            queryString["vtype"] = vType;
            //queryString[  "stype"] = "";
            //queryString[  "maxhi"] = "";
            //queryString[ "maxlo"] = "";
            //queryString[  "perhi"] = "";
            //queryString[  "perlo"] = "";
            //queryString[  "ephi"] = "";
            //queryString[ "eplo"] = "";
            //queryString[  "riselo"] = "";
            //queryString[  "risehi"] = "";
            //queryString["yrlo"] = startYear;
            // queryString["yrhi"] = endYear;
            //queryString[  "filter"] = "";
            //queryString[ "order"] = "";

            return queryString.ToString(); // Returns "key1=value1&key2=value2", all URL-encoded
        }

        public static string FitFormat(string entry, int slotSize)
        {
            //Returns a string which is the entry truncated to the slot Size, if necessary
            if (entry == null) return "                    ".Substring(0, slotSize);
            if (entry.Length > slotSize)
                return entry.Substring(0, slotSize - 1).PadRight(slotSize);
            else
                return entry.PadRight(slotSize);
        }


    }

}

