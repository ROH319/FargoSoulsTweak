using AFargoTweak.Configs;
using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Forces;
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
        public List<Item> ExtraWizardedItem = new();
        public override void PreUpdate()
        {
            if (Main.GameUpdateCount % 60 == 0)
            {
                FargoChangesLoader.Item_LoadChange();
                FargoChangesLoader.Projectile_LoadChange();
            }
            base.PreUpdate();
        }
        public override void ResetEffects()
        {
            base.ResetEffects();
            //if (ExtraWizardedItem == null) ExtraWizardedItem = new();
            ExtraWizardedItem.Clear();
            if (Player.FargoSouls().WizardEnchantActive)
            {
                for (int i = 3; i <= 8; i++)
                {
                    if (!Player.armor[i].IsAir && (Player.armor[i].type == ModContent.ItemType<WizardEnchant>() || Player.armor[i].type == ModContent.ItemType<CosmoForce>()))
                    {
                        int slot = i + Math.Min(AccConfig.Instance.ExtraWizardSlot, 9 - i);
                        for(int j = i; j < slot; j++)
                        {
                            Item ench = Player.armor[j + 2];
                            if(ench != null && !ench.IsAir && ench.ModItem != null && ench.ModItem is BaseEnchant)
                            {
                                ExtraWizardedItem.Add(ench);
                            }
                        }
                    }
                }
            }
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
