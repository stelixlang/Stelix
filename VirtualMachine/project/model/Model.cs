using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VirtualMachine.project.section;
using VirtualMachine.utils;

namespace VirtualMachine.project.model
{
    public class Model : Section
    {
        public string Name { get; protected set; }
        public string Location { get; protected set; }
        public ModelType ModelModelType { get; }
        
        // maybe change object
        public Dictionary<uint, Dictionary<string, object>> Variables { get; } = new Dictionary<uint, Dictionary<string, object>>();

        public List<IFunction> Functions { get; } = new List<IFunction>(); // dictinatory<stirng, Function> ?
        
        public List<string> DeclaredVariables { get; } = new List<string>(); //change this to dictinatory<string, model> maybe
        public Model(string location, string name, ModelType modelModelType)
        {
            Location = location;
            Name = name;
            ModelModelType = modelModelType;
        }
        
        //todo: maybe change binaryreader to something better (like some custom stack)
        public uint CreateInstance(BinaryReader binaryReader = null)
        {
            uint id = RandomUtils.NextUint(uint.MaxValue);
            
            
            Dictionary<string, object> variables = new Dictionary<string, object>();
            foreach (var varName in DeclaredVariables)
            {
                variables.Add(varName, null);
            }

            Variables.Add(id, variables);
            
            
            
            Init(binaryReader, id);
            
            
            return id;
        }
        
        public virtual void Init(BinaryReader binaryReader, uint pointer)
        {
            //TODO: Maybe change construct function
            
            /* finds class construct method / public Example() { } */
            IFunction function = Functions.FirstOrDefault(x => x.Name == "*");

            if (function == null)
            {
                return;
            }

            if (function.GetType().IsInstanceOfType(typeof(NativeFunction)))
            {
                Stack nativeFunctionStack = new Stack();
                nativeFunctionStack.Push(binaryReader);
                function.Execute(nativeFunctionStack, this, pointer);
                return;
            }
            
            Stack stack = new Stack(((Function)function).Arguments.Values.ToList(), binaryReader);
            /* execute construct method */
            function.Execute(stack,  this, pointer);
        }

        public enum ModelType : byte
        {
            CLASS,
            ENUM,
            STRUCT,
            MARKER
        }
    }
}