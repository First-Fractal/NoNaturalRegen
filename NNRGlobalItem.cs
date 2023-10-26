using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;

namespace NoNaturalRegen
{
    public class NNRGlobalItem : GlobalItem
    {
        public override bool? UseItem(Item item, Player player)
        {
            if (item.healLife > 0 && !NNRConfig.Instance.allowHealingPotion)
            {
                //no one should surivie the nurse
                player.immune = false;
                player.immuneTime = 0;
                player.creativeGodMode = false;
                player.onHitDodge = false;
                player.shadowDodge = false;

                //pick a random death message for dying to the healing potion
                string deathMessageType = "Mods.NoNaturalRegen.DeathMessage.Heal" + Main.rand.Next(0, 8).ToString();

                //insta-kill the player
                player.Hurt(PlayerDeathReason.ByCustomReason(Language.GetTextValue(deathMessageType, player.name, item.Name)), 9999999, 1, dodgeable: false, armorPenetration: 9999);
            }
            return base.UseItem(item, player);
        }
    }
}
