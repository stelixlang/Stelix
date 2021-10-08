using System.IO;

namespace VirtualMachine.project
{
    public class SFile
    {
        public string FileName { get; }
        public BinaryReader Binary { get; }

        public string Location
        {
            get
            {
                return Path.GetDirectoryName(FileName);
            }
        }

        public SFile(string fileName, BinaryReader binary)
        {
            FileName = fileName;
            Binary = binary;
        }
    }
}