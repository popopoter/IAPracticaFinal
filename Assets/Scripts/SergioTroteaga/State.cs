using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
     public class State
    {
       public  Vector2 position;
        public Move.MoveDirection from;

    public State(Vector2 vec,Move.MoveDirection from)
        {
            position = vec;
            this.from = from;
        }
    public State(int x,int y)
        {
             position = new Vector2(x,y);
        }
    


   public State[] expand()
        {


            Queue<State> queue = new Queue<State>();

            State candidate;
            candidate = new State(position + Vector2.up, Move.MoveDirection.Up);
            if (candidate.isLegit())
                queue.Enqueue(candidate);
            candidate = new State(position + Vector2.down, Move.MoveDirection.Down);
            if (candidate.isLegit())
                queue.Enqueue(candidate);
            candidate = new State(position + Vector2.right,Move.MoveDirection.Right);
            if (candidate.isLegit())
                queue.Enqueue(candidate);

            candidate = new State(position + Vector2.left, Move.MoveDirection.Left);
            if (candidate.isLegit())
                queue.Enqueue(candidate);


            return queue.ToArray();
        }

        public bool isLegit()
        {
            if ( position.x < 0 || position.y < 0)
                return false;
            return true;
        }
    }


}