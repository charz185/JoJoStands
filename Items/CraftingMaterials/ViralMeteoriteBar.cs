using JoJoStands.Items.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoJoStands.Items.CraftingMaterials
{
    public class ViralMeteoriteBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Viral Meteorite Bar");
            Tooltip.SetDefault("You feel your soul interacting with the metal.");

        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(0, 0, 50, 0);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ViralMeteorite>(), 3)
                .AddTile(TileID.Furnaces)
                .Register();
        }
    }
}