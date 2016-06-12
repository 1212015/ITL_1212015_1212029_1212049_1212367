using System;
using Microsoft.Xna.Framework;
using System.IO;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace OctopusPathfinder.Manager
{

    public enum ScreenState
    {
        TransitionOn,
        Active,
        TransitionOff,
        Hidden,
    }

   
    public abstract class GameScreen
    {
        
        protected ContentManager Content
        {
            get { return content; }
        }

        private ContentManager content;

        public bool IsPopup
        {
            get { return isPopup; }
             set { isPopup = value; }
        }

        bool isPopup = false;
        
       
        public TimeSpan TransitionOnTime
        {
            get { return transitionOnTime; }
            protected set { transitionOnTime = value; }
        }

        TimeSpan transitionOnTime = TimeSpan.Zero;


        public TimeSpan TransitionOffTime
        {
            get { return transitionOffTime; }
            protected set { transitionOffTime = value; }
        }

        TimeSpan transitionOffTime = TimeSpan.Zero;


        public float TransitionPosition
        {
            get { return transitionPosition; }
            protected set { transitionPosition = value; }
        }

        float transitionPosition = 1;


        public float TransitionAlpha
        {
            get { return 1f - TransitionPosition; }
        }


        public ScreenState ScreenState
        {
            get { return screenState; }
            protected set { screenState = value; }
        }

        ScreenState screenState = ScreenState.TransitionOn;


        public bool IsExiting
        {
            get { return isExiting; }
            protected internal set { isExiting = value; }
        }

        bool isExiting = false;


        public bool IsActive
        {
            get
            {
                return !otherScreenHasFocus &&
                       (screenState == ScreenState.TransitionOn ||
                        screenState == ScreenState.Active);
            }
        }

        bool otherScreenHasFocus;


        public ScreenManager ScreenManager
        {
            get { return screenManager; }
            internal set { screenManager = value; }
        }

        ScreenManager screenManager;


        public virtual void LoadContent() 
        {
            if (content == null)
                content = new ContentManager(this.screenManager.Game.Services, "Content");

            TextureManager.LoadContent(Content);
        }


        public virtual void UnloadContent() { }


        public virtual void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                      bool coveredByOtherScreen)
        {
            this.otherScreenHasFocus = otherScreenHasFocus;

            if (isExiting)
            {
                
                screenState = ScreenState.TransitionOff;

                if (!UpdateTransition(gameTime, transitionOffTime, 1))
                {
                   
                    ScreenManager.RemoveScreen(this);
                }
            }
            else if (coveredByOtherScreen)
            {
                
                if (UpdateTransition(gameTime, transitionOffTime, 1))
                {
                    
                    screenState = ScreenState.TransitionOff;
                }
                else
                {
                    
                    screenState = ScreenState.Hidden;
                }
            }
            else
            {
                //Otherwise the screen should transition on and become active.
                if (UpdateTransition(gameTime, transitionOnTime, -1))
                {
                    // Still busy transitioning.
                    screenState = ScreenState.TransitionOn;
                }
                else
                {
                    //Transition finished!
                    screenState = ScreenState.Active;
                }
            }
        }

   
        bool UpdateTransition(GameTime gameTime, TimeSpan time, int direction)
        {
           
            float transitionDelta;

            if (time == TimeSpan.Zero)
                transitionDelta = 1;
            else
                transitionDelta = (float)(gameTime.ElapsedGameTime.TotalMilliseconds /
                                          time.TotalMilliseconds);

            // Update the transition position.
            transitionPosition += transitionDelta * direction;

            
            if (((direction < 0) && (transitionPosition <= 0)) ||
                ((direction > 0) && (transitionPosition >= 1)))
            {
                transitionPosition = MathHelper.Clamp(transitionPosition, 0, 1);
                return false;
            }

            return true;
        }


        public virtual void HandleInput( ) { }

        public virtual void Draw(GameTime gameTime) { }

        public void ExitScreen()
        {
            if (TransitionOffTime == TimeSpan.Zero)
            {               
                ScreenManager.RemoveScreen(this);
            }
            else
            {
                isExiting = true;
            }
        }

       

    }
}