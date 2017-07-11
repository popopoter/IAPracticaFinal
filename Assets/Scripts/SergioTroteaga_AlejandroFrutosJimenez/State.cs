using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts{

    public class State{
        public  Vector2 position;
        public Move.MoveDirection from;
    
        //contructor que ve la posicion en la que está y te permite moverte en una de las cuatro direcciones
        public State(Vector2 vec,Move.MoveDirection from){
                position = vec;
                this.from = from;
            }

        //constructor que asigna una posicion
        public State(int x,int y){
                 position = new Vector2(x,y);
            }

        //Funcion que sirve para decir a que sitios puede espandir y si son validos o no
        public State[] expand(){

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

        //comprueba si la posicion es valida o no 
        public bool isLegit(){
                if ( position.x < 0 || position.y < 0)
                    return false;
                return true;
        }
    }
}