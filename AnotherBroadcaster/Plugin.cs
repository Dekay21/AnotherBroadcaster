using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;

namespace AnotherBroadcaster
{
    [ApiVersion(2, 1)]
    public class Plugin : TerrariaPlugin
    {
        /// <summary>
        /// The name of the plugin.
        /// </summary>
        public override string Name => "AnotherBroadcaster";

        /// <summary>
        /// The version of the plugin in its current state.
        /// </summary>
        public override Version Version => new(1, 0, 0);

        /// <summary>
        /// The author(s) of the plugin.
        /// </summary>
        public override string Author => "Dekay";

        /// <summary>
        /// A short, one-line, description of the plugin's purpose.
        /// </summary>
        public override string Description => "Creating your custom broadcasting messages";

        private Config config;
        public static bool IsBroadcasting { get { return TShock.Players.Any(player => player != null); } }

        /// <summary>
        /// The plugin's constructor
        /// Set your plugin's order (optional) and any other constructor logic here
        /// </summary>
        public Plugin(Main game) : base(game)
        {
        }

        /// <summary>
        /// Performs plugin initialization logic.
        /// Add your hooks, config file read/writes, etc here
        /// </summary>
        public override void Initialize()
        {
            ServerApi.Hooks.NetGreetPlayer.Register(this, PlayerJoined);
            GeneralHooks.ReloadEvent += Reload;

            config = Config.Load();
        }

        private void PlayerJoined(GreetPlayerEventArgs args)
        {
            if (TShock.Players.Count(player => player != null) == 1)
            {
                config.Messages.ForEach(m => m.Broadcast());
            }
            
        }

        private void Reload(ReloadEventArgs e)
        {
            config.Messages.ForEach(m => m.Active = false);
            config = Config.Load();
            config.Messages.ForEach(m => m.Broadcast());
        }

        /// <summary>
        /// Performs plugin cleanup logic
        /// Remove your hooks and perform general cleanup here
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.NetGreetPlayer.Deregister(this, PlayerJoined);
                GeneralHooks.ReloadEvent -= Reload;
            }
            base.Dispose(disposing);
        }
    }
}