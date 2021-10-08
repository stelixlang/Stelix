using System.Collections;
using System.Net;
using VirtualMachine.natives.attr;
using VirtualMachine.project.model;

namespace VirtualMachine.natives.web
{
    public class WebC
    {
        [NATIVE_FUNC(typeof(string))]
        public static string DownloadString(Stack stack, Model model, uint pointer)
        {
            return new WebClient().DownloadString((string)stack.Pop());
        }
    }
}