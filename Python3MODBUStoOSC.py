from pyModbusTCP.client import ModbusClient
from pythonosc import udp_client

modbus_client = ModbusClient(host="192.168.8.126", port=1502, auto_open=True)
osc_client = udp_client.SimpleUDPClient("192.168.8.126", 11111)

while(True):
        regs = modbus_client.read_holding_registers(0, 2)
        if regs:
                osc_client.send_message("/modbus", regs)
        else:
                print("read error")
