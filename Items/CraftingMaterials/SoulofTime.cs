using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace JoJoStands.Items.CraftingMaterials
{
	public class SoulofTime : ModItem
	{
		public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 9));
            Tooltip.SetDefault("This soul seems to make things slower around you");
            DisplayName.SetDefault("Soul of Time");
		}

		public override void SetDefaults()
        {
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 99;
			Item.rare = 8;
			Item.value = Item.buyPrice(0, 2, 47, 85);
		}

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            gravity = 3f;
            maxFallSpeed = 0f;
        }
    }
}