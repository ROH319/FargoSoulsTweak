using AFargoTweak.Configs;
using AFargoTweak.Content.ItemChanges.Accessories;
using AFargoTweak.Content.NPCChanges;
using AFargoTweak.Content.NPCChanges.VanillaEternity;
using AFargoTweak.Content.ProjectileOverrides;
using AFargoTweak.Content.ProjectileOverrides.BossWeapons;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Content.Items.Weapons.SwarmDrops;
using FargowiltasSouls.Content.Patreon;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Content.Projectiles.Minions;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.CodeAnalysis.FlowAnalysis;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace AFargoTweak
{
    public partial class AFTModSystem : ModSystem
    {
        public delegate string NameDelegate(ProjectileDefinition pd);
        public static string NameHook(NameDelegate orig, ProjectileDefinition pd)
        {
            string result = orig(pd);
            return pd.IsUnloaded ? Language.GetTextValue("Mods.ModLoader.Unloaded") : string.Concat(Lang.GetProjectileName(pd.Type).Value, " ", pd.Name);
        }
        
        public override void Load()
        {
            On_Player.UpdateManaRegen += On_Player_UpdateManaRegen;
            On_Player.NebulaLevelup += BalancedComputationOrb.On_Player_NebulaLevelup;
            //On_Player.AddBuff += BalancedComputationOrb.On_Player_AddBuff;
            //Type DOE = Type.GetType("Terraria.ModLoader.Config.UI.DefinitionOptionElement");
            //Type entitydefinition = typeof(ProjectileDefinition);
            //Type constructedType = DOE.MakeGenericType(entitydefinition);
            //object instance = Activator.CreateInstance(constructedType);
            //MethodInfo SetItem = DOE.GetMethod("SetItem", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            //PropertyInfo Unloaded = constructedType.GetProperty("Unloaded", BindingFlags.NonPublic | BindingFlags.Instance);

            //Action<object, object> mydelegate = (Action<object,object>)Delegate.CreateDelegate(typeof(Action<object, object>), SetItem);
            //mydelegate = (doe, item) =>
            //{

            //};
            //MonoModHooks.Add(SetItem, mydelegate);煞笔
            PropertyInfo displayName = typeof(ProjectileDefinition).GetProperty("DisplayName");
            MethodInfo getMethod = displayName.GetGetMethod();
            MonoModHooks.Add(getMethod, NameHook);

            if (PreferenceConfig.Instance.SansGolem)
            {
                MethodInfo loadGolemSprite = typeof(GolemPart).GetMethod("LoadGolemSpriteBuffered", BindingFlags.Public | BindingFlags.Static);
                MonoModHooks.Add(loadGolemSprite, SansGolem.LoadGolemSpriteBuffered);
            }

            //修复演算宝珠反向魔力病减伤
            MethodInfo patreonPlrItemHit = typeof(PatreonPlayer).GetMethod("ModifyHitNPCWithItem");
            MonoModHooks.Add(patreonPlrItemHit, BalancedComputationOrb.ModifyHitNPCItemHook); ;
            MethodInfo patreonPlrProjHit = typeof(PatreonPlayer).GetMethod("ModifyHitNPCWithProj");
            MonoModHooks.Add(patreonPlrProjHit, BalancedComputationOrb.ModifyHitNPCProjHook);
            //修改触发效果后的回魔CD
            MethodInfo patreonOnHitNPC = typeof(PatreonPlayer).GetMethod("OnHitNPC");
            MonoModHooks.Add(patreonOnHitNPC, BalancedComputationOrb.OnHitNPCHook);
            MethodInfo patreonGItem = typeof(PatreonGlobalItem).GetMethod("CanUseItem");
            MonoModHooks.Add(patreonGItem, BalancedComputationOrb.CanUseItemHook);

            //巫师魔石
            MethodInfo fsplrForceEff = typeof(FargoSoulsPlayer).GetMethod("ForceEffect", new Type[] { typeof(ModItem) });
            MonoModHooks.Add(fsplrForceEff, BalancedWizardEnch.ForceHook);

            //龙之终焉火球倍率
            //MethodInfo dragonBreath2Shoot = typeof(DragonBreath2).GetMethod("Shoot");
            //MonoModHooks.Add(dragonBreath2Shoot, BalancedDragonBreath2.ShootHook);

            //衍射暗星炮烈焰死亡射线无敌帧修改
            //MethodInfo primeDeathRayOnHit = typeof(PrimeDeathray).GetMethod("OnHitNPC");
            //MonoModHooks.Add(primeDeathRayOnHit, BalancedPrimeDeathRay.OnHitNPCHook);

            #region DarkStar 暗星
            //MethodInfo darkstarFriTex = typeof(MechElectricOrbFriendly).GetProperty("Texture")?.GetGetMethod();
            //MonoModHooks.Add(darkstarFriTex, BalancedDarkStarFriendly.DSFriendlyTextureHook);
            //MethodInfo darkstarTex = typeof(MechElectricOrb).GetProperty("Texture")?.GetGetMethod();
            //MonoModHooks.Add(darkstarTex, BalancedDarkStar.DSTextureHook);
            //MethodInfo darkstarDesTex = typeof(MechElectricOrbDestroyer).GetProperty("Texture")?.GetGetMethod();
            //MonoModHooks.Add(darkstarDesTex, BalancedDarkStarDestroyer.DSDestroyerTexHook);
            //MethodInfo darkstarHomTex = typeof(MechElectricOrbHoming).GetProperty("Texture")?.GetGetMethod();
            //MonoModHooks.Add(darkstarHomTex, BalancedDarkStarHoming.DSHomingTexHook);
            //MethodInfo darkstarHomFriTex = typeof(MechElectricOrbHomingFriendly).GetProperty("Texture")?.GetGetMethod();
            //MonoModHooks.Add(darkstarHomFriTex, BalancedDarkStarHomFri.DSHomFriHook);
            //MethodInfo darkstarTwinTex = typeof(MechElectricOrbTwins).GetProperty("Texture")?.GetGetMethod();
            //MonoModHooks.Add(darkstarTwinTex, BalancedDarkStarTwin.DSTwinTexHook);
            #endregion

            //突变体
            MethodInfo mutantChooseAttack = typeof(MutantBoss).GetMethod("ChooseNextAttack", BindingFlags.NonPublic | BindingFlags.Instance);
            MonoModHooks.Add(mutantChooseAttack, BalancedMutant.ChooseAttackHook);
            MethodInfo mutantP1Checks = typeof(MutantBoss).GetMethod("SpearTossDirectP1AndChecks", BindingFlags.NonPublic | BindingFlags.Instance);
            MonoModHooks.Add(mutantP1Checks, BalancedMutant.P1CheckHook);
            MethodInfo mutantEModeSpecialEffects = typeof(MutantBoss).GetMethod("EModeSpecialEffects", BindingFlags.NonPublic | BindingFlags.Instance);
            MonoModHooks.Add(mutantEModeSpecialEffects, BalancedMutant.EMSEHook);

            //小花
            //IL.FargowiltasSouls.Content.Bosses.VanillaEternity.Plantera.SafePreAI += BalancedPlantera.PatchAI;
            MethodInfo planteraPreAI = typeof(Plantera).GetMethod("SafePreAI");
            MonoModHooks.Modify(planteraPreAI, BalancedPlantera.PatchAI);

            //毁灭者
            MethodInfo destroyersegmentOHByProj = typeof(DestroyerSegment).GetMethod("SafeOnHitByProjectile");
            MonoModHooks.Add(destroyersegmentOHByProj, BalancedDestroyerSegment.OHByProjHook);

            //骷髅王
            MethodInfo skehandPreAI = typeof(SkeletronHand).GetMethod("SafePreAI");
            MonoModHooks.Add(skehandPreAI, BalancedSkeletronHand.SafePreAIHook);
            MethodInfo skehandPostAI = typeof(SkeletronHand).GetMethod("SafePostAI");
            MonoModHooks.Add(skehandPostAI, BalancedSkeletronHand.SafePostAIHook);

            base.Load();
        }


        public static void On_Player_UpdateManaRegen(On_Player.orig_UpdateManaRegen orig, Player self)
        {
            float leftManaRegenDelay = 0f;
            if (self.manaRegenDelay > 20f)
            {
                leftManaRegenDelay = self.manaRegenDelay - 20f;
                self.manaRegenDelay = 20f;
            }
            orig(self);
            self.manaRegenDelay += leftManaRegenDelay;
        }

        public override void Unload()
        {
            On_Player.UpdateManaRegen -= On_Player_UpdateManaRegen;
            On_Player.NebulaLevelup -= BalancedComputationOrb.On_Player_NebulaLevelup;
            //On_Player.AddBuff -= BalancedComputationOrb.On_Player_AddBuff;
            //IL.FargowiltasSouls.Content.Bosses.VanillaEternity.Plantera.SafePreAI -= BalancedPlantera.PatchAI;
            base.Unload();
        }
    }
}
