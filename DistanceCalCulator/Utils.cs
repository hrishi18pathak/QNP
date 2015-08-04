using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DistanceCalCulator
{
    public class Utils
    {
        public static double ToDegrees(double radians)
        {
            // This method uses double precission internally, 
            // though it returns single float 
            double factor = 180 / Math.PI;
            return (double)(radians * factor);
        }
        
        private static double ToRadian(double val) { return val * (Math.PI / 180); }



        //------------------------------------------------------------------------------------
        public static int Bearing(double lat1, double lon1, double lat2, double lon2)
        {
            var latitude1 = ToRadian(lat1);
            var latitude2 = ToRadian(lat2);

            var longitudeDifference = ToRadian(lon2 - lon1);

            var y = Math.Sin(longitudeDifference) * Math.Cos(latitude2);
            var x = Math.Cos(latitude1) * Math.Sin(latitude2) -
                    Math.Sin(latitude1) * Math.Cos(latitude2) * Math.Cos(longitudeDifference);

            return (int)(ToDegrees(Math.Atan2(y, x)) + 360) % 360;
        }

        //------------------------------------------------------------------------------------
        public static double distance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = Math.Round((dist * 0.8684), 1);

            }
            return (dist);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts decimal degrees to radians             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts IAS to TAS            :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        public static double IAStoTAS(double IAS,double ALT)
        {
            return (IAS + (IAS / 50)*(  ALT / 1000));
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts Track to Heading with wind correction :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        public static double TRACKtoHEADING( int wINDDir , int wINDSPEED,double TAS,int bearing)
        {
            return (bearing + rad2deg(Math.Asin(wINDSPEED / TAS * Math.Sin(deg2rad(bearing - wINDDir + 180)))));

        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts Track to Heading with wind correction :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        public static double TAStoGroundspeed(double TAS,int bearing ,double heading,double wINDSPEED, double wINDDIR)

        {
            return (TAS * Math.Cos(deg2rad(bearing - heading)) + wINDSPEED * Math.Cos(deg2rad(bearing - wINDDIR + 180)));

        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function calculates Time                               :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        public static double calculateTIME(double dist, double groundspeed)
        {
            return((dist)*60 / groundspeed);
        }
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function displays calculated Time in Hr:Min format                               :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        public static string displayTIME(double time1)
        {
            double temp = 0;
            double min = 0;
            double hhour = 0;
            if (time1 >= 60)
                {//Seperate hours
                    temp = Math.Round(time1);
                    temp = temp / 60;
                    hhour = Math.Floor(temp);
                    temp -= hhour;
                    // store minutes
                    min = Math.Round(temp * 60);
                }
            else
                min = Math.Round(time1);

            // Create padding character
            char pad;
            char.TryParse("0", out pad);

            // Create hr/min strings
            string hHour = hhour.ToString();
            string mMin = min.ToString().PadLeft(2, pad);
            

            // Append hr/min
            string hrmin = string.Format("{0}:{1}", hHour, mMin);
            return (hrmin);
            
            
        }


        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts input from deg,min and second text boxes and convert
        //::  to   single value.Also adds -ve sign if South or West to respective Lat or Long value. :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::


        public static double ConvertDegreeAngleToDouble(double degrees, double minutes, double seconds, double decimalSeconds)
        {
            //Decimal degrees = 
            //   whole number of degrees, 
            //   plus minutes divided by 60, 
            //   plus seconds divided by 3600
            //   plus decimalSeconds divided by 1000
            return degrees + (minutes / 60) + (seconds / 3600) + (decimalSeconds / 3600000);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //Decimal Degree conversion to Deg, Min and Sec.
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
               
        
        public static string DDtoDMS(double coordinate, DataForm.CoordinateType type)
        {
            // Set flag if number is negative
            bool neg = coordinate < 0;

            // Work with a positive number
            coordinate = Math.Abs(coordinate);

            // Get d/m/s components
            double d = Math.Floor(coordinate);
            coordinate -= d;
            coordinate *= 60;
            double m = Math.Floor(coordinate);
            coordinate -= m;
            coordinate *= 60;
            double s = Math.Round(coordinate);

            // Create padding character
            char pad;
            char.TryParse("0", out pad);

            // Create d/m/s strings
            string dd = d.ToString();
            string mm = m.ToString().PadLeft(2, pad);
            string ss = s.ToString().PadLeft(2, pad);

            // Append d/m/s
            string dms = string.Format("{0}°{1}'{2}\"", dd, mm, ss);

            // Append compass heading
            switch (type)
            {
                case DataForm.CoordinateType.longitude:
                    dms += neg ? "W" : "E";
                    break;
                case DataForm.CoordinateType.latitude:
                    dms += neg ? "S" : "N";
                    break;
            }


            // Return formated string
            return dms;
        }

        public static double StrToDouble(string num)
        {
            double val = 0.0;
            Double.TryParse(num,out val);
            return val;
        }

        public static void  getSunRiseSunset(DateTime dDate,double lat,double longit,out string sunriseStr,out string sunsetStr)
        {


            double iZone = Convert.ToDouble(ApplicationState.Instance.getDoubleUtcOffset());
            double JulianDay = GetJulianDay(dDate, iZone);
            getJulianCetury(dDate,JulianDay,lat,longit,iZone,out sunriseStr,out sunsetStr);

        }

        public static double GetJulianDay(DateTime dDate, double iZone)
        {

                double fJD;
                double iGreg;
                double fA;
                double fB;
                double fC;
                double fD;
                
                double iYear = dDate.Year;
                double iMonth = dDate.Month;
                double iDay = dDate.Day;
                double iHour = dDate.Hour;
                double iMinute = dDate.Minute;
                double iSecond = dDate.Second;

                double fFrac = iDay + ((iHour + (iMinute / 60) + (iSecond / 60 / 60)) / 24);

                if (iYear < 1582)
                    iGreg = 0;
                else
                    iGreg = 1;

                if ((iMonth == 1) || (iMonth == 2))
                {
                    iYear = iYear - 1;
                    iMonth = iMonth + 12;
                }
                fA = (long)Math.Floor(iYear / 100);
                fB = (2 - fA + (long)Math.Floor(fA / 4)) * iGreg;

                if (iYear < 0)
                    fC = (int)Math.Floor((365.25 * iYear) - 0.75);
                else
                    fC = (int)Math.Floor(365.25 * iYear);
                fD = (int)Math.Floor(30.6001 * (iMonth + 1));
                fJD = fB + fC + fD + 1720994.5;
                fJD = fJD + fFrac;
                return fJD;

    }

    public static void getJulianCetury(DateTime dtDate,double JulianDay,double latitude,double longitude,double timeZone,
                                out string sunriseStr,out string sunsetStr)
    {
        DateTime sunrise = dtDate;
        DateTime sunset = dtDate;
        double julianCentury = (JulianDay - 2451545) / 36525;
        double meanLongSun = (280.46646 + julianCentury * (36000.76983 + julianCentury * 0.0003032) % 360);
        double meanAnomSun = 357.52911 + julianCentury * (35999.05029 - 0.0001537 * julianCentury);
        double earthEcceOrbit = 0.016708634 - julianCentury * (0.000042037 + 0.0000001267 * julianCentury);
        double sunEqnofCtr = Math.Sin(deg2rad(meanAnomSun)) * (1.914602 - julianCentury * (0.004817 + 0.000014 * julianCentury)) + Math.Sin(deg2rad(2 * meanAnomSun)) * (0.019993 - 0.000101 * julianCentury) + Math.Sin(deg2rad(3 * meanAnomSun)) * 0.000289;
        double sunTrueLong = meanLongSun + sunEqnofCtr;
        double sunTrueAnom = meanAnomSun + sunEqnofCtr;
        double sunRadVect = (1.000001018 * (1 - earthEcceOrbit * earthEcceOrbit)) / (1 + earthEcceOrbit * Math.Cos(deg2rad(sunTrueAnom)));
        double sunAppLong = sunTrueLong - 0.00569 - 0.00478 * Math.Sin(deg2rad(125.04 - 1934.136 * julianCentury));
        double meanObliqeEcclip = 23 + (26 + ((21.448 - julianCentury * (46.815 + julianCentury * (0.00059 - julianCentury * 0.001813)))) / 60) / 60;
        double obliqeCorrection = meanObliqeEcclip + 0.00256 * Math.Cos(deg2rad(125.04 - 1934.136 * julianCentury));
        double sunRightAscn = rad2deg(Math.Atan2(Math.Cos(deg2rad(obliqeCorrection)) * Math.Sin(deg2rad(sunAppLong)), Math.Cos(deg2rad(sunAppLong))));
        double sunDeclnDeg = rad2deg(Math.Asin(Math.Sin(deg2rad(obliqeCorrection)) * Math.Sin(deg2rad(sunAppLong))));
        double varY = Math.Tan(deg2rad(obliqeCorrection / 2)) * Math.Tan(deg2rad(obliqeCorrection / 2));
        double equationOfTime = 4 * rad2deg(varY * Math.Sin(2 * deg2rad(meanLongSun)) - 2 * earthEcceOrbit * Math.Sin(deg2rad(meanAnomSun)) + 4 * earthEcceOrbit * varY * Math.Sin(deg2rad(meanAnomSun)) * Math.Cos(2 * deg2rad(meanLongSun)) - 0.5 * varY * varY * Math.Sin(4 * deg2rad(meanLongSun)) - 1.25 * earthEcceOrbit * earthEcceOrbit * Math.Sin(2 * deg2rad(meanAnomSun)));
        double haSunrise = rad2deg(Math.Acos(Math.Cos(deg2rad(90.833)) / (Math.Cos(deg2rad(latitude)) * Math.Cos(deg2rad(sunDeclnDeg))) - Math.Tan(deg2rad(latitude)) * Math.Tan(deg2rad(sunDeclnDeg))));
        int solarNoon = Convert.ToInt32(((720 - 4 * longitude - equationOfTime + timeZone * 60) / 1440)*24*60);
        TimeSpan solarNoonTime = TimeSpan.FromMinutes(solarNoon);
        int sunRise = Convert.ToInt32((((720 - 4 * longitude - equationOfTime + timeZone * 60) - haSunrise*4)/1440)*24*60);
        TimeSpan sunRiseTime = TimeSpan.FromMinutes(sunRise);
        sunriseStr = sunRiseTime.ToString();
        int sunSet = Convert.ToInt32((((720 - 4 * longitude - equationOfTime + timeZone * 60) + haSunrise * 4) / 1440) * 24 * 60);
        TimeSpan sunSetTime = TimeSpan.FromMinutes(sunSet);
        sunsetStr = sunSetTime.ToString();
        //MessageBox.Show(DateTime.Today + " " + "SunRise: " + sunRiseTime + " " + "SunSet: " + sunSetTime.ToString());
       

}

    }
}
