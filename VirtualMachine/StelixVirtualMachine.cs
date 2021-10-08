using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using VirtualMachine.parser;
using VirtualMachine.project;
using VirtualMachine.project.model;
using VirtualMachine.project.section;

namespace VirtualMachine
{
    public class StelixVirtualMachine
    {
        private static List<IParser> parsers = new List<IParser>();
        public static List<StelixProject> Projects { get; } = new List<StelixProject>();

        public static IFunction FindFunction(string className, string functionName)
        {
            foreach (StelixProject project in Projects)
            {
                IFunction function = project.FindFunction(className, functionName);
                if (function != null)
                {
                    return function;
                }
            }
            return null;
        }
        
        public static StelixProject ParseProject(ZipArchive zipArchive)
        {
            List<SFile> files = new List<SFile>(zipArchive.Entries.Count);
            
            foreach (ZipArchiveEntry entry in zipArchive.Entries)
           
            {
                SFile file = new SFile(entry.FullName, new BinaryReader(entry.Open()));
                files.Add(file);
            }
            //todo: find name from meta file
            StelixProject stelixProject = new StelixProject("test");
            StelixVirtualMachine.Projects.Add(stelixProject);
            Interpeter interpeter = new Interpeter(stelixProject);
            interpeter.Parse(files);
            return stelixProject;
        }
    }
}