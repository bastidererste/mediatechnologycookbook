


//listen for tcp messages on 127.0.0.1 on port 20000 
nc -l 127.0.0.1 20000




# send byte message from hex 
echo -ne "\xFF\xFF\xFF\xFF\x30\x30\xFF\x43" | nc 127.0.0.1 20000

# send string message with newline "\n"
echo "HELLO" | nc 127.0.0.1 20000

# send string message with carriage return \r\n
echo -ne "HELLO\x0D\x0A" | nc 127.0.0.1 20000




//listen for udp messages on 127.0.0.1 with port 20000 
nc -u -l 127.0.0.1 20000




# send byte message from hex 
echo -ne "\xFF\xFF\xFF\xFF\x30\x30\xFF\x43" | nc -u 127.0.0.1 20000

# send string message with newline "\n"
echo "HELLO" | nc -u 127.0.0.1 20000

# send string message with carriage return \r\n
echo -ne "HELLO\x0D\x0A" | nc -u 127.0.0.1 20000






