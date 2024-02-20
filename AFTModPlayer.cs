using FargowiltasSouls;
using FargowiltasSouls.Core.ModPlayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AFargoTweak
{
    public class AFTModPlayer : ModPlayer
    {
        public override void PreUpdate()
        {
            FargoChangesLoader.Item_LoadChange();
            FargoChangesLoader.Projectile_LoadChange();
            base.PreUpdate();
        }
        public override void PostUpdateBuffs()
        {
            for(int i = 0; i < Player.buffType.Length; i++)
            {
                if (Player.buffType[i] >= 176 && Player.buffType[i] <= 178)
                {
                    Player.DelBuff(i);
                }
            }
            base.PostUpdateBuffs();
        }
        public override void PostUpdate()
        {
            if(Player.itemAnimation > 0 && Player.GetModPlayer<PatreonPlayer>().CompOrb)
            {
                Item item = Player.HeldItem;
                //Main.NewText($"{item.reuseDelay} {item.useTime} {item.useAnimation} {Player.itemAnimation}");
                if(item.damage > 0 && item.pick == 0 && item.hammer == 0 && item.axe == 0)
                {
                    Player.manaRegenDelay = (int)Player.maxRegenDelay;
                }
            }
            base.PostUpdate();
        }
        public override void UpdateEquips()
        {
            if (Player.FargoSouls().UniverseSoul)
            {
                Player.manaCost -= AFargoTweak.ConfigInstance.UniversalSoulManaCoseRedu / 100f;
                Player.maxTurrets += AFargoTweak.ConfigInstance.UniversalSoulExtraTurrets;
            }
            if (Player.FargoSouls().MagicSoul)
            {
                Player.manaCost -= AFargoTweak.ConfigInstance.MagicSoulManaCostRedu / 100f;
            }
            if (Player.FargoSouls().SummonSoul)
            {
                Player.maxTurrets += AFargoTweak.ConfigInstance.SummonSoulExtraTurrets;
            }
            
            
            base.UpdateEquips();
        }
        public override void OnEnterWorld()
        {
            for(int i = 0; i < 50; i++)
            {
                Item item = Player.inventory[i];
                if (item.stack == 1 && FargoChangesLoader.ItemChanges.ContainsKey(item.type))
                {
                    var prefix = item.prefix;
                    item.SetDefaults(Player.inventory[i].type);
                    item.prefix = prefix;
                }
                //Player.inventory[i].CloneDefaults(Player.inventory[i].type);
            }
            base.OnEnterWorld();
        }
    }
}
