using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace NoNaturalRegen
{
    public class NNRGlobalBuff : GlobalBuff
    {
        //this is run every tick of every buff
        public override void Update(int type, Player player, ref int buffIndex)
        {
            //check if the player isnt allow to use healing potion, and the current buff is potion sickness
            if (!NNRConfig.Instance.allowHealingPotion && type == BuffID.PotionSickness)
            {
                //no one should surivie the nurse
                player.immune = false;
                player.immuneTime = 0;
                player.creativeGodMode = false;
                player.onHitDodge = false;
                player.shadowDodge = false;

                //pick a random death message for dying to the healing potion
                string deathMessageType = "Mods.NoNaturalRegen.DeathMessage.Heal" + Main.rand.Next(0, 8).ToString();

                string potion = " ";
                foreach(Item item in player.inventory)
                {
                    if (item.healLife > 0)
                    {
                        potion = item.Name;
                        break;
                    }
                }

                //insta-kill the player
                player.Hurt(PlayerDeathReason.ByCustomReason(Language.GetTextValue(deathMessageType, player.name, potion)), 9999999, 1, dodgeable: false, armorPenetration: 9999);
            }
            base.Update(type, player, ref buffIndex);
        }
    }
}
