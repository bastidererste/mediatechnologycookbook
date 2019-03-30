
import socket

#
IP = "127.0.0.1"
PORT = 20000

#MESSAGE = "HELLO"
#MESSAGE = "HELLO\r\n"
MESSAGE = '0200000000020d0a'.decode('hex')

s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
s.settimeout(1000)

s.connect((IP,PORT))

s.send(MESSAGE)

s.close()
