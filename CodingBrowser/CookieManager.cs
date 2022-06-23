using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingBrowser
{
    internal static class CookieManager
    {
        private const string COOKIE_FILE = @"c:\temp\Firefox\Cookies.json";

        internal static void SaveToFile(ICookieJar cookieJar)
        {
            var cookieSerialized = JsonConvert.SerializeObject(cookieJar.AllCookies);

            File.WriteAllText(COOKIE_FILE, cookieSerialized);
        }

        internal static void LoadFromFile(ICookieJar cookieJar)
        {
            string jsonString = File.ReadAllText(COOKIE_FILE);
            var cookieCustoms = JsonConvert.DeserializeObject<List<CookieCustom>>(jsonString);

            if (cookieCustoms != null)
                foreach (var c in cookieCustoms)
                    cookieJar.AddCookie(new Cookie(
                    name: c.Name
                    , value: c.Value
                    , domain: c.Domain
                    , path: c.Path
                    , expiry: c.Expiry
                    , secure: c.Secure
                    , isHttpOnly: c.IsHttpOnly
                    , sameSite: c.SameSite));
        }

        private class CookieCustom { 
        public String Name { get; set; }
        public String Value { get; set; }
        public String Domain { get; set; }
        public String Path { get; set; }
        public bool Secure { get; set; }
        public bool IsHttpOnly { get; set; }
        public String SameSite { get; set; }
        public DateTime? Expiry { get; set; }

        }
    }
}
