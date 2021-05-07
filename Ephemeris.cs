using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace TransientSDB
{
    class Ephemeris
    {

        public static int AddCalculatedEphemeris(XElement sdbXResults)
        {
            int width = 0;
            XElement xRecords = sdbXResults.Element(XMLParser.SDBListX);
            foreach (XElement tgtX in sdbXResults.Elements(XMLParser.SDBEntryX))
            {
                string nextTransitString = "";
                string periodX = tgtX.Element(SearchEXO.TAPPeriod).Value;
                string julDateX = tgtX.Element(SearchEXO.TAPTransitMid).Value;
                if (periodX != "" && julDateX != "")
                {
                    double period = Convert.ToDouble(periodX);
                    double jDate = Convert.ToDouble(julDateX);
                    DateTime nextTransit = NextTransit(jDate, period);
                    nextTransitString = nextTransit.ToString("MM/dd/yyyy HH:mm:ss");
                    width = Utility.Bigger(nextTransitString.Length, width);
                }
                tgtX.Add(new XElement("NextTransit", nextTransitString));
            }
            return width;
        }

        private static DateTime JulianToUTC(double jDate)
        {
            DateTime utc = AstroMath.Celestial.JulianToDate(jDate);
            return utc;
        }

        private static DateTime NextTransit(double julDate, double periodDays)
        {
            DateTime rightNow = DateTime.UtcNow;
            DateTime firstTransit = JulianToUTC(julDate);
            TimeSpan span = rightNow - firstTransit;
            int cycles = (int)(span.TotalDays / periodDays) + 1;
            TimeSpan spanToNext = TimeSpan.FromDays(cycles * periodDays);
            DateTime nextTransit = firstTransit + spanToNext;
            return nextTransit;
        }
    }
}
