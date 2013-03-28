using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinRTCore.Utilities.Storage
{
    public abstract class IndexedElement
    {
        public abstract string Name { get; }

        public abstract string Path { get; }
    }
}
