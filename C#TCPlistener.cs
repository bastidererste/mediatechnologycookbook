using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

class MyTcpListener
{
    public static void Main()
    {
        TcpListener server= null;
        bool done = false;
        try
        {
    
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 11000);
            server.Start();

            // Buffer for received data
            Byte[] bytes = new Byte[256];
            String data = null;

            while (!done)
            {
                Console.Write("Waiting for clients... ");

                // wait for clients to connect
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected!");

                data = null;

                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();

                int i;

                // Loop to receive all the data sent by the client.
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("Received: {0}", data);

                    // Process the data sent by the client.
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                    switch (data)
                    {
                        case "case1":
                            Console.WriteLine("message 'case1' was received");
                            break;
                        case "exit":
                            Console.WriteLine("message 'exit' was received");
                            done = true;
                            break;
                        default:
                            break;
                    }


                    // Send back a response.
                    stream.Write(msg, 0, msg.Length);
                }

                // Shutdown and end connection
                client.Close();
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine( e);
        }
        finally
        {
            // Stop listening for new clients.
            server.Stop();
        }


        Console.WriteLine("TCP server stopped...");

    }
}
