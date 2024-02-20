using AFargoTweak.Configs;
using AFargoTweak.Content.ItemChanges.Accessories;
using AFargoTweak.Content.NPCChanges;
using AFargoTweak.Content.ProjectileOverrides;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Content.Items.Weapons.SwarmDrops;
using FargowiltasSouls.Content.Patreon;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using FargowiltasSouls.Core.ModPlayers;
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

            //龙之终焉火球倍率
            //MethodInfo dragonBreath2Shoot = typeof(DragonBreath2).GetMethod("Shoot");
            //MonoModHooks.Add(dragonBreath2Shoot, BalancedDragonBreath2.ShootHook);

            //衍射暗星炮烈焰死亡射线无敌帧修改
            MethodInfo primeDeathRayOnHit = typeof(PrimeDeathray).GetMethod("OnHitNPC");
            MonoModHooks.Add(primeDeathRayOnHit, BalancedPrimeDeathRay.OnHitNPCHook);

            //MethodInfo mutantSpearDashDirect = typeof(MutantBoss).GetMethod("SpearDashDirectP2", BindingFlags.Instance | BindingFlags.NonPublic);
            //MonoModHooks.Modify(mutantSpearDashDirect, BalancedMutant.PatchSpearDashDirectP2);
            //MethodInfo mutantBulletHellp2 = typeof(MutantBoss).GetMethod("BoundaryBulletHellP2", BindingFlags.Instance | BindingFlags.NonPublic);
            //MonoModHooks.Add(mutantBulletHellp2, BalancedMutant.BulletHellHook);
            MethodInfo mutantChooseAttack = typeof(MutantBoss).GetMethod("ChooseNextAttack", BindingFlags.NonPublic | BindingFlags.Instance);
            MonoModHooks.Add(mutantChooseAttack, BalancedMutant.ChooseAttackHook);
            IL.FargowiltasSouls.Content.Bosses.VanillaEternity.Plantera.SafePreAI += BalancedPlantera.PatchAI;
            

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
            IL.FargowiltasSouls.Content.Bosses.VanillaEternity.Plantera.SafePreAI -= BalancedPlantera.PatchAI;
            base.Unload();
        }
    }
}
