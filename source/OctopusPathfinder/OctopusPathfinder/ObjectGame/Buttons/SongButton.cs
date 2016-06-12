using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OctopusPathfinder.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OctopusPathfinder.ObjectGame
{
    class SongButton
    {
        public static Vector2 cPos;
        static Color color;

        static MouseState curMouseState;
        static MouseState preMouseState;

        static Rectangle rectPosition;

        public static void HandleInput()
        {
            curMouseState = Mouse.GetState();

            if (rectPosition.Contains((int)curMouseState.X, (int)curMouseState.Y))
            {
                color = Color.CornflowerBlue;

                if (rectPosition.Contains((int)curMouseState.X, (int)curMouseState.Y) &&
                    !rectPosition.Contains((int)preMouseState.X, (int)preMouseState.Y))
                {
                    SoundManager.PlaySound(Sounds.OnButton);
                }

                if (curMouseState.LeftButton == ButtonState.Pressed && preMouseState.LeftButton == ButtonState.Released)
                {
                    SoundManager.PlaySound(Sounds.Click);
                    SoundManager.MuteSong = !SoundManager.MuteSong;
                }
            }
            else
            {
                color = Color.White;

                if (SoundManager.MuteSong == true)
                {
                    color = Color.Gray;
                }
            }

            preMouseState = curMouseState;
        }


        public static void Update(GameTime gameTime)
        {
            rectPosition = new Rectangle((int)cPos.X, (int)cPos.Y, 46, 46);

            HandleInput();
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.Song), rectPosition, color);
        }
    }
}