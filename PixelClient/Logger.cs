using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelClient
{
    public class Logger
    {
        public string filePath;
        public Logger(string FilePath)
        {
            this.filePath = FilePath;
        }
        public void Log(string message)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath,true))
            {
                streamWriter.WriteLine($"{DateTime.Now} : {message}");
                streamWriter.Close();
            }
        }
    }
}
