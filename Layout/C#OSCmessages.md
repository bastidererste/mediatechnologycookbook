

### Problem

Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.

### Solution
```cs
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Ventuz.OSC;
using System.Collections.Generic;

namespace OSCsender
{
    class MainClass
    {
        public static UdpWriter oscClient;
        public static int glob_id=0;

        public static void Main(string[] args)
        {
            
            oscClient = new UdpWriter("127.0.0.1", 22222);

            //Broadcasting
            //oscClient = new UdpWriter("239.0.0.1", 22222);


            //#### simple message with adress /position and one argument
            OscMessage osc_message = new OscElement("/position", 2000);
            oscClient.Send(osc_message);


            //#### message with multiple arguments of different types
            OscMessage osc_message_multiple = new OscElement("/position", 2033, 0.5f, "hello", true );
            oscClient.Send(osc_message_multiple);


            //message with an array of floats as argument
            float[] floats = new float[] { 1.0f, 2.3f, 3f, 4f, 5f, 6f, 7f, 9f, 10f } ;
            object[] object_array = floats.Cast<object>().ToArray();
            OscMessage osc_message_multiple_2 = new OscElement("/positions", object_array);
            oscClient.Send(osc_message_multiple_2);


            //#### up to three messages sent simultanuously
            OscMessage osc_message1 = new OscElement("/position1", 1233);
            OscMessage osc_message2 = new OscElement("/position2", 23523);
            OscMessage osc_message3 = new OscElement("/position3", 2322);
            OscBundle osc_bundle = new OscBundle(DateTime.MinValue, osc_message1, osc_message2, osc_message3);
            oscClient.Send(osc_bundle);


            //#### more than three messages sent simultanuously as OSC bundle
            OscBundle osc_bundle_alternative = new OscBundle();
            OscMessage osc_message_1 = new OscElement("/position1", 34234);
            OscMessage osc_message_2 = new OscElement("/position2", 234521);
            OscMessage osc_message_3 = new OscElement("/position3", 2322);
            OscMessage osc_message_4 = new OscElement("/position4", 12222);
            osc_bundle_alternative.AddElement(osc_message_1);
            osc_bundle_alternative.AddElement(osc_message_2);
            osc_bundle_alternative.AddElement(osc_message_3);
            osc_bundle_alternative.AddElement(osc_message_4);
            oscClient.Send(osc_bundle_alternative);

        }
    }   
}

```
### Discussion


Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.

Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.

Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.


### See also

Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.
