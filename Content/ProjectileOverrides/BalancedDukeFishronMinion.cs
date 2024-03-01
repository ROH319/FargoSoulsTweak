
using FargowiltasSouls.Content.Patreon.DemonKing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AFargoTweak.Content.ProjectileOverrides
{
    public class BalancedDukeFishronMinion : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ProjectileType<DukeFishronMinion>();
        }
        public override bool PreAI(Projectile projectile)
        {
            if (projectile.localAI[1] > 30)
                projectile.localAI[1] = 30;
            return base.PreAI(projectile);
        }
    }
}
