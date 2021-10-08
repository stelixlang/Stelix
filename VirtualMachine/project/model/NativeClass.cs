using System.Collections.Generic;
using VirtualMachine.project.section;

namespace VirtualMachine.project.model
{
    public class NativeClass : NativeModel
    {
        
        public NativeClass(string location, string name) : base(location, name, ModelType.CLASS)
        {
        }
    }
}