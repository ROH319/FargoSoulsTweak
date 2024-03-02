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
using Terraria.DataStructures;

namespace AFargoTweak.Configs
{
    public class ProjectileOverriderConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static ProjectileOverriderConfig Instance; 
        public List<ProjOverPreset> Presets = new()
        {
            ProjOverPreset.DefaultBalanceSet()
        }
        ;
        [Header("GlobalTest")]
        [DefaultValue(AFTUtils.NPCImmunityType.None)]
        [DrawTicks]
        public AFTUtils.NPCImmunityType GlobalImmunityType;

        [DefaultValue(10)]
        [Range(-2, 100)]
        public int GlobalImmunityCD;

        [DefaultValue(AFTUtils.ForcesImmunity.None)]
        [DrawTicks]
        public AFTUtils.ForcesImmunity GlobalIgnoreImmunity;
        public override void OnChanged()
        {
            if(Presets.Find(preset => preset.PresetName == Language.GetTextValue("Mods.AFargoTweak.Configs.ConfigExtra.DefaultPreset")) == null)
            {
                Presets.Add(ProjOverPreset.DefaultBalanceSet());
            }
            base.OnChanged();
        }
    }
    public class ProjOverPreset
    {
        public static ProjOverPreset DefaultBalanceSet() =>
            new ProjOverPreset()
            {
                PresetName = Language.GetTextValue("Mods.AFargoTweak.Configs.ConfigExtra.DefaultPreset"),
                Enabled = true
                ,
                ProjChanges = new List<ProjOverrider>()
                {
                    new ProjOverrider() { proj = new ProjectileDefinition("FargowiltasSouls", "PlasmaArrow"), Remarks = "纳米核心(远程)",Enabled = true,OnSpawnDamageMult = 109},
                    new ProjOverrider() { proj = new ProjectileDefinition("FargowiltasSouls","PlasmaDeathRay"), Remarks = "纳米核心(魔法)",Enabled = true,OnSpawnDamageMult = 120},
                    new ProjOverrider() { proj = new ProjectileDefinition("FargowiltasSouls","PrimeDeathray"), Remarks = "衍射暗星炮", Enabled = true,ProjImmuneType = AFTUtils.NPCImmunityType.IDStatic, ImmunityCD = 12},
                    new ProjOverrider() { proj = new ProjectileDefinition("FargowiltasSouls","GolemHeadProj"), Remarks = "山崩", Enabled = true,Scale = 200},
                    new ProjOverrider() { proj = new ProjectileDefinition("FargowiltasSouls","RazorbladeTyphoonFriendly2"), Remarks = "怒海狂涛",Enabled = true,OnSpawnDamageMult = 180},
                    new ProjOverrider() { proj = new ProjectileDefinition("FargowiltasSouls","DragonFireball"), Remarks = "龙之终焉",Enabled = true, OnSpawnDamageMult = 200},
                    new ProjOverrider() { proj = new ProjectileDefinition("FargowiltasSouls","DukeFishronMinion"),Remarks = "修复无穿甲bug",Enabled = true,ArmorPen = 400}
                    //new ProjOverrider() { proj = new ProjectileDefinition("FargowiltasSouls","MechElectricOrbHomingFriendly"),Enabled = true, Penetrate = 1}
                }
            };
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
            Remarks = "";
            OnSpawnDamageMult = 100;
            ProjImmuneType = AFTUtils.NPCImmunityType.None;
            ImmunityCD = 10;
            Penetrate = -10;
            Scale = 100;
            ArmorPen = -1;
            //var a = Main.projectile[0];
            //if (a != null)
            //{
            //    a.SetDefaults(proj.Type);
            //    if (a.usesLocalNPCImmunity)
            //    {
            //        ProjImmuneType = AFTUtils.NPCImmunityType.Local;
            //        ImmunityCD = a.localNPCHitCooldown;
            //    }
            //    else if (a.usesIDStaticNPCImmunity)
            //    {
            //        ProjImmuneType = AFTUtils.NPCImmunityType.IDStatic;
            //        ImmunityCD = a.idStaticNPCHitCooldown;
            //    }
            //}
        }
        public ProjectileDefinition proj;
        public bool Enabled;
        public string Remarks;
        [DefaultValue(100)]
        [Range(-10000,100000000)]
        public int OnSpawnDamageMult;
        [DefaultValue(AFTUtils.NPCImmunityType.None)]
        [DrawTicks]
        public AFTUtils.NPCImmunityType ProjImmuneType;
        [DefaultValue(10)]
        public int ImmunityCD;
        [DefaultValue(-10)]
        [Range(-10,100000000)]
        public int Penetrate;
        [DefaultValue(100)]
        [Range(0,100000000)]
        public int Scale;
        [DefaultValue(-1)]
        [Range(-1,100000000)]
        public int ArmorPen;
        //public EntityDefinition Source;
        public override bool Equals(object obj) => obj is not ProjOverrider other ? base.Equals(obj) :
            proj.Equals(other.proj) &&
            Enabled == other.Enabled && OnSpawnDamageMult == other.OnSpawnDamageMult
            && ProjImmuneType == other.ProjImmuneType && ImmunityCD == other.ImmunityCD
            && Penetrate == other.Penetrate && Scale == other.Scale && ArmorPen == other.ArmorPen;
        public override int GetHashCode() => new { Enabled, OnSpawnDamageMult, ProjImmuneType, ImmunityCD, Penetrate, Scale, ArmorPen }.GetHashCode();
    }

}
