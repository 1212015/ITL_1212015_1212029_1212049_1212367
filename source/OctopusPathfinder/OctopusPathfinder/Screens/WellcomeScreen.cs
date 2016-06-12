using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OctopusPathfinder.Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OctopusPathfinder.Screens
{
    class WellcomeScreen : GameScreen
    {
        float fDepth = 0;
        bool bIsLigther = true;

        public override void LoadContent()
        {
            base.LoadContent();

            SoundManager.PlaySound(Sounds.Load);

            LevelMenu.lLevelsStar = new List<sbyte>();

            DataManager.ReadFile();
            LevelMenu.lLevelsStar.Add(0);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            
            if (!bIsLigther)
            {
                fDepth -= 0.02f;
            }
            else
            {
                fDepth += 0.007f;
            }
            
            if (fDepth >= 1)
            {
                bIsLigther = false;
            }

            if ((!bIsLigther) && (fDepth < 0))
            {
                ScreenManager.AddScreen(new GameMenu());
                ExitScreen();

                SoundManager.PlaySong(SongBackground.songInMenu);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.CSLogo), new Rectangle(0, 0, 800, 600), new Rectangle(0, 0, 2, 1), new Color(fDepth, fDepth, fDepth));
            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.CSLogo), new Rectangle(260, 200, (int)(360 * 0.75f), (int)(260 * 0.75f)), new Color(fDepth, fDepth, fDepth));
            
            spriteBatch.End();
        }
    }
}
