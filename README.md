# AnotherBroadcaster

This plugin allows you to create broadcasting messages which are executed periodically.

## Configuration
The config can be found in the tshock path `tshock/AnotherBroadcaster.json`
It contains a list of all broadcasting messages
```json
[
    {
        "Text": "[Broadcast] This is an example",
        "Color": "#ff00ff",
        "Interval": "2h30min"
    }
]
```
`Text`: Text of the Broadcast

`Color`: Text Color (#rrggbb in hex-format)

`Interval`: Interval at which the message is sent (supports `d` (days), `h` (hours), `min` (minutes) and `s` (seconds))
