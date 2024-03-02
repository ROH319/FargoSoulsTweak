using Fargowiltas;
using Fargowiltas.Items;
using Fargowiltas.NPCs;
using Fargowiltas.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace AFargoTweak
{
    public static class AFTUtils
    {
        public static FargoPlayer Fargo(this Player player)
        {
            return player.GetModPlayer<FargoPlayer>();
        }
        public static FargoGlobalNPC Fargo(this NPC npc)
        {
            
            return npc.GetGlobalNPC<FargoGlobalNPC>();
        }
        public static FargoGlobalItem Fargo(this Item item)
        {
            return item.GetGlobalItem<FargoGlobalItem>();
        }
        public static FargoGlobalProjectile Fargo(this Projectile projectile)
        {
            return projectile.GetGlobalProjectile<FargoGlobalProjectile>();
        }

        public static AFTModPlayer AFT(this Player player)
            => player.GetModPlayer<AFTModPlayer>();

        public enum NPCImmunityType
        {
            None,
            Local,
            IDStatic,
            NPC
        }

        public enum ForcesImmunity
        {
            None,
            Disable,
            Enable
        }

        public enum DeviMusic
        {
            Default,
            LexusCyanixs,
            DanceOfDeviance
        }
        public enum AbomMusic
        {
            Default,
            Stigma
        }
        public enum MutantMusic
        {
            Default,
            rePrologue,
            SteelRed,
            Storia
        }
    }
}
