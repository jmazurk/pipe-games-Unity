using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FilterFlockBehavior
{
    public override Vector2 CalculateMovement(FlockAgent agent, List<Transform> surrounding, Flock flock)
    {
        if(surrounding.Count == 0) return Vector2.zero;

        Vector2 cohesionMovement = Vector2.zero;
        List<Transform> filteredSurrounding = (filter == null) ? surrounding : filter.Filter(agent, surrounding); 
        foreach(Transform obj in filteredSurrounding){
            cohesionMovement += (Vector2) obj.position;
        }
        cohesionMovement /= surrounding.Count;

        cohesionMovement -= (Vector2) agent.transform.position;

        return cohesionMovement;
    }

}
