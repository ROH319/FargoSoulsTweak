using FargowiltasSouls.Content.Items.Armor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AFargoTweak.Content.ItemChanges.Armors
{
    public class BalancedStyxCrown : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<StyxCrown>();
        }
        public override void UpdateEquip(Item item, Player player)
        {
            base.UpdateEquip(item, player);
            player.maxTurrets += AFargoTweak.ConfigInstance.StyxCrownExtraTurrets;
        }
    }
}
