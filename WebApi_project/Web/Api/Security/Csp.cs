using System;
using System.Web;

namespace Api.Security
{
    /// <summary>
    /// Content Security Policy.
    /// </summary>
    public class Csp
    {
        private static readonly Lazy<string> _cspRule = new Lazy<string>(GetCspString);
        private static string CspString => _cspRule.Value;

        /// <summary>
        /// CSP specifies rules on loading static files: recommended is loading all only from app server. 
        /// </summary>
        public static void ApplyCsp(HttpContext context)
        {
            // Apply CSP header defined by W3C Specs as standard header, used by Chrome version 25 and later, Firefox version 23 and later, Opera version 19 and later
            context.Response.Headers.Add("Content-Security-Policy", CspString);
            // Apply header used by Firefox until version 23, and Internet Explorer version 10
            context.Response.Headers.Add("X-Content-Security-Policy", CspString);
            // Apply header used by Chrome until version 25
            context.Response.Headers.Add("X-WebKit-CSP", CspString);
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        public static string GetCspString()
        {
            // 'default-src: self' allows specifying white list (all static files, AJAX connections, iframes, media etc., which are not coming from the server hosting app, are by default not allowed to be loaded)
            return $"default-src 'self'; {GetCspStyleSheetRule()}; {GetCspFrameRule()}; {GetCspScriptRule()}; {GetCspImagesRule()}; {GetCspFontsRule()}";
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        public static string GetCspStyleSheetRule()
        {
            return "style-src 'self'";
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        public static string GetCspFontsRule()
        {
            return "font-src 'self'";
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        public static string GetCspFrameRule()
        {
            return "frame-src 'self'";
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        public static string GetCspScriptRule()
        {
            return "script-src 'self'";
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        public static string GetCspImagesRule()
        {
            return "img-src 'self'";
        }
    }
}