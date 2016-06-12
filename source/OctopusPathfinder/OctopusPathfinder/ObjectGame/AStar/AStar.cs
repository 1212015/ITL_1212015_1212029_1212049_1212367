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
    class AStar
    {
        List<int> iMatrixMap;

        int iRow = 0;
        int iColumn = 0;

        List<Node> listOpen = new List<Node>();
        List<Node> listClose = new List<Node>();
        List<Vector2> listRoad = new List<Vector2>();

        Vector2 cStart;
        Vector2 cGoal;

        public AStar(Vector2 cStart, Vector2 cGoal, int[,] iArrayLevelMap)
        {

            iMatrixMap = new List<int>();

            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    iMatrixMap.Add(iArrayLevelMap[i, j]);
                }
            }
            
            this.cStart = cStart;
            this.cGoal = cGoal;

            iColumn = 16;
            iRow = 11;

            //Pathfinder();

        }

        public void Pathfinder(Vector2 cStart, Vector2 cGoal)
        {
            this.cStart = cStart;
            this.cGoal = cGoal;

            Node currentNode = new Node(cStart);

            listOpen.Clear();
            listClose.Clear();
            listRoad.Clear();

            while (currentNode.Position != cGoal)
            {
                Expansion(currentNode);
                listClose.Add(currentNode);
                currentNode = listOpen[0];
                listOpen.RemoveAt(0);
            }

            while (currentNode.Base != cStart)
            {
                listRoad.Add(currentNode.Base);
                FindNodeBase(ref currentNode);
            } 

            //listRoad.Add(vStart);

            listRoad.Reverse();
        }

        void FindNodeBase(ref Node currentNode)
        {
            int i = 0;

            while (currentNode.Base != listClose[i].Position)
            {
                i++;
            }

            currentNode = listClose[i];
        }

        bool IsHavedClose(Node node)
        {

            for (int i = 0; i < listClose.Count; i++)
            {
                if (node.Position == listClose[i].Position)
                {
                    if (listClose[i].Step > node.Step)
                    {
                        listOpen.Add(listOpen[i]);
                        listClose.RemoveAt(i);
                    }

                    return true;
                }
            }

            return false;
        }

        bool IsHavedOpen(Node node)
        {
            for (int i = 0; i < listOpen.Count; i++)
            {
                if (node.Position == listOpen[i].Position)
                {
                    if (listOpen[i].Step > node.Step)
                    {
                        listOpen[i].Step = node.Step;
                        listOpen[i].Indicators = node.Indicators;
                        listOpen[i].Base = node.Base;
                    }

                    return true;
                }
            }

            return false;
        }

        void Expansion(Node currentNode)
        {
            //int i = (int)currentNode.Position.X - 1;
            //int j = iMatrixMap[(int)(currentNode.Position.X - 1) *
            //                                                        iColumn + (int)currentNode.Position.Y];

            if (currentNode.Position.X - 1 >= 0 && iMatrixMap[(int)(currentNode.Position.X - 1) *
                                                                    iColumn + (int)currentNode.Position.Y] != 1)
            {
                Node nodeTemp = new Node(currentNode);

                nodeTemp.Step += 1;

                nodeTemp.Position = new Vector2(currentNode.Position.X - 1, currentNode.Position.Y);

                nodeTemp.Indicators = nodeTemp.Step + Distance(nodeTemp.Position, cGoal);

                nodeTemp.Base = currentNode.Position;

                if (!IsHavedOpen(nodeTemp) && !IsHavedClose(nodeTemp))
                {
                    listOpen.Add(nodeTemp);
                }

            }

            //i = (int)currentNode.Position.X + 1;
            //j = iMatrixMap[(int)(currentNode.Position.X + 1) *
            //                                                        iColumn + (int)currentNode.Position.Y];

            if (currentNode.Position.X + 1 < iRow && iMatrixMap[(int)(currentNode.Position.X + 1) *
                                                                    iColumn + (int)currentNode.Position.Y] != 1)
            {
                //int i = iMatrixMap[(int)(currentNode.Position.X + 1) *
                //                                                      (int)currentNode.Position.Y +
                //                                                      (int)currentNode.Position.Y];
                Node nodeTemp = new Node(currentNode);

                nodeTemp.Position = new Vector2(currentNode.Position.X + 1, currentNode.Position.Y);

                nodeTemp.Step += 1;

                nodeTemp.Indicators = nodeTemp.Step + Distance(nodeTemp.Position, cGoal);

                nodeTemp.Base = currentNode.Position;

                if (!IsHavedOpen(nodeTemp) && !IsHavedClose(nodeTemp))
                {
                    listOpen.Add(nodeTemp);
                }
            }

            //i = (int)currentNode.Position.Y - 1;
            //j = iMatrixMap[(int)(currentNode.Position.X) *
            //                                                        iColumn + (int)currentNode.Position.Y - 1];

            if (currentNode.Position.Y - 1 >= 0 && iMatrixMap[(int)(currentNode.Position.X) *
                                                                    iColumn + (int)currentNode.Position.Y - 1] != 1)
            {
                Node nodeTemp = new Node(currentNode);

                nodeTemp.Position = new Vector2(currentNode.Position.X, currentNode.Position.Y - 1);

                nodeTemp.Step += 1;

                nodeTemp.Indicators = nodeTemp.Step + Distance(nodeTemp.Position, cGoal);

                nodeTemp.Base = currentNode.Position;

                if (!IsHavedOpen(nodeTemp) && !IsHavedClose(nodeTemp))
                {
                    listOpen.Add(nodeTemp);
                }
            }            

            if (currentNode.Position.Y + 1 < iColumn && iMatrixMap[(int)(currentNode.Position.X) *
                                                                    iColumn + (int)currentNode.Position.Y + 1] != 1)
            {
                Node nodeTemp = new Node(currentNode);

                nodeTemp.Position = new Vector2(currentNode.Position.X, currentNode.Position.Y + 1);

                nodeTemp.Step += 1;

                nodeTemp.Indicators = nodeTemp.Step + Distance(nodeTemp.Position, cGoal);

                nodeTemp.Base = currentNode.Position;

                if (!IsHavedOpen(nodeTemp) && !IsHavedClose(nodeTemp))
                {
                    listOpen.Add(nodeTemp);
                }
            }

            listOpen.Sort();

        }

        double Distance(Vector2 Vector21, Vector2 Vector2)
        {
            return (Math.Sqrt(Math.Pow((Vector21.X - Vector2.X), 2) + Math.Pow((Vector21.Y - Vector2.Y), 2)));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 cCircle)
        {
            if (listRoad.Count != 0)
            {

                

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

               
            }
        }

       


        public int Steps
        {
            get
            {
                return listRoad.Count;
            }
        }

    }
}
