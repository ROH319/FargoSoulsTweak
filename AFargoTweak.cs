using AFargoTweak.Configs;
using FargowiltasSouls.Content.Patreon.Volknet.Projectiles;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace AFargoTweak
{
	public class AFargoTweak : Mod
	{
        public override void Load()
        {
            base.Load();
            FargoMod = ModLoader.GetMod("FargowiltasSouls");
            AFargoTweak.Instance = this;
            ConfigInstance = ModContent.GetInstance<AFTConfig>();

        }
        public override void Unload()
        {
            
            AFargoTweak.Instance = null;
            ConfigInstance = null;
            base.Unload();
        }
        
        public static AFTConfig ConfigInstance;
        public static AFargoTweak Instance;
        public static Mod FargoMod;
    }
}