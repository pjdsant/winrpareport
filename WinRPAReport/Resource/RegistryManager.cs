﻿using Microsoft.Win32;

namespace WinRPAReport.Resource
{
    public class RegistryManager
    {

        public string ReadRegistry()
        {
            var result = "-1";

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\PJSIT");

            if (key != null)
            {
                result = key.GetValue("Clicks").ToString();
                key.Close();
            }

            return result;
        }

        public void WriteRegistry()
        {
            int clicks;

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\PJSIT");

            if (key != null)
            {
                clicks = int.Parse(key.GetValue("Clicks").ToString());

                string clickDone = clicks++.ToString();

                key.SetValue("Clicks", clickDone);
                key.Close();
            }

        }
    }
}
