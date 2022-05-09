﻿using JoJoStands.Buffs.ItemBuff;
using JoJoStands.Items.CraftingMaterials;
using JoJoStands.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoJoStands.Items
{
    public class LockT1 : StandItemClass
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Lock (Tier 1)");
            Tooltip.SetDefault("Make people that harm you overwhelmed with Guilt!");
        }

        public override int standTier => 1;

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.rare = ItemRarityID.LightPurple;
        }

        public override bool ManualStandSpawning(Player player)
        {
            MyPlayer mPlayer = player.GetModPlayer<MyPlayer>();
            mPlayer.standAccessory = true;
            mPlayer.standType = 1;
            mPlayer.poseSoundName = "TheGuiltierYouFeel";
            player.AddBuff(ModContent.BuffType<LockActiveBuff>(), 10);
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<StandArrow>())
                .AddIngredient(ModContent.ItemType<WillToEscape>(), 1)
                .AddTile(ModContent.TileType<RemixTableTile>())
                .Register();
        }
    }
}