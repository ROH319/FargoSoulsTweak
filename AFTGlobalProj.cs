using AFargoTweak.Configs;
using FargowiltasSouls;
using FargowiltasSouls.Content.Patreon.Volknet;
using FargowiltasSouls.Content.Patreon.Volknet.Projectiles;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using FargowiltasSouls.Content.Projectiles.Minions;
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

        }
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            base.ModifyHitNPC(projectile, target, ref modifiers);
            //if (FargoChangesLoader.ProjectileChanges != null)
            //{
            //    var list = FargoChangesLoader.ProjectileChanges.FindAll(change => change.Type == projectile.type);
            //    for(int i = 0; i < list.Count; i++)
            //    {
            //        list[i].ApplyChanges_ModifyHit(projectile, ref modifiers);
            //    }
            //}
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if(FargoChangesLoader.ProjectileChanges != null)
            {
                FargoChangesLoader.ProjectileChanges.FindAll(change => change.Type == projectile.type).ForEach((change) =>
                {
                    change.ApplyChanges_OnSpawn(projectile);
                });
            }
            //if(projectile.type == ModContent.ProjectileType<DragonFireball>())
            //{
            //    projectile.damage = (int)(projectile.damage * AFargoTweak.ConfigInstance.DragonBreath2FireBallDmgMul / 100f);
            //}
            if(source is EntitySource_ItemUse)
            {
                EntitySource_ItemUse parentSource = source as EntitySource_ItemUse;
                if(parentSource.Item.type == ModContent.ItemType<NanoCore>() && projectile.type != ModContent.ProjectileType<PlasmaArrow>())
                {
                    projectile.damage = (int)(projectile.damage * 1.75f / 1.6f);
                }
            }
            if(source is EntitySource_Parent)
            {
                EntitySource_Parent parentSource = source as EntitySource_Parent;
                if(parentSource.Entity is Projectile p && p.type == ModContent.ProjectileType<DestroyerBody2>())
                {
                    projectile.penetrate = 1;
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
