using System;
using System.Net.Sockets;
using System.Text;

namespace udpSender
{

    class MainClass
    {


        public static void Main(string[] args)
        {

            UdpClient client = new UdpClient();
            client.Connect("127.0.0.1", 20000);

            //send string without carriage return and/or newline
            string MESSAGE = "HELLO";
            byte[] data = Encoding.ASCII.GetBytes(MESSAGE);
            client.Send(data, data.Length);

            //send string with carriage return and newline
            MESSAGE = "HELLO\r\n";
            data = Encoding.ASCII.GetBytes(MESSAGE);
            client.Send(data, data.Length);

            //send bytes from hex
            byte[] data2 = { 0xFF, 0xFE };
            client.Send(data2, data2.Length);


            client.Close();

        }

    }

}
