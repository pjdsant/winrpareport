using Microsoft.Win32;
using System;
using System.Globalization;

namespace WinRPAReport.Resource
{
    public class RegistryManager
    {
        public string ReadRegistryClicks()
        {
            var result = "0";
            if (ReadRegistryEnabled())
            {   
                var subKey = @"PJSIT\" + GetDataToday();
                RegistryKey key = null;
                try
                {
                    key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + subKey, true);

                    if (key != null)
                    {
                        if (key.GetValue("Clicks") == null)
                        {
                            key.SetValue("Clicks", "0");
                        }

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
            }
            return result;
        }


        public void WriteRegistryClicks()
        {
            if (ReadRegistryEnabled())
            {
                int clicks;
                var subKey = @"PJSIT\" + GetDataToday();
                RegistryKey key = null;

                try
                {
                    key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + subKey, true);

                    if (key != null)
                    {
                        if(key.GetValue("Clicks") == null)
                        {
                            key.SetValue("Clicks", "0");
                        }

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

        public bool ReadRegistryEnabled()
        {
            var result = false;
            var subKey = @"PJSIT";
            RegistryKey key = null;

            try
            {
                key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + subKey, true);

                if (key != null)
                {
                   
                    if (key.GetValue("EnabledCountClicks") == null)
                    {
                        key.SetValue("EnabledCountClicks", "False");
                    }
                    
                    if (key.GetValue("EnabledCountClicks").ToString().ToUpper() == "TRUE")
                    {
                        result = true;
                    }
                    key.Close();
                }
                else
                {
                    key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + subKey, true);
                    key.SetValue("EnabledCountClicks", "True");
                    result = true;
                    key.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public void SetAlarm(string strAlarme)
        {
            var subKey = @"PJSIT";
            RegistryKey key = null;

            try
            {
                key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + subKey, true);

                if (key != null)
                {
                    key.SetValue("LastAlarmMessage", strAlarme);
                    key.Close();
                }
                else
                {
                    key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + subKey, true);
                    key.SetValue("LastAlarmMessage", "");
                    key.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
