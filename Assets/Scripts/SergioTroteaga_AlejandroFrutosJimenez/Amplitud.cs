using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts{

 public class Amplitud : IMind{


       private LinkedList<State> encontrados;
       private Queue<Node> porExplorar;
       private Stack<Node> solution;
       private GenerateMap map;
    
        //constructor en el que creas la lista,cola y pila, y en el que generas el mapa        
        public Amplitud(State start, GenerateMap map){
            
            encontrados = new LinkedList<State>();
            porExplorar = new Queue<Node>();
            solution = new Stack<Node>();

            this.map = map;
            encontrados.AddLast(start);
            porExplorar.Enqueue(new Node(start, null));
            Debug.Log("Comenzamos con amplitud ");

            while (porExplorar.Count > 0 && solution.Count <= 0){
                explore();

           }

            if (solution.Count > 0)
                Debug.Log("Hay ganador");
        }

        //Sirve para moverse a la siguiente direccion, y comprueba si has llegado a la meta,dandote las coordenadas
        public Move.MoveDirection GetNextMove(Vector2 currentPos, GenerateMap map){

            if (map.GetTile((int)currentPos.y,(int)currentPos.x)== GenerateMap.TileType.Goal){
                Debug.Log("Estas en el objetivo");
                return Move.MoveDirection.Up;
            }

            if(solution.Count > 0)
                return solution.Pop().state.from;
            
            Debug.Log("No hay solucion :(");
            return Move.MoveDirection.Up;

        }

        //expñora el mapa y comprueba si has ganado, en que estado estas y si la casilla es valida
        public void explore(){

            Node node = porExplorar.Dequeue();
            Debug.Log(node.state.position);

            if (isSolution(node)){
                
                Debug.Log("WIN WIN WIN");
                queueSolution(node);
                return;
            }

            State[] candidates = node.state.expand();

            foreach (State state in candidates){
                
                //Si sale de rango no exploramos
                if ( state.position.y >= map.rows  ||  state.position.x >= map.cols )
                    continue;
                //Si es un muro no exploramos
                if (map.GetTile((int)state.position.y, (int)state.position.x) == GenerateMap.TileType.Wall)
                    continue;
                
                if (isNew(state)){
                    
                    encontrados.AddLast(state);
                    porExplorar.Enqueue(new Node(state, node));
                }
            }

        }

        //comprueba si el estado ha cambiado
        private bool isNew(State state){
            foreach (State old in encontrados){
                if (old.position == state.position)
                    return false;
            }
            return true;
        }

        //comprueba si estas en la solucion
        public bool isSolution(Node node){
            if ( map.GetTile((int)node.state.position.y, (int)node.state.position.x) == GenerateMap.TileType.Goal)
                return true;
            return false;
        }

        //guarda la solucion en la cola, y crea un nodo para ello
        public void queueSolution(Node solutionNode){
            Node walker = solutionNode;

            while(walker!= null){
                solution.Push(walker);
                walker = walker.father;
            }

            solution.Pop();
        }

    }

}