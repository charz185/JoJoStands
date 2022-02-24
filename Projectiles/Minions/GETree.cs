using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoJoStands.Projectiles.Minions
{
    public class GETree : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 13;
        }

        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 41;
            projectile.friendly = true;
            projectile.penetrate = 9999;
            projectile.timeLeft = 5;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.scale = 2f;
            drawOriginOffsetX = -10;
            drawOriginOffsetY = 21;
        }

        private bool timeLeftDeclared = false;
        private bool shrinkAndDie = false;

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                for (int p = 0; p < Main.maxProjectiles; p++)
                {
                    Projectile otherProj = Main.projectile[p];
                    if (otherProj.active && projectile.Hitbox.Intersects(otherProj.Hitbox))
                    {
                        Player otherPlayer = Main.player[otherProj.owner];
                        if (projectile.owner != otherProj.owner && player.team != otherPlayer.team)
                        {
                            if (MyPlayer.Sounds)
                            {
                                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/sound/Punch_land").WithVolume(.3f));
                            }
                            Dust.NewDust(otherProj.position + otherProj.velocity, projectile.width, projectile.height, DustID.FlameBurst, otherProj.velocity.X * -0.5f, otherProj.velocity.Y * -0.5f);
                            otherPlayer.Hurt(PlayerDeathReason.ByCustomReason(otherPlayer.name + " loved the damage reflection given by " + player.name + "'s damage-reflecting tree too much."), otherProj.damage, 1, true);
                            otherProj.Kill();
                        }
                    }
                }
            }

            projectile.velocity.X = 0f;
            projectile.velocity.Y = 3f;
            projectile.direction = -1;
            if (projectile.ai[0] == 2f && !timeLeftDeclared)
            {
                projectile.timeLeft = 900;
                timeLeftDeclared = true;
            }
            if (projectile.ai[0] == 3f && !timeLeftDeclared)
            {
                projectile.timeLeft = 1200;
                timeLeftDeclared = true;
            }
            if (projectile.ai[0] == 4f && !timeLeftDeclared)
            {
                projectile.timeLeft = 1500;
                timeLeftDeclared = true;
            }
            if (projectile.ai[0] == 5f && !timeLeftDeclared)
            {
                projectile.timeLeft = 1800;
                timeLeftDeclared = true;
            }

            if (projectile.timeLeft <= 181)
            {
                shrinkAndDie = true;
            }
            if (!shrinkAndDie)
            {
                if (projectile.frame <= 11)
                    projectile.frameCounter++;

                if (projectile.frameCounter >= 14)
                {
                    projectile.frame += 1;
                    projectile.frameCounter = 0;
                }
            }
            if (shrinkAndDie)
            {
                projectile.frameCounter++;
                if (projectile.frameCounter >= 14)
                {
                    projectile.frame -= 1;
                    projectile.frameCounter = 0;
                }
                if (projectile.frame <= 0)
                {
                    projectile.Kill();
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = target.damage;
            knockback = Math.Abs(target.velocity.X);      //they're just gonna have to go back as fast as they were going
        }
    }
}