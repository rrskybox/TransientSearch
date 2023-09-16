/*
* Utility Class
*
* Class of common routines used by the Transient Server app
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
using System.Drawing;
using System.Windows.Forms;

namespace TransientSDB
{
    public static class Utility
    {
        public static int ColumnEnd(int startColumn, int columnWidth) => startColumn + columnWidth - 1;

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

        public static double ParseRADecString(string radec, char separator)
        {
            //Converts a string in either decimal or sexidecimal format to a double
            //if the string splits because it has internal spaces, then treat as sexidecimal
            //  otherwise treat as decimal 
            char[] remChar = { 'h', 'm', 's', 'd' };

            for (int i = 0; i < radec.Length; i++) 
                if (radec[i] == '\"') radec = radec.Remove(i, 1);
            string[] radecSplit = radec.Split(separator);
            if (radecSplit.Length == 1) return Convert.ToDouble(radec);
            else
            {
                int radecsign = 1;
                double radecSec = 0;
                 for (int i = 0; i < radecSplit.Length;i++) radecSplit[i] = radecSplit[i].TrimEnd(remChar);
                if (radecSplit.Length == 2)
                    radecSec = 0.0;
                else
                    radecSec = Convert.ToDouble(radecSplit[2]);
               if (radecSplit[0].Contains("-")) radecsign = -1;
                double radecDouble = radecsign *
                    (Math.Abs(Convert.ToDouble(radecSplit[0])) + Convert.ToDouble(radecSplit[1]) / 60.0 + radecSec / 3600.0);
                return radecDouble;
            }
        }

        public static string RADecToSexidecimal(double radec, bool hourFlag)
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

        public static double DegreesToHours(double ra)
        {
            return ra * 24.0 / 360.0;
        }

        public static int Bigger (int a, int b)
        {
            if (a > b) return a;
            else return b;
        }
                public static string ParseNameString(string name)
        {
            //Converts a string in either decimal or sexidecimal format to a double
            //if the string splits because it has internal spaces, then treat as sexidecimal
            //  otherwise treat as decimal 
            for (int i = 0; i < name.Length; i++) if (name[i] == '\"') { name = name.Remove(i, 1); }
            return name;
        }

        public static double? ParseRADecString(string radec)
        {
            //Converts a string in either decimal or sexidecimal format to a double
            //if the string splits because it has internal spaces, then treat as sexidecimal
            //  otherwise treat as decimal 
            //Supported formats are: hh mm ss, hh'h' mm'm' ss's', hh mm ss, hh:mm:ss, dd mm ss, dd'd' mm'm' ss's', dd:mm:ss, h.hhhh, d.dddd
            //
            //Initially remove leading and trailing spaces, convert semicolons to spaces and remove any quotes
            //If the string in contains a character (h, d, m, s) then is is treated as a three group string
            //If the string does not contain those characters then it is treated as a double formated string
            char[] remChar = { 'h', 'd', 'm', 's', ' ' };
            radec = radec.TrimEnd(' ').TrimStart(' ');
            radec = radec.Replace(':', ' ');
            for (int i = 0; i < radec.Length; i++)
                if (radec[i] == '\"') radec = radec.Remove(i, 1);
            string[] radecSplit = radec.Split(' ');
            if (radecSplit.Length == 1) return Convert.ToDouble(radec);
            else
            {
                if (radecSplit.Length < 3) return 0;
                for (int i = 0; i < 3; i++) radecSplit[i] = radecSplit[i].TrimEnd(remChar);
                int radecsign = 1;
                if (radecSplit[0].Contains("-")) radecsign = -1;
                try
                {
                    double? radecDouble = radecsign *
                       (Math.Abs(Convert.ToDouble(radecSplit[0])) + Convert.ToDouble(radecSplit[1]) / 60.0 + Convert.ToDouble(radecSplit[2]) / 3600.0);
                    return radecDouble;
                }
                catch (Exception ex)
                { return null; };
            }
        }



    }
}
