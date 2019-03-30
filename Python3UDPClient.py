import socket

IP = "127.0.0.1"
PORT = 20000

s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
server_address = ('127.0.0.1', 20000)
s.connect((IP,PORT))

# send string without carriage return and/or newline
MESSAGE = b'HELLO'
s.send(MESSAGE)

# send string without carriage return "\r" and "\n" newline
MESSAGE = b'HELLO\r\n'
s.send(MESSAGE)

# send bytes from hex-string
MESSAGE = b'\xFF\xFF\x0D\x0A'
s.send(MESSAGE)

# close connection
s.close()
