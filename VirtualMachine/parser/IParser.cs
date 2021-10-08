using System.IO;

namespace VirtualMachine.parser
{
    public interface IParser
    {
        public void Parse(ref BinaryReader reader, Interpeter interpeter);
    }
}