import socket

HOST = '192.168.8.138'
PORT = 5000

sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.connect((HOST, PORT))

while True:
  data = sock.recv(1024)
  print(repr(data))

sock.close()
