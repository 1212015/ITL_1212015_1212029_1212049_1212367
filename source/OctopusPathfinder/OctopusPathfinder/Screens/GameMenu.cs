using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
    class GameMenu : GameScreen
    {
        float fVEC = 5;

        Vector2 cStartingButtonPosition = new Vector2((800 - 206) / 2, 580);
        Vector2 cRectPosition;

        ConvertButton btStarting;
        ConvertButton btAbout;
        ConvertButton btHelping;
        ConvertButton btExiting;

        bool bIsButtonGoingUp = true;

        public GameMenu()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5f);
            TransitionOffTime = TimeSpan.FromMinutes(0.5f);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            cRectPosition = new Vector2(cStartingButtonPosition.X - 32, 330);

            btStarting = new ConvertButton(TextureManager.GetTexture2D(ETexture2D.Button), 4, 1, 4, 30, "START",
                                           false, ScreenManager, this, new LevelMenu(), cStartingButtonPosition );

            btAbout = new ConvertButton(TextureManager.GetTexture2D(ETexture2D.Button), 4, 1, 4, 30, "ABOUT",
                                        false, ScreenManager, this, new AboutGame(), new Vector2(cStartingButtonPosition.X, cStartingButtonPosition.Y + 50));

            btHelping = new ConvertButton(TextureManager.GetTexture2D(ETexture2D.Button), 4, 1, 4, 30, "HELP",
                                          false, ScreenManager, this, new Helping(), new Vector2(cStartingButtonPosition.X, cStartingButtonPosition.Y + 100));

            btExiting = new ConvertButton(TextureManager.GetTexture2D(ETexture2D.Button), 4, 1, 4, 30,
                                          "EXIT", true, ScreenManager, this, new ExitGame(), new Vector2(cStartingButtonPosition.X, cStartingButtonPosition.Y + 150));

            SongButton.cPos = new Vector2(650, 20);
            SoundButton.cPos = new Vector2(700, 20);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (IsActive)
            {
                btStarting.Update(gameTime);
                btAbout.Update(gameTime);
                btHelping.Update(gameTime);
                btExiting.Update(gameTime);

                SongButton.Update(gameTime);
                SoundButton.Update(gameTime);

                if (fVEC != 0)
                {
                    if (bIsButtonGoingUp)
                    {
                        fVEC = -10;
                    }
                    else
                    {
                        fVEC = 5;
                    }

                    btStarting.RectY += fVEC;
                    btAbout.RectY += fVEC;
                    btExiting.RectY += fVEC;
                    btHelping.RectY += fVEC;

                    if (btStarting.RectY <= 320)
                    {
                        bIsButtonGoingUp = false;
                    }

                    if ((btStarting.RectY >= cRectPosition.Y + 50) && (!bIsButtonGoingUp))
                    {
                        fVEC = 0;
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.MenuBG), new Rectangle(-10, 0, 850, 650), Color.White);
            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.Name), new Rectangle(0, 0, 800, 250), Color.White);
            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.Waves), new Rectangle(-10, 300, 850, 350), Color.White);

            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.Tentacle), new Rectangle(250, 180, 600, 164), new Rectangle(0, 0, 600, 164), Color.White, 0,
                                new Vector2(), SpriteEffects.None, 1);
            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.Tentacle), new Rectangle(-100, 240, 600, 164), new Rectangle(0, 0, 600, 164), Color.White, 0,
                                new Vector2(), SpriteEffects.FlipHorizontally, 1);

            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.Rect2), cRectPosition, Color.White);

            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.CSLogo), new Rectangle(10, (600 - 104 - 10), 144, 104), Color.White);

            SongButton.Draw(spriteBatch);
            SoundButton.Draw(spriteBatch);

            btStarting.Draw(spriteBatch);
            btAbout.Draw(spriteBatch);
            btHelping.Draw(spriteBatch);
            btExiting.Draw(spriteBatch);

            spriteBatch.End();

            if (!IsActive)
            {
                ScreenManager.FadeBackBufferToBlack(0.6f);
            }
        }
    }
}
