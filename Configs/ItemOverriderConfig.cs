using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace AFargoTweak.Configs
{
    public class ItemOverriderConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

    }
    public class ItemOverrider
    {
        public ItemOverrider()
        {

        }
        public ItemDefinition item;
        [DefaultValue(1)]
        [Range(0,10000000)]
        public int Damage;
        [DefaultValue(20)]
        [Range(0,1000)]
        public int UseTime;
        [DefaultValue(20)]
        [Range(0,1000)]
        public int UseAnimation;
    }
}
