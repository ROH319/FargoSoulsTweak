using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using System.Reflection;
using FargowiltasSouls;
using Terraria.ModLoader.Core;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Content.Items.Weapons.SwarmDrops;

namespace AFargoTweak.Content.ProjectileOverrides
{
    public class BalancedBlender : GlobalProjectile
    {
        //public static int BlenderYoyoProjType = ModContent.Find<ModProjectile>("BlenderYoyoProj").Type;
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ModContent.Find<ModProjectile>("FargowiltasSouls", "BlenderYoyoProj").Type || entity.type == ModContent.Find<ModProjectile>("FargowiltasSouls","BlenderOrbital").Type;
        }
        public override bool PreAI(Projectile projectile)
        {
            Player player = Main.player[projectile.owner];
            if (player.HeldItem.type == ModContent.ItemType<Blender>())
            {
                projectile.CritChance = player.GetWeaponCrit(player.HeldItem);
            }
            return base.PreAI(projectile);
        }
    }
}
