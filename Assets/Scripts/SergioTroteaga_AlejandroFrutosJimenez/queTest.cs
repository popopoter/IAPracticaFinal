using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class queTest : MonoBehaviour
    {
        void Start()
        {
            PriorityQueue queue = new PriorityQueue();
            Node a = new Node(new State(0, 0), null);
            a.fCost = 9;
            Node b = new Node(new State(0, 0), null);
            b.fCost = 11;
            Node c = new Node(new State(0, 0), null);
            c.fCost = 6;
            

            queue.push(a);
            print(queue.list.Count);
            queue.push(b);
            print(queue.list.Count);
            queue.push(c);
            print(queue.list.Count);

            print(queue.pop().fCost);
            print(queue.list.Count);
            print(queue.pop().fCost);
            print(queue.list.Count);
            print(queue.pop().fCost);
            print(queue.list.Count);


        }
    }
}
