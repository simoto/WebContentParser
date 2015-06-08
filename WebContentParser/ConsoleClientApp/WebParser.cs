namespace ConsoleClientApp
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading;

    public class WebParser
    {
        private static string siteUrl;
        private static string concreteRoute;
        private static string contentStartString;
        private static string contentEndString;
        private static int contentStartSkip;
        private static int startIndex;
        private static int endIndex;
        private static int contentStartIndex;
        private static int contentEndIndex;

        public static void Main()
        {
            ReadUserInputs();

            using (WebClient client = new WebClient())
            {
                ////Adding headers to simulate using browser
                client.Headers["User-Agent"] =
                "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
                "(compatible; MSIE 6.0; Windows NT 5.1; " +
                ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";

                ////Adding concrete encoding for request/response
                client.Encoding = Encoding.UTF8;

                for (int i = startIndex; i <= endIndex; i++)
                {
                    string htmlResult = client.DownloadString(siteUrl + concreteRoute + i);
                    contentStartIndex = htmlResult.IndexOf(contentStartString) + contentStartString.Length + contentStartSkip;
                    contentEndIndex = htmlResult.IndexOf(contentEndString, contentStartIndex);

                    string content = htmlResult.Substring(contentStartIndex, contentEndIndex - contentStartIndex);
                    //// int startIndex2 = arr.IndexOf("rel=\"category tag\">") + 19;
                    //// int endEndex2 = arr.IndexOf("</a>", startIndex2);
                    ////string content2 = htmlResult.Substring(startIndex2, endEndex2 - startIndex2);

                    ////if we dont have a content
                    if (content.Length < 2)
                    {
                        continue;
                    }
                    ////writing the content to text file 
                    using (StreamWriter writer = new StreamWriter("textFile.txt", true))
                    {
                        writer.WriteLine(i);
                        writer.WriteLine(content.Replace("<BR>", string.Empty));
                        writer.WriteLine();
                    }

                    Console.WriteLine("Content with index {0} is added!", i);
                    Thread.Sleep(1000);
                }
                //// Download data.              
            }
        }

        private static void ReadUserInputs()
        {
            Console.Write("Enter your url(http://example.com): ");
            siteUrl = Console.ReadLine();
            Console.Write("Enter concrete route(/articles/): ");
            concreteRoute = Console.ReadLine();
            Console.Write("Enter a number for url start index: ");
            startIndex = int.Parse(Console.ReadLine());
            Console.Write("Enter a number for url end index: ");
            endIndex = int.Parse(Console.ReadLine());
            Console.Write("Enter a string value to match from where content starts: ");
            contentStartString = Console.ReadLine();
            Console.Write("Enter a number for how many letters you want to skip from the content start: ");
            contentStartSkip = int.Parse(Console.ReadLine());
            Console.Write("Enter a string value to match the end of the content: ");
            contentEndString = Console.ReadLine();
        }
    }
}
