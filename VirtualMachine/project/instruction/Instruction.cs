using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using VirtualMachine.project.instruction.impl;

namespace VirtualMachine.project.instruction
{
    public class Instruction
    {

        protected BinaryReader data;
        public static Dictionary<byte, Type> All { get; } = new Dictionary<byte, Type>() 
        {
            {(byte) StelixCodes.INSTRUCTION_DEBUG, typeof(TestInstruction) },
            {(byte) StelixCodes.INSTRUCTION_CALL_FUNCTION, typeof(CallFunctionInstruction) }
        };
        
        
        public Instruction(BinaryReader binaryReader)
        {
            data = binaryReader;
        }
        

        public virtual void Execute() { }
    }
}