using System;
using System.Collections;
using System.IO;
using System.Reflection;
using VirtualMachine.project.model;

namespace VirtualMachine.project.section
{
    public class NativeFunction : IFunction
    {
        public MethodInfo _method { get; }

        public string Name { get; }
        public NativeFunction(string name, MethodInfo method)
        {
            Name = name;
            _method = method;
        }

        public void Execute(Stack stack, Model model, uint instance)
        {
            _method.Invoke(null, new Object[]{stack, model, instance}); 
        }
        
    }
}