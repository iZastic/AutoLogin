using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLogin
{
    public class Settings
    {
        string ProgramFilesx86()
        {
            if (8 == IntPtr.Size || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }

        public string WowPath { get; set; }
        public bool HasPassword { get; set; }

        public Settings()
        {
            WowPath = ProgramFilesx86() + @"\World of Warcraft";
            HasPassword = false;
        }
    }
}
