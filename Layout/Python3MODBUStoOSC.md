

### Problem

When I was on a car reveal project in China for a german car manufacturer, we had a large moving LED screen. To reveal the car, this LED screen would split in the middle and open up like elevator doors. The content on the LED screen however should stay fixed to its position on the stage. 

The hardware to controll the screen was a siemens programmable logic controller (plc). The controler had only the commands "open", "close" and "stop". There was no output for "poition" what so ever. They simply forgott. The plc was locked up by the programmer, so simply adding a new funtion was not an option.

### Solution

By taking a closer look at the setup i found modbus tcp interfaces at the motors attached to the LED screen. 

Modbus is an industrial protocol standard that was created for communication between plcs. Modbus TCP takes Modbus data packets and transmitts them over standard Ethernet networks.

Modbus data is most often read and written as "registers" which are 16-bit pieces of data. Here for example is the holding register that starts at address 40001.

| Address  | 0  | 1  | 2  |  3 | 4  |  5 | 6  |7   |8   |9   |
|---|---|---|---|---|---|---|---|---|---|---|
| 40001-40010  | 0  | 0 | 21145  | 123  | 0  | 0  |  0 |0   |0   |0   |
| 40011-40020  | 0  | 0  | 0  |   0| 0  | 0  | 0  |0   | 0  |0   |
| 40021-40030  |  0 | 0  | 0  | 0 |0  |0   | 0  | 0  | 0  | 0  |

The register is either a signed or unsigned 16-bit integer. If you expect a 32-bit integer you read two registers.

I had a look at the documentation of the modbus motor controller and saw that the incremental counter for the position can be read from address 40003 to 40004

The documentation also sayd that the regsiters are big-endian, so the higher the byte the lower the address.

So, iconnected my mashine to the LED screens network, fired up pyCharm, created a new project and installed pyModbusTCP.

```py
from pyModbusTCP.client import ModbusClient
```

To connect to the modbus controller we need its ip. The port is almost allways 502. There are exceptions, however... With this info i created a client.

```py
c = ModbusClient(host="192.168.178.223", port=502, auto_open=True)
```
As holding register allwas start at 40001 (or in very rare cases at 40000), read_holding_registers() omits this large numbers and only takes in the begining index and how many registers to read including the start register. In my case thats 2 and 2. From 40003 (the fisrt) to 40004 (the second). Modbus registers are zero indexed, thats why the start address is NOT 3 but 2.

```py
regs = c.read_holding_registers(2, 2)
```

If the address to start reading would have been 40062 it would have been 61 and 2.

```py
regs = c.read_holding_registers(61, 2)
```
regs is now an array that holds the two 16bit values that make up the recent position.
As i kow from the documentation that two 16bit integers give the position as a 32bit integer in big-endian, i use the bitshifting operator <<. 

```py
position = (regs[0] << 16) + regs[1]
    
```
Voila. 



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

:exclamation: Programming moving equipment large or small can be very dangerous to you and the crew around. Take care of savety precautions. Reading holding registers can be considered save, but whyle debugin take care nobofy is in the moving equipments direct vicinity!


### See also

Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.
