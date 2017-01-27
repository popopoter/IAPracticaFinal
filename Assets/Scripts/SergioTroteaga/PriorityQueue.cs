using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.SergioTroteaga
{
    class PriorityQueue 
    {
        private List< Node> list;
        public PriorityQueue(){

            list = new  List<Node>();

            }
        public bool isEmpty()
        {
            if (list.Count == 0)
            {
                return true;
            }
            return false;
        }
        public Node pop()
        {
            Node min = null;
            foreach (Node node in list)
            {
                if(min == null)
                {
                    min = node;
                }
                if(min.fCost > node.fCost)
                {
                    min = node;
                }
            }
            list.Remove(min);
            return min;
        }
        public void push(Node node)
        {
            
            list.Add(node);
        }
        public bool isBetter(Node node)             
        {
            int oldIndx = list.IndexOf(node);
            if (list[oldIndx].acumuletedCost > node.acumuletedCost)
            {
                list[oldIndx].acumuletedCost = node.acumuletedCost;
                list[oldIndx].father = node.father;
                list[oldIndx].state.from = node.state.from;
               
            }
                
            return list.Contains(node);
        }

    }
}
