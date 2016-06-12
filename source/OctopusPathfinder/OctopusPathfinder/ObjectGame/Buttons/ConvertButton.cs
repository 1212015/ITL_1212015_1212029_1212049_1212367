using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OctopusPathfinder.Manager;
using OctopusPathfinder.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using OctopusPathfinder.Screens;


namespace OctopusPathfinder.ObjectGame
{
    class ConvertButton : Button
    {
        GameScreen currentScreen;
        GameScreen nextScreen;

        ScreenManager screenManager;

        bool bClickedConvert = false;

        public ConvertButton(Texture2D ttAnimation, int iRow, int iColumn, int iNumberFrame,
                             int iMiliseconPerFrame, string sName, bool bPause, ScreenManager screenManager,
                             GameScreen currentScreen, GameScreen nextScreen, Vector2 cButton)
            : base(ttAnimation, iRow, iColumn, iNumberFrame, iMiliseconPerFrame, sName, bPause, cButton)
        {
            this.currentScreen = currentScreen;
            this.nextScreen = nextScreen;

            this.screenManager = screenManager;

            this.sName = sName;

            Curtain.sName = sName;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);           

            if (sName == Curtain.sName )
            {
                Curtain.Update(gameTime);
            }

            if (bClicked)
            {
                bClickedConvert = true;
                bClicked = false;
            }

            if (bClickedConvert)
            {
                Curtain.sName = this.sName;

                if (bPause == false)
                {
                    if (nextScreen == null)
                    {
                        DataManager.WriteToFile();
                        screenManager.Game.Exit();
                    }
                    else
                    {
                        Curtain.bIsGoing = true;
                        

                        if (Curtain.bCanConvert)
                        {
                            //DataManager.WriteToFile();
                            screenManager.AddScreen(nextScreen);
                            screenManager.OnlyOne();
                        }
                    }
                }
                else
                {
                    if (nextScreen == null)
                    {
                        screenManager.RemoveScreen(currentScreen);
                    }
                    else
                    {
                        bClickedConvert = false;
                        screenManager.AddScreen(nextScreen);
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            
            Curtain.Draw(spriteBatch);
        }

        public override float RectY
        {
            get
            {
                return rect.Y;
            }

            set
            {
                rect.Y = (int)value;

                cName = new Vector2(((rect.Right - TextureManager.GetSpriteFont(ESpriteFont.FontMenu).
                                             MeasureString(sName).X - rect.Left) / 2 + rect.Left),
                                            (rect.Bottom - TextureManager.GetSpriteFont(ESpriteFont.FontMenu).
                                             MeasureString(sName).Y - rect.Top) / 2 + rect.Top);
            }
        }
    }
}