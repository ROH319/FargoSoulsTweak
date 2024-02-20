using FargowiltasSouls;
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
    public class BalancedDestroyerMin : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ProjectileType<DestroyerHead2>() || entity.type == ModContent.ProjectileType<DestroyerBody2>() || entity.type == ModContent.ProjectileType<DestroyerTail2>();
        }
        public override void SetDefaults(Projectile entity)
        {
            base.SetDefaults(entity);
            //entity.FargoSouls().noInteractionWithNPCImmunityFrames = false;
            //entity.usesLocalNPCImmunity = true;
            //entity.localNPCHitCooldown = 6;
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            //Main.NewText($"{target.immune[projectile.owner]}");
            base.OnHitNPC(projectile, target, hit, damageDone);
            //Main.NewText($"{target.immune[projectile.owner]}");
        }
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            //Main.NewText($"{target.immune[projectile.owner]}");
            base.ModifyHitNPC(projectile, target, ref modifiers);
            //Main.NewText($"{target.immune[projectile.owner]}");
        }
    }
}
