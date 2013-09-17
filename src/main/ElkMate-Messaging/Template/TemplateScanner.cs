using System.Collections.Generic;
using System.IO;

namespace SmartElk.ElkMate.Messaging.Template
{
    public abstract class TemplateScanner
    {
        private DirectoryInfo _directory;

        protected TemplateScanner(DirectoryInfo directory)
        {
            _directory = directory;
        }

        public virtual DirectoryInfo Directory
        {
            get { return _directory; }
            protected set { _directory = value; }
        }


        public abstract IEnumerable<ITemplate> Scan(string searchPattern = "*.*");
    }
}