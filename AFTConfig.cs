using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;
using AFargoTweak;

namespace AFargoTweak
{
    public class AFTConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;


        [Header("BalanceChangeAcc")]
        [DefaultValue(300)]
        [Range(0, 3000)]
        public int ComputationOrbRegenCD;

        [DefaultValue(10)]
        public int ComputationOrbManaCost;

        [DefaultValue(30)]
        public int ComputationOrbCritDmg;

        [DefaultValue(15)]
        public int MagicSoulManaCostRedu;
        [DefaultValue(25)]
        public int UniversalSoulManaCoseRedu;

        [DefaultValue(-3)]
        [Range(-10,10)]
        public int SummonSoulExtraTurrets;
        [DefaultValue(-1)]
        [Range(-10,10)]
        public int UniversalSoulExtraTurrets;

        [DefaultValue(-2)]
        [Range(-10,10)]
        public int StyxCrownExtraTurrets;
        [DefaultValue(-2)]
        [Range(-10,10)]
        public int EridanusHatExtraTurrets;

        [Header("BalanceChangeWeapon")]
        [DefaultValue(200)]
        [Range(0,1000)]
        public int DragonBreath2FireBallDmgMul;

        [DefaultValue(true)]
        public bool PrimeDeathrayStaticImmunity;

        [DefaultValue(12)]
        public int PrimeDeathrayImmunityCD;

        [Header("GlobalTest")]
        [DefaultValue(AFTUtils.NPCImmunityType.None)]
        [DrawTicks]
        public AFTUtils.NPCImmunityType GlobalImmunityType;

        [DefaultValue(10)]
        [Range(-2,100)]
        public int GlobalImmunityCD;

        [DefaultValue(AFTUtils.ForcesImmunity.None)]
        [DrawTicks]
        public AFTUtils.ForcesImmunity GlobalIgnoreImmunity;
        //[DefaultValue(true)]
        //[ReloadRequired]
        //public bool PinkyDuke;
    }
}
