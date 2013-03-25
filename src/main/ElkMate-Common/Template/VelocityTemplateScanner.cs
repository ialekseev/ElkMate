using System.Collections.Generic;
using System.IO;
using System.Linq;
using Commons.Collections;
using NVelocity.App;

namespace ElkMate.Common.Template
{
    public class VelocityTemplateScanner : TemplateScanner
    {
        private readonly VelocityEngine _velocityEngine;

        public VelocityTemplateScanner(DirectoryInfo directory) : base(directory)
        {
            _velocityEngine = new VelocityEngine();
            var props = new ExtendedProperties();
            props.SetProperty("file.resource.loader.path", directory.FullName);
            props.SetProperty("file.resource.loader.modificationCheckInterval", "15");
           
            _velocityEngine.Init(props);
        }

// ReSharper disable OptionalParameterHierarchyMismatch
        public override IEnumerable<ITemplate> Scan(string searchPattern = "*.vm")
// ReSharper restore OptionalParameterHierarchyMismatch
        {
            return Directory.GetFiles(searchPattern).Select(file => new VelocityTemplate(file.FullName, _velocityEngine)).ToList().Cast<ITemplate>().ToList();
        }
    }
}