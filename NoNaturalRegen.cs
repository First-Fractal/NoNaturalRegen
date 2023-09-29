using Terraria;
using Terraria.ModLoader;

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
}

