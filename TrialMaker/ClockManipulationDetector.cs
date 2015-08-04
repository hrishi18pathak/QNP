using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SoftwareLocker
{
    class ClockManipulationDetector
    {
        public static bool DetectClockManipulation(DateTime thresholdTime)
        {
            DateTime adjustedThresholdTime = new DateTime(thresholdTime.Year, thresholdTime.Month, thresholdTime.Day, 23, 59, 59);

            EventLog eventLog = new System.Diagnostics.EventLog("system");

            foreach (EventLogEntry entry in eventLog.Entries)
            {
                if (entry.TimeWritten > adjustedThresholdTime)
                    return true;
            }

            return false;
        }


        public static bool DetectClockManipulation(string TSFileName)
        {
            string FileContents = FileReadWrite.ReadFile(TSFileName);
            FileContents = FileContents.Trim(new char[] { ',' });
            IEnumerable<long> timeStamps = string.IsNullOrEmpty(FileContents) ? Enumerable.Empty<long>() : FileContents.Split(',').Select(s => long.Parse(s));
            timeStamps = timeStamps.Concat(new[] {DateTime.Now.Ticks});
            return !timeStamps.Zip(timeStamps.Skip(1), (a, b) => a.CompareTo(b) <= 0).All(b => b);
        }

    }
}


