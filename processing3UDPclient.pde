import hypermedia.net.*;

UDP client;  
void setup() {

  client = new UDP("127.0.0.1", 20000);

  //send string without carriage return and/or newline
  String message = "HELLO";
  client.send( message);

  //send string with carriage return "\r" and "\n" newline
  String message = "HELLO\r\n";
  client.send( message);

  //send bytes from hex 
  byte[] message = {(byte)0xff, (byte)0xff};
  client.send( message);

}
