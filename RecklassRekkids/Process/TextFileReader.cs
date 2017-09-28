using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecklassRekkids
{
    public class TextFileReader
    {
        private readonly string _filePath;
        public List<string> ContractString { get; set; }

        public TextFileReader(string filePath)
        {
            ContractString = new List<string>();
            _filePath = filePath;
            ReadTextFile();
        }

        private void ReadTextFile()
        {
            try
            {
                 
                using (StreamReader sr = new StreamReader(_filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        ContractString.Add(line);
                    }

                }
            }
            catch (FileLoadException ex)
            {
                throw new FileLoadException(ex.Message);
            }

        }
    }
}
