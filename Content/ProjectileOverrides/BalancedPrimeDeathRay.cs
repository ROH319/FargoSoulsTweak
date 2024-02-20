using FargowiltasSouls.Content.Projectiles.BossWeapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AFargoTweak.Content.ProjectileOverrides
{
    public class BalancedPrimeDeathRay : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ProjectileType<PrimeDeathray>();
        }
        public override void SetDefaults(Projectile entity)
        {
            if (AFargoTweak.ConfigInstance.PrimeDeathrayStaticImmunity)
            {
                entity.usesIDStaticNPCImmunity = true;
                entity.idStaticNPCHitCooldown = AFargoTweak.ConfigInstance.PrimeDeathrayImmunityCD;
            }
            base.SetDefaults(entity);
        }
        public delegate void OnHitNPCDelegate(PrimeDeathray self, NPC target, NPC.HitInfo hit, int damageDone);
        public static void OnHitNPCHook(OnHitNPCDelegate orig, PrimeDeathray self, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!AFargoTweak.ConfigInstance.PrimeDeathrayStaticImmunity)
            {
                target.immune[self.Projectile.owner] = 6;
            }
            target.AddBuff(BuffID.OnFire, 600);
        }
    }
}
