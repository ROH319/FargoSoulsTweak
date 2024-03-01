using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.Config;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using Terraria.ModLoader.Config.UI;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Patreon.Volknet.Projectiles;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Terraria.Localization;

namespace AFargoTweak.Configs
{
    public class ProjectileOverriderConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static ProjectileOverriderConfig Instance;
        public List<ProjOverPreset> Presets = new()
        {
            new ProjOverPreset()
            {
                PresetName = Language.GetTextValue("Mods.AFargoTweak.Configs.ConfigExtra.DefaultPreset"),
                Enabled = true
                , ProjChanges = new List<ProjOverrider>()
                {
                    new ProjOverrider() { proj = new ProjectileDefinition("FargowiltasSouls", "PlasmaArrow"),Enabled = true,OnSpawnDamageMult = 109},
                    new ProjOverrider() { proj = new ProjectileDefinition("FargowiltasSouls","PlasmaDeathRay"),Enabled = true,OnSpawnDamageMult = 120},
                    new ProjOverrider() { proj = new ProjectileDefinition("FargowiltasSouls","PrimeDeathray"),Enabled = true,ProjImmuneType = AFTUtils.NPCImmunityType.IDStatic, ImmunityCD = 12}
                }
            }
        }
        ;
        public override void OnChanged()
        {
            
            base.OnChanged();
        }
    }
    public class ProjOverPreset
    {
        public ProjOverPreset()
        {
            ProjChanges = new();
        }
        public bool Enabled;
        public string PresetName;
        public List<ProjOverrider> ProjChanges;
        public override bool Equals(object obj) => obj is not ProjOverPreset other ? base.Equals(obj) : Enabled == other.Enabled &&
            PresetName == other.PresetName && ProjChanges.Equals(other.ProjChanges);
        public override int GetHashCode() => new { Enabled, PresetName, ProjChanges }.GetHashCode();
    }
    [BackgroundColor(0,255,0)]
    public class ProjOverrider
    {
        public ProjOverrider()
        {
            proj = new ProjectileDefinition(ProjectileID.WoodenArrowFriendly);
            OnSpawnDamageMult = 100;
            ProjImmuneType = AFTUtils.NPCImmunityType.None;
            ImmunityCD = 10;
            //OnSpawnDamageMult = 100;
            //ProjImmuneType = AFTUtils.NPCImmunityType.None;
            //ImmunityCD = 10;
            var a = Main.projectile[0];
            if (a != null)
            {
                a.SetDefaults(proj.Type);
                if (a.usesLocalNPCImmunity)
                {
                    ProjImmuneType = AFTUtils.NPCImmunityType.Local;
                    ImmunityCD = a.localNPCHitCooldown;
                }
                else if (a.usesIDStaticNPCImmunity)
                {
                    ProjImmuneType = AFTUtils.NPCImmunityType.IDStatic;
                    ImmunityCD = a.idStaticNPCHitCooldown;
                }
            }
        }
        public ProjectileDefinition proj;
        public bool Enabled;
        [DefaultValue(100)]
        [Range(-10000,10000)]
        public int OnSpawnDamageMult;
        [DefaultValue(AFTUtils.NPCImmunityType.None)]
        [DrawTicks]
        public AFTUtils.NPCImmunityType ProjImmuneType;
        [DefaultValue(10)]
        public int ImmunityCD;
        public override bool Equals(object obj) => obj is not ProjOverrider other ? base.Equals(obj) :
            Enabled == other.Enabled && OnSpawnDamageMult == other.OnSpawnDamageMult
            && ProjImmuneType == other.ProjImmuneType && ImmunityCD == other.ImmunityCD;
        public override int GetHashCode() => new { Enabled, OnSpawnDamageMult, ProjImmuneType, ImmunityCD }.GetHashCode();
    }
}
