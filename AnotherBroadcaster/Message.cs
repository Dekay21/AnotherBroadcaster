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
                TShock.Players.ForEach(player =>
                {
                    if (player != null)
                    {
                        player.SendMessage(Text, c);
                    }
                });
                Broadcast();
            }
        }

        private int IntervalMillis()
        {
            int result = 0;

            string days = new Regex("\\d+d").Match(Interval).Value;
            if (days.Length > 1)
                result += int.Parse(days[..(days.Length - 1)]);
            result *= 60;

            string hours = new Regex("\\d+h").Match(Interval).Value;
            if (hours.Length > 1)
                result += int.Parse(hours[..(hours.Length - 1)]);
            result *= 60;

            string minutes = new Regex("\\d+min").Match(Interval).Value;
            if (minutes.Length > 3)
                result += int.Parse(minutes[..(minutes.Length - 3)]);
            result *= 60;

            string seconds = new Regex("\\d+s").Match(Interval).Value;
            if (seconds.Length > 1)
                result += int.Parse(seconds[..(seconds.Length - 1)]);
            return result * 1000;
        }

        private Microsoft.Xna.Framework.Color ColorObject()
        {
            if (Color.StartsWith("#") && Color.Length == 7)
            {
                byte r = byte.Parse(Color[1..3], System.Globalization.NumberStyles.HexNumber);
                byte g = byte.Parse(Color[3..5], System.Globalization.NumberStyles.HexNumber);
                byte b = byte.Parse(Color[5..7], System.Globalization.NumberStyles.HexNumber);
                return new Microsoft.Xna.Framework.Color(r, g, b);
            } /*else // TODO get color from color name
            {
                var c = typeof(Microsoft.Xna.Framework.Color).GetProperty(Color);
                if (c != null)
                    return (Microsoft.Xna.Framework.Color)c.GetValue(null);
            }*/
            TShock.Log.ConsoleError("Invalid color encountered: " + Color);
            return Microsoft.Xna.Framework.Color.White;
        }
    }
}