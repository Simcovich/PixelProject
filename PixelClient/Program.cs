using PixelModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixelClient
{
    class Program
    {
        static LogicManager logic;
        static void Main(string[] args)
        {
            logic = new LogicManager("LogFile.txt");
        }
    }
}
