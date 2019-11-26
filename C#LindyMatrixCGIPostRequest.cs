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

            WebRequest request = WebRequest.Create("http://192.168.1.44/m.cgi");

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.ContentLength = 17;
            
            var postData = "param1=" + Uri.EscapeDataString("5"); //Mapping
            postData += "&param2=" + Uri.EscapeDataString("1"); //MappingIndex
            
            
            var data = Encoding.ASCII.GetBytes(postData);         
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            Console.WriteLine(responseString);

        }
    }
}

