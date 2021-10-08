using System;
using System.Collections;
using System.IO;

namespace VirtualMachine.project.instruction.impl
{
    public class TestInstruction : Instruction
    {
        private string text;
        public TestInstruction(BinaryReader binaryReader) : base(binaryReader)
        {
            text = data.ReadString();
            binaryReader.ReadByte(); // EX
        }
        public override void Execute()
        {
            Console.WriteLine(text);
        }
    }
}