using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using VirtualMachine.project;
using VirtualMachine.project.model;

namespace VirtualMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "test.stx";
            if (args.Length == 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        var demoFile = archive.CreateEntry("main.stelix");

                        using (var entryStream = demoFile.Open())
                        using (var binaryWriter = new BinaryWriter(entryStream))
                        {
                            binaryWriter.Write((byte) StelixCodes.MODEL);
                            binaryWriter.Write((byte) Model.ModelType.CLASS);
                            binaryWriter.Write("Main");
                            binaryWriter.Write((byte) StelixCodes.EX);

                            binaryWriter.Write((byte) StelixCodes.CR_FUNC);
                            binaryWriter.Write("void");
                            binaryWriter.Write("start");
                            binaryWriter.Write("TEMP");
                            binaryWriter.Write((byte) StelixCodes.EX);

                            binaryWriter.Write((byte) StelixCodes.INSTRUCTION_CALL_FUNCTION);
                            binaryWriter.Write("system/Shell");
                            binaryWriter.Write("WriteLn");
                            binaryWriter.Write(0);
                            binaryWriter.Write("hello world");
                            binaryWriter.Write((byte) StelixCodes.EX);
                            
                            
                            binaryWriter.Write((byte) StelixCodes.INSTRUCTION_CALL_FUNCTION);
                            binaryWriter.Write("Main");
                            binaryWriter.Write("start");
                            binaryWriter.Write(0);
                            binaryWriter.Write((byte) StelixCodes.EX);
                            
                            binaryWriter.Write((byte) StelixCodes.END_FUNC);
                            binaryWriter.Write((byte) StelixCodes.EX);

                            binaryWriter.Write((byte) StelixCodes.END_MODEL);
                            binaryWriter.Write((byte) StelixCodes.EX);
                        }
                    }


                    using (var fileStream = new FileStream("test.stx", FileMode.Create))
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        memoryStream.CopyTo(fileStream);
                    }
                }
            }
            else
            {
                fileName = args[0];
            }
            
            Console.WriteLine(fileName);




            ZipArchive projectFile = ZipFile.OpenRead(fileName);
            StelixProject project = StelixVirtualMachine.ParseProject(projectFile);


            Model model =
                (ClassModel) project.LoadedModels.First(x => x.Key == "Main").Value;
            model.Functions[0].Execute(null, model, 0);

        }
    }
}