using System;
using System.Collections.Generic;
using System.IO;
using VirtualMachine.project.model;

namespace VirtualMachine.utils
{
    public class Stack : System.Collections.Stack
    {

        public Stack()
        {
        }
        
        public Stack(List<Type> types, BinaryReader binReader)
        {
            foreach (Type type in types)
            {
                if (type.IsInstanceOfType(typeof(Model)))
                {
                    Model model = (Model) Activator.CreateInstance(type);
                    model.CreateInstance(binReader);
                    Push(model);
                }
                else
                {
                    switch (Type.GetTypeCode(type))
                    {
                        case TypeCode.String:
                        {
                            Push(binReader.ReadString());
                            break;
                        }
                        case TypeCode.Int32:
                        {
                            Push(binReader.ReadInt32());
                            break;
                        }
                        case TypeCode.Int16:
                        {
                            Push(binReader.ReadInt16());   
                            break;
                        }
                        case TypeCode.Int64:
                        {
                            Push(binReader.ReadInt64());
                            break;
                        }
                        case TypeCode.SByte:
                        {
                            Push(binReader.ReadSByte());
                            break;
                        }
                        case TypeCode.Double:
                        {
                            Push(binReader.ReadDouble());
                            break;
                        }
                        case TypeCode.Char:
                        {
                            Push(binReader.ReadChar());
                            break;
                        }
                        case TypeCode.Decimal:
                        {
                            Push(binReader.ReadDecimal());
                            break;
                        }
                        default:
                        {
                            Push(null);
                            break;
                        }
                    }
                }
            }
        }
    }
}