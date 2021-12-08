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
        
            // The password to the main web login to log into the projector needs to be empty.

            TcpClient socket = new TcpClient("192.168.8.126", 4352);
            NetworkStream client = socket.GetStream();

            string messageCC = "\x02PON\x03\r";

            byte[] MessageCC = Encoding.ASCII.GetBytes(messageCC);

            client.Write(MessageCC, 0, MessageCC.Length);
            
            client.Close();
        }
    }
}
