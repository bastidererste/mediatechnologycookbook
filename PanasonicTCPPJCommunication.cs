using System;
using System.Net;
using System.Net.Sockets;

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

            string messagePJ = "%1POWR1\r";

            byte[] MessagePJ = Encoding.ASCII.GetBytes(messagePJ);

            client.Write(MessagePJ, 0, MessagePJ.Length);

            client.Close();
            
        }
    }
}





