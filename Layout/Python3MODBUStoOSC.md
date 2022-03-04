

### Problem

When I was on a car reveal project in China for a large german car manufacturer, we had a large moving LED screen. To reveal the car, this LED screen would split in the middle and open up like elevator doors. The content on the LED screen however should stay fixed to its position. 

The hardware to controll the screen was a siemens programmable logic controller (plc). The controler had only the commands "open", "close" and "stop". There was no output for "poition" what so ever. They simply forgott.The plc was locked up by the programmer. 

By taking a closer look at the setup i found modbus tcp interfaces at the motors attached to the LED screen. 

Modbus is an industrial protocol standard that was created for communication between plcs. Modbus TCP takes Modbus data packets and transmitts them over standard Ethernet networks.

Modbus data is most often read and written as "registers" which are 16-bit pieces of data. 

| Address  | 0  | 1  | 2  |  3 | 4  |  5 | 6  |7   |8   |9   |
|---|---|---|---|---|---|---|---|---|---|---|
| 40001-40010  |   |   |   |   |   |   |   |   |   |   |
| 40011-40020  |   |   |   |   |   |   |   |   |   |   |
| 40021-40030  |   |   |   |   |   |   |   |   |   |   |

Most often, the register is either a signed or unsigned 16-bit integer. If a 32-bit integer or floating point is required, these values are actually read as a pair of registers. The most commonly used register is called a Holding Register, and these can be read or written. The other possible type is Input Register, which is read-only.

The wide data simply consists of two consecutive "registers" treated as a single wide register. Floating point in 32-bit IEEE 754 standard, and 32-bit integer data, are widely used.

Most Control Solutions Modbus products default to placing the high order register first, or in the lower numbered register. This is known as "big endian", and is consistent with Modbus protocol which is by definition big endian itself. The byte order for all 16-bit values is most significant byte first.

### Solution
```py
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

```
### Discussion


Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.

Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.

Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.


### See also

Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.
