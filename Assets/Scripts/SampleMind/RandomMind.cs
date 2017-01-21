using UnityEngine;
using System.Collections;

using Assets.Scripts;

public class RandomMind : IMind {
    public RandomMind()
    {
        
    }
    public Move.MoveDirection GetNextMove(Vector2 currentPos, GenerateMap map)
    {
        
        return Move.MoveDirection.Up;
    }
}
