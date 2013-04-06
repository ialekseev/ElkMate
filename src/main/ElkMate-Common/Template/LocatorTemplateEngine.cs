using System;

namespace ElkMate.Common.Template
{
    public class LocatorTemplateEngine : ITemplateEngine
    {        
	    public Func<string, ITemplate> TemplateFinder { get; protected set; }

		public LocatorTemplateEngine(Func<string, ITemplate> templateFinder)
		{
			TemplateFinder = templateFinder;
		}
        
        #region ITemplateEngine Members

        public virtual ITemplate FindTemplateByName(string name)
        {
            try
            {
	            return TemplateFinder(name);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Template '{0}' wasn't registered", name), exception);
            }
        }

        #endregion
    }
}