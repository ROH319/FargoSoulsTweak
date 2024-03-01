using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Projectiles.Souls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AFargoTweak.Content.ProjectileOverrides
{
    public class BalancedChlorofuck : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ProjectileType<Chlorofuck>();
        }
        public override bool PreAI(Projectile projectile)
        {
            Player player = Main.player[projectile.owner];
            projectile.damage = player.FargoSouls().ForceEffect<ChlorophyteEnchant>() ? 65 : 35;
            return base.PreAI(projectile);
        }
    }
}
