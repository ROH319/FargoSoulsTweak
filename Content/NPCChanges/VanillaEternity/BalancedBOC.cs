using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace AFargoTweak.Content.NPCChanges.VanillaEternity
{
    public class BalancedBOC : EModeNPCBehaviour
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(NPCID.BrainofCthulhu);
        public override void SetDefaults(NPC entity)
        {
            base.SetDefaults(entity);
            entity.lifeMax = (int)Math.Round(entity.lifeMax * 1.5f);
        }
        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            base.ModifyIncomingHit(npc, ref modifiers); 
            if (npc.life > 0)
                modifiers.FinalDamage /= Math.Max(0.2f, (float)Math.Sqrt((double)npc.life / npc.lifeMax));
            Main.NewText($"{modifiers.FinalDamage.Multiplicative}");
        }
    }
}
