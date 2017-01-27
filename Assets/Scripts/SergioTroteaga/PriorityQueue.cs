using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.SergioTroteaga
{
    public class PriorityQueue 
    {
        public LinkedListNode<Node> lastVisited;
        public LinkedList <Node> list;
        public PriorityQueue(){

            list = new LinkedList<Node>();

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
            Node min = list.First.Value;

         
            list.RemoveFirst();
            return min;
        }
        public void push(Node node)
        {
            if (list.Count == 0)
            {
                list.AddFirst(node);
                return;
            }
            LinkedListNode<Node> OldNode = list.First;
            while(OldNode != null)
            {
                if (node.fCost < OldNode.Value.fCost)
                {
                    list.AddBefore(OldNode, node);
                    return;
                }
                OldNode = OldNode.Next;
            }
            list.AddLast(node);
        }
        public bool contains(Node node)
        {
            LinkedListNode<Node> oldNode = list.First;
            while (oldNode != null)
            {
                if (oldNode.Value.Equals(node))
                {
                    lastVisited = oldNode;
                    return true;
                }

                oldNode = oldNode.Next;
            }

            return false;
        }
    }
}
