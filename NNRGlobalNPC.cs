using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;

namespace NoNaturalRegen
{
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
            }
            else
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
}
