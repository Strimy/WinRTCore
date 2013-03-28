using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinRTCore.Utilities
{
    public enum IndexMethod
    {
        XML,
        SQL
    }

    public class IndexedStorage
    {
        public string IndexPrefix { get; private set; }
        public IndexMethod IndexingMethod { get; private set; }

        public IndexedStorage()
            : this("IS", IndexMethod.XML)
        {
            
        }

        public IndexedStorage(string indexPrefix, IndexMethod indexingMethod)
        {
            IndexingMethod = indexingMethod;
            IndexPrefix = indexPrefix;

            LoadIndex();
        }

        private void LoadIndex()
        {

        }
    }
}
