﻿using System;
using System.IO;

namespace GP.Log
{
    public class Logger
    {
        public void WriteLog(string logMessage)
        {
            StreamWriter logFile = File.AppendText("Log.txt");
            logFile.WriteLine(DateTime.Now + " - " + logMessage);
            logFile.Close();
        }
    }
}
