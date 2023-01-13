using Newtonsoft.Json;
using System.Text.RegularExpressions;
using TShockAPI;

namespace AnotherBroadcaster
{
    class Message
    {
        public string Text { get; set; }
        public string Color { get; set; }
        public string Interval { get; set; }
        [JsonIgnore]
        public bool Active = true;

        public Message() {
            Text = string.Empty;
            Interval = string.Empty;
            Color = string.Empty;
        }

        public async void Broadcast()
        {
            await Task.Delay(IntervalMillis());
            if (Active && Plugin.IsBroadcasting)
            {
                var c = ColorObject();

                TShock.Log.ConsoleInfo(Text);
                TSPlayer.All.SendMessage(Text, c);
                Broadcast();
            }
        }

        private int IntervalMillis()
        {
            int result;
            if (TShock.Utils.TryParseTime(Interval, out result))
            {
                return result * 1000;
            }

            TShock.Log.ConsoleError("Could not parse interval " + Interval);
            return 10000;
        }

        private Microsoft.Xna.Framework.Color ColorObject()
        {
            if (Color.StartsWith("#") && Color.Length == 7)
            {
                byte r = byte.Parse(Color[1..3], System.Globalization.NumberStyles.HexNumber);
                byte g = byte.Parse(Color[3..5], System.Globalization.NumberStyles.HexNumber);
                byte b = byte.Parse(Color[5..7], System.Globalization.NumberStyles.HexNumber);
                return new Microsoft.Xna.Framework.Color(r, g, b);
            } else
            {
                var c = typeof(Microsoft.Xna.Framework.Color).GetProperty(Color);
                if (c != null)
                    return (Microsoft.Xna.Framework.Color)c.GetValue(null);
            }
            TShock.Log.ConsoleError("Invalid color encountered: " + Color);
            return Microsoft.Xna.Framework.Color.White;
        }
    }
}