using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OctopusPathfinder.Effects;
using Microsoft.Xna.Framework;
using OctopusPathfinder.Manager;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using OctopusPathfinder.Screens;

namespace OctopusPathfinder.ObjectGame
{
    enum Actions
    {
        Stay,
        Down,
        InWater,
        Left,
        Right,
        Up,
        Dead
    }

    class Octopus
    {
        private Animation octopusStay;
        private Animation octopusDown;
        private Animation octopusInWater;
        private Animation octopusLeft;
        private Animation octopusRight;
        private Animation octopusUp;
        private Animation octopusDead;

        private Vector2 cPosition = Vector2.Zero;
        private Vector2 cPositionStart = Vector2.Zero;
        private Vector2 vVelocity = new Vector2(2, 2);

        private Vector2 cModPosition = Vector2.Zero;

        private int iSteps = 0;
        private Vector2 cPositionCircle;

        private int iMiliseconPerFrame = 30;

        private KeyboardState oldKeyboardState;

        private Actions octopusActions = Actions.Stay;

        private Vector2 cLimitMovement;

        //public Octopus(Vector2 cPosition, Vector2 cLimitMovement, int iSteps)
        public Octopus(Vector2 cPosition, Vector2 cLimitMovement)
        {
            octopusDown = new Animation(TextureManager.GetTexture2D(ETexture2D.OctopusDown),
                            3, 3, 7, iMiliseconPerFrame, false);
            octopusInWater = new Animation(TextureManager.GetTexture2D(ETexture2D.OctopusInWater),
                            5, 3, 15, iMiliseconPerFrame, false);
            octopusLeft = new Animation(TextureManager.GetTexture2D(ETexture2D.OctopusLeft),
                            3, 3, 8, iMiliseconPerFrame, false);
            octopusRight = new Animation(TextureManager.GetTexture2D(ETexture2D.OctopusRight),
                            3, 3, 8, iMiliseconPerFrame, false);
            octopusStay = new Animation(TextureManager.GetTexture2D(ETexture2D.OctopusStay),
                            5, 6, 30, iMiliseconPerFrame, false);
            octopusUp = new Animation(TextureManager.GetTexture2D(ETexture2D.OctopusUp),
                            3, 3, 7, iMiliseconPerFrame, false);
            octopusDead = new Animation(TextureManager.GetTexture2D(ETexture2D.OctopusDead),
                            4, 3, 10, 40, true);

            this.cPosition = cPosition;
            this.cPositionStart = cPosition;
            this.cLimitMovement = cLimitMovement;

            //this.iSteps = iSteps;
            cPositionCircle = cPosition - new Vector2(-12, 24);
        }

       

        public void HandleInput()
        {


            oldKeyboardState = Keyboard.GetState();

            if (octopusActions == Actions.Stay)
            {
                if (oldKeyboardState.IsKeyDown(Keys.Up))
                {
                    cPosition.Y -= vVelocity.Y;
                    octopusActions = Actions.Up;

                    //iSteps -= 1;
                }


                else if (oldKeyboardState.IsKeyDown(Keys.Down))
                {
                    cPosition.Y += vVelocity.Y;
                    octopusActions = Actions.Down;

                    //iSteps -= 1;
                }


                else if (oldKeyboardState.IsKeyDown(Keys.Left))
                {
                    cPosition.X -= vVelocity.X;
                    octopusActions = Actions.Left;

                    //iSteps -= 1;
                }


                else if (oldKeyboardState.IsKeyDown(Keys.Right))
                {
                    cPosition.X += vVelocity.X;
                    octopusActions = Actions.Right;

                    //iSteps -= 1;
                }

                LimitMovement();
            }


        }

        public void Update(GameTime gameTime)
        {
            switch (octopusActions)
            {
                case Actions.Down:
                    octopusDown.Update(gameTime);
                    break;
                case Actions.Up:
                    octopusUp.Update(gameTime);
                    break;
                case Actions.Right:
                    octopusRight.Update(gameTime);
                    break;
                case Actions.Left:
                    octopusLeft.Update(gameTime);
                    break;
                case Actions.Stay:
                    octopusStay.Update(gameTime);
                    break;
                case Actions.Dead:
                    octopusDead.Update(gameTime);
                    break;
            }

            octopusInWater.Update(gameTime);
            octopusLeft.Update(gameTime);
            octopusRight.Update(gameTime);
            octopusStay.Update(gameTime);
            octopusUp.Update(gameTime);
            octopusDead.Update(gameTime);



            if ((((cPosition.X - cPositionStart.X) % 40) != 0) || (((cPosition.Y - cPositionStart.Y) % 40) != 0))
            {
                cModPosition.X = (cPosition.X - cPositionStart.X) % 40;
                cModPosition.Y = (cPosition.Y - cPositionStart.Y) % 40;
            }



            if (((cModPosition.X % 40) != 0) && (cModPosition.X > 0))
            {

                cPosition.X += vVelocity.X;
                cModPosition.X += vVelocity.X;

            }
            else if (((cModPosition.X % 40) != 0) && (cModPosition.X < 0))
            {
                cPosition.X -= vVelocity.X;
                cModPosition.X -= vVelocity.X;

            }

            else if (((cModPosition.Y % 40) != 0) && (cModPosition.Y > 0))
            {
                cPosition.Y += vVelocity.Y;
                cModPosition.Y += vVelocity.Y;

            }

            else if ((cModPosition.Y % 40 != 0) && (cModPosition.Y) < 0)
            {
                cPosition.Y -= vVelocity.Y;
                cModPosition.Y -= vVelocity.Y;

            }
            else
            {
                octopusActions = Actions.Stay;
            }

            if ((cModPosition.X == 40 ||
                 cModPosition.Y == 40 ||
                 cModPosition.X == -40 ||
                 cModPosition.Y == -40) &&
                 octopusActions != Actions.Stay)
            {
                //iSteps -= 1;
            }

            if (octopusActions == Actions.Stay)
            {
                cPositionStart = cPosition;
            }

            //if (iSteps == 0 && octopusActions == Actions.Stay)
            //{
            //    octopusActions = Actions.Dead;
            //}

            cPositionCircle = cPosition - new Vector2(-12, 24);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (octopusActions)
            {
                case Actions.Down:
                    octopusDown.Draw(spriteBatch, cPosition);
                    break;
                case Actions.Up:
                    octopusUp.Draw(spriteBatch, cPosition);
                    break;
                case Actions.Right:
                    octopusRight.Draw(spriteBatch, cPosition);
                    break;
                case Actions.Left:
                    octopusLeft.Draw(spriteBatch, cPosition);
                    break;
                case Actions.Stay:
                    octopusStay.Draw(spriteBatch, cPosition);
                    break;
                case Actions.Dead:
                    octopusDead.Draw(spriteBatch, cPosition);
                    break;
            }

            if (octopusActions == Actions.Stay)
            {
                spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.CircularShape),
                                cPositionCircle, Color.White);
            }
            else
            {
                spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.CircularShape),
                                new Rectangle(Convert.ToInt32(cPositionCircle.X), Convert.ToInt32(cPositionCircle.Y), 31, 34), Color.White);
                spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.CircularShape),
                                new Rectangle(Convert.ToInt32(cPositionCircle.X), Convert.ToInt32(cPositionCircle.Y), 30, 35), Color.White);
            }

            //spriteBatch.DrawString(TextureManager.GetSpriteFont(ESpriteFont.FontNormal),
            //                iSteps.ToString(), cPositionCircle + new Vector2(16f, 16f),
            //                Color.Black, 0.0f,
            //                new Vector2(
            //                TextureManager.GetSpriteFont(ESpriteFont.FontMenu).MeasureString(iSteps.ToString()).X / 2,
            //                TextureManager.GetSpriteFont(ESpriteFont.FontMenu).MeasureString(iSteps.ToString()).Y / 2),
            //                1f, SpriteEffects.None, 0.0f);
        }

        public bool CheckKeyboard(Keys key)
        {
            KeyboardState currKeyboardState = Keyboard.GetState();

            bool resulf = (oldKeyboardState.IsKeyDown(key) &&
                            (currKeyboardState.IsKeyUp(key)));

            oldKeyboardState = currKeyboardState;

            return resulf;
        }

        public void LimitMovement()
        {
            if (cPosition.X < cLimitMovement.X)
            {
                cPosition.X = cLimitMovement.X;
                //octopusActions = Actions.Stay;
            }

            if (cPosition.X > (cLimitMovement.X + 520))
            {
                cPosition.X = cLimitMovement.X + 520;
                //octopusActions = Actions.Stay;
            }


            if (cPosition.Y < cLimitMovement.Y)
            {
                cPosition.Y = cLimitMovement.Y;
                //octopusActions = Actions.Stay;
            }

            if (cPosition.Y > cLimitMovement.Y + 320)
            {
                cPosition.Y = cLimitMovement.Y + 320;
                //octopusActions = Actions.Stay;
            }
        }

        public Vector2 CCircle
        {
            get
            {
                return cPositionCircle;
            }
           
        }

        public Vector2 CPosition
        {
            get
            {
                return cPosition;
            }

            set
            {
                cPosition = value;
            }
        }

        public Actions OctopusAction
        {
            get
            {
                return octopusActions;
            }
            set
            {
                octopusActions = value;
            }
        }

        public Vector2 CPositionStart
        {
            get
            {
                return cPositionStart;
            }
        }
    }
}
