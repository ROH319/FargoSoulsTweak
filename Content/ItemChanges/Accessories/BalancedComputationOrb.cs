using FargowiltasSouls;
using FargowiltasSouls.Content.Patreon;
using FargowiltasSouls.Core.ModPlayers;
using rail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AFargoTweak.Content.ItemChanges.Accessories
{
    public class BalancedComputationOrb
    {
        public delegate void ModifyHitNPCItemDelegate(PatreonPlayer patreonPlayer, Item item, NPC target, ref NPC.HitModifiers modifiers);
        public static void ModifyHitNPCItemHook(ModifyHitNPCItemDelegate orig, PatreonPlayer patreonPlayer, Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            //伤害*1.25改为暴击伤害*1.3
            if (patreonPlayer.CompOrb && item.DamageType != DamageClass.Magic && item.DamageType != DamageClass.Summon)
            {
                modifiers.FinalDamage *= 0.8f;
                modifiers.CritDamage += AFargoTweak.ConfigInstance.ComputationOrbCritDmg / 100f;
            }
            //先设为反向减伤，在复原
            var oriManaRedu = patreonPlayer.Player.manaSickReduction;
            patreonPlayer.Player.manaSickReduction = 1f - oriManaRedu;
            orig(patreonPlayer, item, target, ref modifiers);
            patreonPlayer.Player.manaSickReduction = oriManaRedu;
        }
        public delegate void ModifyHitNPCProjDelegate(PatreonPlayer patreonPlayer, Projectile proj, NPC target, ref NPC.HitModifiers modifiers);
        public static void ModifyHitNPCProjHook(ModifyHitNPCProjDelegate orig, PatreonPlayer patreonPlayer, Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (patreonPlayer.CompOrb && proj.DamageType != DamageClass.Magic && proj.DamageType != DamageClass.Summon)
            {
                modifiers.FinalDamage *= 0.8f;
                modifiers.CritDamage += AFargoTweak.ConfigInstance.ComputationOrbCritDmg / 100f;
            }
            var oriManaRedu = patreonPlayer.Player.manaSickReduction;
            patreonPlayer.Player.manaSickReduction = 1f - oriManaRedu;
            orig(patreonPlayer, proj, target, ref modifiers);
            patreonPlayer.Player.manaSickReduction = oriManaRedu;
        }
        public delegate void OnHitNPCDelegate(PatreonPlayer patreonPlayer, NPC target, NPC.HitInfo hit, int damageDone);
        public static void OnHitNPCHook(OnHitNPCDelegate orig, PatreonPlayer patreonPlayer, NPC target, NPC.HitInfo hit, int damageDone)
        {

            if (patreonPlayer.CompOrb && patreonPlayer.CompOrbDrainCooldown <= 0)
            {
                patreonPlayer.CompOrbDrainCooldown = 15;
                if (patreonPlayer.Player.statMana >= AFargoTweak.ConfigInstance.ComputationOrbManaCost)
                {
                    patreonPlayer.Player.statMana -= AFargoTweak.ConfigInstance.ComputationOrbManaCost;
                    patreonPlayer.Player.manaRegenDelay = AFargoTweak.ConfigInstance.ComputationOrbRegenCD;
                }
            }
            orig(patreonPlayer, target, hit, damageDone);
        }
        public delegate bool CanUseItemDelegate(PatreonGlobalItem patreonGItem, Item item, Player player);
        public static bool CanUseItemHook(CanUseItemDelegate orig, PatreonGlobalItem patreonGItem, Item item, Player player)
        {

            if (item.damage > 0 && player.GetModPlayer<PatreonPlayer>().CompOrb
                && item.DamageType != DamageClass.Magic && item.DamageType != DamageClass.Summon
                && item.pick == 0 && item.hammer == 0 && item.axe == 0)
            {
                if (player.statMana < AFargoTweak.ConfigInstance.ComputationOrbManaCost)
                    return false;
                player.statMana -= AFargoTweak.ConfigInstance.ComputationOrbManaCost;
                //设为自己的回魔CD
                player.manaRegenDelay = AFargoTweak.ConfigInstance.ComputationOrbRegenCD;
                player.GetModPlayer<PatreonPlayer>().CompOrbDrainCooldown = item.useTime + item.reuseDelay + 30;
            }
            return true;
        }
        public static void On_Player_NebulaLevelup(On_Player.orig_NebulaLevelup orig, Player self, int type)
        {
            if (self.GetModPlayer<PatreonPlayer>().CompOrb
                && self.HeldItem.DamageType != DamageClass.Magic && self.HeldItem.DamageType != DamageClass.Summon
                && (type >= 176 && type <= 178))
            {
                return;
            }
            orig(self, type);
        }

        public static void On_Player_AddBuff(On_Player.orig_AddBuff orig, Player self, int type, int timeToAdd, bool quiet, bool foodHack)
        {
            //if(self.GetModPlayer<PatreonPlayer>().CompOrb && self.HeldItem.DamageType != DamageClass.Magic && self.HeldItem.DamageType != DamageClass.Summon
            //     && (type >= 176 || type <= 178))
            //{
            //    return;
            //}
            orig(self, type,timeToAdd,quiet,foodHack);
        }
    }
}
