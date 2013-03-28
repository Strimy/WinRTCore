using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace WinRTCore.Utilities.Storage
{
    public class IndexedFolder : IndexedElement
    {


        public IndexedFolder()
        {
            
        }

        public StorageFolder Retrieve()
        {
            return null;
        }

        public override string Name
        {
            get { throw new NotImplementedException(); }
        }

        public override string Path
        {
            get { throw new NotImplementedException(); }
        }
    }
}
