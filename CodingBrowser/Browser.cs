using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using F = System.Windows.Forms;

namespace CodingBrowser
{
    public class Browser
    {
        private WebDriver driver;

        private Thread InstanceCaller;

        private Browser(string url)
        {
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CodinGameExtension");
            DriverManager dm = new DriverManager(appDataPath);
            string binary = dm.SetUpDriver(new FirefoxConfig());
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(Path.GetDirectoryName(binary));
#if !DEBUG
            service.HideCommandPromptWindow = true;
#endif
            driver = new FirefoxDriver(service, new FirefoxOptions
            {
                //option Eager => DOM access is ready, but other resources like images may still be loading
                PageLoadStrategy = PageLoadStrategy.Eager,
            });
            driver.Navigate().GoToUrl(url);

            if (CookieManager.CookieFileExist())
            {
                CookieManager.LoadFromFile(driver.Manage().Cookies);
                driver.Navigate().Refresh();
            }

            InstanceCaller = new Thread(new ThreadStart(CookieChecker));
            InstanceCaller.Start();
        }

        private void CookieChecker()
        {
            string url = "";
            while (true)
            {
                try
                {
                    if (IsCodingGameOpen())
                    {
                        if (driver.Url != url)
                        {
                            url = driver.Url;
                            CheckCookie();
                        }
                    }
                }
                catch 
                {
                }
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Create cookie if user logged in
        /// Remove cookie if user logged out
        /// Need to be launch frequently (every 300ms)
        /// </summary>
        public void CheckCookie()
        {
            if (IsCodingGameOpen())
            {
                if (ClassExist(Element.BUTTON_LOGIN))
                {
                    if (CookieManager.CookieFileExist())
                        CookieManager.DeleteCookieFile();
                }
                else
                {
                    if (!CookieManager.CookieFileExist())
                        CookieManager.SaveToFile(driver.Manage().Cookies);
                }
            }
        }

        public bool IsCodingGameOpen()
        {
            try
            {
                return driver.Url.IndexOf("codingame.com", StringComparison.OrdinalIgnoreCase) > 0;
            }
            catch (WebDriverException)
            {
                return false;
            }
        }


        public bool ClassExist(string className)
        {
            if (!IsCodingGameOpen()) return false;

            var elements = driver.FindElements(By.ClassName(className));
            return elements.Any();

        }

        private static Browser instance;

        public static Browser Start()
        {
            return Start("https://www.codingame.com/");
        }

        public static Browser Start(string url)
        {
            if (instance == null)
            {
                instance = new Browser(url);
            }
            return instance;
        }

        public static void Refresh()
        {
            if (instance != null)
            {
                instance.driver.Navigate().Refresh();
            }
        }

        public static void Shutdown()
        {
            if (instance != null)
            {
                instance.InstanceCaller.Abort();
                instance.driver.Quit();
            }
        }

        public void SendCode(string code)
        {
            if (CanSendCode())
            {
                var el = driver.FindElement(By.ClassName(Element.ZONE_CODE));
                if (el != null)
                {
                    var body = driver.FindElement(By.TagName("BODY"));
                    el.Click();
                    body.SendKeys(Keys.LeftControl + "a");
                    var backup = F.Clipboard.GetDataObject();
                    F.Clipboard.SetText(code);
                    body.SendKeys(Keys.LeftControl + "v");
                    el.Click();
                    F.Clipboard.SetDataObject(backup);
                }
            }
        }

        public bool CanSendCode()
        {
            return ClassExist(Element.ZONE_CODE);
        }

        public bool CanLaunchTest()
        {
            return IsEnabledButton(Element.BUTTON_PLAY);
        }

        public bool IsEnabledButton(string b)
        {
            if (ClassExist(b))
            {
                var el = driver.FindElement(By.ClassName(b));
                return el.Enabled;
            }
            else
                return false;
        }

        public void LaunchTest()
        {
            if (IsEnabledButton(Element.BUTTON_REPLAY))
            {
                PushButton(Element.BUTTON_REPLAY);
            }
            else if (IsEnabledButton(Element.BUTTON_PLAY))
            {
                PushButton(Element.BUTTON_PLAY);
            }
        }

        private void PushButton(string b)
        {
            var el = driver.FindElement(By.ClassName(b));
            if (el != null)
            {
                Console.WriteLine("clik b");
                el.Click();
            }
        }

        public void Submitcode() { throw new NotImplementedException(); }

        /// <summary>
        /// Not used
        /// </summary>
        /// <returns></returns>
        public string RetrieveCode()
        {
            var el = driver.FindElement(By.ClassName(Element.ZONE_CODE));
            if (el != null)
            {

                var body = driver.FindElement(By.TagName("BODY"));
                el.Click();
                body.SendKeys(Keys.LeftControl + "a");
                var backup = F.Clipboard.GetDataObject();
                body.SendKeys(Keys.LeftControl + "c");
                el.Click();
                var result = F.Clipboard.GetText().ToString();
                F.Clipboard.SetDataObject(backup);
                return result;
            }
            return "";
        }
    }
}
