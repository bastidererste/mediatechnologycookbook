
/*


        Add this header file (ofxOSCReceiverThread.h) to src folder
        
        in ofApp.h add...
        
        #include "ofxOSCReceiverThread.h"

        public:
          void receivedMessage(ofxOscMessage &m);
          ofxOSCReceiverThread ofR;
          
        
        in odApp.cpp add...
        
        void ofApp::setup(){
    
            ofR.setup(9999);
            ofR.start();
            ofAddListener(ofR.messageReceived, this, &ofApp::receivedMessage);
        }
        
        void ofApp::receivedMessage(ofxOscMessage &m){
                ofLog(OF_LOG_NOTICE, "messagereceivedInEventNotify ");
                
                if ( m.getAddress() == "/adress" )
                {

                        int firstArgument = m.getArgAsInt32( 0 );
                        ofLog(OF_LOG_NOTICE, "the movieIndex is " + ofToString(firstArgument));
            
                }
                
            
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
                        ofNotifyEvent(messageReceived, m, this);
                        ofLog(OF_LOG_NOTICE, "messagereceived ");
                        
                    }
                    
                }
            }
            unlock();
        }
    }
    
    
    
    ofEvent<ofxOscMessage> messageReceived;
    
private:
    
    ofxOscReceiver receiver;
    ofxOscMessage msg;
    
};
