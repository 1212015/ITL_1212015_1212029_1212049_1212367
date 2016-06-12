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
    class LevelMenu : GameScreen
    {
        ConvertButton btBackToMenu;

        public static List<sbyte> lLevelsStar;

        List<Rectangle> lCButtonsPosition;

        List<ConvertButton> lLevelButton;

        public override void LoadContent()
        {
            base.LoadContent();

            btBackToMenu = new ConvertButton(TextureManager.GetTexture2D(ETexture2D.Button), 4, 1, 4, 30, "Main Menu",
                                 false, ScreenManager, this, new GameMenu(), new Vector2((800 - 206) / 2, 510) );

            lCButtonsPosition = new List<Rectangle>();

            lLevelButton = new List<ConvertButton>();

            

            for (int i = 0; i < 24; i++)
            {
                lCButtonsPosition.Add(new Rectangle(50 + (i % 6) * 125, 100 + (i / 6) * 100, 72, 50));

                lLevelButton.Add(new ConvertButton(TextureManager.GetTexture2D(ETexture2D.LevelButton), 4, 1, 4, 30,
                                 string.Format("{0}", i + 1), false, ScreenManager, this, new MainGame(i), new Vector2(lCButtonsPosition[i].X, lCButtonsPosition[i].Y)));
            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            Curtain.sName = "Main Menu";

            btBackToMenu.Update(gameTime);            

            for (int i = 0; i < lLevelsStar.Count; i++)
            {                               
                lLevelButton[i].Update(gameTime);
            }

            if (SoundManager.bIsSongInGame)
            {
                SoundManager.PlaySong(SongBackground.songInMenu);
            }
        }
        
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.MenuBG), new Rectangle(0, 0, 800, 600), Color.White);
            //spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.Background), new Rectangle(0, 0, 900, 60), new Rectangle(10, 0, 700, 10), Color.White);

            spriteBatch.DrawString(TextureManager.GetSpriteFont(ESpriteFont.FontHeading), "SELECT LEVEL", new Vector2(50, 15), Color.White);

            
            for (int i = 0; i < 24; i++)
            {
                if (lLevelsStar.Count > i)
                {
                    lLevelButton[i].Draw(spriteBatch);
                }
                else
                {
                   spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.LevelButton), lCButtonsPosition[i],
                                        new Rectangle(0, 199 / 4 * 2, 72, 50), new Color(0.7f, 0.7f, 0.7f));

                    spriteBatch.DrawString(TextureManager.GetSpriteFont(ESpriteFont.FontMenu), string.Format("{0}", i + 1),
                        new Vector2(((lCButtonsPosition[i].Right - TextureManager.GetSpriteFont(ESpriteFont.FontMenu).
                                             MeasureString(string.Format("{0}", i + 1)).X -
                                             lCButtonsPosition[i].Left) / 2 + lCButtonsPosition[i].Left),
                                            (lCButtonsPosition[i].Bottom - TextureManager.GetSpriteFont(ESpriteFont.FontMenu).
                                             MeasureString(string.Format("{0}", i + 1)).Y -
                                             lCButtonsPosition[i].Top) / 2 + lCButtonsPosition[i].Top),
                                             new Color(0.7f, 0.7f, 0.7f));
                    
                }

                    for (int j = 0; j < 3; j++)
                {
                    if ((lLevelsStar.Count > i) && ((j + 1) <= lLevelsStar[i]))
                    {
                        spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.Star),
                                         new Rectangle(lCButtonsPosition[i].X + (j % 3) * 25,
                                         lCButtonsPosition[i].Y + 55, 20, 20), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.Star),
                                         new Rectangle(lCButtonsPosition[i].X + (j % 3) * 25,
                                         lCButtonsPosition[i].Y + 55, 20, 20), new Color(0.2f, 0.2f, 0.3f)); //Brown
                    }
                }
            }

            btBackToMenu.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}

