using System.IO;
using VirtualMachine.project.section;

namespace VirtualMachine.parser.impl
{
    public class SectionCreateParser : IParser
    {
        public void Parse(ref BinaryReader reader, Interpeter interpeter)
        {
            string name = reader.ReadString();
            reader.ReadByte(); // EX
            Section section = new Section();
            interpeter.NestedSections.Cursor.InnerSections.Add(name, section);
            interpeter.NestedSections.Pass(section);
        }
    }
}