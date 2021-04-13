using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TransientSDB
{
    public class ExportTSXEditPastePhoto
    {
        public static void XMLToClipboard(XElement xml)
        {
            const string tsxHead = "SN      Host Galaxy      Date         R.A.    Decl.    Offset   Mag.   Disc. Ref.            SN Position         Posn. Ref.       Type  SN      Discoverer(s)";

            //Create a text string to be filled in for the clipboard: Column headings and two newlines.
            string cbText = tsxHead + "\n\n";
            //Duplicate the TSX photo input format -- i.e. make it the same as the Harvard IUA display format
            //  as it is copied into the clipboard
            IEnumerable<XElement> xmlList = xml.Elements("SNEntry");
            foreach (XElement xmlItem in xmlList)
            {
                cbText += xmlItem.Element("Name").Value.Replace("SN ", "").PadRight(8);
                //Name of the Host Galaxy, if any
                cbText += FitFormat(xmlItem.Element("Host_Name").Value, 17);
                //Discovery Date
                cbText += FitFormat(xmlItem.Element("Discovery_Date_UT").Value, 12);
                //Truncated RA and Dec for locale
                cbText += FitFormat(xmlItem.Element("RA").Value, 8);
                cbText += FitFormat(xmlItem.Element("DEC").Value, 12);
                //Offsets?
                cbText += "       ";  //offsets
                                      //Magnitude
                                      //cbText += xmlItem.Element("Discovery_Mag").Value.Substring(0, 4).PadRight(8); ;
                if (xmlItem.Element("Discovery_Mag") != null) cbText += FitFormat(xmlItem.Element("Discovery_Mag").Value, 8);
                else cbText += "        ";  //Pad 8
                                            //Catelogs, truncated at 15 chars, if any
                cbText += FitFormat(xmlItem.Element("Ext_catalogs").Value, 15);
                //Actual RA/Dec location
                cbText += FitFormat(xmlItem.Element("RA").Value, 12);
                cbText += FitFormat(xmlItem.Element("DEC").Value, 14);
                //filler for Position Reference
                cbText += "                 ";
                //Supernova Type
                cbText += FitFormat(xmlItem.Element("Obj_Type").Value.Replace("SN ", ""), 6);
                //Supernova Name (as derived from entry name
                cbText += FitFormat(xmlItem.Element("Name").Value.Replace("SN ", ""), 8);
                //Discoverer
                //cbText += FitFormat(xmlItem.Element("Discovering_Groups").Value, 12);
                //New Line
                cbText += "\n";
            }
            System.Windows.Forms.Clipboard.SetText(cbText, TextDataFormat.UnicodeText);
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

