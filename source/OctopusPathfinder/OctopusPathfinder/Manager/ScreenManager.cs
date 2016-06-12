using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace OctopusPathfinder.Manager
{

    public class ScreenManager : DrawableGameComponent
    {
        List<GameScreen> screens = new List<GameScreen>();
        List<GameScreen> screensToUpdate = new List<GameScreen>();


        SpriteBatch spriteBatch;

        bool isInitialized;

        bool traceEnabled;

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }


        public bool TraceEnabled
        {
            get { return traceEnabled; }
            set { traceEnabled = value; }
        }


        public ScreenManager(Game game)
            : base(game)
        {


        }


        public override void Initialize()
        {
            base.Initialize();

            isInitialized = true;
        }


        protected override void LoadContent()
        {
            ContentManager content = Game.Content;

            spriteBatch = new SpriteBatch(GraphicsDevice);

            SoundManager.LoadContent(content);
            //SoundManager.PlaySong(SongBackground.songInGame);

            foreach (GameScreen screen in screens)
            {
                screen.LoadContent();
            }
        }

        protected override void UnloadContent()
        {
            foreach (GameScreen screen in screens)
            {
                screen.UnloadContent();
            }
        }



        public override void Update(GameTime gameTime)
        {
            screensToUpdate.Clear();

            foreach (GameScreen screen in screens)
                screensToUpdate.Add(screen);

            bool otherScreenHasFocus = !Game.IsActive;

            bool coveredByOtherScreen = false;



            while (screensToUpdate.Count > 0)
            {

                GameScreen screen = screensToUpdate[screensToUpdate.Count - 1];

                screensToUpdate.RemoveAt(screensToUpdate.Count - 1);

                screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);


                if (screen.ScreenState == ScreenState.TransitionOn ||
                    screen.ScreenState == ScreenState.Active)
                {

                    if (!otherScreenHasFocus)
                    {
                        screen.HandleInput();

                        otherScreenHasFocus = true;
                    }

                    if (!screen.IsPopup)
                        coveredByOtherScreen = true;
                }
            }

        }

        public override void Draw(GameTime gameTime)
        {
            foreach (GameScreen screen in screens)
            {
                if (screen.ScreenState == ScreenState.Hidden)
                    continue;

                screen.Draw(gameTime);
            }
        }


        public void AddScreen(GameScreen screen)
        {
            screen.ScreenManager = this;
            screen.IsExiting = false;


            if (isInitialized)
            {
                screen.LoadContent();
            }


            screens.Add(screen);


        }


        public void OnlyOne()
        {
            screens.RemoveRange(0, screens.Count - 1);
        }

        public void RemoveScreen(GameScreen screen)
        {
            // If we have a graphics device, tell the screen to unload content.
            if (isInitialized)
            {
                screen.UnloadContent();
            }

            screens.Remove(screen);
            screensToUpdate.Remove(screen);

        }



        public GameScreen[] GetScreens()
        {
            return screens.ToArray();
        }

        public void FadeBackBufferToBlack(float alpha)
        {
            Viewport viewport = GraphicsDevice.Viewport;

            spriteBatch.Begin();

            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.CSLogo),
                             new Rectangle(0, 0, viewport.Width, viewport.Height),
                             Color.Black * alpha);

            spriteBatch.End();
        }
    }
}
