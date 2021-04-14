///TNSManagemet Class
///
/// Class for TNS CSV Imported file conversions to TSX SDB Text file 
/// 
/// This class serves as method template for conversions from all 
///   catalog sources
///

using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TransientSDB
{
    public class TNSManagement
    {
        // url of TNS and TNS-sandbox api                                     
        const string url_tns_search = "http://wis-tns.weizmann.ac.il/search?";

        const string tnsListIdentifier = "TNS Supernova List";
        const string tnsName = "www.TNS.org";

        private SDBDesigner sdbDesign;
        private XElement sdbXResults;
        private XDocument sdbXDoc;

        public int SearchBackDays { get; set; }
        public bool SearchSN { get; set; }
        public bool SearchClassified { get; set; }

        public void GetAndSet()
        {
            sdbDesign = new SDBDesigner();
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
            string weburl = MakeSearchQuery();
            string contents;
            WebClient client = new WebClient();
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            try
            {
                string urlSearch = url_tns_search + MakeSearchQuery();
                contents = client.DownloadString(urlSearch);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download Error: " + ex.Message);
                return null;
            };

            //Clean up the column headers so they can be used as XML item names
            string[] lines = contents.Split('\n');
            lines[0] = lines[0].Replace(" ", "_");
            lines[0] = lines[0].Replace("/", "");
            lines[0] = lines[0].Replace("(", "");
            lines[0] = lines[0].Replace(")", "");
            lines[0] = lines[0].Replace(".", "");
            lines[0] = lines[0].Replace("\"", "");

            //Split into rows and load the header line as csv file
            char[] csvSplit = new char[] { '\t' };
            string[] headers = lines[0].Split(csvSplit, System.StringSplitOptions.None).Select(x => x.Trim('\"')).ToArray();
            int[] widths = new int[headers.Length];

            //create an xml working database
            XElement xml = new XElement(XMLParser.SDBListX);
            for (int line = 1; line < lines.Length; line++)
            {
                lines[line] = lines[line].Replace("\"", "");
                string[] entries = lines[line].Split(csvSplit, System.StringSplitOptions.None);
                XElement xmlItem = new XElement(XMLParser.SDBEntryX);
                for (int i = 0; i < headers.Length; i++)
                {
                    if (headers[i].Contains("RA") || headers[i].Contains("DEC"))
                        entries[i] = Utility.ParseRADecString(entries[i], ':').ToString();
                    xmlItem.Add(new XElement(headers[i], entries[i]));
                    if (entries[i].Length > widths[i]) widths[i] = entries[i].Length;
                }
                //fill in clipboard fields
                //Name
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

        /// <summary>
        /// Method to translate TNS formatted data field types to 
        ///   TSX SDB data field types
        /// </summary>
        /// <param name="xmlDB"></param>
        /// <param name="colMap"></param>
        /// <returns>XML-formated TSX SDB import text file header</returns>
        private XDocument ResultsXMLtoSDBHeader(XElement xmlDB)
        {
            //Translates TNS Formatted data into TSX readable text header
            //  but still XML formatted
            //
            //Create a TSXSDB formatter to work with
            //  this object will include a standard set of control fields
            //  and empty sets of builtin and user data fields
            //tsxSDBdesign = new SDBDesigner();
            //Stick with the standard set of control fields
            // Except for identifier and sdbdescription
            sdbDesign.ControlFields.Single(cf => cf.ControlName == SDBDesigner.IdentifierX).ControlValue = "Transient Name Server";
            sdbDesign.ControlFields.Single(cf => cf.ControlName == SDBDesigner.SDBDescriptionX).ControlValue = "tns.com";
            //Map the tns fields on to the tsx built-in and user-defined fields
            //  keeping track of the start of the column
            int fieldStart = 1;
            foreach (DataColumn sb in sdbDesign.HeaderMap)
            //for (int i = 0; i < tsxSDBdesign .HeaderMap.Count; i++)
            //tsxSDBdesign.DataFields.Single(f => f.SDBColumnName == SDBDesigner.LabelOrSearchX).TNSColumnName = "Name";
            {
                string fieldName = sb.SourceDataName;
                int fieldWidth = sb.ColumnWidth;
                switch (fieldName)
                {
                    case "ID":
                        sb.SourceDataName = "ID";
                        sb.TSXEntryName = "ID";
                        sb.IsBuiltIn = false;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "Name":
                        sb.SourceDataName = "Name";
                        sb.TSXEntryName = SDBDesigner.LabelOrSearchX;
                        sb.IsBuiltIn = true;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "RA":
                         sb.SourceDataName = "RA";
                        sb.TSXEntryName = SDBDesigner.RAHoursX;
                        sb.IsBuiltIn = true;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "DEC":
                        sb.SourceDataName = "DEC";
                        sb.TSXEntryName = SDBDesigner.DecDegreesX;
                        sb.IsBuiltIn = true;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "Obj_Type":
                        sb.SourceDataName = "Obj_Type";
                        sb.TSXEntryName = SDBDesigner.ObjectTypeX;
                        sb.IsBuiltIn = true;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "Redshift":
                        sb.SourceDataName = "Redshift";
                        sb.TSXEntryName = "Redshift";
                        sb.IsBuiltIn = false;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "Host_Name":
                        sb.SourceDataName = "Host_Name";
                        sb.TSXEntryName = "Host_Name";
                        sb.IsBuiltIn = false;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "Host_RedShift":
                        sb.SourceDataName = "Red Shift";
                        sb.TSXEntryName = "Host_RedShift";
                        sb.IsBuiltIn = false;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "Reporting_Groups":
                        //sb.ColumnStart = fieldStart;
                        //tsxHdr.DataFields.Add(sb);
                        //fieldStart += fieldWidth;
                        break;
                    case "Discovery_Data_Sources":
                        break;
                    case "Classifying_Groups":
                        break;
                    case "Associated_Groups":
                        break;
                    case "Disc_Internal_Name":
                        break;
                    case "Disc_Instruments":
                        break;
                    case "Class_Instruments":
                        break;
                    case "TNS_AT":
                        break;
                    case "Public":
                        break;
                    case "End_Prop_Period":
                        break;
                    case "Discovery_MagFlux":
                         sb.SourceDataName = "Discovery_MagFlux";
                       sb.TSXEntryName = SDBDesigner.MagnitudeX;
                        sb.IsBuiltIn = true;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "Discovery_Filter":
                        //sb.ColumnStart = fieldStart;
                        //tsxHdr.DataFields.Add(sb);
                        //fieldStart += fieldWidth;
                        break;
                    case "Discovery_Date_UT":
                        sb.SourceDataName = "Discovery_Date_UT";
                        sb.TSXEntryName = "Discovery_Date_UT";
                        sb.IsBuiltIn = false;
                        sb.ColumnStart = fieldStart;
                        sb.ColumnWidth = fieldWidth;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "Sender":
                        break;
                    case "Remarks":
                        break;
                    case "Ext_catalogs":
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

        /// <summary>
        /// Generates URL query to TNS Server
        /// </summary>
        /// <returns></returns>
        private string MakeSearchQuery()
        {
            //Returns a url string for querying the TNS website

            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            queryString["format"] = "tsv";

            queryString["name"] = "";
            queryString["name_like"] = "0";
            queryString["isTNS_AT"] = "yes";
            queryString["public"] = "all";
            if (SearchClassified)
                queryString["unclassified_at"] = "0";
            else queryString["unclassified)at"] = "1";
            if (SearchSN) 
                queryString["classified_sne"] = "1";
            else queryString["classified_sne"] = "0";
            queryString["ra"] = "";
            queryString["decl"] = "";
            queryString["radius"] = "";
            queryString["coords_unit"] = "deg";
            queryString[@"groupid[]"] = "null";
            queryString["classifier_groupid[]"] = "null";
            queryString["objtype[]"] = "null";
            queryString["AT_objtype[]"] = "null";
            queryString["discovered_period_value"] = SearchBackDays.ToString();
            queryString["discovered_period_units"] = "days";
            queryString["discovery_mag_min"] = "";
            queryString["discovery_mag_max"] = "";
            queryString["internal_name"] = "";
            queryString["redshift_min"] = "";
            queryString["redshift_max"] = "";
            queryString["spectra_count"] = "";
            queryString["discoverer"] = "";
            queryString["classifier"] = "";
            queryString["discovery_instrument[]"] = "";
            queryString["classification_instrument[]"] = "";
            queryString["hostname"] = "NGC";
            queryString["associated_groups[]"] = "null";
            queryString["ext_catid"] = "";
            queryString["num_page"] = "50";

            queryString["display[redshift]"] = "0";
            queryString["display[hostname]"] = "0";
            queryString["display[host_redshift]"] = "0";
            queryString["display[source_group_name]"] = "0";
            queryString["display[classifying_source_group_name]"] = "0";
            queryString["display[discovering_instrument_name]"] = "0";
            queryString["display[classifing_instrument_name]"] = "0";
            queryString["display[programs_name]"] = "0";
            queryString["display[internal_name]"] = "0";
            queryString["display[isTNS_AT]"] = "0";
            queryString["display[public]"] = "0";
            queryString["display[end_pop_period]"] = "0";
            queryString["display[pectra_count]"] = "0";
            queryString["display[discoverymag]"] = "0";
            queryString["display[Bdiscmagfilter]"] = "0";
            queryString["display[discoverydate]"] = "0";
            queryString["display[discoverer ]"] = "0";
            queryString["display[sources]"] = "0";
            queryString["display[bibcode]"] = "0";
            queryString["display[ext_catalogs]"] = "0";

            return queryString.ToString(); // Returns "key1=value1&key2=value2", all URL-encoded
        }


    }
}
