

namespace Assets.Scripts{

    public  class Node{
            
        //variables necesarias para el algoritmo de amplitud y el de A* de cada nodo
        public State state;
        public Node father;
        public float fCost;
        public float acumuletedCost;


        //constructor con el estado y con el nodo padre
        public Node(State state, Node father){

            this.state = state;
            this.father = father;
        }

        // sirve para devolver la posicion de un objeto
        public override bool Equals(object obj){

            Node other = (Node)obj;
            return state.position.Equals(other.state.position);
        }
    }
}
