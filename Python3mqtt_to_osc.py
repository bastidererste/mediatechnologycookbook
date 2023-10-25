import argparse
import paho.mqtt.client as mqtt
from pythonosc import udp_client

# Configuration
MQTT_BROKER = "localhost"
MQTT_PORT = 1883
MQTT_TOPIC = "#"
OSC_IP = "127.0.0.1"
OSC_PORT = 5005

# Callback for when a message is received
def on_message(client, userdata, message):
    print(f"Received message: topic={message.topic} payload={message.payload.decode('utf-8')}")
    osc_client = udp_client.SimpleUDPClient(OSC_IP, OSC_PORT)
    osc_client.send_message(message.topic, message.payload.decode('utf-8'))

def main():
    # Set up MQTT client and callbacks
    mqtt_client = mqtt.Client()
    mqtt_client.on_message = on_message
    mqtt_client.connect(MQTT_BROKER, MQTT_PORT, 60)
    mqtt_client.subscribe(MQTT_TOPIC)
    print(f"Subscribed to MQTT topic: {MQTT_TOPIC}")

    try:
        # Blocking loop to keep subscribing and forwarding messages
        mqtt_client.loop_forever()
    except KeyboardInterrupt:
        print("Disconnected, exiting...")
        mqtt_client.disconnect()

if __name__ == "__main__":
    main()
