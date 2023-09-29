using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using System.ComponentModel;

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

        //this function will make it that the nurse will insta-kill the player, if set to true.
        public override bool ModifyNurseHeal(NPC nurse, ref int health, ref bool removeDebuffs, ref string chatText)
        {
            //check if the player doesn't allow the  player to heal with the nurse
            if (!NNRConfig.Instance.allowNurseHealing)
            {
                //no one should surivie the nurse
                Player.immune = false;
                Player.immuneTime = 0;
                Player.creativeGodMode = false;
                Player.onHitDodge = false;
                Player.shadowDodge = false;

                //pick a random death message for dying to the nurse
                string deathMessageType = "Mods.NoNaturalRegen.DeathMessage.Nurse" + Main.rand.Next(0, 7).ToString();

                //insta-kill the player
                Player.Hurt(PlayerDeathReason.ByCustomReason(Language.GetTextValue(deathMessageType, Player.name, nurse.FullName)), 999999, 1, dodgeable: false, armorPenetration: 9999);
                return false;
            }
            
            return base.ModifyNurseHeal(nurse, ref health, ref removeDebuffs, ref chatText);
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

