using System.IO;

namespace VirtualMachine.parser.impl
{
    public class EndModelParser : IParser
    {
        public void Parse(ref BinaryReader reader, Interpeter interpeter)
        {
            interpeter.CurrentModel = null;
            interpeter.NestedSections.Eject();
            reader.ReadByte(); // EX
        }
    }
}