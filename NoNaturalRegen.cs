using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using System.ComponentModel;
using Microsoft.Xna.Framework;

namespace NoNaturalRegen
{
	public class NoNaturalRegen : Mod
	{
	}

	public class NNRPlayer : ModPlayer
	{
        //this function makes the natural regen set to zero. regen accessories will still act normally.
        public override void UpdateLifeRegen()
        {
            Player.lifeRegenTime = 0;
            base.UpdateLifeRegen();
        }
    }

    public class NNRGlobalNPC : GlobalNPC
    {
        static FFLib ff = new FFLib();

        //function that will make the nurse insta kill the player
        void NurseKillPlayer(NPC npc)
        {
            //check if the current npc is the nurse
            if (npc.type == NPCID.Nurse)
            {
                //no one should surivie the nurse
                Player player = Main.player[npc.FindClosestPlayer()];
                player.immune = false;
                player.immuneTime = 0;
                player.creativeGodMode = false;
                player.onHitDodge = false;
                player.shadowDodge = false;

                //pick a random death message for dying to the nurse
                string deathMessageType = "Mods.NoNaturalRegen.DeathMessage.Nurse" + Main.rand.Next(0, 8).ToString();

                //insta-kill the player
                player.Hurt(PlayerDeathReason.ByCustomReason(Language.GetTextValue(deathMessageType, player.name, npc.FullName)), 9999999, 1, dodgeable: false, armorPenetration: 9999);
            }
        }

        //this function will make it that the nurse will insta-kill the player, if set to true.
        public override void OnChatButtonClicked(NPC npc, bool firstButton)
        {
            //check if the player doesn't allow the  player to heal with the nurse
            if (!NNRConfig.Instance.allowNurseHealing)
            {
                NurseKillPlayer(npc);
            } else
            {
                //check if the player doesn't allow the nurse to heal during a boss, and a boss is currently alive.
                if (!NNRConfig.Instance.allowNurseBoss && ff.IsBossAlive())
                {
                    NurseKillPlayer(npc);
                }    
            }
            base.OnChatButtonClicked(npc, firstButton);
        }
    }
     
    //this is a class for the regen information that is below the minimap.
    public class NNRRegenDisplay : InfoDisplay
    {
        //give it the correct name when hovering over it.
        public override LocalizedText DisplayName => Language.GetText("Mods.NoNaturalRegen.NNRRegenDisplay");

        //make it always active
        public override bool Active()
        {
            return true;
        }
        //make it display the correct value
        public override string DisplayValue(ref Color displayColor)
        {
            //get the regen
            int regen = Main.player[Main.myPlayer].lifeRegen / 2;

            //change the color of the display, depening on the regen
            if (regen == 0)
            {
                displayColor = InactiveInfoTextColor;
            } else if (regen < 0) {
                displayColor = new Color(255, 19, 19, Main.mouseTextColor); 
            }

            //display the string
            return regen.ToString() + Language.GetTextValue("Mods.NoNaturalRegen.NNRInfoDisplayUnit");
        }
    }

    public class NNRGlobalItem: GlobalItem
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

                //pick a random death message for dying to the nurse
                string deathMessageType = "Mods.NoNaturalRegen.DeathMessage.Heal" + Main.rand.Next(0, 8).ToString();

                //insta-kill the player
                player.Hurt(PlayerDeathReason.ByCustomReason(Language.GetTextValue(deathMessageType, player.name, item.Name)), 9999999, 1, dodgeable: false, armorPenetration: 9999);
            }
            return base.UseItem(item, player);
        }
    }

    public class NNRConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static NNRConfig Instance;

        [Header("GeneralOptions")]
        [DefaultValue(false)]
        public bool allowNurseBoss;

        [DefaultValue(true)]
        public bool allowNurseHealing;

        [DefaultValue(true)]
        public bool allowHealingPotion;
    }
}

