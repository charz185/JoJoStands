using JoJoStands.Items.CraftingMaterials;
using JoJoStands.Tiles;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoJoStands.Items
{
    public class AerosmithT3 : StandItemClass
    {
        public override int StandSpeed => 10;
        public override int StandType => 2;
        public override string StandProjectileName => "Aerosmith";
        public override int StandTier => 3;
        public override Color StandTierDisplayColor => AerosmithFinal.AerosmithTierColor;
        public override string Texture
        {
            get { return Mod.Name + "/Items/AerosmithT1"; }
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Aerosmith (Tier 3)");
            // Tooltip.SetDefault("Left-click to move and right-click to shoot bullets at the enemies!\nSpecial: Remote Control\nSecond Special: Drop a bomb on enemies!\nPassive: Carbon Radar\nThe farther the stand is from you, the less damage it does.\nUsed in Stand Slot");
        }

        public override void SetDefaults()
        {
            Item.damage = 63;
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 1;
            Item.value = 0;
            Item.noUseGraphic = true;
            Item.rare = ItemRarityID.LightPurple;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AerosmithT2>())
                .AddIngredient(ItemID.Radar)
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddIngredient(ItemID.SoulofFlight, 8)
                .AddIngredient(ModContent.ItemType<WillToFight>())
                .AddIngredient(ModContent.ItemType<WillToProtect>(), 2)
                .AddTile(ModContent.TileType<RemixTableTile>())
                .Register();
        }
    }
}
