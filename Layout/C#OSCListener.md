### Problem

Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.

### Solution
```cs
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Ventuz.OSC;


class MainClass
{

    private const int listenPort = 22222;
    private static bool done = false;

    public static void Main(string[] args)
    {
        
        UdpReader listener = new UdpReader(listenPort);
        Console.WriteLine("UDP listener started...");

        try
        {
            
            do
            {
                //listeneing for OSC Messages
                OscMessage m=null;
                try
                {
                    m = listener.Receive();

                }
                catch (Exception)
                {
                    Console.WriteLine("received malformed message or message was no OSC message");
                }

                //check if message is a bundle of messages
                if (m != null && m.GetType() == typeof(OscBundle))
                {

                    OscBundle bundle = (OscBundle)m;


                    foreach (OscElement element in bundle.Elements.Cast<OscElement>().ToList())
                    {

                        if (element != null && element.Match("/position"))
                        {
                            Console.WriteLine("received "+element.Address + " message in bundle with typeTags " + getArgumentTypeTag(element));

                        }


                        if (element != null && element.Match("/exit"))
                        {
                            Console.WriteLine("received " + element.Address + " message in bundle with typeTags " + getArgumentTypeTag(element));

                            done = true;

                        }

                    }
                }
                //check if message is a single element
                else if (m != null && m.GetType() == typeof(OscElement))
                {
                    OscElement element = (OscElement)m;


                    if (element != null && element.Match("/position"))
                    {
                        Console.WriteLine(getArgumentTypeTag(element));

                        Console.WriteLine("received " + element.Address + " message as a single element with typeTags " + getArgumentTypeTag(element));

                    }


                    if (element != null && element.Match("/exit"))
                    {
                        Console.WriteLine("received " + element.Address + " message as a single element with typeTags " + getArgumentTypeTag(element));

                        done = true;
                    }


                }
            }
            while (!done);



        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        finally
        {
            Console.WriteLine("Stopped listener");

        }


    }


    public static string getArgumentTypeTag(OscElement element)
    {
        string[] typeTag = new string[element.Args.Length];

        int i = 0;
        foreach (var t in element.Args)
        {
            typeTag[i] = t.GetType().ToString();
            i++;
        }

        return String.Join(",",typeTag);
    }



}
```
### Discussion


Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.

Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.

Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.


### See also

Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.
