
import socket

IP = "127.0.0.1"
PORT = 4352
# Create a TCP/IP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_address = (IP, PORT)

# Connect the socket to the port where the server is listening. TCP is a connection based communication protocol.
sock.connect(server_address)

# send a simple string
MESSAGE = b'hello'
sock.sendall(MESSAGE)

#send a string with carriage return \n and new line \n
MESSAGE = b'hello\r\n'
sock.sendall(MESSAGE)

#send bytes by using escape character \ and hex notation x
MESSAGE = b'\xFF\xFE'
sock.sendall(MESSAGE)

#TCP sockets need to be closed.
sock.close()
