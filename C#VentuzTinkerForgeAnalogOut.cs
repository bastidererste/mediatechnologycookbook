using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ventuz.Kernel;
using Tinkerforge;



public class Script : ScriptBase, System.IDisposable
{
    
    // This member is used by the Validate() method to indicate
    // whether the Generate() method should return true or false
    // during its next execution.
    private bool changed;
	private string HOST = "localhost";
	private int PORT = 4223;
	BrickletAnalogOutV2 ao;
	IPConnection ipcon;
    
    // This Method is called if the component is loaded/created.
    public Script()
    {
        // Note: Accessing input or output properties from this method
        // will have no effect as they have not been allocated yet.

		// Don't use device before ipcon is connected
    }
    
    // This Method is called if the component is unloaded/disposed
    public virtual void Dispose()
    {
		ipcon.Disconnect();
    }
    
    // This Method is called if an input property has changed its value
	  public override void Validate()
	  {
		// Remember: set changed to true if any of the output 
		// properties has been changed, see Generate()
		

		// Set output voltage to 3.3V
		ao.SetOutputVoltage(Input);
	  }
    
    // This Method is called every time before a frame is rendered.
    // Return value: if true, Ventuz will notify all nodes bound to this
    //               script node that one of the script's outputs has a
    //               new value and they therefore need to validate. For
    //               performance reasons, only return true if output
    //               values really have been changed.
    public override bool Generate()
    {
        if (changed)
        {
            changed = false;
            return true;
        }

        return false;
	  }
	
	  // This Method is called if the function/method connect is invoked by the user or a bound event.
	  // Return true if the Validate() Method should be called this frame!
	  public bool Onconnect(int arg)
	  {		
			ipcon = new IPConnection(); // Create IP connection
			ao = new BrickletAnalogOutV2(brickletuid, ipcon); // Create device object

			ipcon.Connect(HOST, PORT); // Connect to brickd
		
		
		
		  return false;
	  }

}
