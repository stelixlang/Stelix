using System;
using System.Collections;
using System.IO;
using VirtualMachine.natives.attr;
using VirtualMachine.project.model;

namespace VirtualMachine.natives
{
    public class Shell
    {
        [NATIVE_FUNC(typeof(string))]
        public static void WriteLn(Stack stack, Model model, uint pointer)
        {
            Console.WriteLine((string)stack.Peek());
        }
        
        [NATIVE_FUNC()]
        public static void Clear(Stack stack, Model model, uint pointer)
        {
            Console.Clear();
        }
        
        [NATIVE_FUNC()]
        public static void ReadKey(Stack stack, Model model, uint pointer)
        {
            Console.ReadKey();
        }
    }
}