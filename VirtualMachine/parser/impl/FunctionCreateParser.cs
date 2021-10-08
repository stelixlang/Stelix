using System;
using System.Collections.Generic;
using System.IO;
using VirtualMachine.project.model;
using VirtualMachine.project.section;

namespace VirtualMachine.parser.impl
{
    public class FunctionCreateParser : IParser
    {
        public void Parse(ref BinaryReader reader, Interpeter interpeter)
        {
            string returnType = reader.ReadString();
            string name = reader.ReadString();
            string argumentsRaw = reader.ReadString();
            
            reader.ReadByte(); // EX
            
            Function function = new Function(name, new List<KeyValuePair<string, Type>>());
            
            // if (interpeter.CurrentModel.GetType() == typeof(ClassModel))
            // {
                function.Declared = interpeter.CurrentModel;
                interpeter.CurrentModel.Functions.Add(function);
            // }
            
            interpeter.NestedSections.Pass(function);
        }
    }
}