using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OctopusPathfinder.Effects
{
    class Animation
    {
        protected Texture2D ttAnimation;
        protected List<Rectangle> listFrames = new List<Rectangle>();

        private int iWidthFrame = 0;
        private int iHeightFrame = 0;
        protected int iIndexFrame = 0;

        protected int iMiliseconPerFrame = 0;
        protected int iCounter = 0;

        private int iRow = 0;
        private int iColumn = 0;
        protected int iNumberFrame = 0;

        protected bool bPause = false;


        private List<int> listIndex = new List<int>();

        public Animation(Texture2D ttAnimation, int iRow, int iColumn,
            int iNumberFrame, int iMiliseconPerFrame, bool bPause)
        {
            this.ttAnimation = ttAnimation;

            iHeightFrame = ttAnimation.Height / iRow;
            iWidthFrame = ttAnimation.Width / iColumn;

            this.iRow = iRow;
            this.iColumn = iColumn;
            this.iNumberFrame = iNumberFrame;
            this.iMiliseconPerFrame = iMiliseconPerFrame;
            this.bPause = bPause;

            CaculateRectangle();
        }


        public virtual void Update(GameTime gameTime)
        {
            iCounter += gameTime.ElapsedGameTime.Milliseconds;

            if (iCounter <= iMiliseconPerFrame)
            {
                return;
            }

            iCounter -= iMiliseconPerFrame;


            if (bPause == true && (iIndexFrame) == iNumberFrame - 1)
            {
                iIndexFrame = iNumberFrame - 1;
            }
            else
            {
                iIndexFrame = (iIndexFrame + 1) % (iNumberFrame);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 cPositonTexture)
        {
            spriteBatch.Draw(ttAnimation, cPositonTexture, listFrames[iIndexFrame], Color.White);

        }

        public void CaculateRectangle()
        {
            int iTemp = 1;
            for (int i = 0; (i < iRow) && (iTemp <= iNumberFrame); i++)
            {
                for (int j = 0; (j < iColumn) && (iTemp <= iNumberFrame); j++)
                {
                    Rectangle tempRectangle = new Rectangle(j * iWidthFrame, i * iHeightFrame,
                                                    iWidthFrame, iHeightFrame);
                    listFrames.Add(tempRectangle);
                    iTemp++;
                }
            }


        }
    }
}