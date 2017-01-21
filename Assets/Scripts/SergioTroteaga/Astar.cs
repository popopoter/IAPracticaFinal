            
using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Astar : IMind
    {


       private LinkedList<State> encontrados;
       private SortedList<float,Node> porExplorar;
       private Stack<Node> solution;
        private GenerateMap map;
            public Astar(State start, GenerateMap map)
        {
            
            encontrados = new LinkedList<State>();
            porExplorar = new SortedList<float, Node>();
            solution = new Stack<Node>();

            this.map = map;
            encontrados.AddLast(start);
            porExplorar.Add(0,new Node(start, null));
            Debug.Log("WOn ");
            while (porExplorar.Count > 0 || solution.Count <= 0)
            {
               

                explore();

            }
            if(solution.Count > 0)
            {
                Debug.Log("Hay ganador");
            }

        }
        public Move.MoveDirection GetNextMove(Vector2 currentPos, GenerateMap map)
        {

            return solution.Pop().state.from;



        }
    
    public void explore()
        {
            Node node = porExplorar.Values[0];
            porExplorar.Remove(porExplorar.Keys[0]);

            Debug.Log(node.state.position);

            if (isSolution(node))
            {
                
                Debug.Log("WIN WIN WIN");
                queueSolution(node);
                return;

            }

            State[] candidates = node.state.expand();
            foreach (State state in candidates)
            {
                
                //Si sale de rango no exploramos
                if ( state.position.y >= map.rows  ||  state.position.x >= map.cols )
                    break;
                //Si es un muro no exploramos
                if (map.GetTile((int)state.position.y, (int)state.position.x) == GenerateMap.TileType.Wall)
                {
                   
                    break;
                }
                if (isNew(state))
                {
                    
                    encontrados.AddLast(state);
                    porExplorar.Add(heuristic(state.position,new State(13,7).position),new Node(state, node));

                    Debug.Log("As");
                }
            }
        }
        static public float heuristic(Vector2 a, Vector2 b)
        {


            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        } 
        private bool isNew(State state)
        {
            foreach (State old in encontrados)
            {
                if (old.position == state.position)
                    return false;
            }
            return true;
        }

        public bool isSolution(Node node)
        {
            if ( map.GetTile((int)node.state.position.y, (int)node.state.position.x) == GenerateMap.TileType.Goal)
            {
               
                return true;


            }
            
            return false;
        }
        public void queueSolution(Node solutionNode)
        {
            Node walker = solutionNode;

            while(walker!= null)
            {
                solution.Push(walker);
                walker = walker.father;
            }
            solution.Pop();
        }

    }
}
         
