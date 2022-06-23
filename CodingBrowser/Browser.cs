using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F = System.Windows.Forms;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using System.IO;
using WebDriverManager.Helpers;
using WebDriverManager.DriverConfigs;

namespace CodingBrowser
{
    public class Browser
    {

        IWebDriver driver;

        private Browser(string url)
        {
            // Download/Update Driver
         //  var c = new FirefoxConfig();

           // var dm  = new DriverManager();

         //   var driverUrl = UrlHelper.BuildUrl(c.GetUrl64(), c.GetLatestVersion());

           // dm.SetUpDriver(driverUrl, @"C:\temp");

           // SetUpDriver(new FirefoxConfig());

            // Start Session
            driver = new FirefoxDriver(@"c:\temp\firefox");
      
            // Go to Url
            driver.Navigate().GoToUrl(url);

            //Load cookie
            CookieManager.LoadFromFile(driver.Manage().Cookies);

            //Refresh
            driver.Navigate().Refresh();
        }

        private static Browser instance;


        public string SetUpDriver(IDriverConfig config)
        {

            var architecture = ArchitectureHelper.GetArchitecture();

            string version = config.GetLatestVersion();
            var url = architecture.Equals(Architecture.X32) ? config.GetUrl32() : config.GetUrl64();
            url = UrlHelper.BuildUrl(url, version);
            var binaryPath = @"c:\temp\Firefox\";  
            return new DriverManager().SetUpDriver(url, binaryPath);

        }

        public static Browser Start()
        {

            return Start("https://www.codingame.com/ide/challenge/green-circle");

        }

        public static Browser Start(string url)
        {
            if (instance == null)
            {
                instance = new Browser(url);
            }            
                return instance;
        }

        public static void Shutdown()
        {
            if (instance != null)
                instance.driver.Quit();
        }
   

        public void SendCode(string code) {

            var el = driver.FindElement(By.ClassName("monaco-scrollable-element"));
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
        public void LaunchTest() {
            var el = driver.FindElement(By.ClassName("replay"));
            if (el != null)
            {
                Console.WriteLine("clik");
                el.Click();
            }
        }
        public void Submitcode() { throw new NotImplementedException(); }

        public string RetrieveCode()
        {
            var el = driver.FindElement(By.ClassName("monaco-scrollable-element"));
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
