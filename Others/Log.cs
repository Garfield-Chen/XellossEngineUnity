﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace tyoEngineEditor
{
    public class Log
    {
        private string logFile;
        private StreamWriter writer;
        private FileStream fileStream = null;

        public Log()
        {
            logFile = System.IO.Directory.GetCurrentDirectory() + "\\logDir\\log" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            CreateDirectory(logFile);
        }

        public void log(string info)
        {

            try
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(logFile);
                if (!fileInfo.Exists)
                {
                    fileStream = fileInfo.Create();
                    writer = new StreamWriter(fileStream);
                }
                else
                {
                    fileStream = fileInfo.Open(FileMode.Append, FileAccess.Write);
                    writer = new StreamWriter(fileStream);
                }
                writer.WriteLine(DateTime.Now + ": " + info);

            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
        }

        public void CreateDirectory(string infoPath)
        {
            DirectoryInfo directoryInfo = Directory.GetParent(infoPath);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }
    }
}