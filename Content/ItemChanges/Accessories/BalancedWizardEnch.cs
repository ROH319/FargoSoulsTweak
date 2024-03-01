using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.ModPlayers;

namespace AFargoTweak.Content.ItemChanges.Accessories
{
    public class BalancedWizardEnch : GlobalItem
    {
        public int drawTimer = 0;
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.ModItem is BaseEnchant;
        }
        public override bool PreDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            bool returnvalue = base.PreDrawInInventory(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
            Player player = Main.LocalPlayer;
            AFTModPlayer aftplr = player.AFT();
            if (item.ModItem is BaseEnchant && aftplr.ExtraWizardedItem.Contains(item))
            {
                for (int j = 0; j < 12; j++)
                {
                    Vector2 afterimageOffset = (MathHelper.TwoPi * j / 12f).ToRotationVector2() * 1f;
                    float modifier = 0.5f + ((float)Math.Sin(drawTimer / 30f) / 6);
                    Color glowColor = Color.Lerp(Color.Blue with { A = 0 }, Color.Silver with { A = 0 }, modifier) * 0.5f;

                    Texture2D texture = Terraria.GameContent.TextureAssets.Item[item.type].Value;
                    Main.EntitySpriteDraw(texture, position + afterimageOffset, null, glowColor, 0, texture.Size() * 0.5f, item.scale, SpriteEffects.None, 0f);
                }
            }
            drawTimer++;
            return returnvalue;
        }

        public delegate bool ForceEffDele(FargoSoulsPlayer farplr, ModItem modItem);
        public static bool ForceHook(ForceEffDele orig, FargoSoulsPlayer farplr, ModItem modItem)
        {
            bool returnvalue = orig(farplr, modItem);
            AFTModPlayer aftplr = farplr.Player.AFT();
            var wizarditem = aftplr.ExtraWizardedItem;
            bool CheckWizard(int type)
            {
                Item result = wizarditem.Find(item => item.type == type);
                if (result != null)
                    return true;
                return (BaseEnchant.CraftsInto[type] > 0 && CheckWizard(BaseEnchant.CraftsInto[type]));
            }
            if (modItem is BaseEnchant && CheckWizard(modItem.Item.type))
                return true;
            return returnvalue;
        }
    }
}
