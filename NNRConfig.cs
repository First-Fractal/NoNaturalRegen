using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace NoNaturalRegen
{
    public class NNRConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static NNRConfig Instance;

        [Header("GeneralOptions")]
        [DefaultValue(true)]
        public bool allowNurseBoss;

        [DefaultValue(true)]
        public bool allowNurseHealing;

        [DefaultValue(true)]
        public bool allowHealingPotion;
    }
}
