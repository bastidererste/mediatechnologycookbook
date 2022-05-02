using System;
using System.Linq;
using AudioSwitcher.AudioApi.CoreAudio;
using Ventuz.OSC;
namespace OscAudioControl
{
    
   
    internal class Program
    {
        public static void Main(string[] args)
        {
            int listenPort = 9999;
            Console.WriteLine("Searching for DefaultPlaybackDevice...");
            CoreAudioController coreAudioController = new CoreAudioController();
            Console.WriteLine("Found "+coreAudioController.DefaultPlaybackDevice.FullName);
            UdpReader listener = new UdpReader(listenPort);
            UdpWriter oscClient = new UdpWriter("127.0.0.1", 9998);
            Console.WriteLine("UDP listener started on port 9999");
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
                        if (element != null && element.Match("/volume"))
                        {
                            Console.WriteLine("received "+element.Address + " message in bundle with typeTags " + getArgumentTypeTag(element));
                            if (element.Args.Length == 1)
                            {
                                coreAudioController.DefaultPlaybackDevice.SetVolumeAsync((float) element.Args[0]);
                            
                                OscMessage osc_message = new OscElement("/volumemaster", (float) element.Args[0]);
                                oscClient.Send(osc_message);
                            }
                         
                        }
                    
                    }
                }
                //check if message is a single element
                else if (m != null && m.GetType() == typeof(OscElement))
                {
                    OscElement element = (OscElement)m;
                    if (element != null && element.Match("/volume"))
                    {
                        Console.WriteLine("received "+element.Address + " message in bundle with typeTags " + getArgumentTypeTag(element));
                        if (element.Args.Length == 1)
                        {
                            
                            coreAudioController.DefaultPlaybackDevice.SetVolumeAsync((float) element.Args[0]);
                        
                        
                              
                            OscMessage osc_message = new OscElement("/volumemaster", (float) element.Args[0]);
                            oscClient.Send(osc_message);
                            
                        }
                    }
                  
                }
            }
            while (true);
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
    
    
    
}
