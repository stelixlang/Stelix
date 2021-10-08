using System;
using System.Collections.Generic;
using System.Linq;

namespace VirtualMachine.natives.attr
{
    public class NATIVE_FUNC : Attribute
    {
        public List<Type> Types { get; }
        public NATIVE_FUNC(params Type[] types)
        {
            Types = types.ToList();
        }
    }
}