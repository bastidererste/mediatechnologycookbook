
import socket
from pythonosc import udp_client
import numpy as np


HOST = '192.168.1.155' # IP of the robot
PORT = 7001

sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.connect((HOST, PORT))

osc_client = udp_client.SimpleUDPClient("192.168.188.20", 11111)
while True:
	data = sock.recv(1024)
	stringData = data.decode('utf-8').replace('>','')
	x = np.array(stringData.split())
	#print(x)
	osc_client.send_message("/robo", x.astype(np.float))


sock.close()
