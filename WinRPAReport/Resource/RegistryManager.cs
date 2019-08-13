using Microsoft.Win32;

namespace WinRPAReport.Resource
{
    public class RegistryManager
    {
        public string ReadRegistry()
        {
            var result = "";

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\PJSIT");

            if (key != null)
            {
                result = key.GetValue("Clicks").ToString();
            }

            return result;
        }
    }
}
