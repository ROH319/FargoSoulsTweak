using AFargoTweak.Configs;
using FargowiltasSouls.Content.Items.Weapons.BossDrops;
using FargowiltasSouls.Content.Items.Weapons.SwarmDrops;
using FargowiltasSouls.Content.Patreon.Volknet.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AFargoTweak
{
    public class FargoChangesLoader : ModSystem
    {
        public class CommonItemChanges
        {
            public int Damage;
            public int Crit;
            public int UseTime;
            public int UseAnimation;
            public int ArmorPen;
            public int ShootType;
            public float ShootSpeed;
            public int ReuseDelay;
            public int MaxStack;
            public DamageClass DmgType;
            public int Defense;
            public SoundStyle? Sound;
            public int Rare;
            public int BuyValue;
            public bool ChangedDamage;
            public bool ChangedCrit;
            public bool ChangedUseTime;
            public bool ChangedDefense;
            public bool? AutoReuse;
            public bool? NoMelee;
            public bool? Channel;
            public bool? NoUseGraphic;
            public bool? UseTurn;
            public bool? ShootEveryUse;
            public int Mana;
            public int UseStyle;
            public int Height;
            public int Width;
            public CommonItemChanges(int damage = -1, int crit = -1, int useTime = -1, int useAnim = -1, int armorPen = -1, int shootType = -1, float shootSpeed = -1f, int reuseDelay = -1,
                int maxStack = -1, DamageClass dmgType = null, int defense = -1, SoundStyle? sound = null, int rare = -1, int value = -1, bool? autoReuse = null, bool? noMelee = null,
                bool? channel = null, bool? noGraphic = null, bool? useTurn = null, bool? shootEveryUse = null, int mana = -1, int useStyle = -1, int height = -1, int width = -1)
            {
                Damage = damage;
                ChangedDamage = Damage != -1;
                Crit = crit;
                ChangedCrit = Crit != -1;
                UseTime = useTime;
                ChangedUseTime = UseTime != -1;
                UseAnimation = useAnim;
                Defense = defense;
                ChangedDefense = Defense != -1;
                ArmorPen = armorPen;
                ShootType = shootType;
                ShootSpeed = shootSpeed;
                ReuseDelay = reuseDelay;
                MaxStack = maxStack;
                DmgType = dmgType;
                Sound = sound;
                Rare = rare;
                BuyValue = value;
                AutoReuse = autoReuse;
                NoMelee = noMelee;
                Channel = channel;
                NoUseGraphic = noGraphic;
                UseTurn = useTurn;
                ShootEveryUse = shootEveryUse;
                Mana = mana;
                UseStyle = useStyle;
                Height = height;
                Width = width;
            }
            public void ApplyChanges_SetDefault(Item item)
            {
                //CommonItemChanges i = ItemChanges[item.type];
                if (Damage != -1) item.damage = Damage;
                if (UseTime != -1) item.useTime = UseTime;
                if (UseAnimation != -1) item.useAnimation = UseAnimation;
                if (Crit != -1) item.crit = Crit;
                if (ArmorPen != -1) item.ArmorPenetration = ArmorPen;
                if (ShootType != -1) item.shoot = ShootType;
                if (ShootSpeed != -1) item.shootSpeed = ShootSpeed;
                if (MaxStack != -1) item.maxStack = MaxStack;
                if (Sound != null) item.UseSound = Sound;
                if (Rare != -1) item.rare = Rare;
                if (BuyValue != -1) item.value = BuyValue;
                if (AutoReuse != null) item.autoReuse = (bool)AutoReuse;
                if (Mana != -1) item.mana = Mana;
                if (Defense != -1) item.defense = Defense;
                if (ReuseDelay != -1) item.reuseDelay = ReuseDelay;
                if (DmgType != null) item.DamageType = DmgType;
                if (NoMelee != null) item.noMelee = (bool)NoMelee;
                if (Channel != null) item.channel = (bool)Channel;
                if (NoUseGraphic != null) item.noUseGraphic = (bool)NoUseGraphic;
                if (ShootEveryUse != null) item.shootsEveryUse = (bool)ShootEveryUse;
                if (UseStyle != -1) item.useStyle = UseStyle;
                if (Height != -1) item.height = Height;
                if (Width != -1) item.width = Width;
            }
        }

        public class CommonProjectileChanges
        {
            public int Type;
            public float OnSpawnDamageMult;
            public int ExtraUpdates;
            public int[] OnHitBuffType;
            public int[] OnHitBuffDuration;
            public AFTUtils.NPCImmunityType ImmuneType;
            public int ImmunityCD;
            public int Penetrate;
            public float Scale;
            public CommonProjectileChanges(int type, float dmgMult = 1f, AFTUtils.NPCImmunityType immuneType = AFTUtils.NPCImmunityType.None, int immunityCD = 10, int penetrate = -2, float scale = 1f, int exUpdates = -1, int[] onHitBuff = null, int[] onHitBuffTime = null)
            {
                Type = type;
                OnSpawnDamageMult = dmgMult;
                ImmuneType = immuneType;
                ImmunityCD = immunityCD;
                ExtraUpdates = exUpdates;
                OnHitBuffType = onHitBuff;
                OnHitBuffDuration = onHitBuffTime;
                Penetrate = penetrate;
                Scale = scale;
            }
            public void ApplyChanges_OnSpawn(Projectile projectile)
            {
                //CommonProjectileChanges p = ProjectileChanges.Find(proj => proj.Type == projectile.type);
                projectile.damage = (int)(projectile.damage * OnSpawnDamageMult);
                if (ImmuneType != AFTUtils.NPCImmunityType.None)
                {
                    switch (ImmuneType)
                    {
                        case AFTUtils.NPCImmunityType.Local:
                            projectile.usesIDStaticNPCImmunity = false;
                            projectile.usesLocalNPCImmunity = true;
                            projectile.localNPCHitCooldown = ImmunityCD;
                            break;
                        case AFTUtils.NPCImmunityType.IDStatic:
                            projectile.usesLocalNPCImmunity = false;
                            projectile.usesIDStaticNPCImmunity = true;
                            projectile.idStaticNPCHitCooldown = ImmunityCD;
                            break;
                        default:break;
                    }
                }
                if (Penetrate > -2)
                    projectile.penetrate = Penetrate;
                projectile.scale *= Scale;
                if (ExtraUpdates != -1) projectile.extraUpdates = ExtraUpdates;
            }
            public void ApplyChanges_ModifyHit(Projectile projectile, ref NPC.HitModifiers modifiers)
            {
                modifiers.FinalDamage *= OnSpawnDamageMult;
            }
            public void ApplyChanges_OnHit(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
            {
                //CommonProjectileChanges p = ProjectileChanges[projectile.type];
                if (OnHitBuffType != null)
                {
                    for(int i = 0; i < OnHitBuffType.Length; i++)
                    {
                        target.AddBuff(OnHitBuffType[i], OnHitBuffDuration[i]);
                    }
                }
                
            }
        }

        public static Dictionary<int, CommonItemChanges> ItemChanges = new();
        public static List<CommonProjectileChanges> ProjectileChanges = new();

        //public override void PostSetupContent()
        //{
        //    Item_LoadChange();
        //}

        public static void Item_LoadChange()
        {
            if (ItemChanges is not null) ItemChanges.Clear();
            ItemOverriderConfig.Instance.Presets.ForEach((preset) =>
            {
                if (preset.Enabled)
                {
                    preset.ItemChanges.ForEach((item) =>
                    {
                        if (item.Enabled)
                        {
                            if (ItemChanges.ContainsKey(item.item.Type))
                            {
                                ItemChanges.Remove(item.item.Type);
                            }
                            ItemChanges.Add(item.item.Type, new CommonItemChanges(
                                item.Damage > 0 ? item.Damage : -1,
                                useTime: item.UseTime > 0 ? item.UseTime : -1,
                                useAnim: item.UseAnimation > 0 ? item.UseAnimation : -1));
                        }
                    });
                }
            });
        }

        public static void Projectile_LoadChange()
        {
            if (ProjectileChanges is not null) ProjectileChanges.Clear();
            ProjectileOverriderConfig.Instance.Presets.ForEach((preset) =>
            {
                if (preset.Enabled)
                {
                    preset.ProjChanges.ForEach((proj) =>
                    {
                        if (proj.Enabled)
                        {
                            ProjectileChanges.Add(new CommonProjectileChanges(proj.proj.Type,
                                proj.OnSpawnDamageMult != 100 ? proj.OnSpawnDamageMult / 100f : 1f, 
                                proj.ProjImmuneType, 
                                proj.ImmunityCD,
                                proj.Penetrate,
                                proj.Scale != 100 ? proj.Scale / 100f : 1f));
                        }
                    });
                }
            });
            //ProjectileChanges.Add(ModContent.ProjectileType<PlasmaArrow>(), new(1.75f / 1.6f));
            //ProjectileChanges.Add(ModContent.ProjectileType<PlasmaDeathRay>(), new(3 / 2.5f));
        }
    }
}
