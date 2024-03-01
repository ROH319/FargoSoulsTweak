using Fargowiltas.Items;
using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace AFargoTweak
{
    public class AFTGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults(Item entity)
        {
            //FargoGlobalItem farItem = entity.Fargo();
            if(FargoChangesLoader.ItemChanges == null || FargoChangesLoader.ItemChanges.Count < 1)
            {
                FargoChangesLoader.Item_LoadChange();
            }
            if(FargoChangesLoader.ItemChanges != null && FargoChangesLoader.ItemChanges.ContainsKey(entity.type))
            {
                FargoChangesLoader.ItemChanges[entity.type].ApplyChanges_SetDefault(entity);
            }

            base.SetDefaults(entity);
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //return false;
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }
        
    }
}
