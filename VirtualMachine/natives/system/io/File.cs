using System.Collections;
using VirtualMachine.natives.attr;
using VirtualMachine.project.model;

namespace VirtualMachine.natives
{
    public class File
    {
        [NATIVE_FUNC(typeof(string))]
        public static void Create(Stack stack, Model model, uint pointer)
        {
            System.IO.File.Create((string) stack.Pop());   
        }

        [NATIVE_FUNC(typeof(string))]
        public static void Delete(Stack stack, Model model, uint pointer)
        {
            System.IO.File.Delete((string)stack.Pop());
        }

        [NATIVE_FUNC(typeof(string), typeof(string))]
        public static void Write(Stack stack, Model model, uint pointer)
        {
            string text = (string) stack.Pop();
            string name = (string) stack.Pop();
            System.IO.File.WriteAllText(name, text);
        }
        
        
        
    }
}