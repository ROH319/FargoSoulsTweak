using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace AFargoTweak.Configs
{
    public class ItemOverriderConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static ItemOverriderConfig Instance;
        public List<ItemOverPreset> Presets = new()
        {
            ItemOverPreset.DefaultBalanceSet()
        };
        public override void OnChanged()
        {
            if(Presets.Find(preset=>preset.PresetName == Language.GetTextValue("Mods.AFargoTweak.Configs.ConfigExtra.DefaultPreset")) == null)
            {
                Presets.Add(ItemOverPreset.DefaultBalanceSet());
            }
            base.OnChanged();
        }
    }
    public class ItemOverPreset
    {
        public static ItemOverPreset DefaultBalanceSet() =>
            new ItemOverPreset()
            {
                PresetName = Language.GetTextValue("Mods.AFargoTweak.Configs.ConfigExtra.DefaultPreset"),
                ItemChanges = new List<ItemOverrider>()
                {
                    new ItemOverrider() { item = new ItemDefinition("FargowiltasSouls","NukeFishron"), Damage = 951, UseTime = 60, UseAnimation = 60},
                    new ItemOverrider() { item = new ItemDefinition("FargowiltasSouls","DragonBreath2"), Damage = 221},
                    new ItemOverrider() { item = new ItemDefinition("FargowiltasSouls","GolemTome2"), Damage = 394},
                    new ItemOverrider() { item = new ItemDefinition("FargowiltasSouls","GeminiGlaives"), Damage= 386},
                    new ItemOverrider() { item = new ItemDefinition("FargowiltasSouls","StaffOfUnleashedOcean"), Damage = 42},
                    new ItemOverrider() { item = new ItemDefinition(ItemID.Zenith), Damage = 230}
                }
            }; 
        public ItemOverPreset()
        {
            ItemChanges = new();
            Enabled = true;
        }
        public bool Enabled;
        public string PresetName;
        public List<ItemOverrider> ItemChanges;
        public override bool Equals(object obj) => obj is not ItemOverPreset other ? base.Equals(obj) : Enabled == other.Enabled
            && PresetName == other.PresetName && ItemChanges.Equals(other.ItemChanges);
        public override int GetHashCode() => new { Enabled, PresetName, ItemChanges }.GetHashCode();
    }
    [BackgroundColor(255,0,0)]
    public class ItemOverrider
    {
        public ItemOverrider()
        {
            item = new ItemDefinition(ItemID.CopperShortsword);
            Enabled = true;
            Damage = -1;
            UseTime = -1;
            UseAnimation = -1;
        }
        public ItemDefinition item;
        public bool Enabled;
        [DefaultValue(-1)]
        [Range(0,10000000)]
        public int Damage;
        [DefaultValue(-1)]
        [Range(0,1000)]
        public int UseTime;
        [DefaultValue(-1)]
        [Range(0,1000)]
        public int UseAnimation;
        public override bool Equals(object obj) => obj is not ItemOverrider other ? base.Equals(obj) :
            item.Equals(other.item) &&
            Enabled == other.Enabled && Damage == other.Damage && UseTime == other.UseTime
            && UseAnimation == other.UseAnimation;
        public override int GetHashCode() => new { item, Enabled, Damage, UseTime, UseAnimation }.GetHashCode();

    }
}
