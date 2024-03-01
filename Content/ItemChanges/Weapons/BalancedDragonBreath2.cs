using AFargoTweak.Configs;
using FargowiltasSouls.Content.Items.Weapons.SwarmDrops;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AFargoTweak.Content.ItemChanges.Weapons
{
    //public class BalancedDragonBreath2
    //{
    //    public delegate bool ShootDelegate(DragonBreath2 self, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback);
    //    public static bool ShootHook(ShootDelegate orig, DragonBreath2 self, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    //    {
    //        Projectile.NewProjectile(source, position + Vector2.Normalize(velocity) * 60f, velocity.RotatedByRandom(MathHelper.ToRadians(5)), type, damage, knockback, player.whoAmI, Main.rand.Next(3));

    //        if (--self.skullTimer < 0)
    //        {
    //            self.skullTimer = 5;
    //            SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot);
    //            //float ai = Main.rand.NextFloat((float)Math.PI * 2);
    //            /*for (int i = 0; i <= 4; i++)
    //            {
    //                int p = Projectile.NewProjectile(position, new Vector2(speedX, speedY).RotatedByRandom(MathHelper.Pi / 18),
    //                    ModContent.ProjectileType<DragonFireball>(), damage * 3, knockBack, player.whoAmI);
    //                Main.projectile[p].netUpdate = true;
    //            }*/
    //            var dmgMul = ModContent.GetInstance<AccConfig>().DragonBreath2FireBallDmgMul;
    //            Projectile.NewProjectile(source, position, 2f * velocity,//.RotatedByRandom(MathHelper.Pi / 18),
    //                ModContent.ProjectileType<DragonFireball>(), damage * dmgMul, knockback * 6f, player.whoAmI);
    //        }
    //        return false;
    //    }
    //}
}
