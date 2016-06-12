using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OctopusPathfinder.Manager;
using OctopusPathfinder.Screens;

namespace OctopusPathfinder.ObjectGame.AStar
{
    class BeamSearch
    {
        int iSteps = 0;

        List<int> iMatrixMap;

        int iRow = 0;
        int iColumn = 0;

        List<Node> listOpen = new List<Node>();
        List<Node> listClose = new List<Node>();
        List<Vector2> listRoad = new List<Vector2>();

        Vector2 cStart;
        Vector2 cGoal;

        List<Vector2> listAllRoad = new List<Vector2>();

        public BeamSearch(int[,] iArrayLevelMap, int iSteps, int iRow, int iColumn, Vector2 cStart, Vector2 cGoal)
        {

            iMatrixMap = new List<int>();

            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    iMatrixMap.Add(iArrayLevelMap[i, j]);
                }
            }

            this.iSteps = iSteps;

            this.iRow = iRow;
            this.iColumn = iColumn;

            this.cGoal = cGoal;
            this.cStart = cStart;

        }

        void FindNodeBase(ref Node currentNode)
        {            
            for (int i = listClose.Count - 1; i >= 0; i-- )
            {
                if ((currentNode.ID / 5) == listClose[i].ID)
                {                                        
                    currentNode = listClose[i];
                    
                    break;
                }
            }            
        }

        double Distance(Vector2 Vector21, Vector2 Vector2)
        {
            return (Math.Sqrt(Math.Pow((Vector21.X - Vector2.X), 2) + Math.Pow((Vector21.Y - Vector2.Y), 2)));
        }

        void Expansion(Node currentNode)
        {


            if (currentNode.Position.X - 1 >= 0
                && iMatrixMap[(int)(currentNode.Position.X - 1) *
                   iColumn + (int)currentNode.Position.Y] != 1)
            {
                Node nodeTemp = new Node(currentNode);

                nodeTemp.Step += 1;

                nodeTemp.Position = new Vector2(currentNode.Position.X - 1, currentNode.Position.Y);

                nodeTemp.Indicators = nodeTemp.Step + Distance(nodeTemp.Position, cGoal);

                nodeTemp.Base = currentNode.Position;

                nodeTemp.ID = currentNode.ID * 5 + 1;

                if (nodeTemp.Position != currentNode.Base
                    && nodeTemp.Indicators <= iSteps)
                {                                            
                    listOpen.Add(nodeTemp);                    
                }

            }


            if (currentNode.Position.X + 1 < iRow
                && iMatrixMap[(int)(currentNode.Position.X + 1) *
                iColumn + (int)currentNode.Position.Y] != 1)
            {
                Node nodeTemp = new Node(currentNode);

                nodeTemp.Position = new Vector2(currentNode.Position.X + 1, currentNode.Position.Y);

                nodeTemp.Step += 1;

                nodeTemp.Indicators = nodeTemp.Step + Distance(nodeTemp.Position, cGoal);

                nodeTemp.Base = currentNode.Position;

                nodeTemp.ID = currentNode.ID * 5 + 2;


                if (nodeTemp.Position != currentNode.Base
                    && nodeTemp.Indicators <= iSteps)
                {
                    listOpen.Add(nodeTemp);
                }
            }

            if (currentNode.Position.Y - 1 >= 0
                && iMatrixMap[(int)(currentNode.Position.X) *
                iColumn + (int)currentNode.Position.Y - 1] != 1)
            {
                Node nodeTemp = new Node(currentNode);

                nodeTemp.Position = new Vector2(currentNode.Position.X, currentNode.Position.Y - 1);

                nodeTemp.Step += 1;

                nodeTemp.Indicators = nodeTemp.Step + Distance(nodeTemp.Position, cGoal);

                nodeTemp.Base = currentNode.Position;

                nodeTemp.ID = currentNode.ID * 5 + 3;


                if (nodeTemp.Position != currentNode.Base
                    && nodeTemp.Indicators <= iSteps)
                {
                    listOpen.Add(nodeTemp);
                }
            }

            if (currentNode.Position.Y + 1 < iColumn
                && iMatrixMap[(int)(currentNode.Position.X) *
                iColumn + (int)currentNode.Position.Y + 1] != 1)
            {
                Node nodeTemp = new Node(currentNode);

                nodeTemp.Position = new Vector2(currentNode.Position.X, currentNode.Position.Y + 1);

                nodeTemp.Step += 1;

                nodeTemp.Indicators = nodeTemp.Step + Distance(nodeTemp.Position, cGoal);

                nodeTemp.Base = currentNode.Position;

                nodeTemp.ID = currentNode.ID * 5 + 4;

                if (nodeTemp.Position != currentNode.Base
                    && nodeTemp.Indicators <= iSteps)
                {
                    listOpen.Add(nodeTemp);
                }
            }

        }

        public void AllRoad()
        {
            listClose.Clear();
            listOpen.Clear();
            listAllRoad.Clear();
            listRoad.Clear();

            Node currentNode = new Node(cStart);

            listOpen.Add(currentNode);


            while (listOpen.Count != 0)
            {                

                currentNode = listOpen[listOpen.Count - 1];

                listOpen.RemoveAt(listOpen.Count - 1);

                Expansion(currentNode);

                

                listClose.Add(currentNode);


                if (currentNode.Position == cGoal)
                {
                    AddRoad(currentNode);
                }                

                
            }
        }

        

        void AddRoad(Node currentNode)
        {
            while (currentNode.Base != cStart)
            {
                listRoad.Add(currentNode.Base);
                FindNodeBase(ref currentNode);
            }

            listRoad.Reverse();
         
            listAllRoad.AddRange(listRoad);

            listRoad.Clear();

        }

        public void ListAllRoadClear()
        {
            listAllRoad.Clear();
        }

        public void Draw(SpriteBatch spriteBatch, int iRoad)
        {

            if (listAllRoad.Count > 0)
            {
                for (int i = iRoad * (iSteps - 1); i < (iSteps - 1) + (iRoad * (iSteps - 1)); i++)
                {
                    listRoad.Add(listAllRoad[i]);
                }

                for (int i = 0; i < listRoad.Count - 1; i++)
                {
                    Vector2 vTemp = listRoad[i + 1] - listRoad[i];

                    if (vTemp.X > 0)
                    {
                        spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.Jamb),
                                            MainGame.CoordinateTransfor((int)listRoad[i].X, (int)listRoad[i].Y) + new Vector2(17, 17), Color.White);
                    }
                    else if (vTemp.X < 0)
                    {
                        spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.Jamb),
                                            MainGame.CoordinateTransfor((int)listRoad[i].X, (int)listRoad[i].Y) - new Vector2(-17, 17), Color.White);
                    }
                    else if (vTemp.Y > 0)
                    {
                        spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.Crossroad),
                                            MainGame.CoordinateTransfor((int)listRoad[i].X, (int)listRoad[i].Y) + new Vector2(17, 17), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(TextureManager.GetTexture2D(ETexture2D.Crossroad),
                                            MainGame.CoordinateTransfor((int)listRoad[i].X, (int)listRoad[i].Y) - new Vector2(17, -17), Color.White);
                    }
                }

                spriteBatch.DrawString(TextureManager.GetSpriteFont(ESpriteFont.FontNormal),
                                (listAllRoad.Count / (iSteps - 1)).ToString(),  new Vector2(500f, 40f),
                                Color.Black, 0.0f,
                                new Vector2(
                                TextureManager.GetSpriteFont(ESpriteFont.FontMenu).MeasureString(iSteps.ToString()).X / 2,
                                TextureManager.GetSpriteFont(ESpriteFont.FontMenu).MeasureString(iSteps.ToString()).Y / 2),
                                1f, SpriteEffects.None, 0.0f);

                spriteBatch.DrawString(TextureManager.GetSpriteFont(ESpriteFont.FontNormal),
                                iRoad.ToString(), new Vector2(790, 40f),
                                Color.Black, 0.0f,
                                new Vector2(
                                TextureManager.GetSpriteFont(ESpriteFont.FontMenu).MeasureString(iRoad.ToString()).X / 2,
                                TextureManager.GetSpriteFont(ESpriteFont.FontMenu).MeasureString(iRoad.ToString()).Y / 2),
                                1f, SpriteEffects.None, 0.0f);

                listRoad.Clear();

            }
        }

        public int Roads
        {
            get
            {
                return listAllRoad.Count / (iSteps - 1);
            }
        }
    }
}
