using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AFargoTweak.Content.NPCChanges.VanillaEternity
{
    public class BalancedWOFEye : EModeNPCBehaviour
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(NPCID.WallofFleshEye);
        public override void SafePostAI(NPC npc)
        {
            //var eye = npc.GetGlobalNPC<WallofFleshEye>();
            //if (eye.TelegraphingLasers)
            //{

            //}
            base.SafePostAI(npc);
        }
    }
}
