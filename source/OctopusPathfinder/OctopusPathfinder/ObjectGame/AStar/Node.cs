using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OctopusPathfinder.ObjectGame.AStar
{
    class Node : IComparable<Node>
    {
        Vector2 vPosition;
        Vector2 vBase;

        double fIndicators;
        int iStep;

        //bool bBlock = false;
        UInt64 id;


        public Node(Node node)
        {
            this.vPosition = node.vPosition;
            this.fIndicators = node.fIndicators;
            this.vBase = node.vBase;
            this.iStep = node.Step;
            //this.bBlock = node.bBlock;
            this.id = node.id;
        }

        public Node(Vector2 vStart)
        {
            this.vPosition = vStart;
            this.fIndicators = 0;
            this.vBase = new Vector2(-1, -1);
            this.iStep = 0;

            //bBlock = false;
            id = 1;
        }

        public int CompareTo(Node obj)
        {
            Node temp = obj;

            return this.Indicators.CompareTo(temp.Indicators);
        }

        public Vector2 Position
        {
            get
            {
                return vPosition;
            }
            set
            {
                vPosition = value;
            }
        }

        public double Indicators
        {
            get
            {
                return fIndicators;
            }
            set
            {
                fIndicators = value;
            }
        }

        public Vector2 Base
        {
            get
            {
                return vBase;
            }
            set
            {
                vBase = value;
            }
        }

        public int Step
        {
            get
            {
                return iStep;
            }
            set
            {
                iStep = value;
            }
        }

        public UInt64 ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

    }
}
