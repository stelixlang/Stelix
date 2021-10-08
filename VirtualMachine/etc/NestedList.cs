using System.Collections.Generic;
using System.Linq;

namespace VirtualMachine.etc
{
    public class NestedList<X> where X: class
    {
        public List<X> RawList { get; }
        public X Cursor { get; private set; }

        public NestedList()
        {
            RawList = new List<X>();
        }

        public void Pass(X element)
        {
            RawList.Add(element);
            Cursor = element;
        }

        public void Eject()
        {
            RawList.RemoveAt(RawList.Count - 1);
            if (RawList.Count == 0)
            {
                Cursor = null;
                return;
            }
            
            Cursor = RawList.Last();
        }


    }
}