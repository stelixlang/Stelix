using System.Collections;
using System.IO;
using VirtualMachine.project.model;

namespace VirtualMachine.project.section
{
    public interface IFunction
    {
        public string Name { get; }
        public void Execute(Stack stack, Model model, uint instance);
    }
}