using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;


public class AMind : IMind
{
    private Queue<Move.MoveDirection> movements;
    private Queue<Vector2> objectives;
    private Astar AP;
    private Strips strips;//la clase que se encarga de strips
    Vector2 next;
    
    //constructor que declara la cola y los elementos que necesitamos para llegar al goal, llamara a strips para conseguirlo
    public AMind()
    {
        
        movements = new Queue<Move.MoveDirection>();
        next = Vector2.zero;
        AP = null;
        strips = new Strips();
        objectives = strips.planning("Goal");
        /*
        foreach (Vector2 turu in objectives)
        {
            Debug.Log(turu);
        }*/
    }

    // se encarga de hacer el siguiente movimiento hasta que llegue a la meta y deje de moverse
    public Move.MoveDirection GetNextMove(Vector2 currentPos, GenerateMap map)
    {
        
        if (AP == null || currentPos == next)
        {
            if (objectives.Count < 1)
            {
                Debug.Log("No tengo nada que hacer");
                return Move.MoveDirection.Up;
            }
            next = objectives.Dequeue();
            AP = new Astar(currentPos, map, next);
        }


        return AP.GetNextMove(currentPos, map);

    }
}