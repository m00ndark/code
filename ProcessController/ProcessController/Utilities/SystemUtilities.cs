using System.Linq;
using System.Management;

namespace ProcessController.Utilities
{
    public static class SystemUtilities
    {
        public static bool OSIsWindowsSeven()
        {
            bool osIsWindowsSeven = false;
            ManagementObjectSearcher mgmntObjSearcher = new ManagementObjectSearcher("SELECT * FROM  Win32_OperatingSystem");
            try
            {
                ManagementObject managementObj = mgmntObjSearcher.Get().Cast<ManagementObject>().FirstOrDefault();
                if (managementObj != null)
                {
                    object osCaption = managementObj.GetPropertyValue("Caption");
                    osIsWindowsSeven = osCaption.ToString().StartsWith("Microsoft Windows 7");
                }
            }
            catch { }
            return osIsWindowsSeven;
        }
    }
}