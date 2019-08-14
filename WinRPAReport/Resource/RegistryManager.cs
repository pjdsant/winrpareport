using Microsoft.Win32;
using System;
using System.Globalization;

namespace WinRPAReport.Resource
{
    public class RegistryManager
    {
      
        public string ReadRegistry()
        {
            var result = "0";
            var subKey = @"PJSIT\" + GetDataToday();
            RegistryKey key = null;
            try
            {
                key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + subKey, true);

                if (key != null)
                {
                    result = key.GetValue("Clicks").ToString();
                    key.Close();
                }
                else
                {
                    key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + subKey, true);
                    key.SetValue("Clicks", "0");
                    result = key.GetValue("Clicks").ToString();
                    key.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public void WriteRegistry()
        {
            int clicks;
            var subKey = @"PJSIT\" + GetDataToday();
            RegistryKey key = null;

            try
            {
                key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + subKey, true);

                if (key != null)
                {
                    clicks = int.Parse(key.GetValue("Clicks").ToString());
                    int clickDone = clicks + 1;
                    string clickString = clickDone.ToString();
                    key.SetValue("Clicks", clickString);
                    key.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetDataToday()
        {
            try
            {
                var dateToday = DateTime.Now.ToShortDateString();
                return DateTime.ParseExact(dateToday, "yyyy-MM-dd", CultureInfo.CurrentCulture).ToString("yyyyMMdd");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
