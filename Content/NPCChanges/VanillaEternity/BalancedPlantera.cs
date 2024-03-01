using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Systems;

namespace AFargoTweak.Content.NPCChanges
{
    public class BalancedPlantera : EModeNPCBehaviour
    {
        public static void PatchAI(ILContext il)
        {
            var c = new ILCursor(il);
            if (!c.TryGotoNext(i => i.MatchStloc(11)))
            {
                throw new Exception("IL edit 11 failed");
                return;
            }
            c.Index-=6;
            c.EmitDelegate<Func<bool, bool>>((entered) =>
            {
                if (!WorldSavingSystem.MasochistModeReal) return false;//没maso你也想进三阶段？
                return entered;
            });
            //c.Emit(Mono.Cecil.Cil.OpCodes.Pop);//把你EnteredPhase3踹咯
            //c.Emit(Mono.Cecil.Cil.OpCodes.Ldc_I4, 0);//塞个false进去

            if (!c.TryGotoNext(i => i.MatchStloc(56)))
            {
                throw new Exception("IL edit failed");
                return;
            }
            c.Index += 2;
            c.EmitDelegate<Func<bool, bool>>((maso) =>
            {
                return false;
            });
            //if (!c.TryGotoNext(i => i.MatchStloc(123)))
            //{
            //    throw new Exception("IL edit failed");
            //    return;
            //}
            //c.Index--;
            //c.Emit(Mono.Cecil.Cil.OpCodes.Pop);//把你maso踹了让你叶绿水晶环无法预判
            //c.Emit(Mono.Cecil.Cil.OpCodes.Ldc_I4, 0);//塞个false进去
        }

        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(NPCID.Plantera);
        public override bool SafePreAI(NPC npc)
        {
            var plantera = npc.GetGlobalNPC<Plantera>();
            if (!WorldSavingSystem.MasochistModeReal) plantera.EnteredPhase3 = true;
            plantera.CrystalRedirectTimer = 0;
            return base.SafePreAI(npc);
        }
    }
    //    public class BalancedCrystalLeafShot : GlobalProjectile
    //    {
    //        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
    //        {
    //            return entity.type == ModContent.ProjectileType<CrystalLeafShot>();
    //        }
    //        public override void AI(Projectile projectile)
    //        {
    //            //projectile.ai[1] = 0;
    //            //projectile.netUpdate = true;
    //            base.AI(projectile);
    //        }
    //    }
}
