## socat packet forwarding

This script forwardes UDP packets from a local port to another mashines network interface.

```
#!/bin/bash

while true; do
    socat UDP-RECV:4001 UDP-SENDTO:192.168.178.56:4001
    echo "socat exited with status $?. Restarting..." >&2
    sleep 1  # Wait for 1 second before restarting
done
```
1. **Shebang (`#!/bin/bash`):** This line indicates that the script should be executed using the Bash shell.

2. **Infinite Loop (`while true; do ... done`):** 
   - The script uses an infinite loop to continuously execute the commands within the loop. This just ensures that if the connection is lost on any of the connections it automatically restarts the forwarfing process.

3. **socat Command:** 
   - `socat UDP-RECV:4001 UDP-SENDTO:192.168.178.56:4001`
   - This command uses `socat`, a utility for data transfer between two locations.
   - `UDP-RECV:4001` listens for UDP packets on port 4001 on any of the local interfaces.
   - `UDP-SENDTO:192.168.178.56:4001` forwards the received packets to the IP address `192.168.178.56` on port 4001.
   - Essentially, this command creates a UDP forwarder, receiving packets on one port and sending them to a specified IP and port.

4. **Error Handling and Restart Message:**
   - `echo "socat exited with status $?. Restarting..." >&2`
   - This line is executed if `socat` exits for any reason.
   - `$?` captures the exit status of `socat`.
   - The message is output to standard error (`stderr`), providing information on the exit status of `socat` and indicating that the script is attempting to restart `socat`.

5. **Sleep Command:**
   - `sleep 1` causes the script to wait for 1 second before the next iteration of the loop starts.
   - This is to prevent immediate restarts and potential resource exhaustion in case of repeated `socat` failures.


To use this script:

Copy the script into a file, say socat_loop.sh.
Give it execute permission using the command chmod +x socat_loop.sh.
Run the script with ./socat_loop.sh.

