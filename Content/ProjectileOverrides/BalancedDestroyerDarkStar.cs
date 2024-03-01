using FargowiltasSouls.Content.Projectiles.Minions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AFargoTweak.Content.ProjectileOverrides
{
    public class BalancedDestroyerDarkStar : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ProjectileType<MechElectricOrbHomingFriendly>();
        }
        public override void SetDefaults(Projectile entity)
        {
            base.SetDefaults(entity);
            entity.penetrate = 1;
        }
    }
}
