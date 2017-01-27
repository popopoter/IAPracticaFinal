using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.SergioTroteaga
{
    class queTest : MonoBehaviour
    {
        void Start()
        {
            PriorityQueue queue = new PriorityQueue();
            Node a = new Node(new State(0, 0), null);
            a.fCost = 15;
            Node b = new Node(new State(0, 0), null);
            b.fCost = 11;
            print(queue.isEmpty());

            queue.push(a);
            queue.push(b);
            print(queue.isEmpty());
            print(queue.pop().fCost);
            print(queue.pop().fCost);

        }
    }
}
