using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OctopusPathfinder.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using OctopusPathfinder.ObjectGame;
using OctopusPathfinder.Effects;

namespace OctopusPathfinder.Screens
{
    class ExitGame : GameScreen
    {
        Vector2 cRect = new Vector2(100, 150);

        ConvertButton bYes;
        ConvertButton bNo;

        public override void LoadContent()
        {
            base.LoadContent();

            bYes = new ConvertButton(TextureManager.GetTexture2D(ETexture2D.Button), 4, 1, 4, 30,
                                     "YES", false, ScreenManager, this, null, new Vector2(150, 300));

            bNo = new ConvertButton(TextureManager.GetTexture2D(ETexture2D.Button), 4, 1, 4, 30,
                                    "NO", true, ScreenManager, this, null, new Vector2(450, 300));
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            bNo.Update(gameTime);
            bYes.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.Rect), cRect, Color.White);

            spriteBatch.DrawString(TextureManager.GetSpriteFont(ESpriteFont.FontMenu),
                                    "Are you sure you want to exit?", new Vector2(150, 200), Color.GhostWhite);

            bYes.Draw(spriteBatch);
            bNo.Draw(spriteBatch);
       
            spriteBatch.End();
        }
    }
}