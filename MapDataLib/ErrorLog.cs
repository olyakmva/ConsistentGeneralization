﻿using System.IO;

namespace MapDataLib
{
    
    public static class ErrorLog
    {
        public static void WriteToLogFile(string msg)
        {
            string fileName = "log.txt";
            FileStream f = new FileStream(fileName ,FileMode.Append,FileAccess.Write);
            using (var sr = new StreamWriter(f))
            {
                sr.WriteLine("{0}", msg);
            }
        }
    }
   
}
