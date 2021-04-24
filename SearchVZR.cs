using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;


namespace TransientSDB
{
    class SearchVZR
    {

        const string VZRQueryURL = "http://vizier.u-strasbg.fr/viz-bin/votable?";
        //const string WhiteDwarfCatalog = "J/AJ/154/32/table2";
        public const string WhiteDwarfCatalog = "J/AJ/145/136/stars";
        public const string RedDwarfCatalog = "J/A+A/441/653/stars";

        public string SDBIdentifier { get; set; } = "VIZIER WD";
        public string SDBDescription { get; set; } = "Vizier White Dwarf Query";
        public string SearchType { get; set; }
        public string VZRCatalog { get; set; }

        private SDBDesigner sdbDesign;
        private XElement sdbXResults;
        private XDocument sdbXDoc;

        public void GetAndSet()
        {
            sdbDesign = new SDBDesigner();
            switch (SearchType)
            {
                case WhiteDwarfCatalog:
                    sdbDesign.SearchPrefix = "VWD";
                    SDBIdentifier = "VIZIER WD";
                    SDBDescription = "VIZIER White Dwarf Query";
                    sdbDesign.DefaultObjectIndex = 20;  //MixedDeepSpace
                    sdbDesign.DefaultObjectDescription = "White Dwarf";
                    VZRCatalog = WhiteDwarfCatalog;
                    break;
                case RedDwarfCatalog:
                    sdbDesign.SearchPrefix = "VRD";
                    SDBIdentifier = "VIZIER RD";
                    SDBDescription = "VIZIER Red Dwarf Query";
                    sdbDesign.DefaultObjectIndex = 20;  //MixedDeepSpace
                    sdbDesign.DefaultObjectDescription = "Red Dwarf";
                    VZRCatalog = RedDwarfCatalog;
                    break;
            }

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
            string vzrResults = "";
            WebClient client = new WebClient();
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };

            string vzrURLquery = VZRQueryURL + MakeSearchQuery();
            //string vzrURLquery1 = URL_vzr_search + "table=vzrplanets&select=pl_hostname,ra,dec,gaia_gmag&order=dec&format=xml";
            try
            {
                vzrResults = client.DownloadString(vzrURLquery);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download Error: " + ex.Message);
                return null;
            };
            //Standard VOTable parse
            XElement vzrDoc = XElement.Parse(vzrResults);
            XNamespace vzrNS = XNamespace.Get(vzrDoc.Attribute("xmlns").Value);
            XElement resource = vzrDoc.Element(vzrNS + "RESOURCE");
            XElement table = resource.Element(vzrNS + "TABLE");
            XElement xdataTable = table.Element(vzrNS + "DATA");
            XElement xData = xdataTable.Element(vzrNS + "TABLEDATA");

            XElement headerRecordX = new XElement("SDBDataFields");

            //vzr parse
            List<string> dHeader = new List<string>();
            IEnumerable<XElement> fields = table.Elements(vzrNS + "FIELD");
            foreach (XElement xf in fields)
            {
                string fname = xf.Attribute("name").Value.ToString();
                dHeader.Add(fname);
            }
            int[] widths = new int[dHeader.Count];
            IEnumerable<XElement> dRecords = xData.Elements(vzrNS + "TR");
            XElement sdbX = new XElement(XMLParser.SDBListX);
            foreach (XElement rec in dRecords)
            {
                IEnumerable<XElement> dResults = rec.Elements(vzrNS + "TD");
                XElement xmlItem = new XElement(XMLParser.SDBEntryX);
                int dIndex = 0;
                foreach (XElement dItem in dResults)
                {
                    string dItemValue = dItem.Value.ToString();
                    //Convert RA from Sexidecimal hours to decimal hours
                    if (dHeader[dIndex].Contains("RAJ2000"))
                        dItemValue = (Utility.ParseRADecString(dItem.Value.ToString(), ' ')).ToString();
                    //Convert Dec from Sexidecimal degrees to decimal degrees
                    if (dHeader[dIndex].Contains("DEJ2000"))
                        dItemValue = (Utility.ParseRADecString(dItem.Value.ToString(), ' ')).ToString();
                    //if RedDwarf then uses ra degrees
                    if (dHeader[dIndex].Contains("_RA"))
                        dItemValue = Utility.RAHoursToDegrees(Convert.ToDouble(dItem.Value)).ToString();
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
                switch (fieldName)
                {
                    case "WD":
                        sb.TSXEntryName = SDBDesigner.LabelOrSearchX;
                        sb.IsBuiltIn = true;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "DENIS":
                        sb.TSXEntryName = SDBDesigner.LabelOrSearchX;
                        sb.IsBuiltIn = true;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "Name":
                        sb.TSXEntryName = SDBDesigner.LabelOrSearchX;
                        sb.IsBuiltIn = true;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "RAJ2000":
                        sb.TSXEntryName = SDBDesigner.RAHoursX;
                        sb.IsBuiltIn = true;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "DEJ2000":
                        sb.TSXEntryName = SDBDesigner.DecDegreesX;
                        sb.IsBuiltIn = true;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "_RA":
                        sb.TSXEntryName = SDBDesigner.RAHoursX;
                        sb.IsBuiltIn = true;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "_DE":
                        sb.TSXEntryName = SDBDesigner.DecDegreesX;
                        sb.IsBuiltIn = true;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "gmag":
                        sb.TSXEntryName = SDBDesigner.MagnitudeX;
                        sb.IsBuiltIn = true;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
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
                    case "Imag":
                        sb.TSXEntryName = SDBDesigner.MagnitudeX;
                        sb.IsBuiltIn = true;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);

                        DataColumn sbImag = new DataColumn();
                        sbImag.SourceDataName = fieldName;
                        sbImag.TSXEntryName = fieldName;
                        sbImag.IsBuiltIn = false;
                        sbImag.ColumnStart = fieldStart;
                        sbImag.ColumnWidth = fieldWidth;
                        sbImag.IsPassed = true;
                        sbImag.IsDuplicate = true;
                        sdbDesign.DataFields.Add(sbImag);
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

        private string MakeSearchQuery()
        {
            //Returns a url string for querying the vsx website

            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            queryString["-source"] = VZRCatalog;
            if (VZRCatalog == WhiteDwarfCatalog) queryString["-out"] = "Name RAJ2000 DEJ2000 gmag";
            if (VZRCatalog == RedDwarfCatalog) queryString["-out"] = "DENIS _RA _DE Imag";
            //["-out"] = "**";

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

