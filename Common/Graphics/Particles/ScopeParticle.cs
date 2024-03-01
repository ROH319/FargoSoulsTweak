using FargowiltasSouls.Common.Graphics.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace AFargoTweak.Common.Graphics.Particles
{
    public class ScopeParticle : Particle
    {
        public int Owner;
        public override Texture2D MainTexture => (Texture2D)AFTAsset.RequestTexture("AFargoTweak/Common/Graphics/Particles/EmptyTex");
        public ScopeParticle(Vector2 worldPosition, Vector2 velocity, Color drawColor, float scale, int lifetime, int owner)
        {
            Position = worldPosition;
            Velocity = velocity;
            DrawColor = drawColor;
            Scale = new(scale);
            MaxLifetime = lifetime;
            Owner = owner;
            
        }
        public override void Update()
        {

            base.Update();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
