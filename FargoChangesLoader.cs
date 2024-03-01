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
                CommonItemChanges i = ItemChanges[item.type];
                if (i.Damage != -1) item.damage = i.Damage;
                if (i.UseTime != -1) item.useTime = i.UseTime;
                if (i.UseAnimation != -1) item.useAnimation = i.UseAnimation;
                if (i.Crit != -1) item.crit = i.Crit;
                if (i.ArmorPen != -1) item.ArmorPenetration = i.ArmorPen;
                if (i.ShootType != -1) item.shoot = i.ShootType;
                if (i.ShootSpeed != -1) item.shootSpeed = i.ShootSpeed;
                if (i.MaxStack != -1) item.maxStack = i.MaxStack;
                if (i.Sound != null) item.UseSound = i.Sound;
                if (i.Rare != -1) item.rare = i.Rare;
                if (i.BuyValue != -1) item.value = i.BuyValue;
                if (i.AutoReuse != null) item.autoReuse = (bool)i.AutoReuse;
                if (i.Mana != -1) item.mana = i.Mana;
                if (i.Defense != -1) item.defense = i.Defense;
                if (i.ReuseDelay != -1) item.reuseDelay = i.ReuseDelay;
                if (i.DmgType != null) item.DamageType = i.DmgType;
                if (i.NoMelee != null) item.noMelee = (bool)i.NoMelee;
                if (i.Channel != null) item.channel = (bool)i.Channel;
                if (i.NoUseGraphic != null) item.noUseGraphic = (bool)i.NoUseGraphic;
                if (i.ShootEveryUse != null) item.shootsEveryUse = (bool)i.ShootEveryUse;
                if (i.UseStyle != -1) item.useStyle = i.UseStyle;
                if (i.Height != -1) item.height = i.Height;
                if (i.Width != -1) item.width = i.Width;
            }
        }

        public class CommonProjectileChanges
        {
            public float OnSpawnDamageMult;
            public int ExtraUpdates;
            public int[] OnHitBuffType;
            public int[] OnHitBuffDuration;
            public CommonProjectileChanges(float dmgMult = -1f,int exUpdates = -1, int[] onHitBuff = null, int[] onHitBuffTime = null)
            {
                OnSpawnDamageMult = dmgMult;
                ExtraUpdates = exUpdates;
                OnHitBuffType = onHitBuff;
                OnHitBuffDuration = onHitBuffTime;
            }
            public void ApplyChanges_OnSpawn(Projectile projectile)
            {
                CommonProjectileChanges p = ProjectileChanges[projectile.type];
                if (p.OnSpawnDamageMult > 0) projectile.damage = (int)(projectile.damage * p.OnSpawnDamageMult);
                if (p.ExtraUpdates != -1) projectile.extraUpdates = p.ExtraUpdates;
            }
            public void ApplyChanges_OnHit(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
            {
                CommonProjectileChanges p = ProjectileChanges[projectile.type];
                if (p.OnHitBuffType != null)
                {
                    for(int i = 0; i < OnHitBuffType.Length; i++)
                    {
                        target.AddBuff(p.OnHitBuffType[i], p.OnHitBuffDuration[i]);
                    }
                }
                
            }
        }

        public static Dictionary<int, CommonItemChanges> ItemChanges = new Dictionary<int, CommonItemChanges>();
        public static Dictionary<int, CommonProjectileChanges> ProjectileChanges = new Dictionary<int, CommonProjectileChanges>();

        //public override void PostSetupContent()
        //{
        //    Item_LoadChange();
        //}

        public static void Item_LoadChange()
        {
            if (ItemChanges is not null) ItemChanges.Clear();
            ItemChanges.Add(ModContent.ItemType<NukeFishron>(), new(damage: 951, useAnim: 60, useTime: 60));//核子猪鲨
            ItemChanges.Add(ModContent.ItemType<DragonBreath2>(), new(damage: 221));//龙之终焉
            ItemChanges.Add(ModContent.ItemType<RockSlide>(), new(damage: 394));
            ItemChanges.Add(ModContent.ItemType<GeminiGlaives>(), new(damage: 386));
            //ItemChanges.Add(ItemID.Zenith, new(damage: 230));
        }

        public static void Projectile_LoadChange()
        {
            if (ProjectileChanges is not null) ProjectileChanges.Clear();
            ProjectileChanges.Add(ModContent.ProjectileType<PlasmaArrow>(), new(1.75f / 1.6f));
            ProjectileChanges.Add(ModContent.ProjectileType<PlasmaDeathRay>(), new(3 / 2.5f));
        }
    }
}
