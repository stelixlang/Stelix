using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using VirtualMachine.etc;
using VirtualMachine.parser;
using VirtualMachine.parser.impl;
using VirtualMachine.project;
using VirtualMachine.project.model;
using VirtualMachine.project.section;

namespace VirtualMachine
{
    public class Interpeter
    {
        public SFile CurrentFile { get; private set; }
        
        public Model CurrentModel { get; set; }
        
        public StelixProject Project { get; }

        public NestedList<Section> NestedSections { get; } = new NestedList<Section>();

        private InstructionParser _instructionParser = new InstructionParser();
        private Dictionary<StelixCodes, IParser> _parsers = new Dictionary<StelixCodes, IParser>()
        {
            { StelixCodes.MODEL, new ModelParser() },
            { StelixCodes.END_MODEL, new EndModelParser() },
            { StelixCodes.CR_FUNC, new FunctionCreateParser() },
            { StelixCodes.END_FUNC, new FunctionEndParser() },
        };

        public Interpeter(StelixProject project)
        {
            Project = project;
        }
        

        public void Parse(List<SFile> files)
        {
            foreach (SFile file in files)
            {
                BinaryReader mainBinary = file.Binary;
                CurrentFile = new SFile(file.FileName, mainBinary);

                try
                {
                    while (true)
                    {
                        StelixCodes stelixCode = (StelixCodes) mainBinary.ReadByte();
                  
                        if ((byte) stelixCode > (byte)StelixCodes.INSTRUCTION_START_ID)
                        {
                            _instructionParser.Parse((byte)stelixCode, ref mainBinary, this);
                            continue;
                        }
                        IParser parser = _parsers[stelixCode];
                        parser.Parse(ref mainBinary, this);
                    }
                }
                catch (EndOfStreamException ex)
                {
                    // excepted
                }
            }
        }
    }
}