using Microsoft.Win32;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace WinRPAReport.Resource
{
    public class RegistryManager
    {
        public string ReadRegistryClicks(Form form)
        {
            var result = "0";
            if (ReadRegistryEnabled(form))
            {   
                var subKey = @"PJSIT\" + GetDataToday(form);
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
                catch (Exception ex)
                {
                    SetAlarm("ReadRegistryClicks --> " + ex.Message.ToString() , form);
                    throw;
                }
            }
            return result;
        }

        internal void ClearAlarm(string v, Form1 form1)
        {
            throw new NotImplementedException();
        }

        public void WriteRegistryClicks(Form form)
        {
            if (ReadRegistryEnabled(form))
            {
                int clicks;
                var subKey = @"PJSIT\" + GetDataToday(form);
                RegistryKey key = null;

                try
                {
                    key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + subKey, true);

                    if (key != null)
                    {
                        //if (key.GetValue("Clicks1").ToString() == "ze")
                        //{

                        //}

                        if (key.GetValue("Clicks") == null)
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
                catch (Exception ex)
                {
                    SetAlarm("WriteRegistryClicks --> " + ex.Message.ToString(), form);
                    //throw;
                }
            }
        }

        public string GetDataToday(Form form)
        {
            try
            {
                var dateToday = DateTime.Now.ToShortDateString();
                return DateTime.ParseExact(dateToday, "yyyy-MM-dd", CultureInfo.CurrentCulture).ToString("yyyyMMdd");
            }
            catch (Exception ex )
            {
                SetAlarm("GetDataToday --> " + ex.Message.ToString(), form );
                throw;
            }
        }

        public bool ReadRegistryEnabled(Form form)
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
            catch (Exception ex)
            {
                SetAlarm("ReadRegistryEnabled --> " + ex.Message.ToString(), form);
            }

            return result;
        }

        public void SetAlarm(string strAlarme , Form form)
        {
            var subKey = @"PJSIT";
            RegistryKey key = null;

            if (strAlarme != "Clear")
            {

                form.BackColor = System.Drawing.Color.Red;

                //Form1.DefaultBackColor = Color.FromArgb(204, 0, 0); //change to red

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
}
