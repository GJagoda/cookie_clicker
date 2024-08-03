using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.IO;
using System.Runtime.ExceptionServices;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
    static void Main(string[] args)
    {
        string geckoDriverPath = "geckodriver.exe";
        string url = "https://orteil.dashnet.org/cookieclicker/";
        FirefoxOptions options = new FirefoxOptions();
        //options.AddArgument("--headless");
        IWebDriver driver = new FirefoxDriver(geckoDriverPath, options);
        driver.Navigate().GoToUrl(url);

        /*IWebElement button = driver.FindElement(By.XPath("//button[@aria-label='Consent']"));
        button.Click();
        button = driver.FindElement(By.Id("langSelect-EN"));
        button.Click();*/


        

        int firstCookies = 150;
        int cookies = 0;
        int product = 25;
        int reqCookie = 1100;

        first(driver);
        cookieClick(driver, firstCookies);
        //buttonClickXpath(driver, "/html/body/div[3]/svg");
        Thread.Sleep(TimeSpan.FromSeconds(2));

        for(int i = 1; i <= product; i++) {
            prodClick(driver, cookies, reqCookie, i);

            Console.WriteLine(i);
            Console.WriteLine("Ciastka"+numOfCookies(driver));
        }

        //driver.Quit();
    }
    static int f(string str)
    {
        string cleanedStr = str.Replace(",", "");
        int result;
        if (int.TryParse(cleanedStr, out result))
        {
            return result;
        }
        else
        {
            throw new FormatException("The input string cannot be converted to an integer.");
        }
    }

    static void prodClick(IWebDriver driver, int cookies, int reqCookie, int i){
        double cookieModifier = 1.5;
        Console.WriteLine(i);
        
        if (i == 1){
            for(int j = 0; cookies<520; j++){
                cookieClick(driver, 100);
                try
                {
                    buttonClickXpath(driver, "//*[@id=\"product1\"]");
                }
                catch
                {
                    Console.WriteLine("Error with product " + i);
                }
                cookies = numOfCookies(driver);
            }
        }
        while (cookies < i * reqCookie * Math.Pow(3, i-1)){
            Console.WriteLine("Ciastka: " + numOfCookies(driver) + " < " + i * reqCookie * Math.Pow(2, i - 1));
            try
            {
                buttonClickXpath(driver, "//*[@id=\"product" + i + "\"]");
            }
            catch{
                Console.WriteLine("Error with product " + i);
                i--;
            }
            cookies = numOfCookies(driver);
        }
        
    }
    static int numOfCookies(IWebDriver driver){
        int cookie = 0;
        string pageTitle = driver.Title;
        int spaceIndex = pageTitle.IndexOf(' ');
        if (spaceIndex != -1){
            string partOfTitle = pageTitle.Substring(0, spaceIndex);
            cookie = f(partOfTitle);
            return cookie;

        }
        return -1;
        
    }


    static void cookieClick(IWebDriver driver,int n){
        int i = 0;
        while (i < n) {
            buttonClickId(driver, "bigCookie");
            i++;
        }
    }
    static void buttonClickId(IWebDriver driver, string id){

        IWebElement button;
        try
        {
            button = driver.FindElement(By.Id(id));
        }
        catch
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
            button = driver.FindElement(By.Id(id));
        }
        button.Click();
    }
    static void buttonClickXpath(IWebDriver driver, string xpath){
        IWebElement button;
        try{
            button = driver.FindElement(By.XPath(xpath));
            button.Click();
        }
        catch{
            Thread.Sleep(TimeSpan.FromSeconds(2));
            try{
                button = driver.FindElement(By.XPath(xpath));
                button.Click();
            }
            catch{
                Console.WriteLine("Problem with xpath: "+xpath);
            }
        }
    }

    static void first(IWebDriver driver)
    {
        buttonClickXpath(driver, "//button[@aria-label='Consent']");
        Thread.Sleep(TimeSpan.FromSeconds(2));
        buttonClickId(driver, "langSelect-EN");
        buttonClickXpath(driver, "/html/body/div[1]/div/a[1]");
        buttonClickXpath(driver, "//img[@aria-label='Close']");
        buttonClickXpath(driver, "/html/body/div/div[2]/div[18]/div[1]/div[1]/div");//*[@id="numbersButton"]
        buttonClickXpath(driver, "//*[@id=\"numbersButton\"]");
        buttonClickXpath(driver, "//*[@id=\"notisButton\"]");
        buttonClickXpath(driver, "//*[@id=\"fancyGraphicsButton\"]");
        buttonClickXpath(driver, "/html/body/div/div[2]/div[18]/div[1]/div[1]/div");

    }
}
