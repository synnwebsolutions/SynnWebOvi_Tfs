using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynnCore.Log
{
    public static class FileLogger
    {
        public static string FileName { get; set; }
        public static string Format { get; set; }

        public static void  Init(string fileName, string messageFormat)
        {
            FileName = fileName;
            Format = messageFormat;
        }

        public static void AddLog(Exception ex)
        {
            if (!string.IsNullOrEmpty(FileName) && !string.IsNullOrEmpty(Format))
            {
                using (var fs = File.Open(FileName, FileMode.OpenOrCreate))
                { }
                //string msg = string.Format("{0} - {1} [{2}]", DateTime.Now.ToString(), ex.Message, ex.StackTrace);
                string msg = string.Format(Format, DateTime.Now.ToString(), ex.Message, ex.StackTrace);
                File.AppendAllText(FileName, msg);
            }
            else
                throw new Exception("File Name Or Format not Initiated - Please Invoke Init Method !");
        }
    }
}
