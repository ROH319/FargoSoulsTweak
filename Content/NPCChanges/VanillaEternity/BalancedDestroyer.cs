using FargowiltasSouls;
using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AFargoTweak.Content.NPCChanges.VanillaEternity
{
    public class BalancedDestroyer : EModeNPCBehaviour
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(NPCID.TheDestroyer);

        public override bool SafePreAI(NPC npc)
        {
            var destroyer = npc.GetGlobalNPC<Destroyer>();
            if (!destroyer.InPhase2)
            {
                if (npc.life < (int)(npc.lifeMax * (WorldSavingSystem.MasochistModeReal ? 0.975 : 0.875)))
                {
                    destroyer.InPhase2 = true;
                    destroyer.AttackModeTimer = Destroyer.P2_COIL_BEGIN_TIME;
                    npc.netUpdate = true;
                    if (npc.HasPlayerTarget)
                        SoundEngine.PlaySound(SoundID.Roar, Main.player[npc.target].Center);
                    //Main.NewText("PH2");
                }
            }
            bool result = base.SafePreAI(npc);
            return result;
        }
    }
    public class BalancedDestroyerSegment
    {
        public delegate void SafeOHByProj(DestroyerSegment embehavior, NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone);
        public static void OHByProjHook(SafeOHByProj orig, DestroyerSegment embehavior, NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            int oridmg = projectile.damage;
            orig(embehavior, npc, projectile, hit, damageDone);
             projectile.damage = oridmg;
            //if (oridmg != projectile.damage)
            //    Main.NewText($"{oridmg} {projectile.damage}");
        }
        //public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(NPCID.TheDestroyerBody, NPCID.TheDestroyerTail);

        //public override void SafeModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        //{
        //    //if (owner != null && !owner.InPhase2)
        //    //projectile.numHits = 0;
        //    float mul1 = modifiers.FinalDamage.Multiplicative;
        //    float mul2 = -1f;
        //    if (projectile.numHits > 0 && !FargoSoulsUtil.IsSummonDamage(projectile))
        //    {
        //        modifiers.FinalDamage /= 2.0f / 3.0f + 1.0f / 3.0f * 1f / projectile.numHits;
        //        mul2 = modifiers.FinalDamage.Multiplicative;
        //    }
        //    base.SafeModifyHitByProjectile(npc, projectile, ref modifiers);
        //    //Main.NewText($"{mul1} {mul2} {modifiers.FinalDamage.Multiplicative}");
        //}
        //public override void SafeOnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        //{
        //    int oridmg = projectile.damage;
        //    base.SafeOnHitByProjectile(npc, projectile, hit, damageDone);
        //    Main.NewText($"{oridmg} {projectile.damage}");
        //}
        //public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        //{
        //    return entity.type == NPCID.TheDestroyerBody || entity.type == NPCID.TheDestroyerTail;
        //}
        //public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        //{
        //    var add = modifiers.FinalDamage.Additive;
        //    var mul = modifiers.FinalDamage.Multiplicative;
        //    if (projectile.numHits > 0 && !FargoSoulsUtil.IsSummonDamage(projectile))
        //    {
        //        modifiers.FinalDamage /= 2.0f / 3.0f + 1.0f / 3.0f * 1f / projectile.numHits;
        //    }
        //    base.ModifyHitByProjectile(npc, projectile, ref modifiers);
        //    //Main.NewText($"{add} {mul} {modifiers.FinalDamage.Additive} {modifiers.FinalDamage.Multiplicative}");

        //}
        //public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        //{
        //    int oridmg = projectile.damage;
        //    base.OnHitByProjectile(npc, projectile, hit, damageDone);
        //    Main.NewText($"{oridmg} {projectile.damage}");
        //}
    }
}
