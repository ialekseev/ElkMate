using System.Collections.Generic;
using System.IO;

namespace ElkMate.Common.Template
{
    public abstract class TemplateScanner
    {
        private DirectoryInfo _directory;

        public virtual DirectoryInfo Directory
        {
            get { return _directory; }
            protected set { _directory = value; }
        }

        protected TemplateScanner(DirectoryInfo directory)
        {
            _directory = directory;
        }

        
        public abstract IEnumerable<ITemplate> Scan(string searchPattern = "*.*");
    }
}