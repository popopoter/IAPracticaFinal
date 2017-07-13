
using UnityEngine;
using System.Collections.Generic;

public class Strips
{
    //funcion que te devuelve una cola con todos los objetivos y que usa la funcion recursiva de abajo para almacenarlos
    public Queue<Vector2> planning(string tag)
    {
        GameObject finalGoal = GameObject.FindGameObjectWithTag(tag);
        Queue<Vector2> plan = new Queue<Vector2>();
        resolvePrecondicion(finalGoal, plan, new List<Vector2>());

        return plan;
    }

    //funcion que almacena las precondicones desde las que tenga goal asi como los demas items y las va añadiendo a la cola, y a la lista las resueltas
    private void resolvePrecondicion(GameObject goal, Queue<Vector2> plan, List<Vector2> precondicionesResueltas)
    {

        List<GameObject> precondiciones = goal.GetComponent<ItemLogic>().Required;
        foreach (GameObject precondicion in precondiciones)
        {
            if (precondicionesResueltas.Contains(precondicion.transform.position))
            {
                continue;
            }
            else
            {
                resolvePrecondicion(precondicion, plan, precondicionesResueltas);
            }

        }

        precondicionesResueltas.Add(goal.transform.position);
        plan.Enqueue(goal.transform.position);

    }
}