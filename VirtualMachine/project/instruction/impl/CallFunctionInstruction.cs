using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using VirtualMachine.natives.attr;
using VirtualMachine.project.model;
using VirtualMachine.project.section;

namespace VirtualMachine.project.instruction.impl
{
    public class CallFunctionInstruction : Instruction
    {
        private IFunction function;
        private Model model;
        private uint instance;
        private Stack stack;
        
        public CallFunctionInstruction(BinaryReader binaryReader) : base(binaryReader)
        {
            string modelName = binaryReader.ReadString();
            string funcName = binaryReader.ReadString();
            instance = binaryReader.ReadUInt32();
            
            // todo: change this 
            model = StelixVirtualMachine.Projects[0].FindModel(modelName);
            if (model != null)
            {
                // todo: maybe change this to a Dictionary?
                function = model.Functions.FirstOrDefault(x => x.Name == funcName);
                if (function.GetType() == (typeof(Function)))
                {
                    stack = new Stack(((Function) function).Arguments.Values.ToList());
                }
                else
                {
                    NativeFunction nativeFunction = (NativeFunction) function;
                    NATIVE_FUNC nativeFuncAttribute = nativeFunction._method.GetCustomAttribute<NATIVE_FUNC>();
                    stack = new utils.Stack(nativeFuncAttribute.Types, binaryReader);
                }
            }



        }
        public override void Execute()
        {
            function.Execute(stack, model, instance);
        }
        
    }
}