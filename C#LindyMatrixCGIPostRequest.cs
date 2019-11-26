using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace web
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            //Expect100Continue issue: https://haacked.com/archive/2004/05/15/http-web-request-expect-100-continue.aspx/            

            WebRequest request = WebRequest.Create("http://192.168.1.44/m.cgi");
            request.Method = "POST";
            
            //Switch between saved mappings on Lindy 8x8 HDMI 2.0 18G Matrix Switch with post webrequest
            var postData = "param1=" + Uri.EscapeDataString("5"); //Option - use 5 to use saved mappings
            postData += "&param2=" + Uri.EscapeDataString("1"); //MappingIndex - index of mapping to select from list of saved mappings

            var data = Encoding.ASCII.GetBytes(postData);


            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }




            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            Console.WriteLine(responseString + data.Length);

        }
    }
}
