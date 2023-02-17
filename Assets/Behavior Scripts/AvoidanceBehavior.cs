using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilterFlockBehavior
{
    public override Vector2 CalculateMovement(FlockAgent agent, List<Transform> surrounding, Flock flock)
    {
        if(surrounding.Count == 0) 
            return Vector2.zero; 

        Vector2 avoidanceMovement = Vector2.zero;
        int totalAvoidanceWeight = 0;
        List<Transform> filteredSurrounding = (filter == null) ? surrounding : filter.Filter(agent, surrounding); 
        foreach(Transform obj in filteredSurrounding){
            if(flock.SquareAvoidanceRadius <= Vector2.SqrMagnitude(agent.transform.position - obj.position)) continue;

            totalAvoidanceWeight += 1;
            avoidanceMovement += (Vector2) (agent.transform.position - obj.position);
        }

        if(totalAvoidanceWeight == 0){
            return Vector2.zero;
        }

        avoidanceMovement /= totalAvoidanceWeight;
        //avoidanceMovement += (Vector2) agent.transform.position;
        
        return avoidanceMovement;
    }

}
