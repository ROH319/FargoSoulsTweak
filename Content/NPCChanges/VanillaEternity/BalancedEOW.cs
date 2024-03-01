using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace AFargoTweak.Content.NPCChanges.VanillaEternity
{
    public class BalancedEOW : EModeNPCBehaviour
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(NPCID.EaterofWorldsHead, NPCID.EaterofWorldsBody, NPCID.EaterofWorldsTail);
        public override void SetDefaults(NPC entity)
        {
            base.SetDefaults(entity);
            entity.lifeMax = (int)Math.Round(entity.lifeMax * 1.25f);
        }
        public override void OnFirstTick(NPC npc)
        {
            FieldInfo mass = typeof(EaterofWorlds).GetField("MassDefenseTimer", BindingFlags.Instance | BindingFlags.NonPublic);
            mass.SetValue(npc.GetGlobalNPC<EaterofWorlds>(), int.MaxValue);
            base.OnFirstTick(npc);
        }
    }
}
