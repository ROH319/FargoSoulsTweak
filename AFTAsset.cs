using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace AFargoTweak
{
    public class AFTAsset : ModSystem
    {
        public static Dictionary<string, Asset<Texture2D>> TextureAsset = new();
        public static Asset<Texture2D> RequestTexture(string path, AssetRequestMode mode = AssetRequestMode.ImmediateLoad)
        {
            Asset<Texture2D> tex;
            if(TextureAsset.TryGetValue(path, out tex))
            {
                return tex;
            }
            tex = ModContent.Request<Texture2D>(path, mode);
            TextureAsset.Add(path, tex);
            return tex;
        }
    }
}
