import struct 

#little-endian 4 byte array of 1000.0
data = [0x00, 0x00, 0x7a, 0x44]
value = struct.unpack("<f", bytes(data))
print(value[0])

#big-endian 4 byte array of 1000.0
data = [0x44, 0x7a, 0x00, 0x00]
value = struct.unpack(">f", bytes(data))
print(value[0])
