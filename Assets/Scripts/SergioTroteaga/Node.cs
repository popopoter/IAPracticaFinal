using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class Node
    {
        public State state;
        public Node father;
        public float fCost;
        public float acumuletedCost;
        public Node(State state, Node father)
        {
            this.state = state;
            this.father = father;
            

        }
        public Node(State state, Node father,int cost)
        {
            this.state = state;
            this.father = father;
            fCost = cost;


        }
        public override bool Equals(object obj)
        {
            Node other = (Node)obj;
            return state.position.Equals(other.state.position);
        }
        public override int GetHashCode()
        {
            int x = (int)state.position.x;
            int y = (int)state.position.y;
                //elegant 
            return x < y ? y * y + x : x * x + x + y; ;
           
        }


    }

}
