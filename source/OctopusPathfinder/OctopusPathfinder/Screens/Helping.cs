using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OctopusPathfinder.Manager;
using OctopusPathfinder.ObjectGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OctopusPathfinder.Screens
{
    class Helping : GameScreen
    {
        ConvertButton btOK;

        public override void LoadContent()
        {
            base.LoadContent();

            btOK = new ConvertButton(TextureManager.GetTexture2D(ETexture2D.Button), 4, 1, 4, 30, "OK",
                                 false, ScreenManager, this, new GameMenu(), new Vector2((800 - 206) / 2, 535));
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            btOK.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.MenuBG), new Rectangle(0, 0, 800, 600), Color.White);
            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.Rect), new Rectangle(10, 70, 780, 450), Color.White);

            spriteBatch.DrawString(TextureManager.GetSpriteFont(ESpriteFont.FontHeading), "HELP", new Vector2(50, 30), Color.White);

            spriteBatch.DrawString(TextureManager.GetSpriteFont(ESpriteFont.FontNormal), "To save your Octopus from the Sun by", new Vector2(30, 130), Color.Azure);
            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.KeyBoard), new Rectangle(480, 110, 162, 90), Color.White);

            spriteBatch.DrawString(TextureManager.GetSpriteFont(ESpriteFont.FontNormal), "The circle with number which your step Octopus can move.", new Vector2(120, 210), Color.White);
            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.HelpStart), new Rectangle(35, 190, 80, 90), Color.White);

            spriteBatch.DrawString(TextureManager.GetSpriteFont(ESpriteFont.FontNormal), "On the way, the rocks are obstacles,\n   Octopus can't go through it", new Vector2(150, 280), Color.White);
            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.HelpRock), new Rectangle(520, 270, 120, 90), Color.White);

            spriteBatch.DrawString(TextureManager.GetSpriteFont(ESpriteFont.FontNormal), "Get Octopus to the pond to win. The opposite, it'll die", new Vector2(120, 390), Color.White);
            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.HelpWin), new Rectangle(30, 370, 80, 90), Color.White);
            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.HelpDied), new Rectangle(680, 370, 80, 90), Color.White);
    
            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.CSLogo), new Rectangle(10, 530, 83, 60), Color.White);

            btOK.Draw(spriteBatch);

            spriteBatch.End();
        }


    }
}
