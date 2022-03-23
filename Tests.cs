using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace ClassLibrary1
{
    [TestClass]
    public class Tests
    {
        private IWebDriver driver;
        private string basUrl;


        [TestInitialize]
        public void SetUp()
        {
            //открыть сайт на новом окне

            this.driver = new ChromeDriver();
            
            this.basUrl = "https://meteo.paraplan.net/en/";

            this.driver.Navigate().GoToUrl(this.basUrl);
            this.driver.Manage().Window.Maximize();

        }

        [TestMethod]
        public void Test()
        {
            // 3.2 нажать на ссылку Five-day weather forecast 
            this.driver.FindElement(By.XPath("//a[text()='Five-day weather forecast']")).Click();

            // 3.3 убедится что есть текст Five-day weather forecast

            Assert.IsNotNull(isElementExists(By.XPath("//a[text()='Five-day weather forecast']")));

            //3.4  убедится что на странице есть ссылка Skew-T log-P diagram
            // 3.5  переходим по ссылке 
            if (isElementExists(By.LinkText("Skew-T log-P diagram")) == true)
            {
                this.driver.FindElement(By.LinkText("Skew-T log-P diagram")).Click();
            }

            // 3.6  убеждаемся что переход состоялся

            string diagramUrl = "https://meteo.paraplan.net/en/forecast/saint-petersburg/aerological_diagram/";
            string current_Url = this.driver.Url;

            Assert.AreEqual(current_Url  , diagramUrl);

        }


        private bool isElementExists(By text)
        {
            try
            {
                driver.FindElement(text);
                return true;
            }
            catch(NoSuchElementException)
            {

                return false;
            }


        }



        [TestCleanup]
        public void CleanUp()
        {
            this.driver.Close();
            this.driver.Quit();
        }


    }
}
