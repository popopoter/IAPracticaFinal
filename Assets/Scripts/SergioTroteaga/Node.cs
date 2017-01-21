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

        public Node(State state, Node father)
        {
            this.state = state;
            this.father = father;

        }
   
       

    }

}
