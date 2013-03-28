using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace WinRTCore.Utilities
{
    public class IndexedStorage
    {
        private StorageFolder m_refBaseFolder;
        

        public IndexedStorage()
            : this("")
        {
        }

        public IndexedStorage(string root)
        {

        }
    }
}
