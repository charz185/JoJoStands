using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ObjectData;

namespace JoJoStands.Tiles
{
    public class QuietLifeTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.Width = 4;
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.CoordinateHeights = new int[]
            {
                16,
                16,
                16,
                16
            };
            TileObjectData.addTile(Type);
            disableSmartCursor = true;
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Quiet Life>();
            AddMapEntry(new Color(120, 85, 60), name);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 4, 4, ModContent.ItemType<QuietLife>());
        }
    }
}