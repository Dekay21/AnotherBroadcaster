# AnotherBroadcaster

This plugin allows you to create broadcasting messages which are executed periodically.

## Configuration
The config can be found in the tshock path `tshock/AnotherBroadcaster.json`
It contains a list of all broadcasting messages
```json
[
    {
        "Text": "Message 1",
        "Color": "#ff00ff",
        "Interval": "2h30m"
    },
    {
        "Text": "Message 2",
        "Color": "Blue",
        "Interval": "1h"
    }
]
```
`Text`: Text of the Broadcast

`Color`: Text Color (#rrggbb in hex-format or [Color name](https://www.foszor.com/blog/xna-color-chart/))

`Interval`: Interval at which the message is sent (supports `d`ays, `h`ours, `m`inutes and `s`econds)
