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
using OctopusPathfinder.ObjectGame.AStar;

namespace OctopusPathfinder.Screens
{
    class MainGame : GameScreen
    {
        Vector2 cBackground = new Vector2(67, 72);

        //int idLevel;
        Level listLevels;
        //int[,] arrCurrentLevel;

        Octopus octopus;

        //Item item;
        Button buttonFindRoad;
        Button buttonBeanSearch;

        ConvertButton buttonNextLevel;
        ConvertButton buttonBack;

        AStar astar;

        Vector2 cStart;
        Vector2 cGoal;

        BeamSearch beamSearch;

        int idLevel = 0;
        int iRoad = -1;

        public MainGame( int idLevel)
        {
            
            listLevels = new Level();
            //arrCurrentLevel = Level.listLevels[0];
            if (idLevel >= 11)
            {
                idLevel = 0;
            }

            this.idLevel = idLevel;

            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (Level.listLevels[idLevel][i, j] == 2)
                    {
                        cStart = new Vector2(i ,j);
                    }
                    else if (Level.listLevels[idLevel][i, j] == 3)
                    {
                        cGoal = new Vector2(i, j);
                    }
                }
            }

            

            astar = new AStar(cStart, cGoal, Level.listLevels[idLevel]);

            beamSearch = new BeamSearch(Level.listLevels[idLevel], 0, 11, 16, cStart, cGoal);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            octopus = new Octopus(CoordinateTransfor((int)cStart.X, (int)cStart.Y) - new Vector2(7.5f, 9.1f),
                                  CoordinateTransfor(1, 1) - new Vector2(7.5f, 9.1f));

            buttonFindRoad = new Button(TextureManager.GetTexture2D(ETexture2D.HelpButton), 4, 1, 4, 30,
                                        "Help", false, new Vector2(360, 15));

            buttonNextLevel = new ConvertButton(TextureManager.GetTexture2D(ETexture2D.Button), 4, 1, 4, 30,
                                         "Next Level", false, ScreenManager, this, new MainGame(idLevel + 1), new Vector2(580, 550));

            buttonBack = new ConvertButton(TextureManager.GetTexture2D(ETexture2D.Button), 4, 1, 4, 30,
                                            "Back", false, ScreenManager, this, new LevelMenu(), new Vector2(20, 550));

            buttonBeanSearch = new Button(TextureManager.GetTexture2D(ETexture2D.Button), 4, 1, 4, 30,
                                        "HillClimbing", false, new Vector2(540, 15));
        }

        public override void HandleInput()
        {
            base.HandleInput();

            octopus.HandleInput();

            if (octopus.OctopusAction != Actions.Stay)
            {
                if (CollisionWall(octopus.CPositionStart))
                {
                    octopus.CPosition = octopus.CPositionStart;

                }
            }

            if (buttonFindRoad.Clicked)
            {
                astar.Pathfinder(MatrixTransfor(octopus.CPosition), cGoal);
                beamSearch = new BeamSearch(Level.listLevels[idLevel], astar.Steps + 1, 11, 16, MatrixTransfor(octopus.CPosition), cGoal);
                iRoad = -1;
                buttonFindRoad.Clicked = false;
            }
           
            if (buttonBeanSearch.Clicked)
            {

                if (iRoad < 0)
                {
                    //beamSearch.ListAllRoadClear();
                    beamSearch.AllRoad();
                }

                if (astar.Steps != 0)
                {
                    iRoad++;
                }

                if (iRoad >= beamSearch.Roads)
                {
                    iRoad = 0;
                }

                buttonBeanSearch.Clicked = false;
            }

        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            octopus.Update(gameTime);

            buttonBeanSearch.Update(gameTime);
            buttonFindRoad.Update(gameTime);
            buttonNextLevel.Update(gameTime);
            buttonBack.Update(gameTime);


            if (!SoundManager.bIsSongInGame)
            {
                SoundManager.PlaySong(SongBackground.songInGame);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.MenuBG), new Rectangle(0, 0, 800, 600), Color.White);

            spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.Background), cBackground, Color.White);

            spriteBatch.DrawString(TextureManager.GetSpriteFont(ESpriteFont.FontMenu), "Level " + (idLevel + 1).ToString(), new Vector2(340, 80), Color.White);

            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (Level.listLevels[idLevel][i, j] == 1)
                    {
                        spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.Rock),
                                CoordinateTransfor(i, j), Color.White);
                    }

                    if (Level.listLevels[idLevel][i, j] == 3)
                    {
                        spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.Exit),
                                 CoordinateTransfor(i, j), Color.White);
                    }
                }
            }

            
            beamSearch.Draw(spriteBatch, iRoad);
            astar.Draw(spriteBatch, octopus.CCircle);


            octopus.Draw(spriteBatch);

            spriteBatch.DrawString(TextureManager.GetSpriteFont(ESpriteFont.FontNormal),
                               astar.Steps.ToString(), octopus.CCircle + new Vector2(20f, 16f),
                               Color.White, 0.0f,
                               new Vector2(
                               TextureManager.GetSpriteFont(ESpriteFont.FontMenu).MeasureString(astar.Steps.ToString()).X / 2,
                               TextureManager.GetSpriteFont(ESpriteFont.FontMenu).MeasureString(astar.Steps.ToString()).Y / 2),
                               1f, SpriteEffects.None, 0.0f);

            

            buttonBeanSearch.Draw(spriteBatch);
            buttonFindRoad.Draw(spriteBatch);
            buttonNextLevel.Draw(spriteBatch);
            buttonBack.Draw(spriteBatch);

            
            

            spriteBatch.End();
        }

        public Vector2 MatrixTransfor(Vector2 cPosition)
        {
            cPosition += new Vector2(7.5f, 9.1f);
            Vector2 cIndexMatrix = new Vector2(((int)cPosition.Y - 112) / 40, ((int)cPosition.X - 67) / 40);

            if (octopus.CPosition.X > octopus.CPositionStart.X)
            {
                cIndexMatrix.Y += 1;
            }
            else if (octopus.CPosition.X < octopus.CPositionStart.X)
            {
                cIndexMatrix.Y -= 1;
            }
            else if (octopus.CPosition.Y > octopus.CPositionStart.Y)
            {
                cIndexMatrix.X += 1;
            }
            else if (octopus.CPosition.Y < octopus.CPositionStart.Y)
            {
                cIndexMatrix.X -= 1;
            }

            return cIndexMatrix;

        }

        static public Vector2 CoordinateTransfor(int i, int j)
        {
            return new Vector2(j * 40 + 67, i * 40 + 112);
        }

        public bool CollisionWall(Vector2 cPositionStart)
        {
            cPositionStart = MatrixTransfor(cPositionStart);

            int i = (int)cPositionStart.X;
            int j = (int)cPositionStart.Y;

            if (Level.listLevels[idLevel][i, j] == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
