using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace AFargoTweak.Configs
{
    public class AccConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static AccConfig Instance;

        //[Header("BalanceChangeAcc")]
        [DefaultValue(300)]
        [Range(0, 3000)]
        public int ComputationOrbRegenCD;

        [DefaultValue(10)]
        public int ComputationOrbManaCost;

        [DefaultValue(30)]
        public int ComputationOrbCritDmg;

        [DefaultValue(1)]
        [Range(0,6)]
        public int ExtraWizardSlot;

        [DefaultValue(15)]
        public int MagicSoulManaCostRedu;
        [DefaultValue(25)]
        public int UniversalSoulManaCoseRedu;

        [DefaultValue(-3)]
        [Range(-10, 10)]
        public int SummonSoulExtraTurrets;
        [DefaultValue(-1)]
        [Range(-10, 10)]
        public int UniversalSoulExtraTurrets;

        [DefaultValue(-2)]
        [Range(-10, 10)]
        public int StyxCrownExtraTurrets;
        [DefaultValue(-2)]
        [Range(-10, 10)]
        public int EridanusHatExtraTurrets;

        
        //[DefaultValue(true)]
        //[ReloadRequired]
        //public bool PinkyDuke;
    }
}
