using System;
using System.IO;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Wepie.YuqueConverter
{
    public class Converter
    {
        public static void Run(string[] args)
        {
            // 运行前先关闭所有浏览器，再通过命令打开一个debug浏览器，例如：
            // "C:\Program Files\Google\Chrome\Application\chrome.exe" --remote-debugging-port=9222
            var options = new ChromeOptions();
            options.DebuggerAddress = "localhost:9222";
            using (var driver = new ChromeDriver(options))
            {
                HandleThirdInformation(driver);
            }
        }

        private static void HandleThirdInformation(IWebDriver driver)
        {
            HandleHtmlContent(driver, "https://wepie.yuque.com/docs/share/e87695d5-bfec-475f-8c16-fc4b4ff9f3d5?#", @"C:\Workspace\finaltank-home\src\policy\third\official.html", "坦克无敌第三方信息共享清单");
            HandleHtmlContent(driver, "https://wepie.yuque.com/docs/share/058fd1f7-e659-4951-8295-cc43b97c674f?#", @"C:\Workspace\finaltank-home\src\policy\third\M233.html", "坦克无敌第三方信息共享清单");
            HandleHtmlContent(driver, "https://wepie.yuque.com/docs/share/c8922220-8d82-4546-8ba5-5d7a93e6a8cf?#", @"C:\Workspace\finaltank-home\src\policy\third\M4399.html", "坦克无敌第三方信息共享清单");
            HandleHtmlContent(driver, "https://wepie.yuque.com/docs/share/66193558-7f04-4ad2-8aa4-9b304cf0226e?#", @"C:\Workspace\finaltank-home\src\policy\third\BiliBili.html", "坦克无敌第三方信息共享清单");
            HandleHtmlContent(driver, "https://wepie.yuque.com/docs/share/c32c9339-82b2-4dcb-8741-6416fc89cb83?#", @"C:\Workspace\finaltank-home\src\policy\third\AliGame.html", "坦克无敌第三方信息共享清单");
            HandleHtmlContent(driver, "https://wepie.yuque.com/docs/share/6dac359f-e731-4247-8815-77f76ca6f04a?#", @"C:\Workspace\finaltank-home\src\policy\third\Meizu.html", "坦克无敌第三方信息共享清单");
            HandleHtmlContent(driver, "https://wepie.yuque.com/docs/share/dd5ee736-8cb4-4331-b0ad-e159ef27eb6b?#", @"C:\Workspace\finaltank-home\src\policy\third\Xiaomi.html", "坦克无敌第三方信息共享清单");
            HandleHtmlContent(driver, "https://wepie.yuque.com/docs/share/cf24a080-95c9-4284-aeea-718909502079?#", @"C:\Workspace\finaltank-home\src\policy\third\Huawei.html", "坦克无敌第三方信息共享清单");
            HandleHtmlContent(driver, "https://wepie.yuque.com/docs/share/a557105a-77a0-4432-8d8b-e6dea2bb1f5b?#", @"C:\Workspace\finaltank-home\src\policy\third\Vivo.html", "坦克无敌第三方信息共享清单");
            HandleHtmlContent(driver, "https://wepie.yuque.com/docs/share/1c271d56-4a74-4116-9c53-2ce5337511b2?#", @"C:\Workspace\finaltank-home\src\policy\third\Oppo.html", "坦克无敌第三方信息共享清单");
            HandleHtmlContent(driver, "https://wepie.yuque.com/docs/share/a6b65f9d-8a1e-4f01-98ff-ad3c5c079136?#", @"C:\Workspace\finaltank-home\src\policy\third\Yingyongbao.html", "坦克无敌第三方信息共享清单");
            HandleHtmlContent(driver, "https://wepie.yuque.com/docs/share/b5399759-e940-41ca-b359-0073d8a4daff?#", @"C:\Workspace\finaltank-home\src\policy\third\Xiao7.html", "坦克无敌第三方信息共享清单");
            HandleHtmlContent(driver, "https://wepie.yuque.com/docs/share/5b10a600-69ff-456a-a035-741c0431f744?#", @"C:\Workspace\finaltank-home\src\policy\third\Douyin.html", "坦克无敌第三方信息共享清单");
            HandleHtmlContent(driver, "https://wepie.yuque.com/docs/share/e87695d5-bfec-475f-8c16-fc4b4ff9f3d5?#", @"C:\Workspace\finaltank-home\src\policy\third\Momoyu.html", "坦克无敌第三方信息共享清单");
        }

        private static void HandleHtmlContent(IWebDriver driver, string url, string localFilePath, string title)
        {
            Console.WriteLine($"url:{url}");

            driver.Navigate().GoToUrl(url);

            var source = driver.PageSource;

            var sourceDoc = new HtmlDocument();
            sourceDoc.LoadHtml(source);

            var sourceBodyNode = sourceDoc.DocumentNode.SelectSingleNode("//div[@class='ne-viewer-body']");
            if (sourceBodyNode == null)
            {
                Console.WriteLine($"can't find node in {url}");
                return;
            }

            var targetFileText = File.ReadAllText(localFilePath);
            var targetDoc = new HtmlDocument();
            targetDoc.LoadHtml(targetFileText);
            var targetBodyNode = targetDoc.DocumentNode.SelectSingleNode("//div[@class='ne-viewer']");
            targetBodyNode = targetBodyNode.ParentNode.ReplaceChild(sourceBodyNode.Clone(), targetBodyNode);
            targetBodyNode.ReplaceClass("ne-viewer", "ne-viewer-body");

            var titleNode = targetDoc.DocumentNode.SelectSingleNode("//title");
            ((HtmlTextNode)titleNode.FirstChild).Text = title;
            var descriptionNode = targetDoc.DocumentNode.SelectSingleNode("//meta[@name='description']");
            descriptionNode.SetAttributeValue("content", title);
            var h1Node = targetDoc.DocumentNode.SelectSingleNode("//body/h1");
            ((HtmlTextNode)h1Node.FirstChild).Text = title;

            targetDoc.Save(localFilePath);
        }
    }
}
