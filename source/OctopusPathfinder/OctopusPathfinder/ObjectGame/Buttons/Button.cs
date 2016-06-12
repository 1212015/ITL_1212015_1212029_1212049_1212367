using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OctopusPathfinder.Effects;
using OctopusPathfinder.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OctopusPathfinder.ObjectGame
{
    class Button : Animation
    {
        protected bool bClicked;

        protected Vector2 cTexture = Vector2.Zero;

        protected Rectangle rect;

        protected MouseState previousMouseState = Mouse.GetState();
        protected MouseState curentMouseState = Mouse.GetState();

        protected string sName;
        protected Vector2 cName;

        public Button(Texture2D ttAnimation, int iRow, int iColumn, int iNumberFrame,
                      int iMiliseconPerFrame, string sName, bool bPause, Vector2 cButton)
            : base(ttAnimation, iRow, iColumn, iNumberFrame, iMiliseconPerFrame, bPause)
        {
            this.rect = new Rectangle((int)cButton.X, (int)cButton.Y,
                                             ttAnimation.Width / iColumn, ttAnimation.Height / iRow);

            this.sName = sName;
            this.cName = new Vector2(((rect.Right - TextureManager.GetSpriteFont(ESpriteFont.FontMenu).
                                             MeasureString(sName).X - rect.Left) / 2 + rect.Left),
                                            (rect.Bottom - TextureManager.GetSpriteFont(ESpriteFont.FontMenu).
                                             MeasureString(sName).Y - rect.Top) / 2 + rect.Top);
        }

        public override void Update(GameTime gameTime)
        {
            curentMouseState = Mouse.GetState();
            
            if (rect.Contains((int)curentMouseState.X, (int)curentMouseState.Y))
            {
                if (iIndexFrame < iNumberFrame - 1)
                {
                    iIndexFrame += 1;
                }

                if (!rect.Contains((int)previousMouseState.X, (int)previousMouseState.Y))
                {
                    SoundManager.PlaySound(Sounds.OnButton);
                }

                if (CheckMouseState())
                {
                    bClicked = true;

                    SoundManager.PlaySound(Sounds.Click);
                }
            }
            else
            {
                iIndexFrame = 0;
            }

            previousMouseState = Mouse.GetState();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ttAnimation, rect, listFrames[iIndexFrame], Color.White);

            spriteBatch.DrawString(TextureManager.GetSpriteFont(ESpriteFont.FontMenu), sName,
                                   cName, Color.White);
        }

        public bool CheckMouseState()
        {
            bool bResulf = (curentMouseState.LeftButton == ButtonState.Released &&
                            previousMouseState.LeftButton == ButtonState.Pressed);

            return bResulf;
        }

        public virtual float RectY
        {
            get
            {
                return rect.Y;
            }

            set
            {
                rect.Y = (int)value;
            }
        }

        public bool Clicked
        {
            get
            {
                return bClicked;
            }
            set
            {
                bClicked = value;
            }
        }

    }
}
