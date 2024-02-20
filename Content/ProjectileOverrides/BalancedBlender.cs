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

namespace AFargoTweak.Content.ProjectileOverrides
{
    public class BalancedBlender : GlobalProjectile
    {
        //public static int BlenderYoyoProjType = ModContent.Find<ModProjectile>("BlenderYoyoProj").Type;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ModContent.Find<ModProjectile>("FargowiltasSouls", "BlenderYoyoProj").Type || entity.type == ModContent.Find<ModProjectile>("FargowiltasSouls","BlenderOrbital").Type;
        }
        public override bool PreAI(Projectile projectile)
        {
            projectile.CritChance = (int)(Main.player[projectile.owner].GetCritChance(projectile.DamageType) * 100);
            return base.PreAI(projectile);
        }
    }
}
