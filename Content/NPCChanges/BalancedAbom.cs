using AFargoTweak.Configs;
using FargowiltasSouls.Content.Bosses.AbomBoss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AFargoTweak.Content.NPCChanges
{
    public class BalancedAbom : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<AbomBoss>();
        }
        public override void SetDefaults(NPC entity)
        {
            base.SetDefaults(entity);
            if(PreferenceConfig.Instance.AbomMusicType == AFTUtils.AbomMusic.Stigma && ModLoader.TryGetMod("FargowiltasMusic", out Mod musicMod))
            {
                entity.ModNPC.Music = MusicLoader.GetMusicSlot(musicMod, "Assets/Music/Stigma");
            }
        }
    }
}
