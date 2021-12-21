

### Problem

Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.

### Solution
```cs

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuplex.WebView;

public class BrowserControl : MonoBehaviour
{
    public WebViewPrefab webViewPrefab;
    // Start is called before the first frame update
    async void Start()
    {
    
    
        webViewPrefab.Initialized += WebViewPrefabOnInitialized;
      
        
    }

    private async void WebViewPrefabOnInitialized(object sender, EventArgs e)
    {
        Debug.Log("initialized");
        webViewPrefab.WebView.LoadUrl("http://localhost:8888/");
        webViewPrefab.WebView.MessageEmitted += WebViewOnMessageEmitted;

        webViewPrefab.WebView.LoadProgressChanged += (sender, eventArgs) =>
        {
            if (eventArgs.Type == ProgressChangeType.Finished)
            {
                Debug.Log("Page title: " + webViewPrefab.WebView.Title);
                webViewPrefab.WebView.PageLoadScripts.Add(@"
                if (typeof sendMessageToCSharp !== 'function') { 
              
                    function sendMessageToCSharp() {
                        var matches = [];
                        var inputs = document.getElementsByTagName('input');
                        for(var key in inputs) {
                            var value = inputs[key].value;
                        
                                matches.push(value);                            
                        }
                        var m = matches.join('')
                        window.vuplex.postMessage({ type: 'greeting', message: m });
                    }
                    const inputs = document.getElementsByTagName('input');
                    for(let i = 0; i < inputs.length; i++) {
                        if(inputs[i].type.toLowerCase() == 'submit') {
                            inputs[i].setAttribute('onclick', 'sendMessageToCSharp()');    
                        }
                    }
                }");


            }
        };
    }

    private void WebViewOnMessageEmitted(object sender, EventArgs<string> e)
    {
        Debug.Log("message: "+e.Value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
```
### Discussion


Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.

Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.

Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.


### See also

Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.
