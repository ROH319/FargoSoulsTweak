using FargowiltasSouls.Content.Projectiles.BossWeapons;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Content.Projectiles.Minions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AFargoTweak.Content.ProjectileOverrides.BossWeapons
{
    public class BalancedDarkStarFriendly
    {
        public delegate string DSFriendlyTextureDelegate(MechElectricOrbFriendly self);
        public static string DSFriendlyTextureHook(DSFriendlyTextureDelegate orig, MechElectricOrbFriendly self)
        {
            return "Terraria/Images/Projectile_12";
        }
    }
    //public class BalancedDarkStar : GlobalProjectile
    //{
    //    public delegate string DSTextureDelegate(MechElectricOrb self);
    //    public static string DSTextureHook(MechElectricOrb self)
    //    {
    //        return "Terraria/Images/Projectile_12";
    //    }
    //    public static List<int> DarkStarTypes;
    //    public bool lastSecondAcc;
    //    public override bool InstancePerEntity => true;
    //    public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
    //    {
    //        if (DarkStarTypes == null) return false;
    //        return DarkStarTypes.Contains(entity.type);
    //    }
    //    public override void SetStaticDefaults()
    //    {
    //        DarkStarTypes = new List<int>()
    //        {
    //            ModContent.ProjectileType<MechElectricOrb>(),
    //            ModContent.ProjectileType<MechElectricOrbFriendly>(),
    //            ModContent.ProjectileType<MechElectricOrbDestroyer>(),
    //            ModContent.ProjectileType<MechElectricOrbHoming>(),
    //            ModContent.ProjectileType<MechElectricOrbHomingFriendly>(),
    //            ModContent.ProjectileType<MechElectricOrbTwins>()
    //        };
    //        for(int i = 0; i < DarkStarTypes.Count; i++)
    //        {
    //            TextureAssets.Projectile[DarkStarTypes[i]] = TextureAssets.Projectile[12];
    //        }
    //        base.SetStaticDefaults();
    //    }
    //    public override void AI(Projectile projectile)
    //    {
    //        if (projectile.soundDelay == 0)
    //        {
    //            projectile.soundDelay = 60 + Main.rand.Next(60);
    //            SoundEngine.PlaySound(SoundID.Item9, projectile.position);
    //        }
            
    //        //FieldInfo lastSecondAcc = projectile.ModProjectile.GetType().GetField("lastSecondAccel",BindingFlags.NonPublic);
    //        if (projectile.localAI[1] == 0)
    //        {
    //            projectile.localAI[1] = 1f;
    //            if (projectile.ai[1] == 1f)
    //                SoundEngine.PlaySound(SoundID.Item33, projectile.position);

    //            //doing it this way so projs that inherit from dark star dont inherit the accel
    //            lastSecondAcc = projectile.type == ModContent.ProjectileType<MechElectricOrb>();
    //        }

    //        if (projectile.localAI[0] == 0)
    //            projectile.localAI[0] = 1f;
    //        projectile.alpha += (int)(25.0 * projectile.localAI[0]);
    //        if (projectile.alpha > 200)
    //        {
    //            projectile.alpha = 200;
    //            projectile.localAI[0] = -1f;
    //        }
    //        if (projectile.alpha < 0)
    //        {
    //            projectile.alpha = 0;
    //            projectile.localAI[0] = 1f;
    //        }

    //        projectile.rotation = projectile.rotation + (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 0.01f * projectile.direction;

    //        if (Main.rand.NextBool(30))
    //            Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Enchanted_Pink, (float)(projectile.velocity.X * 0.5), (float)(projectile.velocity.Y * 0.5), 150, default, 1.2f);

    //        Lighting.AddLight(projectile.Center, 0.9f, 0.8f, 0.1f);

    //        if (lastSecondAcc && projectile.ai[0] == -1 && --projectile.ai[1] < 0)
    //            projectile.velocity *= 1.03f;

    //        //cap proj velocity so to reduce the gap in its hitbox
    //        float ratio = projectile.velocity.Length() / (projectile.width * 3);
    //        if (ratio > 1)
    //            projectile.velocity /= ratio;

    //        //base.AI(projectile);
    //    }
    //    public override Color? GetAlpha(Projectile projectile, Color lightColor)
    //    {
    //        return new Color(255, 100, 100, lightColor.A - projectile.alpha);
    //    }
    //    public override void OnKill(Projectile projectile, int timeLeft)
    //    {
    //        SoundEngine.PlaySound(SoundID.Item10, projectile.position);

    //        //Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 58, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 150, new Color(), 1.2f);
    //        /*int Type = Main.rand.Next(16, 18);
    //        if (Projectile.type == 503)
    //            Type = 16;
    //        if (!Main.dedServ)
    //            Gore.NewGore(Projectile.position, new Vector2(Projectile.velocity.X * 0.05f, Projectile.velocity.Y * 0.05f), Type, 1f);*/

    //        Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Enchanted_Gold, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 150, new Color(), 1.2f);
    //        if (!Main.dedServ)
    //            Gore.NewGore(projectile.GetSource_FromThis(), projectile.position, new Vector2(projectile.velocity.X * 0.05f, projectile.velocity.Y * 0.05f), Main.rand.Next(16, 18), 1f);
    //        //base.OnKill(projectile, timeLeft);
    //    }
    //    public override bool PreDraw(Projectile projectile, ref Color lightColor)
    //    {
    //        Texture2D glow = ModContent.Request<Texture2D>("AFargoTweak/Textures/Projectiles/Masomode/DarkStar_Glow", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
    //        int rect1 = glow.Height;
    //        int rect2 = rect1;
    //        Rectangle glowrectangle = new(0, rect2, glow.Width, rect1);
    //        Vector2 gloworigin2 = glowrectangle.Size() / 2f;
    //        Color glowcolor = Color.Lerp(new Color(255, 100, 100, 150), Color.Transparent, 0.8f);
    //        Vector2 drawCenter = projectile.Center - projectile.velocity.SafeNormalize(Vector2.UnitX) * 14;

    //        Main.EntitySpriteDraw(glow, drawCenter - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(glowrectangle),//create small, non transparent trail texture
    //               new Color(255, 100, 100, lightColor.A - projectile.alpha), projectile.velocity.ToRotation() + MathHelper.PiOver2, gloworigin2, projectile.scale * 2, SpriteEffects.None, 0);

    //        for (int i = 0; i < 3; i++) //create multiple transparent trail textures ahead of the projectile
    //        {
    //            Vector2 drawCenter2 = drawCenter + (projectile.velocity.SafeNormalize(Vector2.UnitX) * 12).RotatedBy(MathHelper.Pi / 5 - i * MathHelper.Pi / 5); //use a normalized version of the projectile's velocity to offset it at different angles
    //            drawCenter2 -= projectile.velocity.SafeNormalize(Vector2.UnitX) * 12; //then move it backwards
    //            Main.EntitySpriteDraw(glow, drawCenter2 - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(glowrectangle),
    //                glowcolor, projectile.velocity.ToRotation() + MathHelper.PiOver2, gloworigin2, projectile.scale, SpriteEffects.None, 0);
    //        }

    //        for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i++) //reused betsy fireball scaling trail thing
    //        {

    //            Color color27 = glowcolor;
    //            color27 *= (float)(ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[projectile.type];
    //            float scale = projectile.scale * (ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[projectile.type];
    //            Vector2 value4 = projectile.oldPos[i] - projectile.velocity.SafeNormalize(Vector2.UnitX) * 14;
    //            Main.EntitySpriteDraw(glow, value4 + projectile.Size / 2f - Main.screenPosition + new Vector2(0, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(glowrectangle), color27,
    //                projectile.velocity.ToRotation() + MathHelper.PiOver2, gloworigin2, scale, SpriteEffects.None, 0);
    //        }

    //        return false;
    //    }
    //    public override void PostDraw(Projectile projectile, Color lightColor)
    //    {
    //        //Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Projectile[projectile.type].Value;
    //        Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Projectile[12].Value;
    //        int num156 = Terraria.GameContent.TextureAssets.Projectile[projectile.type].Value.Height; //ypos of lower right corner of sprite to draw
    //        int y3 = num156; //ypos of upper left corner of sprite to draw
    //        Rectangle rectangle = new(0, y3, texture2D13.Width, num156);
    //        Vector2 origin2 = rectangle.Size() / 2f;
    //        Main.EntitySpriteDraw(texture2D13, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), new Color(255, 100, 100, lightColor.A - projectile.alpha), projectile.rotation, origin2, scale: projectile.scale*2, SpriteEffects.None, 0);

    //    }
    //}
    public class BalancedDarkStarDestroyer
    {
        public delegate string DSDestroyerTexDelegate(MechElectricOrbDestroyer self);
        public static string DSDestroyerTexHook(DSDestroyerTexDelegate orig, MechElectricOrbDestroyer self)
        {
            return "Terraria/Images/Projectile_12";
        }
    }
    public class BalancedDarkStarHoming
    {
        public delegate string DSHomingTexDelegate(MechElectricOrbHoming self);
        public static string DSHomingTexHook(DSHomingTexDelegate orig, MechElectricOrbHoming self)
        {
            return "Terraria/Images/Projectile_12";
        }
    }
    public class BalancedDarkStarTwin
    {
        public delegate string DSTwinDelegate(MechElectricOrbTwins self);
        public static string DSTwinTexHook(DSTwinDelegate orig, MechElectricOrbTwins self)
        {
            return "Terraria/Images/Projectile_12";
        }
    }
    public class BalancedDarkStarHomFri
    {
        public delegate string DSHomFriDelegate(MechElectricOrbHomingFriendly self);
        public static string DSHomFriHook(DSHomFriDelegate orig, MechElectricOrbHomingFriendly self)
        {
            return "Terraria/Images/Projectile_12";
        }
    }
    public class BalancedDestroyerLaser : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ProjectileType<DestroyerLaser>();
        }
        public override Color? GetAlpha(Projectile projectile, Color lightColor)
        {
            return Color.Red * projectile.Opacity;
        }
    }
}
