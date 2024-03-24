using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HSBCReward.AppCode.Helpers
{
    public class LogError
    {
         public static void WriteText(string sLogMessage)
        {
            string sLogFile = Directory.GetCurrentDirectory() + ("\\Log\\") + "Log_Syngenta " + DateTime.Now.ToShortDateString().Replace("/", "-") + ".txt";
            try
            {
                if (File.Exists(sLogFile))
                {
                    using (StreamWriter SW = File.AppendText(sLogFile))
                    {
                        SW.WriteLine(DateTime.Now.ToString() + "-" + sLogMessage);
                        SW.Close();
                    }
                }
                else
                {
                    using (StreamWriter SW = File.CreateText(sLogFile))
                    {
                        SW.WriteLine(DateTime.Now.ToString() + "-" + sLogMessage);
                        SW.Close();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}