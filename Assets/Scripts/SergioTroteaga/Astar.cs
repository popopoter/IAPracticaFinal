
using Assets.Scripts.SergioTroteaga;
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


       private LinkedList<State> cerrados;
       private PriorityQueue porExplorar;
       private Stack<Node> solution;
       private GenerateMap map;
       private Vector2 final;
       
            public Astar(Vector2 start, GenerateMap map,Vector2 final)
        {
            
            cerrados = new LinkedList<State>();
            porExplorar = new PriorityQueue();
            solution = new Stack<Node>();

            this.map = map;
            this.final = final;
            State initial = new State((int)start.x, (int)start.y);
            Node first = new Node(initial, null);
            first.fCost = heuristic(first.state.position);
            first.acumuletedCost = 0;
            porExplorar.push(first);

            while ( porExplorar.isEmpty() || solution.Count <= 0)
            {
               

                explore();

            }
            if(solution.Count > 0)
            {
                Debug.Log("Hay ganador");
            }

        }
    
    public void explore()
        {
            Node node = porExplorar.pop();
            cerrados.AddLast(node.state);
            Debug.Log(node.state.position);

            if (isSolution(node))
            {
                Debug.Log("SOLUTIOOON");
                queueSolution(node);
                return;

            }

            State[] candidates = node.state.expand();
            foreach (State state in candidates)
            {
                
                //Si sale de rango no exploramos
                if ( state.position.y >= map.rows  ||  state.position.x >= map.cols )
                    continue;
                //Si es un muro no exploramos
                if (map.GetTile((int)state.position.y, (int)state.position.x) == GenerateMap.TileType.Wall)
                {

                    continue;
                }
                if (!isClose(state))
                {
                    continue;
                    
                }
                Node newNode = new Node(state, node);
                newNode.acumuletedCost = node.acumuletedCost + 1;
                newNode.fCost = newNode.acumuletedCost + heuristic(newNode.state.position);
               if (porExplorar.contains(node))
                    {
                    if(porExplorar.lastVisited.Value.acumuletedCost > node.acumuletedCost)
                    {
                        porExplorar.lastVisited.Value = node;
                        
                    }

                    }
                else
                {
                    porExplorar.push(newNode);

                }   

                
            }
        }
        public Move.MoveDirection GetNextMove(Vector2 currentPos, GenerateMap map)
        {
            return solution.Pop().state.from;
        }
       
         public float heuristic(Vector2 a)
        {
            return Math.Abs(a.x - final.x) + Math.Abs(a.y - final.y);
        } 
        private bool isClose(State state)
        {
            foreach (State old in cerrados)
            {
                if (old.position == state.position)
                    return false;
            }
            return true;
        }

        public bool isSolution(Node node)
        {
            if(node.state.position.Equals(final))
                return true;
            
            
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
         
