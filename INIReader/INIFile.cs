using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace INIReader
{
    public class INIFile
    {
        public Encoding Encoding { get; set; }

        public string Content { get; set; }

        public INIFile()
        { 

        }

        public static INIFile ReadFile(string FilePath)
        {
            INIFile output = new INIFile();
            using (FileStream fs = new FileStream(FilePath, FileMode.Open))
            {
                byte[] Data = new byte[fs.Length];
                fs.Read(Data, 0, Data.Length);
                output.Content = Encoding.UTF8.GetString(Data);
                output.Encoding = Encoding.UTF8;
            }
            return output;
        }
    }
}
