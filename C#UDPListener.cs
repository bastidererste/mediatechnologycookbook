using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        // Define the local endpoint for the socket.
        // This example uses port 11000 on the local computer.
        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 8008);

        // Create a new UDP socket.
        using (Socket udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
        {
            // Bind the socket to the local endpoint.
            udpSocket.Bind(localEndPoint);

            Console.WriteLine("Waiting for a message...");

            // Incoming data from the client.
            byte[] buffer = new byte[1024];
            EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

            while (true)
            {
                // Receive data.
                int bytesReceived = udpSocket.ReceiveFrom(buffer, ref remoteEP);

                // Convert byte array to string
                string receivedText = Encoding.ASCII.GetString(buffer, 0, bytesReceived);
                Console.WriteLine($"Received: {receivedText}");

                // Optional: Send a response back to the client
                string response = "Message received";
                byte[] responseBytes = Encoding.ASCII.GetBytes(response);
                udpSocket.SendTo(responseBytes, remoteEP);
            }
        }
    }
}
