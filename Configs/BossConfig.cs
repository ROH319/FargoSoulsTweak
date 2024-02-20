using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace AFargoTweak.Configs
{
    public class BossConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        
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
