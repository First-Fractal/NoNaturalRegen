using Terraria;
using Terraria.ID;
using Terraria.Chat;
using Terraria.Localization;
using Microsoft.Xna.Framework;

//this is my own libary that I use to store snippits. I don't want it to be it's own mod, so I'll use copy and paste this file when needed.
namespace NoNaturalRegen
{
    public class FFLib
    {
        //list of all the boss parts
        public int[] BossParts = { NPCID.EaterofWorldsHead, NPCID.EaterofWorldsBody, NPCID.EaterofWorldsTail, NPCID.Creeper, NPCID.SkeletronHand, NPCID.SkeletronHead, NPCID.WallofFleshEye, NPCID.TheDestroyer, NPCID.TheDestroyerBody, NPCID.TheDestroyerTail, NPCID.Probe, NPCID.PrimeCannon, NPCID.PrimeLaser, NPCID.PrimeSaw, NPCID.PrimeVice, NPCID.PlanterasHook, NPCID.PlanterasTentacle, NPCID.GolemFistLeft, NPCID.GolemFistRight, NPCID.GolemHead, NPCID.GolemHeadFree, NPCID.CultistBossClone, NPCID.MoonLordCore, NPCID.MoonLordHand, NPCID.MoonLordHead, NPCID.MoonLordFreeEye, NPCID.MoonLordLeechBlob };

        //function for spiting messages to the chat
        public void Talk(string message, Color color)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText(message, color);
            }
            else
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(message), color);
            }
        }

        //function for checking if a boss is currently alive
        public bool IsBossAlive()
        {
            foreach(NPC npc in Main.npc)
            {
                if (npc.boss == true)
                {
                    return true;
                } else
                {
                    foreach (int part in BossParts)
                    {
                        if (npc.type == part)
                        {
                            return true;
                        }
                    }
                }
                
            }
            return false;
        }
    }
}