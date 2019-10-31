
#include <iostream>
#include <sstream>
#include <sys/socket.h>
#include <netinet/in.h>
#include <unistd.h>
#include <string>
#include <iomanip>

using namespace std::string_literals;
using namespace std;

int main(int argc, const char * argv[]) {
    
    struct sockaddr_in remaddr;
    int fd, slen=sizeof(remaddr);
    
    fd=socket(AF_INET, SOCK_DGRAM, 0);
    
    memset((char *) &remaddr, 0, sizeof(remaddr));
    remaddr.sin_family = AF_INET;
    remaddr.sin_port = htons(11111);
    
    string message = "Hello"s;
    sendto(fd, message.data(), message.size(), 0, (struct sockaddr *)&remaddr, slen);
    
    string coded_message = "\xff\xff"s;
    sendto(fd, coded_message.data(), coded_message.size(), 0, (struct sockaddr *)&remaddr, slen);

    int array[10] ;
    array[0]=2147483647;
    array[1]=999;
    sendto(fd, array, sizeof(array), 0,  (struct sockaddr *)&remaddr, slen);
    
    close(fd);
    
    return 0;
}
