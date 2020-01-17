//Send x y data as touchpints with tuio 1.0 / 1.1 protocol 


using Ventuz.OSC;

public static UdpWriter oscClient;
public static int i = 0;


oscClient = new UdpWriter("127.0.0.1", 3333);

if (touchPresent)   //only send alive IDs when there is data to be transmitted. 
{

  tuio_osc_bundle = new OscBundle();
  OscMessage osc_message_1 = new OscElement("/tuio/2Dcur", "alive", 99999); //choose an ID
  //                                                                      X     Y     
  OscMessage osc_message_2 = new OscElement("/tuio/2Dcur", "set", 99999, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f);
  OscMessage osc_message_3 = new OscElement("/tuio/2Dcur", "fseq", i); 

  osc_bundle_alternative.AddElement(osc_message_1);
  osc_bundle_alternative.AddElement(osc_message_2);
  osc_bundle_alternative.AddElement(osc_message_3);


}
else {

  tuio_osc_bundle = new OscBundle();
  OscMessage osc_message_4 = new OscElement("/tuio/2Dcur", "alive"); //no alive ID means no touch present
  OscMessage osc_message_5 = new OscElement("/tuio/2Dcur", "fseq", i);

  osc_bundle_alternative.AddElement(osc_message_6);
  osc_bundle_alternative.AddElement(osc_message_7);
}

i++  //inkrement i for each new Tuio bundle
oscClient.Send(tuio_osc_bundle);
