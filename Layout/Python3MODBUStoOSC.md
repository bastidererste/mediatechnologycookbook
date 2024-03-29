# Python ModBus registers to OSC


When I was on a car reveal project in China for a german car manufacturer, we had a large moving LED screen. To reveal the car, this LED screen would split in the middle and open up like elevator doors. The content on the LED screen however should stay fixed to its position on the stage. 

### Problem

The hardware to controll the screen was a siemens programmable logic controller (plc). The controler had only the commands "open", "close" and "stop". There was no output for "position" what so ever. They simply forgot. The plc was locked up by the programmer, so adding a new function was not an option.

### Solution

By taking a closer look at the setup i found modbus tcp interfaces at the motors attached to the LED screen. 

Modbus is an industrial protocol standard that was created for communication between plcs. Modbus TCP takes Modbus data packets and transmitts them over standard Ethernet networks.

Modbus data is most often read and written as "registers" which are 16-bit pieces of data. Here for example is the holding register that starts at address 40001.

| Address     | 0    | 1    | 2     | 3    | 4    | 5    | 6    | 7    | 8    | 9    |
| ----------- | ---- | ---- | ----- | ---- | ---- | ---- | ---- | ---- | ---- | ---- |
| 40001-40010 | 0    | 0    | 21145 | 123  | 0    | 0    | 0    | 0    | 0    | 0    |
| 40011-40020 | 0    | 0    | 0     | 0    | 0    | 0    | 0    | 0    | 0    | 0    |
| 40021-40030 | 0    | 0    | 0     | 0    | 0    | 0    | 0    | 0    | 0    | 0    |

The register is either a signed or unsigned 16-bit integer. If you expect a 32-bit integer you read two registers.

I had a look at the documentation of the modbus motor controller and saw that the incremental counter for the position can be read from address 40003 to 40004

The documentation also states that the registers are big-endian, so the higher the byte the lower the address.



:warning: WARNING

> Programming on moving equipment large or small can be very dangerous to you and the crew around. Take care of savety precautions. Reading holding registers can be considered save, but nevertheless take care nobody is in the moving equipments direct vicinity while debugging 



I connected my machine to the LED screens network, fired up pyCharm, created a new project and installed pyModbusTCP through the package manager.

```python
from pyModbusTCP.client import ModbusClient
```

To connect to the modbus controller we need its ip. The port is almost allways 502. There are exceptions, however... With this info i created a client.

```python
modbus_client = ModbusClient(host="192.168.178.223", port=502, auto_open=True)
```

As holding register allwas start at 40001 (or in very rare cases at 40000), read_holding_registers() omits this large numbers and only takes in the beginnig index and how many registers to read including the start register. In my case thats 2 and 2. From 40003 (the first) to 40004 (the second). Modbus registers are zero indexed, thats why the start address is NOT 3 but 2.

```python
regs = modbus_client.read_holding_registers(2, 2)
```

If the address to start reading would have been 40062 it would have been 61 and 2.

```python
regs = modbus_client.read_holding_registers(61, 2)
```

**regs** is now an array that holds the two 16bit integers that make up the recent position.
As i kow from the documentation that two 16bit integers together give the position as a 32bit integer in big-endian, I use the bit-shifting operator **<<**. 

```python
position = (regs[0] << 16) + regs[1]
    
```

Voila, **position** now holds the position of the LED segment this controller is attached to.

The complete code so far looks like this:

```python
from pyModbusTCP.client import ModbusClient

modbus_client = ModbusClient(host="192.168.178.223", port=502, auto_open=True)

regs = modbus_client.read_holding_registers(2, 2)
while True:
	if regs:
    	position = (regs[0] << 16) + regs[1]
    	print(position)
	else:
    	print("read error")
```

Now that i have the position, lets get it over to the LED content machine with OSC.

I installed the python-osc package through the package manager and import it like i did with the ModBus library.

```python
from pythonosc import udp_client

```

The ventzu machines IP was 192.168.178.22 and listened on port 8000 for incoming OSC messages

```python
osc_client = udp_client.SimpleUDPClient("192.168.178.22", 8000)
```

Whenever a new position is read, i send an OSC message with the address "/modbus" to the content machine.

```python
while True:
    regs = modbus_client.read_holding_registers(61, 2)
    if regs:
        position = (regs[0] << 16) + regs[1]
        osc_client.send_message("/modbus", position)
    else:
        print("read error")
        
```

The final code:

```python
from pyModbusTCP.client import ModbusClient
from pythonosc import udp_client


modbus_client = ModbusClient(host="192.168.178.223", port=502, auto_open=True)
osc_client = udp_client.SimpleUDPClient("192.168.178.22", 8000)

while True:
    regs = modbus_client.read_holding_registers(61, 2)
    if regs:
        position = (regs[0] << 16) + regs[1]
        osc_client.send_message("/modbus", position)
    else:
        print("read error")
        
        
```

### Discussion

Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.




### See also

Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.
