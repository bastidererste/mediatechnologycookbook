using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;

using System.Text;

namespace PanasonicTCP
{
    class MainClass
    {
        public static void Main(string[] args)
        {
        
            TcpClient socket = new TcpClient("192.168.8.126", 4352);
            NetworkStream client = socket.GetStream();

            byte[] data = new byte[21];

            Int32 bytes = client.Read(data, 0, data.Length);

            String[] response = Encoding.ASCII.GetString(data, 0, bytes).Split(' ');

            string user = "user";
            string pwd = "password";

            string source = user + ":" + pwd + ":"+response[2];

            MD5 md5Hash = MD5.Create();
            string hash = GetMd5Hash(md5Hash, source);

            string messageCCP = hash + "00PON\r";

            byte[] MessageCCP = Encoding.ASCII.GetBytes(messageCCP);

            client.Write(MessageCCP, 0, MessageCCP.Length);

            client.Close();
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}


