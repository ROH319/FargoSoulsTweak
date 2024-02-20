using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AFargoTweak.Content.ProjectileOverrides
{
    public class BalancedNanoBlade : GlobalProjectile
    {
        
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ModContent.Find<ModProjectile>("FargowiltasSouls", "NanoBlade").Type;
        }
        public override bool PreAI(Projectile projectile)
        {
            projectile.ai[1] = 1.25f;
            return base.PreAI(projectile);
        }
    }
}
