using FargowiltasSouls.Content.Patreon.Volknet.Projectiles;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace AFargoTweak.Configs
{
    public class ConfigSystem : ModSystem
    {
        public override void PostSetupContent()
        {

            base.PostSetupContent();

            if (ProjectileOverriderConfig.Instance.Presets != null)
            {
                //ProjOverPreset defaultpreset = ProjectileOverriderConfig.Instance.Presets.Find(pre => pre.PresetName == Language.GetTextValue("Mods.AFargoTweak.Configs.ConfigExtra.DefaultPreset"));
                //if (defaultpreset == null)
                {
                    //defaultpreset.ProjChanges = new Dictionary<ProjectileDefinition, ProjOverrider>()
                    //{
                    //    [new ProjectileDefinition(ModContent.ProjectileType<PlasmaArrow>())] = new ProjOverrider() { Enabled = true, OnSpawnDamageMult = 109 },
                    //    [new ProjectileDefinition(ModContent.ProjectileType<PlasmaDeathRay>())] = new ProjOverrider() { Enabled = true, OnSpawnDamageMult = 120 },
                    //    [new ProjectileDefinition(ModContent.ProjectileType<PrimeDeathray>())] = new ProjOverrider() { Enabled = true, ProjImmuneType = AFTUtils.NPCImmunityType.IDStatic, ImmunityCD = 12 }
                    //};
                    //ProjectileOverriderConfig.Instance.Presets.Add(new ProjOverPreset()
                    //{
                    //    PresetName = Language.GetTextValue("Mods.AFargoTweak.Configs.ConfigExtra.DefaultPreset"),
                    //    Enabled = true,
                    //    ProjChanges = new Dictionary<ProjectileDefinition, ProjOverrider>()
                    //    {
                    //        [new ProjectileDefinition("FargowiltasSouls", "PlasmaArrow")] = new ProjOverrider() { Enabled = true, OnSpawnDamageMult = 109 },
                    //        [new ProjectileDefinition(ModContent.ProjectileType<PlasmaDeathRay>())] = new ProjOverrider() { Enabled = true, OnSpawnDamageMult = 120 },
                    //        [new ProjectileDefinition(ModContent.ProjectileType<PrimeDeathray>())] = new ProjOverrider() { Enabled = true, ProjImmuneType = AFTUtils.NPCImmunityType.IDStatic, ImmunityCD = 12 }
                    //    }
                    //});
                }
            }
        }
        public override void OnModLoad()
        {
            base.OnModLoad();
        }
    }
}
