using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Reflection.Metadata;
using System.Threading;
using Xamarin.Essentials;

namespace TestProjekt
{
    public class Tests
    {
        IWebDriver webDriver;

        [SetUp]
        public void Setup()
        {
            webDriver = new ChromeDriver();
            webDriver.Manage().Window.Maximize();
            webDriver.Navigate().GoToUrl("https://hcomp.azurewebsites.net/");
        }

        public void Login()
        {

            webDriver.FindElement(By.Id("mainbtn")).Click();
            Thread.Sleep(500);
            IWebElement Login = webDriver.FindElement(By.Id("Input_Email"));
            Login.SendKeys("pmach@email.com");

            IWebElement Pass = webDriver.FindElement(By.Id("Input_Password"));
            Pass.SendKeys("Pawel123!");
            webDriver.FindElement(By.CssSelector(".btn.btn-primary.btn-block")).Click();
          

        }

        [Test]
        public void TestLogowanie()
        {
  
            webDriver.FindElement(By.Id("mainbtn")).Click();

            IWebElement Login = webDriver.FindElement(By.Id("Input_Email"));
            Login.SendKeys("pmach@email.com");

            IWebElement Pass = webDriver.FindElement(By.Id("Input_Password"));
            Pass.SendKeys("Pawel123!");

            Thread.Sleep(1500);
            webDriver.FindElement(By.CssSelector(".btn.btn-primary.btn-block")).Click();


        }

        [Test]
        public void TestErrorInvalidLoginLogowanie()
        {
           
            webDriver.FindElement(By.Id("mainbtn")).Click();
            IWebElement Login = webDriver.FindElement(By.Id("Input_Email"));
            Login.SendKeys("bledny@email.com");

            IWebElement Pass = webDriver.FindElement(By.Id("Input_Password"));
            Pass.SendKeys("test123");
            Thread.Sleep(1000);
            webDriver.FindElement(By.CssSelector(".btn.btn-primary.btn-block")).Click();
            Thread.Sleep(2000);

            String wartosc_oczekiwana = "Invalid login attempt.";

            IWebElement m = webDriver.FindElement(By.CssSelector(".text-danger.validation-summary-errors"));
            String wartosc_zwracana = m.Text;
            Assert.AreEqual(wartosc_oczekiwana, wartosc_zwracana);

            Thread.Sleep(1500);
        }


        [Test]
        public void TestRejestracja()
        {
            Login();

            IWebElement el1=webDriver.FindElement(By.XPath("//*[@id='register']"));
            el1.Click();
            IWebElement el2 = webDriver.FindElement(By.XPath("//*[@id='Input_FirstName']"));
            el2.SendKeys("Test1");
            IWebElement el3= webDriver.FindElement(By.XPath("//*[@id='Input_LastName']"));
            el3.SendKeys("Test1");
            IWebElement el4= webDriver.FindElement(By.XPath("//*[@id='Input_Email']"));
            el4.SendKeys("test600@email.com");
            IWebElement el5= webDriver.FindElement(By.XPath("//*[@id='Input_Password']"));
            el5.SendKeys("Test123!");
            IWebElement el6= webDriver.FindElement(By.XPath("//*[@id='Input_ConfirmPassword']"));
            el6.SendKeys("Test123!");
            Thread.Sleep(1000);
            IWebElement el7= webDriver.FindElement(By.XPath("/html/body/div/main/div/form/button"));
            el7.Click();

            webDriver.FindElement(By.XPath("//*[@id='logout']")).Click();

            webDriver.FindElement(By.Id("mainbtn")).Click();

            IWebElement elTestLog = webDriver.FindElement(By.Id("Input_Email"));
            elTestLog.SendKeys("test600@email.com");

            IWebElement elTestPass = webDriver.FindElement(By.Id("Input_Password"));
            elTestPass.SendKeys("Test123!");

            Thread.Sleep(1500);
            webDriver.FindElement(By.CssSelector(".btn.btn-primary.btn-block")).Click();

        }


        [Test]
        public void TestErrorEmptyFieldRejestracja()
        {
            Login();

            IWebElement el2 = webDriver.FindElement(By.XPath("//*[@id='register']"));
            el2.Click();
            IWebElement el3 = webDriver.FindElement(By.XPath("//*[@id='Input_FirstName']"));
            el3.SendKeys("Test1");
            IWebElement el4 = webDriver.FindElement(By.XPath("//*[@id='Input_LastName']"));
            el4.SendKeys("Test1");
            IWebElement el5 = webDriver.FindElement(By.XPath("//*[@id='Input_Email']"));
            //element5.SendKeys("test3@email");
            IWebElement el6 = webDriver.FindElement(By.XPath("//*[@id='Input_Password']"));
            el6.SendKeys("Test123!");
            IWebElement el7 = webDriver.FindElement(By.XPath("//*[@id='Input_ConfirmPassword']"));
            el7.SendKeys("Test123!");
            Thread.Sleep(1000);
            IWebElement el8 = webDriver.FindElement(By.XPath("/html/body/div/main/div/form/button"));
            el8.Click();

            Thread.Sleep(2000);

            IWebElement ERROR = webDriver.FindElement(By.CssSelector(".text-danger.validation-summary-errors"));

            String wartosc_oczekiwana = "The Email field is required.";

            IWebElement zw = webDriver.FindElement(By.CssSelector(".text-danger.validation-summary-errors"));
            String wartosc_zwracana = zw.Text;
            Assert.AreEqual(wartosc_oczekiwana, wartosc_zwracana);

            Thread.Sleep(1500);

        }

        [Test]
        public void TestErrorEmailTakenRejestracja()
        {
            Login();

            IWebElement el2 = webDriver.FindElement(By.XPath("//*[@id='register']"));
            el2.Click();
            IWebElement el3 = webDriver.FindElement(By.XPath("//*[@id='Input_FirstName']"));
            el3.SendKeys("Test1");
            IWebElement el4 = webDriver.FindElement(By.XPath("//*[@id='Input_LastName']"));
            el4.SendKeys("Test1");
            IWebElement el5 = webDriver.FindElement(By.XPath("//*[@id='Input_Email']"));
            el5.SendKeys("test3@email");
            IWebElement el6 = webDriver.FindElement(By.XPath("//*[@id='Input_Password']"));
            el6.SendKeys("Test123!");
            IWebElement el7 = webDriver.FindElement(By.XPath("//*[@id='Input_ConfirmPassword']"));
            el7.SendKeys("Test123!");
            Thread.Sleep(1000);
            IWebElement el8 = webDriver.FindElement(By.XPath("/html/body/div/main/div/form/button"));
            el8.Click();

            Thread.Sleep(2000);
            //TEST WALIDACJI
            IWebElement ERROR = webDriver.FindElement(By.CssSelector(".text-danger.validation-summary-errors"));

            String wartosc_oczekiwana = "is already taken.";
  
            IWebElement zw = webDriver.FindElement(By.CssSelector(".text-danger.validation-summary-errors"));
            String wartosc_zwracana = zw.Text; 

            Assert.IsTrue(wartosc_zwracana.Contains(wartosc_oczekiwana));

            Thread.Sleep(1500);

        }


        [Test]
        public void TestDodawanie()
        {
            Login();

            webDriver.FindElement(By.CssSelector(".btn.btn-primary")).Click();

            IWebElement testAdd = webDriver.FindElement(By.Id("Name"));
            testAdd.SendKeys("Testing2022_Produkt");

            IWebElement testAddPic = webDriver.FindElement(By.CssSelector("input[type='file']"));
            testAddPic.SendKeys(@"C:\Users\user4\Documents\istockphoto.jpg");

            IWebElement testAddItem = webDriver.FindElement(By.Id("Quantity"));
            testAddItem.SendKeys("6");

            webDriver.FindElement(By.XPath("//*[@id='mainbody']/div/main/div[1]/div/div/form/div[4]/input")).Click();
            Thread.Sleep(3000);
        }

        [Test]
        public void TestErrorNameDodawanie()
        {
            Login();

            webDriver.FindElement(By.CssSelector(".btn.btn-primary")).Click();

            IWebElement testAdd = webDriver.FindElement(By.Id("Name"));

            IWebElement testAddPic = webDriver.FindElement(By.CssSelector("input[type='file']"));
            testAddPic.SendKeys(@"C:\Users\user4\Documents\istockphoto.jpg");


            IWebElement testAddItem = webDriver.FindElement(By.Id("Quantity"));
            testAddItem.SendKeys("5");

            webDriver.FindElement(By.XPath("//*[@id='mainbody']/div/main/div[1]/div/div/form/div[4]/input")).Click();
            
            Thread.Sleep(1000);
            IWebElement ERROR = webDriver.FindElement(By.XPath("//*[@id='mainbody']/div/main/div[1]/div/div/form/div[1]/span"));

            String wartosc_oczekiwana = "Proszę wskazać nazwę";
            
            IWebElement zw = webDriver.FindElement(By.CssSelector(".text-danger.field-validation-error"));
            String wartosc_zwracana = zw.Text;
            Assert.AreEqual(wartosc_oczekiwana, wartosc_zwracana);

            Thread.Sleep(1500);
        }

        [Test]
        public void TestErrorQuantityDodawanie()
        {
            Login();

            webDriver.FindElement(By.CssSelector(".btn.btn-primary")).Click();

            IWebElement testAdd = webDriver.FindElement(By.Id("Name"));
            testAdd.SendKeys("Test");

            IWebElement testAddPic = webDriver.FindElement(By.CssSelector("input[type='file']"));
            testAddPic.SendKeys(@"C:\Users\user4\Documents\istockphoto.jpg");


            IWebElement testAddItem = webDriver.FindElement(By.Id("Quantity"));
            //testAddItem.SendKeys("");

            webDriver.FindElement(By.XPath("//*[@id='mainbody']/div/main/div[1]/div/div/form/div[4]/input")).Click();

            Thread.Sleep(1000);
            
            IWebElement ERROR = webDriver.FindElement(By.XPath("//*[@id='mainbody']/div/main/div[1]/div/div/form/div[1]/span"));

            String wartosc_oczekiwana = "Proszę dodać liczbę";
           
            IWebElement zw = webDriver.FindElement(By.CssSelector(".text-danger.field-validation-error"));
            String wartosc_zwracana = zw.Text;
            Assert.AreEqual(wartosc_oczekiwana, wartosc_zwracana);

            Thread.Sleep(1500);

        }


        [Test]
        public void TestEditName()
        {
            Login();
            Thread.Sleep(500);
            webDriver.FindElement(By.XPath("//*[@id='mainbody']/div/main/div/table/tbody/tr[1]/td[4]/a[1]")).Click();
            Thread.Sleep(100);
            IWebElement testEdit = webDriver.FindElement(By.Id("Name"));
            testEdit.Clear();
            testEdit.SendKeys("Test Nowa Wartosc Zedytowana");
            webDriver.FindElement(By.CssSelector(".btn.btn-primary")).Click();
            Thread.Sleep(1500);

        }


        [Test]
        public void TestEditPhoto()
        {
            Login();
            Thread.Sleep(500);
            webDriver.FindElement(By.XPath("//*[@id='mainbody']/div/main/div/table/tbody/tr[1]/td[4]/a[1]")).Click();
            Thread.Sleep(100);

            IWebElement testAddPic = webDriver.FindElement(By.CssSelector("input[type='file']"));
            testAddPic.SendKeys(@"C:\Users\user4\Documents\ciscocos7.jpg");

            webDriver.FindElement(By.CssSelector(".btn.btn-primary")).Click();
            Thread.Sleep(2000);
        }

        [Test]
        public void TestErrorNameEdit()
        {
            Login();
            Thread.Sleep(500);
            webDriver.FindElement(By.XPath("//*[@id='mainbody']/div/main/div/table/tbody/tr[1]/td[4]/a[1]")).Click();
            Thread.Sleep(500);
            IWebElement testEdit = webDriver.FindElement(By.Id("Name"));
            testEdit.Clear();
            webDriver.FindElement(By.CssSelector(".btn.btn-primary")).Click();
            Thread.Sleep(1000);

            IWebElement ERROR = webDriver.FindElement(By.XPath("//*[@id='mainbody']/div/main/div[1]/div/div/form/div[1]/span"));

            String wartosc_oczekiwana = "Proszę wskazać nazwę";

            IWebElement zw = webDriver.FindElement(By.CssSelector(".text-danger.field-validation-error"));
            String wartosc_zwracana = zw.Text;
            Assert.AreEqual(wartosc_oczekiwana, wartosc_zwracana);

            Thread.Sleep(1500);
        }

        [Test]
        public void TestErrorQuantityEdit()
        {
            Login();
            Thread.Sleep(500);
            webDriver.FindElement(By.XPath("//*[@id='mainbody']/div/main/div/table/tbody/tr[1]/td[4]/a[1]")).Click();
            Thread.Sleep(500);
            IWebElement testEdit = webDriver.FindElement(By.Id("Quantity"));
            testEdit.Clear();
            webDriver.FindElement(By.CssSelector(".btn.btn-primary")).Click();
            Thread.Sleep(1000);

            IWebElement ERROR = webDriver.FindElement(By.XPath("//*[@id='mainbody']/div/main/div[1]/div/div/form/div[1]/span"));

            String wartosc_oczekiwana = "Proszę dodać liczbę";
           
            IWebElement zw = webDriver.FindElement(By.CssSelector(".text-danger.field-validation-error"));
            String wartosc_zwracana = zw.Text;
            Assert.AreEqual(wartosc_oczekiwana, wartosc_zwracana);

            Thread.Sleep(1500);

        }

        [Test]
        public void TestUsun()
        {
            Login();
            Thread.Sleep(500);

            webDriver.FindElement(By.XPath("//*[@id='mainbody']/div/main/div/table/tbody/tr[4]/td[4]/a[2]")).Click();
            Thread.Sleep(2000);

            webDriver.FindElement(By.XPath("//*[@id='mainbody']/div/main/div/form/input[5]")).Click();

            Thread.Sleep(1200);

        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Close();
            webDriver.Quit();
        }

  
    }
}
