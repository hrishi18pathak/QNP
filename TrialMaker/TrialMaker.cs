using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.IO;
using Microsoft.VisualBasic;
using System.Windows.Forms;

namespace SoftwareLocker
{
    // Activate Property
    public class TrialMaker
    {
        #region -> Private Variables 

        private string _BaseString;
        private string _Password;
        private string _SoftName;
        private string _RegFilePath;
        private string _HideFilePath;
        private int _DefDays;
        private int _Runed;
        private string _Text;
        private string _Identifier;

        #endregion

        #region -> Constructor 

        /// <summary>
        /// Make new TrialMaker class to make software trial
        /// </summary>
        /// <param name="SoftwareName">Name of software to make trial</param>
        /// <param name="RegFilePath">File path to save password(enrypted)</param>
        /// <param name="HideFilePath">file path for saving hidden information</param>
        /// <param name="Text">A text for contacting to you</param>
        /// <param name="TrialDays">Default period days</param>
        /// <param name="TrialRunTimes">How many times user can run as trial</param>
        /// <param name="Identifier">3 Digit string as your identifier to make password</param>
        public TrialMaker(string SoftwareName,
            string RegFilePath, string HideFilePath,
            string Text, int TrialDays, int TrialRunTimes,
            string Identifier)
        {
            _SoftName = SoftwareName;
            _Identifier = Identifier;

            SetDefaults();

            _DefDays = TrialDays;
            _Runed = TrialRunTimes;

            _RegFilePath = RegFilePath;
            _HideFilePath = HideFilePath;
            _Text = Text;
        }

        private void SetDefaults()
        {
            SystemInfo.UseBaseBoardManufacturer = false;
            SystemInfo.UseBaseBoardProduct = true;
            SystemInfo.UseBiosManufacturer = false;
            SystemInfo.UseBiosVersion = true;
            SystemInfo.UseDiskDriveSignature = true;
            SystemInfo.UsePhysicalMediaSerialNumber = false;
            SystemInfo.UseProcessorID = true;
            SystemInfo.UseVideoControllerCaption = false;
            SystemInfo.UseWindowsSerialNumber = false;

            MakeBaseString();
            MakePassword();
        }

        #endregion

        // Make base string (Computer ID)
        private void MakeBaseString()
        {
            try
            {
                _BaseString = Encryption.Boring(Encryption.InverseByBase(SystemInfo.GetSystemInfo(_SoftName), 10));
            }
            catch (Exception ex)
            {
            }
        }

        private void MakePassword()
        {
            _Password = Encryption.MakePassword(_BaseString, _Identifier);
        }

        /// <summary>
        /// Show registering dialog to user
        /// </summary>
        /// <returns>Type of running</returns>
        public Tuple<RunTypes,string> ShowDialog()
        {
            // check if registered before
            if (CheckRegister() == true)
                return new Tuple<RunTypes,string>(RunTypes.Full, string.Empty);

            frmDialog PassDialog = new frmDialog(_BaseString, _Password, DaysToEnd(), _Runed, _Text);
            
            MakeHideFile();

            DialogResult DR = PassDialog.ShowDialog();

            if (DR == System.Windows.Forms.DialogResult.OK)
            {
                MakeRegFile();
                //Store the timestamp when the application was last opened
                ModifyTimeStampFile();
            
                return new Tuple<RunTypes,string>(RunTypes.Full, PassDialog.RegisteredClientName);
            }
            else if (DR == DialogResult.Retry)
            {
                //Store the timestamp when the application was last opened
                ModifyTimeStampFile();
                return new Tuple<RunTypes,string>(RunTypes.Trial,PassDialog.RegisteredClientName);
            }
            else
            {
                //Store the timestamp when the application was last opened
                ModifyTimeStampFile();
                return new Tuple<RunTypes,string>(RunTypes.Expired, PassDialog.RegisteredClientName);
            }
        }

        private void ModifyTimeStampFile()
        {
            string TSFileName = Path.GetDirectoryName(_HideFilePath) + "\\TS" + _SoftName + ".tsmp";
            string FileContents = string.Empty;
            if (File.Exists(TSFileName))
            {
                FileContents = FileReadWrite.ReadFile(TSFileName);        
            }

            FileContents += DateTime.Now.Ticks + ",";
            FileReadWrite.WriteFile(TSFileName, FileContents);
        }

        // save password to Registration file for next time usage
        private void MakeRegFile()
        {
            FileReadWrite.WriteFile(_RegFilePath, _Password);
        }

        // Control Registeration file for password
        // if password saved correctly return true else false
        private bool CheckRegister()
        { 
            string Password = FileReadWrite.ReadFile(_RegFilePath);

            if (_Password == Password)
                return true;
            else
                return false;
        }

        // from hidden file
        // indicate how many days can user use program
        // if the file does not exists, make it
        private int DaysToEnd()
        {
            FileInfo hf = new FileInfo(_HideFilePath);
            if (hf.Exists == false)
            {
                MakeHideFile(true);
                return _DefDays;
            }
            return CheckHideFile();
        }

        // store hidden information to hidden file
        // Date,DaysToEnd,HowManyTimesRuned,BaseString(ComputerID)
        private void MakeHideFile(bool isFirstTimeCreation=false)
        {
            string HideInfo = string.Empty;
            if (isFirstTimeCreation)
            {
                HideInfo = DateTime.Now.Ticks + ";";
                HideInfo += _DefDays + ";" + _Runed + ";" + _BaseString;
                HideInfo += ";" + DateTime.Now.Add(TimeSpan.FromDays(_DefDays)).Ticks;
                FileReadWrite.WriteFile(_HideFilePath, HideInfo);
            }
            else
            {
                //first read the contents of the file
                string fileContents = FileReadWrite.ReadFile(_HideFilePath);
                // then keep the installation ticks and license expiration ticks constant
                string[] HideInfoParts = fileContents.Split(';');
                HideInfo = HideInfoParts[0] + ";";
                HideInfo += _DefDays + ";" + _Runed + ";" + _BaseString;
                HideInfo += ";" + HideInfoParts[4];
                FileReadWrite.WriteFile(_HideFilePath, HideInfo);
            }
        }

        // Get Data from hidden file if exists
        private int CheckHideFile()
        {
            string[] HideInfo;
            string clockManipulationDetectedFile = Path.GetDirectoryName(_HideFilePath) + "\\ClockManipulationDetected";
            string fileContents = FileReadWrite.ReadFile(_HideFilePath);
            if (File.Exists(clockManipulationDetectedFile))
            {
                _Runed = 0;
                _DefDays = 0;
                return 0;
            }

            HideInfo = fileContents.Split(';');
            long DiffDays;
            int DaysToEnd;

            string TSFileName = Path.GetDirectoryName(_HideFilePath) + "\\TS" + _SoftName + ".tsmp";
            if (ClockManipulationDetector.DetectClockManipulation(TSFileName))
            {
                File.Create(clockManipulationDetectedFile);
                _Runed = 0;
                _DefDays = 0;
                return 0;
            }

            if (_BaseString == HideInfo[3])
            {
                DaysToEnd = Convert.ToInt32(HideInfo[1]);
                if (DaysToEnd <= 0)
                {
                    _Runed = 0;
                    _DefDays = 0;
                    return 0;
                }

                DateTime dt = new DateTime(Convert.ToInt64(HideInfo[0]));
                DiffDays = DateAndTime.DateDiff(DateInterval.Day,
                    dt.Date, DateTime.Now.Date,
                    FirstDayOfWeek.Saturday,
                    FirstWeekOfYear.FirstFullWeek);
                
                DaysToEnd = Convert.ToInt32(HideInfo[1]);
                _Runed = Convert.ToInt32(HideInfo[2]);
                _Runed -= 1;

                DiffDays = Math.Abs(DiffDays);

                _DefDays = DaysToEnd - Convert.ToInt32(DiffDays);
            }
            return _DefDays;
        }

        public enum RunTypes
        { 
            Trial = 0,
            Full,
            Expired,
            UnKnown
        }

        #region -> Properties 

        /// <summary>
        /// Indicate File path for storing password
        /// </summary>
        public string RegFilePath
        {
            get
            {
                return _RegFilePath;
            }
            set
            {
                _RegFilePath = value;
            }
        }

        /// <summary>
        /// Indicate file path for storing hidden information
        /// </summary>
        public string HideFilePath
        {
            get
            {
                return _HideFilePath;
            }
            set
            {
                _HideFilePath = value;
            }
        }

        /// <summary>
        /// Get default number of days for trial period
        /// </summary>
        public int TrialPeriodDays
        {
            get
            {
                return _DefDays;
            }
        }

        /// <summary>
        /// Get or Set TripleDES key for encrypting files to save
        /// </summary>
        public byte[] TripleDESKey
        {
            get
            {
                return FileReadWrite.key;
            }
            set
            {
                FileReadWrite.key = value;
            }
        }

        #endregion

        #region -> Usage Properties 

        public bool UseProcessorID
        {
            get
            {
                return SystemInfo.UseProcessorID;
            }
            set
            {
                SystemInfo.UseProcessorID = value;
            }
        }

        public bool UseBaseBoardProduct
        {
            get
            {
                return SystemInfo.UseBaseBoardProduct;
            }
            set
            {
                SystemInfo.UseBaseBoardProduct = value;
            }
        }

        public bool UseBaseBoardManufacturer
        {
            get
            {
                return SystemInfo.UseBiosManufacturer;
            }
            set
            {
                SystemInfo.UseBiosManufacturer = value;
            }
        }

        public bool UseDiskDriveSignature
        {
            get
            {
                return SystemInfo.UseDiskDriveSignature;
            }
            set
            {
                SystemInfo.UseDiskDriveSignature = value;
            }
        }

        public bool UseVideoControllerCaption
        {
            get
            {
                return SystemInfo.UseVideoControllerCaption;
            }
            set
            {
                SystemInfo.UseVideoControllerCaption = value;
            }
        }

        public bool UsePhysicalMediaSerialNumber
        {
            get
            {
                return SystemInfo.UsePhysicalMediaSerialNumber;
            }
            set
            {
                SystemInfo.UsePhysicalMediaSerialNumber = value;
            }
        }

        public bool UseBiosVersion
        {
            get
            {
                return SystemInfo.UseBiosVersion;
            }
            set
            {
                SystemInfo.UseBiosVersion = value;
            }
        }

        public bool UseBiosManufacturer
        {
            get
            {
                return SystemInfo.UseBiosManufacturer;
            }
            set
            {
                SystemInfo.UseBiosManufacturer = value;
            }
        }

        public bool UseWindowsSerialNumber
        {
            get
            {
                return SystemInfo.UseWindowsSerialNumber;
            }
            set
            {
                SystemInfo.UseWindowsSerialNumber = value;
            }
        }

        #endregion
    }
}