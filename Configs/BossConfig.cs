using AFargoTweak.Content.NPCChanges;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace AFargoTweak.Configs
{
    public class BossConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static BossConfig Instance;

        [Header("Mutant")]
        public MutantConfig MutantSetting = new();
        //[DefaultValue(MutantAttack.All)]
        //[DrawTicks]
        //public MutantAttack Mutantattack;

        public override void OnChanged()
        {
            base.OnChanged();
        }
    }
    public class MutantConfig
    { 
        [DefaultValue(true)]
        public bool MasoCanRandom;

        [DefaultValue(true)]
        public bool LowLifeCanRandom;

        [DefaultValue(false)]
        public bool ForceRandom;

        [DefaultValue(false)]
        public bool AllowRecentAttack;

        [DefaultValue(5)]
        public int SkipP1Required;

        [DefaultValue(true)]
        public bool MasoCanSkip;

        [DefaultValue(2)]
        [Range(1,8)]
        public int MutantEyeCompressFactor;
        public MutantConfig()
        {
            MasoCanRandom = true;
            LowLifeCanRandom = true;
            SkipP1Required = 5;
            MasoCanRandom = true;
            MasoCanSkip = true;
            MutantEyeCompressFactor = 2;
        }
    }
    public enum MutantAttack
    {
        All,
        VoidRay,
        DarkBlueDash,
        BulletHell,
        Pillar,
        EOC,
        LightBlueDash,
        DarkBlueJavelin,
        MechRay,
        Fishron1,
        TrueEye,
        Nuke,
        SlimeRain,
        Fishron2,
        OkuuSpheres,
        LightBlueJavelin,
        Twin,
        Empress,
        MutantSword
    }
}
