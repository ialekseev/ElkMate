using System.Text.RegularExpressions;
using System.Web;
using System.Linq;

namespace SmartElk.ElkMate.Web.Modules
{
    public class SeoUrlModule : IHttpModule
    {
        private readonly string[] _exceptionalPathsForCaseFormatting= {"static"};
        
        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, args) => this.RedirectIfNeeded(HttpContext.Current);            
        }

        private void RedirectIfNeeded(HttpContext context)
        {
           //if (!context.Request.IsLocal)
           {
               DoWwwFormatting(context);
               DoTrailingSlashFormatting(context);
               DoCaseFormatting(context);
           }           
        }

        private readonly static Regex WwwRegex = new Regex("(http|https)://www\\.", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public void DoWwwFormatting(HttpContext context)
        {            
            var url = context.Request.Url;
            var urlHasWww = WwwRegex.IsMatch(url.ToString());
            if (urlHasWww)
            {
                var newUrl = WwwRegex.Replace(url.ToString(), string.Format("{0}://", url.Scheme));
                RedirectPermanently(context, newUrl);
            }            
        }

        public void DoTrailingSlashFormatting(HttpContext context)
        {
            var path = context.Request.Url.AbsolutePath;  
            if (path.Length > 1)
            {
                if (path[path.Length - 1] == '/')
                    RedirectPermanently(context, path.Remove(path.Length - 1) + context.Request.Url.Query);                                    
            }                            
        }

        public void DoCaseFormatting(HttpContext context)
        {
            var path = context.Request.Url.AbsolutePath;
            var lowerPath = path.ToLower();
            
            var isExceptionalPathForCaseFormatting = _exceptionalPathsForCaseFormatting.Any(lowerPath.Contains);
            if (!isExceptionalPathForCaseFormatting && Regex.IsMatch(path, @"[A-Z]"))
            {
                RedirectPermanently(context, path.ToLower()  + context.Request.Url.Query);
            }
        }

        private static void RedirectPermanently(HttpContext context, string url)
        {
            context.Response.StatusCode = 301;
            context.Response.Status = "301 Moved Permanently";
            context.Response.RedirectLocation = url;
            context.Response.End();
        }
        
        public void Dispose()
        {
        }
    }
}
