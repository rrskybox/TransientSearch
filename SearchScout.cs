/*
* SearchScout Class
*
* Class for downloading and parsing database NASA NEO Scout query results
* 
* This class serves as method template for conversions from all 
*  catalog sources
* 
* Author:           Rick McAlister
* Date:             5/12/21
* Current Version:  1.0
* Developed in:     Visual Studio 2019
* Coded in:         C# 8.0
* App Envioronment: Windows 10 Pro, .Net 4.8, TSX 5.0 Build 12978
* 
* API Doc: https://ssd-api.jpl.nasa.gov/doc/scout.html
* 
* Change Log:
* 
* Added this class to make V1.1 Release (5/12/21)
* Added CNEOS Scout NEO query/parse/translate to SDB.txt
* 
*/
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Net;
using System.Text.Json;
using Newtonsoft.Json;
//using System.Net.Http.Json;

namespace TransientSDB
{
    class SearchScout
    {
        const string URL_NEO_search = "https://ssd-api.jpl.nasa.gov/scout.api";

        #region column headers
        public const string Xneo1kmScore = "neo1kmScore";
        public const string XlastRun = "lastRun";
        public const string XuncP1 = "uncP1";
        public const string Xdec = "dec";
        public const string XneoScore = "neoScore";
        public const string Xrating = "rating";
        public const string Xrate = "rate";
        public const string Xunc = "unc";
        public const string XphaScore = "phaScore";
        public const string Xra = "ra";
        public const string Xelong = "elong";
        public const string XnObs = "nObs";
        public const string Xarc = "arc";
        public const string XtEphem = "tEphem";
        public const string XobjectName = "objectName";
        public const string XtisserandScore = "tisserandScore";
        public const string XcaDist = "caDist";
        public const string XvInf = "vinf";
        public const string XH = "H";
        public const string XrmsN = "rmsN";
        public const string XieoScore = "ieoScore";
        public const string XgeocentricScore = "geocentricScore";
        public const string Xmoid = "moid";
        public const string XVmag = "Vmag";

        #endregion


        private SDBDesigner sdbDesign;
        private XElement sdbXResults;
        private XDocument sdbXDoc;

        public string SDBIdentifier { get; set; }
        public string SDBDescription { get; set; }
        public string SearchType { get; set; }

        public bool GetAndSet()
        {
            sdbDesign = new SDBDesigner();
            sdbDesign.SearchPrefix = "NEO";
            sdbDesign.DefaultObjectIndex = 37;
            sdbDesign.DefaultObjectDescription = "Near Earth Object";
            SDBIdentifier = "NASA Scout Unconfirmed NEO";
            SDBDescription = "NASA CNEOS Scout Server";

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

        private XElement ServerQueryToResultsXML_Old()
        {
            string[] headerFields = new string[]
            {
                         Xneo1kmScore,
                         XlastRun ,
                         XuncP1 ,
                         Xdec ,
                         XneoScore,
                         Xrating ,
                         Xrate ,
                         Xunc ,
                         XphaScore ,
                         Xra ,
                         Xelong ,
                         XnObs ,
                         Xarc ,
                         XtEphem ,
                         XobjectName ,
                         XtisserandScore ,
                         XcaDist ,
                         XvInf,
                         XH ,
                         XrmsN ,
                         XieoScore ,
                         XgeocentricScore ,
                         Xmoid ,
                         XVmag
            };

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
            ScoutJSON scoutBase = System.Text.Json.JsonSerializer.Deserialize<ScoutJSON>(neoResultText);
            int headerCount = Convert.ToInt16(scoutBase.count);
            string[] entries = new string[headerCount];
            int[] widths = new int[headerFields.Count()];

            //create an xml working database
            XElement targetsXML = new XElement(XMLParser.SDBListX);
            //Load DB based on JSON entries
            foreach (ScoutData jdata in scoutBase.data)
            {
                int i = 0;
                XElement xmlItem = new XElement(XMLParser.SDBEntryX);
                xmlItem.Add(new XElement(Xneo1kmScore, jdata.neo1kmScore));
                widths[i] = Utility.Bigger(widths[i], jdata.neo1kmScore.Length);
                i++;
                xmlItem.Add(new XElement(XlastRun, jdata.lastRun));
                widths[i] = Utility.Bigger(widths[i], jdata.lastRun.Length);
                i++;
                xmlItem.Add(new XElement(XuncP1, jdata.uncP1));
                widths[i] = Utility.Bigger(widths[i], jdata.uncP1.Length);
                i++;
                xmlItem.Add(new XElement(Xdec, jdata.dec));
                widths[i] = Utility.Bigger(widths[i], jdata.dec.Length);
                i++;
                xmlItem.Add(new XElement(XneoScore, jdata.neoScore));
                widths[i] = Utility.Bigger(widths[i], jdata.neoScore.Length);
                i++;
                xmlItem.Add(new XElement(Xrating, jdata.rating));
                if (jdata.rating != null)
                    widths[i] = Utility.Bigger(widths[i], jdata.rating.Length);
                i++;
                xmlItem.Add(new XElement(Xrate, jdata.rate));
                widths[i] = Utility.Bigger(widths[i], jdata.rate.Length);
                i++;
                xmlItem.Add(new XElement(Xunc, jdata.unc));
                widths[i] = Utility.Bigger(widths[i], jdata.unc.Length);
                i++;
                xmlItem.Add(new XElement(XphaScore, jdata.phaScore));
                widths[i] = Utility.Bigger(widths[i], jdata.phaScore.Length);
                i++;
                string raDecimal = Utility.ParseRADecString(jdata.ra, ':').ToString("0.000");
                xmlItem.Add(new XElement(Xra, raDecimal));
                widths[i] = Utility.Bigger(widths[i], raDecimal.Length);
                i++;
                xmlItem.Add(new XElement(Xelong, jdata.elong));
                widths[i] = Utility.Bigger(widths[i], jdata.elong.Length);
                i++;
                xmlItem.Add(new XElement(XnObs, jdata.nObs));
                widths[i] = Utility.Bigger(widths[i], jdata.nObs.Length);
                i++;
                xmlItem.Add(new XElement(Xarc, jdata.arc));
                widths[i] = Utility.Bigger(widths[i], jdata.arc.Length);
                i++;
                xmlItem.Add(new XElement(XtEphem, jdata.tEphem));
                widths[i] = Utility.Bigger(widths[i], jdata.tEphem.Length);
                i++;
                xmlItem.Add(new XElement(XobjectName, jdata.objectName));
                widths[i] = Utility.Bigger(widths[i], jdata.objectName.Length);
                i++;
                xmlItem.Add(new XElement(XtisserandScore, jdata.tisserandScore));
                widths[i] = Utility.Bigger(widths[i], jdata.tisserandScore.Length);
                i++;
                xmlItem.Add(new XElement(XcaDist, jdata.caDist));
                if (jdata.caDist != null)
                    widths[i] = Utility.Bigger(widths[i], jdata.caDist.Length);
                i++;
                xmlItem.Add(new XElement(XvInf, jdata.vInf));
                if (jdata.vInf != null)
                    widths[i] = Utility.Bigger(widths[i], jdata.vInf.Length);
                i++;
                xmlItem.Add(new XElement(XH, jdata.H));
                widths[i] = Utility.Bigger(widths[i], jdata.H.Length);
                i++;
                xmlItem.Add(new XElement(XrmsN, jdata.rmsN));
                widths[i] = Utility.Bigger(widths[i], jdata.rmsN.Length);
                i++;
                xmlItem.Add(new XElement(XieoScore, jdata.ieoScore));
                widths[i] = Utility.Bigger(widths[i], jdata.ieoScore.Length);
                i++;
                xmlItem.Add(new XElement(XgeocentricScore, jdata.geocentricScore));
                widths[i] = Utility.Bigger(widths[i], jdata.geocentricScore.Length);
                i++;
                xmlItem.Add(new XElement(Xmoid, jdata.moid));
                widths[i] = Utility.Bigger(widths[i], jdata.moid.Length);
                i++;
                xmlItem.Add(new XElement(XVmag, jdata.Vmag));
                widths[i] = Utility.Bigger(widths[i], jdata.Vmag.Length);
                targetsXML.Add(xmlItem);
            }
            XElement headerRecordX = new XElement("SDBDataFields");
            for (int i = 0; i < widths.Length; i++)
            {
                XElement colRecordX = new XElement(headerFields[i], widths[i]);
                headerRecordX.Add(colRecordX);
            }
            targetsXML.Add(headerRecordX);
            return targetsXML;
        }

        private XElement ServerQueryToResultsXML()
        {
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
            //Convert JSON to XML
            XDocument scoutXML = JsonConvert.DeserializeXNode(neoResultText, "Root");
            //Pull out data entities
            IEnumerable<XElement> sDataX = scoutXML.Element("Root").Elements("data");
            //Create list of header names
            List<string> sHdrFields = new List<string>();
            IEnumerable<XElement> firstData = sDataX.First().Elements();
            foreach (XElement sX in firstData)
                sHdrFields.Add(sX.Name.LocalName);
            //Create array to take data widths (for SDB.txt table columns)
            int[] widths = new int[sHdrFields.Count()];
            //create an xml database to be used for conversion to SDB.txt
            XElement targetsXML = new XElement(XMLParser.SDBListX);
            //Load DB based on data entries
            foreach (XElement jdata in sDataX)
            {
                int i = 0;
                XElement xmlItem = new XElement(XMLParser.SDBEntryX);
                foreach (XElement tdata in jdata.Elements())
                {
                    //if RA, convert to decimal hours
                    if (tdata.Name.LocalName == Xra)
                        tdata.Value =  Utility.ParseRADecString(tdata.Value, ':').ToString("0.000"); 
                    xmlItem.Add(tdata);
                    widths[i] = Utility.Bigger(widths[i], tdata.Value.Length);
                    i++;
                }
                targetsXML.Add(xmlItem);
            }
            //Save data to create header record
            XElement headerRecordX = new XElement("SDBDataFields");
            for (int i = 0; i < widths.Length; i++)
            {
                XElement colRecordX = new XElement(sHdrFields[i], widths[i]);
                headerRecordX.Add(colRecordX);
            }
            targetsXML.Add(headerRecordX);
            return targetsXML;
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
                    case XobjectName:
                        sb.TSXEntryName = SDBDesigner.LabelOrSearchX;
                        sb.IsBuiltIn = true;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case Xra:
                        sb.TSXEntryName = SDBDesigner.RAHoursX;
                        sb.IsBuiltIn = true;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case Xdec:
                        sb.TSXEntryName = SDBDesigner.DecDegreesX;
                        sb.IsBuiltIn = true;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case XVmag:
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
                    case XtEphem:
                        sb.IsBuiltIn = false;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case Xrate:
                        sb.IsBuiltIn = false;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case Xelong:
                        sb.IsBuiltIn = false;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case XcaDist:
                        sb.IsBuiltIn = false;
                        sb.IsPassed = true;
                        sdbDesign.DataFields.Add(sb);
                        fieldStart += fieldWidth;
                        break;
                    case Xneo1kmScore:
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

        public class ScoutJSON
        {
            public string count { get; set; }
            public ScoutSignature signature { get; set; }
            public ScoutData[] data { get; set; }

        }

        public class ScoutData
        {
            public string neo1kmScore { get; set; }
            public string lastRun { get; set; }
            public string uncP1 { get; set; }
            public string dec { get; set; }
            public string neoScore { get; set; }
            public string rating { get; set; }
            public string rate { get; set; }
            public string unc { get; set; }
            public string phaScore { get; set; }
            public string ra { get; set; }
            public string elong { get; set; }
            public string nObs { get; set; }
            public string arc { get; set; }
            public string tEphem { get; set; }
            public string objectName { get; set; }
            public string tisserandScore { get; set; }
            public string? caDist { get; set; }
            public string? vInf { get; set; }
            public string H { get; set; }
            public string rmsN { get; set; }
            public string ieoScore { get; set; }
            public string geocentricScore { get; set; }
            public string moid { get; set; }
            public string Vmag { get; set; }
        }

        public class ScoutSignature
        {
            public string source { get; set; }
            public string version { get; set; }
        }

    }
}

