using System.Web;
using TB.Kutuphane.Entity;

namespace TB.Kutuphane.Common
{
    public static class SessionManager
    {
        private const string CurrentSessionKey = "CurrentSession";
        public const string DisabledUpgradeModalKey = "DisabledUpgradeModal";
        private const int SessionTimeout = 20;

        public static Uye CurrentSession
        {
            get
            {
                var context = HttpContext.Current;
                if (context == null) return null;
                if (context.Session == null) return null;
                var sessionIdentity = context.Session[CurrentSessionKey] as Uye;
                return sessionIdentity;
            }
            set
            {
                HttpContext.Current.Session.Timeout = SessionTimeout;
                HttpContext.Current.Session[CurrentSessionKey] = value;
            }
        }
    }
}
