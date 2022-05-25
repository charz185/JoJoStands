using JoJoStands.Items;
using JoJoStands.Items.CraftingMaterials;
using JoJoStands.Items.Seasonal;
using JoJoStands.Items.Tiles;
using JoJoStands.Networking;
using JoJoStands.Projectiles;
using JoJoStands.Projectiles.PlayerStands.GoldExperienceRequiem;
using JoJoStands.Projectiles.PlayerStands.StarPlatinum;
using JoJoStands.Projectiles.PlayerStands.TestStand;
using JoJoStands.Projectiles.PlayerStands.TheWorld;
using JoJoStands.Projectiles.PlayerStands.Tusk;
using JoJoStands.Tiles;
using JoJoStands.UI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace JoJoStands
{
    public class JoJoStands : Mod
    {
        public static bool SoundsLoaded = false;
        public static bool FanStandsLoaded = false;
        public static Mod JoJoStandsSounds;
        public static Mod JoJoFanStands;

        internal static JoJoStands Instance => GetInstance<JoJoStands>();        //internal so that it's only usable inside this specific project
        internal static CustomizableOptions customizableConfig;

        public static ModKeybind SpecialHotKey;
        public static ModKeybind SecondSpecialHotKey;
        public static ModKeybind StandOutHotKey;
        public static ModKeybind StandAutoModeHotKey;
        public static ModKeybind PoseHotKey;

        public static List<int> timestopImmune = new List<int>();
        public static List<int> standTier1List = new List<int>();
        public static List<int> christmasStands = new List<int>();
        public static List<char> testStandPassword = new List<char>();      //  :) Have *fuuuuuuun!*

        public override void Load()
        {
            SoundsLoaded = ModLoader.TryGetMod("JoJoStandsSounds", out JoJoStandsSounds);
            FanStandsLoaded = ModLoader.TryGetMod("JoJoFanStands", out JoJoFanStands);

            HamonBar.hamonBarTexture = (Texture2D)Request<Texture2D>("JoJoStands/UI/HamonBar", AssetRequestMode.ImmediateLoad);
            ToBeContinued.TBCArrowTexture = (Texture2D)Request<Texture2D>("JoJoStands/UI/TBCArrow", AssetRequestMode.ImmediateLoad);
            BulletCounter.bulletCounterTexture = (Texture2D)Request<Texture2D>("JoJoStands/UI/BulletCounter", AssetRequestMode.ImmediateLoad);
            AerosmithRadar.aerosmithRadarTexture = (Texture2D)Request<Texture2D>("JoJoStands/UI/AerosmithRadar", AssetRequestMode.ImmediateLoad);
            GoldenSpinMeter.goldenRectangleTexture = (Texture2D)Request<Texture2D>("JoJoStands/UI/GoldenSpinMeter", AssetRequestMode.ImmediateLoad);
            GoldenSpinMeter.goldenRectangleSpinLineTexture = (Texture2D)Request<Texture2D>("JoJoStands/UI/GoldenSpinMeterLine", AssetRequestMode.ImmediateLoad);
            SexPistolsUI.sexPistolsUITexture = (Texture2D)Request<Texture2D>("JoJoStands/UI/SexPistolsUI", AssetRequestMode.ImmediateLoad);
            VoidBar.VoidBarTexture = (Texture2D)Request<Texture2D>("JoJoStands/UI/VoidBar", AssetRequestMode.ImmediateLoad);
            VoidBar.VoidBarBarTexture = (Texture2D)Request<Texture2D>("JoJoStands/UI/VoidBarBar", AssetRequestMode.ImmediateLoad);

            standTier1List.Add(ItemType<AerosmithT1>());
            standTier1List.Add(ItemType<GoldExperienceT1>());
            standTier1List.Add(ItemType<HierophantGreenT1>());
            standTier1List.Add(ItemType<KillerQueenT1>());
            standTier1List.Add(ItemType<KingCrimsonT1>());
            standTier1List.Add(ItemType<MagiciansRedT1>());
            standTier1List.Add(ItemType<SexPistolsT1>());
            standTier1List.Add(ItemType<StarPlatinumT1>());
            standTier1List.Add(ItemType<StickyFingersT1>());
            standTier1List.Add(ItemType<TheWorldT1>());
            standTier1List.Add(ItemType<TuskAct1>());
            standTier1List.Add(ItemType<LockT1>());
            standTier1List.Add(ItemType<GratefulDeadT1>());
            standTier1List.Add(ItemType<TheHandT1>());
            standTier1List.Add(ItemType<WhitesnakeT1>());
            standTier1List.Add(ItemType<DollyDaggerT1>());
            standTier1List.Add(ItemType<CenturyBoyT1>());
            standTier1List.Add(ItemType<SilverChariotT1>());
            standTier1List.Add(ItemType<HermitPurpleT1>());
            standTier1List.Add(ItemType<BadCompanyT1>());
            standTier1List.Add(ItemType<CreamT1>());

            timestopImmune.Add(ProjectileType<TheWorldStandT2>());     //only the timestop capable stands as people shouldn't switch anyway
            timestopImmune.Add(ProjectileType<TheWorldStandT3>());
            timestopImmune.Add(ProjectileType<TheWorldStandFinal>());
            timestopImmune.Add(ProjectileType<StarPlatinumStandFinal>());
            //MyPlayer.stopImmune.Add(ProjectileType("StickyFingersFistExtended>());
            timestopImmune.Add(ProjectileType<RoadRoller>());
            timestopImmune.Add(ProjectileType<HamonPunches>());
            timestopImmune.Add(ProjectileType<Fists>());
            timestopImmune.Add(ProjectileType<GoldExperienceRequiemStand>());
            timestopImmune.Add(ProjectileType<TuskAct4Stand>());
            timestopImmune.Add(ProjectileType<TestStandStand>());
            timestopImmune.Add(ProjectileType<StarOnTheTreeStand>());

            christmasStands.Add(ItemType<StarOnTheTree>());
            christmasStands.Add(ItemType<KingClaus>());

            testStandPassword.Add(Convert.ToChar(84));


            //Where we register the hotkeys
            SpecialHotKey = KeybindLoader.RegisterKeybind(Instance, "Special Ability", Keys.F);
            SecondSpecialHotKey = KeybindLoader.RegisterKeybind(Instance, "Second Special Ability", Keys.B);
            StandOutHotKey = KeybindLoader.RegisterKeybind(Instance, "Stand Out", Keys.G);
            PoseHotKey = KeybindLoader.RegisterKeybind(Instance, "Pose", Keys.V);
            StandAutoModeHotKey = KeybindLoader.RegisterKeybind(Instance, "Stand Auto Mode", Keys.C);


            if (!Main.dedServ)      //Manages resource loading cause the server isn't able to load resources
            {
                JoJoStandsShaders.LoadShaders();
                MusicLoader.AddMusicBox(Instance, MusicLoader.GetMusicSlot(Instance, "Sounds/Music/VMMusic"), ItemType<ViralMusicBox>(), TileType<ViralMusicBoxTile>());
            }
        }

        public override void Unload()
        {
            SpecialHotKey = null;
            PoseHotKey = null;
            StandAutoModeHotKey = null;
            StandOutHotKey = null;
            customizableConfig = null;
            standTier1List.Clear();
            timestopImmune.Clear();
            testStandPassword.Clear();
            SoundsLoaded = false;
            FanStandsLoaded = false;
            JoJoStandsSounds = null;
            ToBeContinued.TBCArrowTexture = null;
            HamonBar.hamonBarTexture = null;
            BulletCounter.bulletCounterTexture = null;
            AerosmithRadar.aerosmithRadarTexture = null;
            GoldenSpinMeter.goldenRectangleTexture = null;
            GoldenSpinMeter.goldenRectangleSpinLineTexture = null;
            SexPistolsUI.sexPistolsUITexture = null;
            VoidBar.VoidBarTexture = null;
            VoidBar.VoidBarBarTexture = null;
        }

        public override void AddRecipeGroups()
        {
            RecipeGroup willsGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Wills", new int[]
            {
                ItemType<WillToFight>(),
                ItemType<WillToChange>(),
                ItemType<WillToControl>(),
                ItemType<WillToDestroy>(),
                ItemType<WillToEscape>(),
                ItemType<WillToProtect>()
            });
            RecipeGroup.RegisterGroup("JoJoStandsWills", willsGroup);
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            ModNetHandler.HandlePacket(reader, whoAmI);
        }
    }
}