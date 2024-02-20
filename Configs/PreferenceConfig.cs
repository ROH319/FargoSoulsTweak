using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace AFargoTweak.Configs
{
    public class PreferenceConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        public static PreferenceConfig Instance;

        [Header("ReviveOld")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool SansGolem;

        [DefaultValue(AFTUtils.DeviMusic.Default)]
        [DrawTicks]
        public AFTUtils.DeviMusic DeviMusicType;

        [DefaultValue(AFTUtils.AbomMusic.Default)]
        [DrawTicks]
        public AFTUtils.AbomMusic AbomMusicType;
        public ProjNameDisplayer ProjName = new();
    }
    public class ProjNameDisplayer
    {
        public ProjNameDisplayer()
        {
            Enabled = false;
            Offset = Vector2.Zero;
            TextColor = Color.White;
            BorderColor = Color.Black;
        }
        [DefaultValue(true)]
        public bool Enabled;
        [Range(-100f,100f)]
        public Vector2 Offset;
        public Color TextColor;
        public Color BorderColor;
    }
}
