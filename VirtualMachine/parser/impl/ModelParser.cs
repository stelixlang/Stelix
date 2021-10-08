using System;
using System.IO;
using VirtualMachine.project.model;
using VirtualMachine;

namespace VirtualMachine.parser.impl
{
    public class ModelParser : IParser
    {
        public ModelParser()
        {}

        
        public void Parse(ref BinaryReader reader, Interpeter interpeter)
        {
            byte type = reader.ReadByte();
            string name = reader.ReadString();
            string location = interpeter.CurrentFile.Location;
            
            

            reader.ReadByte(); // EX
            switch (type)
            {
                case (byte)Model.ModelType.CLASS:
                {
                    interpeter.CurrentModel = new ClassModel(location, name);
                    interpeter.Project.LoadedModels.Add(location.Length != 0 ? location + "/" + name : name, interpeter.CurrentModel);
                    interpeter.NestedSections.Pass(interpeter.CurrentModel);
                    return;
                }
            }
            

            Model model = new Model(location, name, (Model.ModelType) type);
            interpeter.CurrentModel = model;
            interpeter.NestedSections.Pass(model);
            interpeter.Project.LoadedModels.Add(location.Length != 0 ? location + "/" + name : name, interpeter.CurrentModel);
        }
    }
}