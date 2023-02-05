using System;
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
                string nextEarliestTransitString = "";
                string nextLatestTransitString = "";
                string periodX = tgtX.Element(SearchEXO.TAPPeriod).Value;
                string julDateX = tgtX.Element(SearchEXO.TAPTransitMid).Value;
                string periodMinusErrorX = tgtX.Element(SearchEXO.TAPPeriodMinusError).Value;
                string periodPlusErrorX = tgtX.Element(SearchEXO.TAPPeriodPlusError).Value;

                if (periodX != "" && julDateX != "")
                {
                    double period = Convert.ToDouble(periodX);
                    double jDate = Convert.ToDouble(julDateX);
                    DateTime nextTransit = NextTransit(jDate, period).ToLocalTime();
                    nextTransitString = nextTransit.ToString("MM/dd/yyyy HH:mm:ss");
                    width = Utility.Bigger(nextTransitString.Length, width);

                    if (periodMinusErrorX != "")
                    {
                        double periodMin = Convert.ToDouble(periodX) + Convert.ToDouble(periodMinusErrorX);
                        DateTime nextEarliestTransit = NextTransit(jDate, periodMin).ToLocalTime();
                        nextEarliestTransitString = nextEarliestTransit.ToString("MM/dd/yyyy HH:mm:ss");
                    }

                    if (periodPlusErrorX != "")
                    {
                        double periodMax = Convert.ToDouble(periodX) + Convert.ToDouble(periodPlusErrorX);
                        DateTime nextLatestTransit = NextTransit(jDate, periodMax).ToLocalTime();
                        nextLatestTransitString = nextLatestTransit.ToString("MM/dd/yyyy HH:mm:ss");
                    }
                }
                tgtX.Add(new XElement(SearchEXO.NextTransitX, nextTransitString));
                tgtX.Add(new XElement(SearchEXO.NextTransitEarliestX, nextEarliestTransitString));
                tgtX.Add(new XElement(SearchEXO.NextTransitLatestX, nextLatestTransitString));
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
