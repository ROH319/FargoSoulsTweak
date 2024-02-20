using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFargoTweak.Content.NPCChanges
{
    public class SansGolem
    {
        public delegate void LoadGolemSpriteBufferedDelegate(bool recolor, int type, Asset<Texture2D>[] vanillaTexture, Dictionary<int, Asset<Texture2D>> fargoBuffer, string texturePrefix);
        public static void LoadGolemSpriteBuffered(LoadGolemSpriteBufferedDelegate orig, bool recolor, int type, Asset<Texture2D>[] vanillaTexture, Dictionary<int, Asset<Texture2D>> fargoBuffer, string texturePrefix)
        {
            if (recolor)
            {
                if (!fargoBuffer.ContainsKey(type))
                {
                    fargoBuffer[type] = vanillaTexture[type];
                    vanillaTexture[type] = AFTAsset.RequestTexture($"AFargoTweak/Textures/Resprites/{texturePrefix}{type}") ?? vanillaTexture[type];
                }
            }
            else
            {
                if (fargoBuffer.ContainsKey(type))
                {
                    vanillaTexture[type] = fargoBuffer[type];
                    fargoBuffer.Remove(type);
                }
            }
        }
    }
}
