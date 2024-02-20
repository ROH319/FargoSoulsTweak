using AFargoTweak.Configs;
using FargowiltasSouls.Content.Bosses.DeviBoss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AFargoTweak.Content.NPCChanges
{
    public class BalancedDevi : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == ModContent.NPCType<DeviBoss>();
        }
        public override void SetDefaults(NPC entity)
        {
            base.SetDefaults(entity);
            if(PreferenceConfig.Instance.DeviMusicType == AFTUtils.DeviMusic.LexusCyanixs && ModLoader.TryGetMod("FargowiltasMusic",out Mod musicMod))
            {
                entity.ModNPC.Music = MusicLoader.GetMusicSlot(musicMod, "Assets/Music/LexusCyanixs");
            }
            else if(PreferenceConfig.Instance.DeviMusicType == AFTUtils.DeviMusic.DanceOfDeviance)
            {
                entity.ModNPC.Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/DanceOfDeviance");
            }
        }
    }
}
