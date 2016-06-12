using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using OctopusPathfinder.Effects;
using OctopusPathfinder.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OctopusPathfinder.ObjectGame
{
    class Curtain
    {
        static Vector2 TOP_LEFT = new Vector2(-200, -400);
        static Vector2 RIGTH_BOTTOM = new Vector2(100, 600);

        static Vector2 cPosition = RIGTH_BOTTOM;

        public static Vector2 vVelocity = new Vector2(- (RIGTH_BOTTOM.X - TOP_LEFT.X) / 80, - (RIGTH_BOTTOM.Y - TOP_LEFT.Y) / 80);
        
        public static bool bIsGoing;

        public static bool bCanConvert = false;

        public static string sName = "";

        public static void Update(GameTime gameTime)
        {
            if (bIsGoing)
            {
                cPosition += vVelocity;

                if (cPosition.X <= TOP_LEFT.X)
                {
                    vVelocity = -vVelocity;
                    bCanConvert = true;
                }
                else
                {
                    if (cPosition.X > RIGTH_BOTTOM.X)
                    {
                        bIsGoing = false;
                        vVelocity = -vVelocity;
                    }
                }

                if (!bIsGoing)
                {
                    bCanConvert = false;
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (bIsGoing)
            {
                spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.Curtain), new Rectangle((int)cPosition.X, (int)cPosition.Y, 1500, 1200), Color.White);
            }
        }
    }
}