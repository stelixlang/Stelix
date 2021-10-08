using System;
using System.Collections.Generic;
using VirtualMachine.project.section;

namespace VirtualMachine.project.model
{
    public class ClassModel : Model
    {
       public ClassModel(string location, string name) : base(location, name, ModelType.CLASS)
        {
            
        }
        
    }
}