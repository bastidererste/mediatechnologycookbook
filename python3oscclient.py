from OSC import OSCClient, OSCMessage

client = OSCClient()
client.connect( ("192.168.100.2", 9000) )

client.send( OSCMessage("/test/", [1.0, 2.0, 3.5 ] ) )
