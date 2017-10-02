using System.Collections.Generic;
using System.IO;

namespace RecklassRekkids.Process
{
    public interface ITextFileReaderService
    { 
        List<string> ReadTextFile(string filePath);


    }
    public class TextFileReaderService : ITextFileReaderService
    {

        public List<string> ReadTextFile(string filePath)
        {
            List<string> contractString = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        contractString.Add(line);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException(ex.Message);
            }
            return contractString;
        }
    }
}
