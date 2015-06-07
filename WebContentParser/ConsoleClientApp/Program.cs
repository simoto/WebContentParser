namespace ConsoleClientApp
{
    using System.IO;
    using System.Net;
    using System.Text;

    class Program
    {
        static void Main()
        {
            using (WebClient client = new WebClient())
            {
                //Adding headers to simulate using browser
                client.Headers["User-Agent"] =
                "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
                "(compatible; MSIE 6.0; Windows NT 5.1; " +
                ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";

                //Adding concrete encoding for request/response
                client.Encoding = Encoding.UTF8;

                // Download data.
                string arr = client.DownloadString("http://vicove.com/vic-60390");
                int startIndex = arr.IndexOf("<div class=\"post-content image-caption-format-1\">") + 63; //63 is length of the passed text
                int endIndex = arr.IndexOf("</p>", startIndex); 

                //writing the content to text file 
                using (TextWriter writer = File.CreateText("textFile.txt"))
                {                    
                    writer.WriteLine(arr.Substring(startIndex, endIndex - startIndex));                   
                }
            }
        }
    }
}
