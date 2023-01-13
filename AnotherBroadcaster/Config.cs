using Newtonsoft.Json;
using TShockAPI;
//using YamlDotNet.Serialization;

namespace AnotherBroadcaster
{
    class Config
    {
        private static readonly string configName = "AnotherBroadcaster";
        public List<Message> Messages = new();

        public static Config Load()
        {
            Config config = new();
            string configPath = Path.Combine(TShock.SavePath, configName);
            if (File.Exists(configPath + ".json"))
            {
                string configText = File.ReadAllText(configPath + ".json");
                List<Message> messages = JsonConvert.DeserializeObject<List<Message>>(configText) ?? throw new Exception("Config is empty");
                config.Messages = messages;
            }
            else
            {
                Message exampleMessage = new()
                {
                    Text = "Edit " + configName + ".json to create custom Broadcasting messages",
                    Color = "#ff0000",
                    Interval = "10s"
                };
                config.Messages.Add(exampleMessage);

                // Create example config
                File.WriteAllText(configPath + ".json", JsonConvert.SerializeObject(config.Messages, Formatting.Indented));
            }
            TShock.Log.ConsoleDebug("Loaded config:\n" + JsonConvert.SerializeObject(config, Formatting.Indented));
            return config;
        }
    }
}
