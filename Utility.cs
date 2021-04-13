using System;
using System.Drawing;
using System.Windows.Forms;

namespace TransientSDB
{
    public static class Utility
    {
        public static int ColumnEnd(int startColumn, int columnWidth) => startColumn + columnWidth-1;

        public static double ReduceTo360(double degrees)
        {
            degrees = Math.IEEERemainder(degrees, 360);
            if (degrees < 0)
            { degrees += 360; }
            return degrees;
        }

        public static void ButtonRed(Button genericButton)
        {
            genericButton.ForeColor = Color.Black;
            genericButton.BackColor = Color.LightSalmon;
            return;
        }

        public static void ButtonGreen(Button genericButton)
        {
            genericButton.ForeColor = Color.Black;
            genericButton.BackColor = Color.PaleGreen;
            return;
        }

        public static bool IsButtonRed(Button genericButton)
        {
            if (genericButton.BackColor == Color.LightSalmon)
            { return true; }
            else
            { return false; }
        }

        public static bool IsButtonGreen(Button genericButton)
        {
            if (genericButton.BackColor == Color.PaleGreen)
            { return true; }
            else
            { return false; }
        }

        public static bool CloseEnough(double testval, double targetval, double percentnear)
        {
            //Cute little method for determining if a value is withing a certain percentatge of
            // another value.
            //testval is the value under consideration
            //targetval is the value to be tested against
            //npercentnear is how close (in percent of target val, i.e. x100) the two need to be within to test true
            // otherwise returns false

            if (Math.Abs(targetval - testval) <= Math.Abs((targetval * percentnear / 100)))
            { return true; }
            else
            { return false; }
        }

        public static string CreateStarLabel(string catName, double RA, double Dec)
        {
            //Creates a name for a blank "Gaia" star
            return (catName + " " + RA.ToString("0.0000") + " " + Dec.ToString("0.0000"));
        }

        public static string ParsePathToFileName(string fullPath)
        {
            //return just the file or directory name from a string containing the full path
            char[] splitter = new char[] { '\\' };
            string[] allwords = fullPath.Split(splitter);
            string lastword = allwords[allwords.Length - 1];
            return lastword;
        }

        public static double RMS(double x, double y)
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }

        public static double ParseRADecString(string radec,char separator)
        {
            //Converts a string in either decimal or sexidecimal format to a double
            //if the string splits because it has internal spaces, then treat as sexidecimal
            //  otherwise treat as decimal 
            char[] remChar = { 'h', 'm', 's', 'd' };

            for (int i = 0; i < radec.Length; i++) if (radec[i] == '\"') radec = radec.Remove(i, 1);
            string[] radecSplit = radec.Split(separator);
            if (radecSplit.Length == 1) return Convert.ToDouble(radec);
            else
            {
                if (radecSplit.Length < 3) return 0;
                for (int i = 0; i < 3; i++) radecSplit[i] = radecSplit[i].TrimEnd(remChar);
                int radecsign = 1;
                if (radecSplit[0].Contains("-")) radecsign = -1;
                double radecDouble = radecsign *
                    (Math.Abs(Convert.ToDouble(radecSplit[0])) + Convert.ToDouble(radecSplit[1]) / 60.0 + Convert.ToDouble(radecSplit[2]) / 3600.0);
                return radecDouble;
            }
        }

        public static string ParseNameString(string name)
        {
            //Converts a string in either decimal or sexidecimal format to a double
            //if the string splits because it has internal spaces, then treat as sexidecimal
            //  otherwise treat as decimal 
            for (int i = 0; i < name.Length; i++) if (name[i] == '\"') { name = name.Remove(i, 1); }
            return name;
        }

         public static bool MatchPoint(Point a, Point b)
        {
            if (a == b)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns true if b is between a and c, inclusive
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsBetween(double a, double b, double c)
        {
            if (a <= c && a <= b && b <= c) return true;
            else if (a >= c && a >= b && b >= c) return true;
            else return false;
        }

        public static string NAGenerator(int iValue, double maxNotNA)
        {
            if (iValue <= maxNotNA) return iValue.ToString("0");
            else return "N/A";
        }

        public static string SexidecimalRADec(double radec, bool hourFlag)
        {
            //turn the double value into xxh yym zzs or xxd yym zzs
            //  depending on hourFlag -- if true then it's RA: hours
            if (radec == 0) return "";
            int sign = Math.Sign(radec);
            radec = Math.Abs(radec);
            int degreeHours = (int)radec;
            radec -= degreeHours;
            radec *= 60;
            int minutes = (int)radec;
            radec -= minutes;
            radec *= 60;
            if (hourFlag) return (sign * degreeHours).ToString("00") + "h " + minutes.ToString("00") + "m " + radec.ToString("00.0") + "s";
            else return (sign * degreeHours).ToString("00") + "d " + minutes.ToString("00") + "m " + radec.ToString("00.0") + "s";
        }

        public static string ParseToSexidecimal(string sex, bool doRA)
        {
            //converts a string in decimal format to a string in sexidecimal format
            //  uses hours if doRA is true
            //  note the AAVSO reports RA in degrees
            double d = Convert.ToDouble(sex);
            int dsign = Math.Sign(d);
            double dAbs = Math.Abs(d);
            if (doRA) //Convert RA degrees to hours
            {
                dAbs = dAbs * 24.0 / 360.0;
            }
            int degHrs = (int)(dAbs);
            dAbs -= degHrs;
            int min = (int)(dAbs * 60);
            dAbs -= (min / 60.0);
            double sec = dAbs * 3600;
            string degHrOut = String.Format("{00}", (dsign * degHrs)).PadLeft(2, '0');
            string minOut = String.Format("{00}", min).PadLeft(2, '0');
            string secOut = sec.ToString("0.000").PadLeft(5, '0');
            //return (dsign * degHrs).ToString("D" + 2) + ":" + min.ToString("I" + 2) + ":" + sec.ToString("D" + 5);
            string leadingSign = "";
            if (!doRA && dsign >= 0)
                leadingSign = "+";
            string sexOut = leadingSign + degHrOut + ":" + minOut + ":" + secOut;
            return sexOut;
        }


    }
}
