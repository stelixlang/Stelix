using System;
using System.IO;
using VirtualMachine.project.instruction;
using VirtualMachine.project.instruction.impl;
using VirtualMachine.project.section;

namespace VirtualMachine.parser.impl
{
    public class InstructionParser
    {
        public void Parse(byte instructionId, ref BinaryReader reader, Interpeter interpeter)
        {
            Section section = interpeter.NestedSections.Cursor;
            //section.Instructions.Add(new CallFunctionInstruction(reader));
            section.Instructions.Add((Instruction)Activator.CreateInstance(Instruction.All[instructionId], reader));
            reader.ReadByte(); // EX
        }
    }
}