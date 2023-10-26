using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace NoNaturalRegen
{
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
            //get the regen info
            double regen = (double)Main.player[Main.myPlayer].lifeRegen / 2;

            //change the color of the display, depening on the regen
            if (regen == 0)
            {
                displayColor = InactiveInfoTextColor;
            }
            else if (regen < 0)
            {
                displayColor = new Color(255, 19, 19);
            }

            //display the string
            return (regen).ToString() + " " + Language.GetTextValue("Mods.NoNaturalRegen.NNRInfoDisplayUnit");
        }
    }
}
