using AFargoTweak.Configs;
using Fargowiltas.NPCs;
using FargowiltasSouls;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
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
            //orig(boss, args);
            float buffer = boss.AttackChoice + 1;
            boss.AttackChoice = 48;
            boss.NPC.ai[1] = 0;
            boss.NPC.ai[2] = buffer;
            boss.NPC.ai[3] = 0;
            boss.NPC.localAI[0] = 0;
            boss.NPC.localAI[1] = 0;
            boss.NPC.localAI[2] = 0;
            //NPC.TargetClosest();
            boss.NPC.netUpdate = true;

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
                bool masocanran = WorldSavingSystem.MasochistModeReal && BossConfig.Instance.MutantSetting.MasoCanRandom;
                bool lowlifecanran = Main.rand.NextFloat(0.8f) + 0.2f > (float)Math.Pow((float)boss.NPC.life / boss.NPC.lifeMax, 2) && BossConfig.Instance.MutantSetting.LowLifeCanRandom;
                bool useRandomizer = boss.NPC.localAI[3] >= 3 && (masocanran || lowlifecanran);
                if (BossConfig.Instance.MutantSetting.ForceRandom) useRandomizer = true;

                if (FargoSoulsUtil.HostCheck)
                {
                    Queue<float> recentAttacks = new(boss.attackHistory); //copy of attack history that i can remove elements from freely

                    //if randomizer, start with a random attack, else use the previous state + 1 as starting attempt BUT DO SOMETHING ELSE IF IT'S ALREADY USED
                    if (useRandomizer)
                        boss.NPC.ai[2] = Main.rand.Next(args);

                    //Main.NewText(useRandomizer ? "(Starting with random)" : "(Starting with regular next attack)");

                    while (recentAttacks.Count > 0)
                    {
                        bool foundAttackToUse = false;

                        for (int i = 0; i < 5; i++) //try to get next attack that isnt in this queue
                        {
                            if (!recentAttacks.Contains(boss.NPC.ai[2]))
                            {
                                foundAttackToUse = true;
                                break;
                            }
                            boss.NPC.ai[2] = Main.rand.Next(args);
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

                if (boss.attackCount++ > maxMemory * 1.25) //after doing this many attacks, shorten queue so i can be more random again
                {
                    boss.attackCount = 0;
                    maxMemory /= 4;
                }

                boss.attackHistory.Enqueue(boss.NPC.ai[2]);
                while (boss.attackHistory.Count > maxMemory)
                    boss.attackHistory.Dequeue();
            }

            boss.endTimeVariance = WorldSavingSystem.MasochistModeReal ? Main.rand.NextFloat() : 0;

            /*text = "";
            foreach (float f in attackHistory)
                text += f.ToString() + " ";
            Main.NewText($"after: {text}");*/
        }
        public delegate void EMSEDelegate(MutantBoss boss);
        public static void EMSEHook(EMSEDelegate orig, MutantBoss boss)
        {
            orig(boss);
            
        }
        public delegate void P1FirstAttackCheckDelegate(MutantBoss boss);
        public static void P1CheckHook(P1FirstAttackCheckDelegate orig, MutantBoss boss)
        {
            orig(boss);
            if (boss.NPC.ai[1] == 61 && boss.NPC.ai[2] < boss.NPC.ai[3] && FargoSoulsUtil.HostCheck)
            {
                Player player = Main.player[boss.NPC.target];
                int skipp1required = BossConfig.Instance.MutantSetting.SkipP1Required;
                bool masocanskip = BossConfig.Instance.MutantSetting.MasoCanSkip;
                if (WorldSavingSystem.EternityMode && WorldSavingSystem.SkipMutantP1 >= skipp1required && (!WorldSavingSystem.MasochistModeReal || masocanskip))
                {
                    boss.AttackChoice = 10; //skip to phase 2
                    boss.NPC.ai[1] = 0;
                    boss.NPC.ai[2] = 0;
                    boss.NPC.ai[3] = 0;
                    boss.NPC.localAI[0] = 0;
                    boss.NPC.netUpdate = true;

                    if (WorldSavingSystem.SkipMutantP1 == skipp1required)
                        FargoSoulsUtil.PrintLocalization($"Mods.{boss.Mod.Name}.NPCs.MutantBoss.SkipP1", Color.LimeGreen);

                    if (WorldSavingSystem.SkipMutantP1 >= skipp1required)
                        boss.NPC.ai[2] = 1; //flag for different p2 transition animation

                    return;
                }

                if (WorldSavingSystem.MasochistModeReal && boss.NPC.ai[2] == 0) //first time only
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath13, boss.NPC.Center);
                    if (FargoSoulsUtil.HostCheck) //spawn worm
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            Vector2 vel = boss.NPC.DirectionFrom(player.Center).RotatedByRandom(MathHelper.ToRadians(120)) * 10f;
                            float ai1 = 0.8f + 0.4f * j / 5f;
                            int current = Projectile.NewProjectile(boss.NPC.GetSource_FromThis(), boss.NPC.Center, vel, ModContent.ProjectileType<MutantDestroyerHead>(), FargoSoulsUtil.ScaledProjectileDamage(boss.NPC.damage), 0f, Main.myPlayer, boss.NPC.target, ai1);
                            //timeleft: remaining duration of this case + extra delay after + successive death
                            Main.projectile[current].timeLeft = 90 * ((int)boss.NPC.ai[3] + 1) + 30 + j * 6;
                            int max = Main.rand.Next(8, 19);
                            for (int i = 0; i < max; i++)
                                current = Projectile.NewProjectile(boss.NPC.GetSource_FromThis(), boss.NPC.Center, vel, ModContent.ProjectileType<MutantDestroyerBody>(), FargoSoulsUtil.ScaledProjectileDamage(boss.NPC.damage), 0f, Main.myPlayer, Main.projectile[current].identity);
                            int previous = current;
                            current = Projectile.NewProjectile(boss.NPC.GetSource_FromThis(), boss.NPC.Center, vel, ModContent.ProjectileType<MutantDestroyerTail>(), FargoSoulsUtil.ScaledProjectileDamage(boss.NPC.damage), 0f, Main.myPlayer, Main.projectile[current].identity);
                            Main.projectile[previous].localAI[1] = Main.projectile[current].identity;
                            Main.projectile[previous].netUpdate = true;
                        }
                    }
                }



                Projectile.NewProjectile(boss.NPC.GetSource_FromThis(), boss.NPC.Center, boss.NPC.DirectionTo(player.Center + player.velocity * 30f), ModContent.ProjectileType<MutantDeathrayAim>(), 0, 0f, Main.myPlayer, 85f, boss.NPC.whoAmI);
                Projectile.NewProjectile(boss.NPC.GetSource_FromThis(), boss.NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearAim>(), FargoSoulsUtil.ScaledProjectileDamage(boss.NPC.damage), 0f, Main.myPlayer, boss.NPC.whoAmI, 3);

                //Projectile.NewProjectile(npc.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearAim>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.whoAmI);
            }
        }
        
        //public bool StoredSpearDash;
        //public bool HalvedDashNumber;
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<MutantBoss>();
        }
        public override void SetDefaults(NPC entity)
        {
            base.SetDefaults(entity);
            if (PreferenceConfig.Instance.MutantMusicTypeP1 != AFTUtils.MutantMusic.Default)
            {
                if (ModLoader.TryGetMod("FargowiltasMusic", out Mod musicMod))
                {
                    switch (PreferenceConfig.Instance.MutantMusicTypeP1)
                    {
                        case AFTUtils.MutantMusic.Storia:
                            entity.ModNPC.Music = MusicLoader.GetMusicSlot(musicMod, "Assets/Music/Storia");
                            break;
                        case AFTUtils.MutantMusic.SteelRed:
                            entity.ModNPC.Music = MusicLoader.GetMusicSlot(musicMod, "Assets/Music/SteelRed");
                            break;
                        case AFTUtils.MutantMusic.rePrologue:
                            entity.ModNPC.Music = MusicLoader.GetMusicSlot(musicMod, "Assets/Music/rePrologue");
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        public override bool PreAI(NPC npc)
        {
            //if ((int)npc.ai[0] == 13 && !StoredSpearDash && !HalvedDashNumber)//前半段
            //{
            //    StoredSpearDash = true;
            //}
            //if ((int)npc.ai[0] == 15 
            //    && StoredSpearDash 
            //    && npc.ai[2] == (int)(npc.localAI[1]/2)
            //    && npc.ai[1] > 29)//冲到一半准备释放追踪幻影眼
            //{
            //    npc.ai[0] = 13;
            //    npc.ai[1] = 0;
            //    npc.ai[2] = 0;//攻击计数清零
            //    npc.ai[3] = 0;
            //    StoredSpearDash = false;
            //    HalvedDashNumber = true;
            //}
            //if (npc.ai[0] == 14 && HalvedDashNumber)//后续冲刺次数减半
            //{
            //    if (npc.localAI[1] != 0)
            //    {
            //        npc.localAI[1] /= 2;
            //        npc.localAI[1] += 1;
            //        HalvedDashNumber = false;
            //    }
            //}
            //Main.NewText($"{npc.ai[0]}");
            return base.PreAI(npc);
        }
        public override void PostAI(NPC npc)
        {
            //Main.NewText($"{npc.ai[1]} {npc.ai[2]} {npc.ai[3]}");
            if (PreferenceConfig.Instance.MutantMusicTypeP2 != AFTUtils.MutantMusic.Default)
            {
                if (WorldSavingSystem.EternityMode && ModLoader.TryGetMod("FargowiltasMusic", out Mod musicMod))
                {
                    switch (PreferenceConfig.Instance.MutantMusicTypeP2)
                    {
                        case AFTUtils.MutantMusic.Storia:
                            npc.ModNPC.Music = MusicLoader.GetMusicSlot(musicMod, "Assets/Music/Storia");
                            break;
                        case AFTUtils.MutantMusic.SteelRed:
                            npc.ModNPC.Music = MusicLoader.GetMusicSlot(musicMod, "Assets/Music/SteelRed");
                            break;
                        case AFTUtils.MutantMusic.rePrologue:
                            npc.ModNPC.Music = MusicLoader.GetMusicSlot(musicMod, "Assets/Music/rePrologue");
                            break;
                        default:
                            break;
                    }
                }
            }
            base.PostAI(npc);
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
        public override void PostAI(Projectile projectile)
        {
            NPC mutant = Main.npc[(int)projectile.ai[0]];
            if (mutant.ai[0] == 13)
            {
                int factor = BossConfig.Instance.MutantSetting.MutantEyeCompressFactor;
                if (projectile.timeLeft > (int)projectile.ai[1] / factor)
                    projectile.localAI[0]--;
                else
                    projectile.localAI[0] += factor - 1;
            }
            base.PostAI(projectile);
        }

    }

    public class BalancedEyeAndSphere : GlobalProjectile
    {
        //public int ritualID = -1;
        public Vector2 oriRitualCenter;
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ProjectileType<MutantEye>() || entity.type == ModContent.ProjectileType<MutantSphereRing>();
        }
        public override void SetDefaults(Projectile entity)
        {
            base.SetDefaults(entity);

            //if (ritualID == -1)
            //{
            //    ritualID = -2;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].type == ModContent.ProjectileType<MutantRitual>())
                    {
                        //ritualID = i;
                        oriRitualCenter = Main.projectile[i].Center;
                        break;
                    }
                }
            //}
        }
        public override void PostAI(Projectile projectile)
        {
            if (projectile.Distance(oriRitualCenter) > 1200f) 
                projectile.timeLeft = 0;
            base.PostAI(projectile);
        }
    }

}
