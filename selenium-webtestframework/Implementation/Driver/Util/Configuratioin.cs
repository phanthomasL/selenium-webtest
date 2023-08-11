 using System.Drawing;

 namespace selenium_webtestframework.Implementation.Driver.Util; 

 public class Configuration
 { 
     public string ApplicationUrl { get; }
     public Size BrowserWindowSize { get; }
     public string BrowserType { get; }
     public string AnmeldeUrl { get; }
        


     public Configuration(string applicationUrl, Size browserWindowSize, string browserType, string anmeldeUrl)
     {
          
         ApplicationUrl = applicationUrl;
          
         BrowserWindowSize = browserWindowSize;
         BrowserType = browserType;
         AnmeldeUrl = anmeldeUrl;
            

     }
 }