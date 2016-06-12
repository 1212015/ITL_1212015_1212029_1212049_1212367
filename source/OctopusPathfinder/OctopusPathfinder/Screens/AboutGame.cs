using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OctopusPathfinder.Manager;
using OctopusPathfinder.ObjectGame;
using OctopusPathfinder.ObjectGame.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OctopusPathfinder.Screens
{
    class AboutGame : GameScreen
    {
        String sInformation;

        ConvertButton btOK;

        public override void LoadContent()
        {
            base.LoadContent();

            btOK = new ConvertButton(TextureManager.GetTexture2D(ETexture2D.Button), 4, 1, 4, 30, "OK",
                                 false, ScreenManager, this, new GameMenu(), new Vector2((800 - 206) / 2, 510));

            sInformation = "Developed by a team from of ITL,\n University Of Science Ho Chi Minh City \n Member: \n 1212015_Phan Long Anh \n 1212029_Dang Thi Linh Chi \n 1212049_Nguyen Trong Du \n 1212367_Nguyen Thai Thu Thao";
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
            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.Rect), new Rectangle(10, 100, 780, 380), Color.White);

            spriteBatch.DrawString(TextureManager.GetSpriteFont(ESpriteFont.FontHeading), "ABOUT", new Vector2(50, 30), Color.White);

            spriteBatch.DrawString(TextureManager.GetSpriteFont(ESpriteFont.FontNormal), sInformation, new Vector2(30, 120), Color.Azure);

            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.CSLogo), new Rectangle(550, 120, 180, 130), Color.White);
            
            btOK.Draw(spriteBatch);


            spriteBatch.End();
        }

        public static List<sbyte> lLevelsStar { get; set; }
    }
}
