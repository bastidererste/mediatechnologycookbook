using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventuz.OSC;

namespace OSC_Dispatcher
{
    class Program
    {


        private static OscDispatcher OSC;
        private static NetReader nr;
        private static bool running = true;

        static void Main(string[] args)
        {

            nr = new UdpReader(9999);
            OSC = new OscDispatcher();
            OSC.AddNetReader(nr);
            OSC.Bundle += OnOscBundle;

            while (running)
            {
                OSC.Purge();
            }


            OSC.ClearCache();
            OSC.RemoveNetReader(nr);
            nr.Dispose();

        }




        private static void OnOscBundle(OscDispatcher disp, OscBundle bndl)
        {
            Console.WriteLine("message!!");
            foreach (OscElement e in bndl.Elements)
            {
              
                if (e.Address == "/exit")
                {

                    running = false;
                }

             

    
              
            }
        }
    }
}
