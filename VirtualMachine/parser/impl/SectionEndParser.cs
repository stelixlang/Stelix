using System.IO;

namespace VirtualMachine.parser.impl
{
    public class SectionEndParser : IParser
    {
        public void Parse(ref BinaryReader reader, Interpeter interpeter)
        {
            reader.ReadByte(); // EX
            interpeter.NestedSections.Eject();
        }
    }
}