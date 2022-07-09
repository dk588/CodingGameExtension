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

        private static string Cookie_File { get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\CodinGameExtension\Cookies.json"; 

            } }

        internal static void SaveToFile(ICookieJar cookieJar)
        {
            var cookieSerialized = JsonConvert.SerializeObject(cookieJar.AllCookies.Where(c=>c.Domain.IndexOf("codingame.com", StringComparison.OrdinalIgnoreCase)>0));

            File.WriteAllText(Cookie_File, cookieSerialized);
        }

        internal static void LoadFromFile(ICookieJar cookieJar)
        {
            string jsonString = File.ReadAllText(Cookie_File);
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

        internal static bool CookieFileExist()
        {
            return File.Exists(Cookie_File);
        }

        internal static void DeleteCookieFile()
        {
            if(CookieFileExist())
                File.Delete(Cookie_File);

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
