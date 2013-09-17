using System.IO;
using System.Text;
using NVelocity;
using NVelocity.App;

namespace SmartElk.ElkMate.Messaging.Template
{
    public class VelocityTemplate : ITemplate
    {
        private const string DefaultObjectNameTemplate = "param{0}";
        private readonly string _fileName;
        private readonly VelocityEngine _velocityEngine;

        public VelocityTemplate(string fileName, VelocityEngine velocityEngine)
        {
            _fileName = fileName;
            _velocityEngine = velocityEngine;
        }

        #region ITemplate Members

        public virtual string Name
        {
            get { return Path.GetFileNameWithoutExtension(_fileName); }
        }

        public string Evaluate(params object[] parts)
        {
            var ctx = new VelocityContext();
            for (int i = 0; i < parts.Length; i++)
            {
                ctx.Put(string.Format(DefaultObjectNameTemplate, i == 0 ? string.Empty : (i + 1).ToString()), parts[i]);
            }

            ctx.Put("Template", Name);

            using (var stringWriter = new StringWriter())
            {
                _velocityEngine.GetTemplate(Path.GetFileName(_fileName), Encoding.UTF8.BodyName)
                               .Merge(ctx, stringWriter);
                return stringWriter.GetStringBuilder().ToString();
            }
        }

        #endregion
    }
}