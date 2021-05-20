/*
* SearchTNS Class
*
* Class for downloading and parsing Transient Name Server database query results
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
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TransientSDB
{
    public class SearchTNS
    {
        // url of TNS and TNS-sandbox api                                     
        const string url_tns_search = "http://wis-tns.weizmann.ac.il/search?";

        private SDBDesigner sdbDesign;
        private XElement sdbXResults;
        private XDocument sdbXDoc;

        public string SDBIdentifier { get; set; } = "Transient Name Server";
        public string SDBDescription { get; set; } = "Supernova Query";
        public int SearchBackDays { get; set; }
        public bool SearchSN { get; set; }
        public bool SearchClassified { get; set; }

        public bool GetAndSet()
        {
            sdbDesign = new SDBDesigner();
            if (SearchSN)
            {
                sdbDesign.DefaultObjectIndex = 60;
                sdbDesign.DefaultObjectDescription = "Supernova";
                SDBIdentifier = "Transient Name Server - Supernova";
                SDBDescription = "IAU Supernova Working Group Transient Name Server";
                sdbDesign.SearchPrefix = "Supernova";
            }
            else
            {
                sdbDesign.DefaultObjectIndex = 20;
                sdbDesign.DefaultObjectDescription = "Unclassified Transient";
                SDBIdentifier = "Transient Name Server - Unclassified AT";
                SDBDescription = "IAU Supernova Working Group Transient Name Server";
                sdbDesign.SearchPrefix = "Unclassifed AT";
            }

            //Import TNS CSV text query and convert to an XML database
            sdbXResults = ServerQueryToResultsXML();
            if (sdbXResults == null) return false;
            //Parse the TNS-specific catalog data for names and widths to be used
            //  for defining columns in the output text data to TSX SDB text file
            //colMap is the generic list of column names and maximum data widths
            sdbDesign.MakeHeaderMap(sdbXResults);
            //Create the TSX SDB Text header by mapping the column map to the 
            //  TSX built-in and user data fields
            sdbXDoc = ResultsXMLtoSDBHeader(sdbXResults);
            return true;
        }

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
            sdbDesign.ControlFields.Single(cf => cf.ControlName == SDBDesigner.IdentifierX).ControlValue = SDBIdentifier;
            sdbDesign.ControlFields.Single(cf => cf.ControlName == SDBDesigner.SDBDescriptionX).ControlValue = SDBDescription;
            //Map the tns fields on to the tsx built-in and user-defined fields
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
                    case "ID":  //1
                        sb.IsBuiltIn = false;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
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
                    case "Obj_Type": //2
                        sb.IsBuiltIn = false;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "Redshift": //3
                        sb.IsBuiltIn = false;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "Host_Name":  //4
                        sb.IsBuiltIn = false;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "Host_RedShift": //5
                        sb.IsBuiltIn = false;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "Reporting_Groups":  //12
                        sb.IsBuiltIn = false;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    //case "Discovery_Data_Sources":  
                    //    sb.IsBuiltIn = false;
                    //    sb.IsPassed = true;
                    //    sdbDesign.DataFields.Add(sb);
                    //    fieldStart += fieldWidth;
                    //    break;
                    //case "Classifying_Groups":
                    //    sb.IsBuiltIn = false;
                    //    sb.ColumnStart = fieldStart;
                    //    sb.ColumnWidth = fieldWidth;
                    //    sb.IsPassed = true;
                    //    sdbDesign.DataFields.Add(sb);
                    //    fieldStart += fieldWidth;
                    //    break;
                    //case "Associated_Groups":
                    //    sb.IsBuiltIn = false;
                    //    sb.IsPassed = true;
                    //    sdbDesign.DataFields.Add(sb);
                    //    fieldStart += fieldWidth;
                    //    break;
                    case "Disc_Internal_Name":  //6
                        sb.IsBuiltIn = false;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "Disc_Instruments": //11
                        sb.IsBuiltIn = false;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    //case "Class_Instruments":
                    //    sb.IsBuiltIn = false;
                    //    sb.IsPassed = true;
                    //    sdbDesign.DataFields.Add(sb);
                    //    fieldStart += fieldWidth;
                    //    break;
                    //case "TNS_AT":
                    //    sb.IsBuiltIn = false;
                    //    sb.IsPassed = true;
                    //    sdbDesign.DataFields.Add(sb);
                    //    fieldStart += fieldWidth;
                    //    break;
                    //case "Public":
                    //    sb.IsBuiltIn = false;
                    //    sb.IsPassed = true;
                    //    sdbDesign.DataFields.Add(sb);
                    //    fieldStart += fieldWidth;
                    //    break;
                    //case "End_Prop_Period":
                    //    sb.IsBuiltIn = false;
                    //    sb.IsPassed = true;
                    //    sdbDesign.DataFields.Add(sb);
                    //    fieldStart += fieldWidth;
                    //    break;
                    case "Discovery_MagFlux": //7
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
                    case "Discovery_Filter":  //8
                        sb.IsBuiltIn = false;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "Discovery_Date_UT": //9
                        sb.IsBuiltIn = false;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case "Sender":
                        break;
                    case "Remarks": //10
                        sb.IsBuiltIn = false;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
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
