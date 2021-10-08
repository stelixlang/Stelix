using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using VirtualMachine.project.instruction;
using VirtualMachine.project.model;

namespace VirtualMachine.project.section
{
    public class Section
    {
        public List<Instruction> Instructions { get; } = new List<Instruction>();
        public Dictionary<string, Section> InnerSections { get; } = new Dictionary<string, Section>();

        // maybe add name?
        
        /* todo: maybe change binaryreader to something better like custom stack 2 */
        public virtual void Execute(Stack stack, Model model, uint instance) // todo maybe stack?
        {
            foreach (Instruction instruction in Instructions)
            {
                instruction.Execute();
            }
        }
    }
}