using AFargoTweak.Configs;
using FargowiltasSouls;
using FargowiltasSouls.Content.Patreon.Volknet;
using FargowiltasSouls.Content.Patreon.Volknet.Projectiles;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AFargoTweak
{
    public class AFTGlobalProj : GlobalProjectile
    {
        public override void SetDefaults(Projectile entity)
        {
            base.SetDefaults(entity);
            if(AFargoTweak.ConfigInstance.GlobalImmunityType == AFTUtils.NPCImmunityType.Local)
            {
                entity.usesLocalNPCImmunity = true;
                entity.localNPCHitCooldown = AFargoTweak.ConfigInstance.GlobalImmunityCD;
            }
            else if(AFargoTweak.ConfigInstance.GlobalImmunityType == AFTUtils.NPCImmunityType.IDStatic)
            {
                entity.usesIDStaticNPCImmunity = true;
                entity.idStaticNPCHitCooldown = AFargoTweak.ConfigInstance.GlobalImmunityCD;
            }
            if(AFargoTweak.ConfigInstance.GlobalIgnoreImmunity == AFTUtils.ForcesImmunity.Enable)
            {
                entity.FargoSouls().noInteractionWithNPCImmunityFrames = true;
            }
            else if (AFargoTweak.ConfigInstance.GlobalIgnoreImmunity == AFTUtils.ForcesImmunity.Disable)
            {
                entity.FargoSouls().noInteractionWithNPCImmunityFrames = false;
            }
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if(FargoChangesLoader.ProjectileChanges != null && FargoChangesLoader.ProjectileChanges.ContainsKey(projectile.type))
            {
                FargoChangesLoader.ProjectileChanges[projectile.type].ApplyChanges_OnSpawn(projectile);
            }
            if(projectile.type == ModContent.ProjectileType<DragonFireball>())
            {
                projectile.damage = (int)(projectile.damage * AFargoTweak.ConfigInstance.DragonBreath2FireBallDmgMul / 100f);
            }
            if(source is EntitySource_ItemUse)
            {
                EntitySource_ItemUse parentSource = source as EntitySource_ItemUse;
                if(parentSource.Item.type == ModContent.ItemType<NanoCore>() && projectile.type != ModContent.ProjectileType<PlasmaArrow>())
                {
                    projectile.damage = (int)(projectile.damage * 1.75f / 1.6f);
                }
            }
            base.OnSpawn(projectile, source);
        }

        public override void PostDraw(Projectile projectile, Color lightColor)
        {
            PreferenceConfig instance = PreferenceConfig.Instance;
            if (instance.ProjName.Enabled)
            {
                Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.MouseText.Value,
                    Lang.GetProjectileName(projectile.type).Value + $"{projectile.ModProjectile?.Name}",
                    projectile.position.X - Main.screenPosition.X + instance.ProjName.Offset.X,
                    projectile.position.Y - Main.screenPosition.Y + instance.ProjName.Offset.Y,
                    instance.ProjName.TextColor,
                    instance.ProjName.BorderColor,
                    Vector2.Zero);
            }
            base.PostDraw(projectile, lightColor);
        }
    }
}
