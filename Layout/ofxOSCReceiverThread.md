

### Problem

Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.

### Solution
```cpp
/*
        
        this threaded OSC receiver will only notify with your app if somthing you decided happed. 
        If theres a lot happennig on OSC, your main apps processing will not be interuppted unless you want it.
        Add this header file (ofxOSCReceiverThread.h) to src folder
        
        in ofApp.h add...
        
        #include "ofxOSCReceiverThread.h"
        public:
          void receivedMessage(int &i);
          ofxOSCReceiverThread ofR;
          
        
        in odApp.cpp add...
        
        void ofApp::setup(){
    
            ofR.setup(9999);
            ofR.start();
            ofAddListener(ofR.messageReceived, this, &ofApp::receivedMessage);
        }
        //only if the integers we chose in ofxOSCReceiverThread.h arrive, this method will be fired
        void ofApp::receivedMessage(int &i){
                ofLog(OF_LOG_NOTICE, "messagereceivedInEventNotify " + ofToString( i ));      
            
        }
*/


#include "ofxOsc.h"

class ofxOSCReceiverThread : public ofThread
{
public:
    
    ~ofxOSCReceiverThread()
    {
        if (isThreadRunning()) stopThread();
    }
    
    void setup(int port)
    {
        receiver.setup(port);
    }
    
    void start()
    {
        startThread();
    }
    
    void stop()
    {
        stopThread();
    }
    
    void threadedFunction()
    {
        ofLog(OF_LOG_NOTICE, "thread started ");
        
        while (isThreadRunning())
        {
            if (lock())
            {
                while (receiver.hasWaitingMessages())
                {
                    
                    if (receiver.getNextMessage(msg))
                    {
                         
                        ofxOscMessage m = msg;

                        if ( m.getAddress() == "/can" )
                        {
                            
                            int i = m.getArgAsInt32( 0 );
                            if(i==2){
                                ofNotifyEvent(messageReceived, i, this);
                                ofLog(OF_LOG_NOTICE, "messagereceived ");
                            }
                        }
                        
                    }
                    
                }
            }
            unlock();
        }
    }
    
    
    
    ofEvent<int> messageReceived;
    
private:
    
    ofxOscReceiver receiver;
    ofxOscMessage msg;
    
};
```
### Discussion


Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.

Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.

Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.


### See also

Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.



