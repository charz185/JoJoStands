using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace JoJoStands.Projectiles.PlayerStands.Cream
{
    public class ExposingCream : ModProjectile
    {
        public override string Texture
        {
            get { return Mod.Name + "/Projectiles/PlayerStands/Cream/Void"; }
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 0;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.hide = true;
            DrawOffsetX = -10;
            DrawOriginOffsetY = -10;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            MyPlayer mPlayer = player.GetModPlayer<MyPlayer>());

            Projectile.frame = 1;
            player.position = Projectile.position + new Vector2(0f, 0f);
            player.AddBuff(ModContent.BuffType<Exposing>(), 2);

            if (player.mount.Type != 0)
            {
                player.mount.Dismount(player);
            }
            if (player.dead || !mPlayer.standOut || mPlayer.creamVoidMode || player.ownedProjectileCounts[ModContent.ProjectileType<ExposingCream>()] >= 2 || mPlayer.creamAnimationReverse && mPlayer.creamFrame == 0 && mPlayer.creamNormalToExposed)
            {
                mPlayer.creamFrame = 0;
                mPlayer.creamNormalToExposed = false;
                mPlayer.creamAnimationReverse = false;
                mPlayer.creamExposedMode = false;
                Projectile.Kill();
                return;
            }
            if (Main.mouseRight && !mPlayer.creamNormalToExposed && !mPlayer.creamExposedToVoid)
            {
                mPlayer.creamFrame = 5;
                mPlayer.creamNormalToExposed = true;
                mPlayer.creamAnimationReverse = true;
            }
            if (Projectile.owner == Main.myPlayer)
            {
                if (!Main.dedServ)
                    mPlayer.creamExposedMode = true;
                float halfScreenWidth = (float)Main.screenWidth / 2f;
                float halfScreenHeight = (float)Main.screenHeight / 2f;
                mPlayer.VoidCamPosition = Projectile.Center - new Vector2(halfScreenWidth, halfScreenHeight);
                if (Main.mouseLeft)
                {
                    Projectile.velocity = Main.MouseWorld - Projectile.position;
                    Projectile.velocity.Normalize();
                    Projectile.velocity *= (1f + mPlayer.creamTier);

                    if (Main.MouseWorld.X > Projectile.position.X)
                    {
                        player.ChangeDir(1);
                    }
                    else
                    {
                        player.ChangeDir(-1);
                    }
                }
                else
                {
                    Projectile.velocity = Vector2.Zero;
                }
                Projectile.netUpdate = true;
            }
        }

        public override bool TileCollideStyle(ref int widht, ref int height, ref bool fallThrough)
        {
            widht = Projectile.width + 14;
            height = Projectile.height + 14;
            fallThrough = true;
            return true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            MyPlayer mPlayer = player.GetModPlayer<MyPlayer>());
            mPlayer.creamExposedMode = false;
            player.fallStart = (int)player.position.Y;
        }
    }
}