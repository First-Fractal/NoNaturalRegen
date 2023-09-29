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
        //this function will make it that the nurse will insta-kill the player, if set to true.
        public override void OnChatButtonClicked(NPC npc, bool firstButton)
        {
            //check if the player doesn't allow the  player to heal with the nurse
            if (!NNRConfig.Instance.allowNurseHealing)
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
                    string deathMessageType = "Mods.NoNaturalRegen.DeathMessage.Nurse" + Main.rand.Next(0, 7).ToString();

                    //insta-kill the player
                    player.Hurt(PlayerDeathReason.ByCustomReason(Language.GetTextValue(deathMessageType, player.name, npc.FullName)), 9999999, 1, dodgeable: false, armorPenetration: 9999);
                }
            }
            base.OnChatButtonClicked(npc, firstButton);
        }
    }

    public class NNRConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static NNRConfig Instance;

        [Header("GeneralOptions")]

        //[Label("$Mods.NoNaturalRegen.Configs.allowNurseHealing.Label")]
        //[Tooltip("$Mods.NoNaturalRegen.Configs.allowNurseHealing.Tooltip")]
        [DefaultValue(false)]
        public bool allowNurseHealing;
    }
}

