using Fargowiltas.NPCs;
using FargowiltasSouls;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AFargoTweak.Content.NPCChanges
{
    public class BalancedMutant : GlobalNPC
    {
        //public static Func<MutantBoss, MutantBoss> MDelegate => new Func<MutantBoss, MutantBoss>((boss) =>
        //{
        //    MethodBase chooseattack = typeof(MutantBoss).GetMethod("ChooseNextAttack", BindingFlags.NonPublic);
        //    chooseattack.Invoke(boss, new object[] { 11, 16, 26, 31, 35, 37, 39, 42, 44 });
        //    Main.NewText($"!!!{Main.time}!!!");
        //    return boss;
        //});
        public static void PatchSpearDashDirectP2(ILContext il)
        {
            var c = new ILCursor(il);
            if (!c.TryGotoNext(i => i.MatchLdcI4(10)))
                return;

            c.Index += 6;
            c.Emit(Mono.Cecil.Cil.OpCodes.Ldarg_0);
            //c.Emit(Mono.Cecil.Cil.OpCodes.Br_S, label2);
            c.EmitDelegate<Func<MutantBoss, MutantBoss>>((mutant) =>
            {
                ChooseNextAttack(mutant, 11, 16, 26, 31, 35, 42, 39, 44);
                //MethodBase chooseattack = typeof(MutantBoss).GetMethod("ChooseNextAttack", BindingFlags.NonPublic | BindingFlags.Instance);
                //chooseattack.Invoke(mutant, new object[] { 11, 16, 26, 31, 31, 35, 42, 39, 42, 44 });
                //Main.NewText($"!!!{Main.time}!!!");
                return mutant;
            });
            c.Emit(Mono.Cecil.Cil.OpCodes.Pop);
        }
        public delegate void BoundaryBulletHellDelegate(MutantBoss boss);
        public static void BulletHellHook(BoundaryBulletHellDelegate orig ,MutantBoss boss)
        {
            int endTime = 360 + 60 + (int)(240 * 2 * (boss.endTimeVariance - 0.33f));
            bool end = false;
            if (boss.NPC.ai[3] > endTime - 1)
            {
                end = true;
            }
            orig(boss);
            if (end)
            {
                //ChooseNextAttack(boss, 13, 19, 20, 21, 24, WorldSavingSystem.MasochistModeReal ? 31 : 26, 33, 41, 44);
                ChooseNextAttack(boss, 19);
                //Main.NewText($"{Main.time}");
            }
        }
        public delegate void ChooseNextAttackDelegate(MutantBoss boss, params int[] args);
        public static void ChooseAttackHook(ChooseNextAttackDelegate orig, MutantBoss boss, params int[] args)
        {
            List<int> newarray = args.ToList();
            //string array = string.Join<int>(",", args);
            //string history = string.Join<float>(",", boss.attackHistory);
            //Main.NewText($"{boss.AttackChoice}__{array}__{history}");//查看突变体招式对应AttackChoice和history
            //var end = boss.attackHistory.LastOrDefault();
            if(boss.AttackChoice == 17)//幻影眼后不接激光
            {
                newarray.RemoveAll(i => i == 11);
            }
            if (boss.AttackChoice == 23 && boss.attackHistory.LastOrDefault() == 21)//淡蓝冲刺后不接猪鲨
            {
                newarray.RemoveAll(i => i == 29 || i == 37);
            }
            args = newarray.ToArray();
            orig(boss, args);
        }
        public static void PatchBoundaryBulletHellP2(ILContext il)
        {
            var c = new ILCursor(il);
            if(!c.TryGotoNext(i => i.MatchRet()))
            {
                return;
            }
            c.Index--;
            c.Emit(Mono.Cecil.Cil.OpCodes.Ldarg_0);
            c.EmitDelegate<Func<MutantBoss, MutantBoss>>((mutant) =>
            {
                ChooseNextAttack(mutant, 13, 19, 20, 21, 24, WorldSavingSystem.MasochistModeReal ? 31 : 26, 33, 41, 44);
                return mutant;
            });
            c.Emit(Mono.Cecil.Cil.OpCodes.Pop);
        }

        public bool StoredSpearDash;
        public bool HalvedDashNumber;
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<MutantBoss>();
        }
        public override bool PreAI(NPC npc)
        {
            if ((int)npc.ai[0] == 13 && !StoredSpearDash && !HalvedDashNumber)//前半段
            {
                StoredSpearDash = true;
            }
            if ((int)npc.ai[0] == 15 
                && StoredSpearDash 
                && npc.ai[2] == (int)(npc.localAI[1]/2)
                && npc.ai[1] > 29)//冲到一半准备释放追踪幻影眼
            {
                npc.ai[0] = 13;
                npc.ai[1] = 0;
                npc.ai[2] = 0;//攻击计数清零
                npc.ai[3] = 0;
                StoredSpearDash = false;
                HalvedDashNumber = true;
            }
            if (npc.ai[0] == 14 && HalvedDashNumber)//后续冲刺次数减半
            {
                if (npc.localAI[1] != 0)
                {
                    npc.localAI[1] /= 2;
                    npc.localAI[1] += 1;
                    HalvedDashNumber = false;
                }
            }
            //Main.NewText($"{npc.ai[0]}");
            return base.PreAI(npc);
        }

        public static void ChooseNextAttack(MutantBoss mutant, params int[] args)
        {
            float buffer = mutant.AttackChoice + 1;
            mutant.AttackChoice = 48;
            mutant.NPC.ai[1] = 0;
            mutant.NPC.ai[2] = buffer;
            mutant.NPC.ai[3] = 0;
            mutant.NPC.localAI[0] = 0;
            mutant.NPC.localAI[1] = 0;
            mutant.NPC.localAI[2] = 0;
            //NPC.TargetClosest();
            mutant.NPC.netUpdate = true;

            //EdgyBossText(RandomObnoxiousQuote());

            /*string text = "-------------------------------------------------";
            Main.NewText(text);

            text = "";
            foreach (float f in attackHistory)
                text += f.ToString() + " ";
            Main.NewText($"history: {text}");*/

            if (WorldSavingSystem.EternityMode)
            {
                //become more likely to use randoms as life decreases
                bool useRandomizer = mutant.NPC.localAI[3] >= 3 && (WorldSavingSystem.MasochistModeReal || Main.rand.NextFloat(0.8f) + 0.2f > (float)Math.Pow((float)mutant.NPC.life / mutant.NPC.lifeMax, 2));

                if (FargoSoulsUtil.HostCheck)
                {
                    Queue<float> recentAttacks = new(mutant.attackHistory); //copy of attack history that i can remove elements from freely

                    //if randomizer, start with a random attack, else use the previous state + 1 as starting attempt BUT DO SOMETHING ELSE IF IT'S ALREADY USED
                    if (useRandomizer)
                        mutant.NPC.ai[2] = Main.rand.Next(args);

                    //Main.NewText(useRandomizer ? "(Starting with random)" : "(Starting with regular next attack)");

                    while (recentAttacks.Count > 0)
                    {
                        bool foundAttackToUse = false;

                        for (int i = 0; i < 5; i++) //try to get next attack that isnt in this queue
                        {
                            if (!recentAttacks.Contains(mutant.NPC.ai[2]))
                            {
                                foundAttackToUse = true;
                                break;
                            }
                            mutant.NPC.ai[2] = Main.rand.Next(args);
                        }

                        if (foundAttackToUse)
                            break;

                        //couldn't find an attack to use after those attempts, forget 1 attack and repeat
                        recentAttacks.Dequeue();

                        //Main.NewText("REDUCE");
                    }

                    /*text = "";
                    foreach (float f in recentAttacks)
                        text += f.ToString() + " ";
                    Main.NewText($"recent: {text}");*/
                }
            }

            if (FargoSoulsUtil.HostCheck)
            {
                int maxMemory = WorldSavingSystem.MasochistModeReal ? 10 : 16;

                if (mutant.attackCount++ > maxMemory * 1.25) //after doing this many attacks, shorten queue so i can be more random again
                {
                    mutant.attackCount = 0;
                    maxMemory /= 4;
                }

                mutant.attackHistory.Enqueue(mutant.NPC.ai[2]);
                while (mutant.attackHistory.Count > maxMemory)
                    mutant.attackHistory.Dequeue();
            }

            mutant.endTimeVariance = WorldSavingSystem.MasochistModeReal ? Main.rand.NextFloat() : 0;

            /*text = "";
            foreach (float f in attackHistory)
                text += f.ToString() + " ";
            Main.NewText($"after: {text}");*/
        }
    }

    public class BalancedMutantSpearSpin : GlobalProjectile
    {
        public bool setPredictive;
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ProjectileType<MutantSpearSpin>();
        }
        public override bool PreAI(Projectile projectile)
        {
            NPC mutant = Main.npc[(int)projectile.ai[0]];
            if (!mutant.active) return false;
            if (mutant.GetGlobalNPC<BalancedMutant>().StoredSpearDash)//前半段
            {
                //Main.NewText($"{Main.time}");
                if (projectile.localAI[1] == 0)
                {
                    projectile.localAI[1] = Main.rand.NextBool() ? -1 : 1;
                    projectile.timeLeft = (int)projectile.ai[1];
                }
                projectile.Center = mutant.Center;
                projectile.direction = mutant.direction;
                if (projectile.timeLeft % 20 == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item1, projectile.Center);
                }
                if (mutant.ai[0] == 13 && !setPredictive)
                {
                    FieldInfo field = typeof(MutantSpearSpin).GetField("predictive", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (field != null)
                    {
                        field.SetValue(projectile.ModProjectile as MutantSpearSpin, true);
                    }
                    setPredictive = true;
                }
                projectile.alpha = 0;
                if (mutant.ai[0] != 4 && mutant.ai[0] != 13 && mutant.ai[0] != 21)
                {
                    projectile.alpha = 255;
                }
                return false;
            }
            return base.PreAI(projectile);
        }
        
    }
}
