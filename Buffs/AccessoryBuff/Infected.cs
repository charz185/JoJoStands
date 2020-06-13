﻿using System;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace JoJoStands.Buffs.AccessoryBuff
{
    public class Infected : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Infected");
            Description.SetDefault("Some sort of otherworldly virus is spreading inside your body.");
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.persistentBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.lifeRegen > 0)
            {
                player.lifeRegen = 0;
            }
            player.lifeRegenTime = 120;
            player.lifeRegen -= 4;

        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            Player player = Main.player[Main.myPlayer];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (npc.boss)
            {
                npc.lifeRegen = -12;
            }
            else
            {
                npc.lifeRegen = -12;
            }
        }
    }
}