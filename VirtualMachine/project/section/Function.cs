using System;
using System.Collections;
using System.Collections.Generic;
using VirtualMachine.project.model;

namespace VirtualMachine.project.section
{
    public class Function : Section, IFunction
    {
        public string Name { get; }

        public Model Declared
        {
            get;
            set;
        }

        public Dictionary<string, Type> Arguments { get; } = new Dictionary<string, Type>();

        public Function(string name)
        {
            Name = name;
        }
        

        public Function(string name, List<KeyValuePair<string, Type>> arguments) : this(name)
        {
            foreach (var keyValuePair in arguments)
            {
                Arguments.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }
    }
}