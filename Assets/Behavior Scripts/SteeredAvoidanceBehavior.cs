using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Steered Avoidance")]
public class SteeredAvoidanceBehavior : FilterFlockBehavior
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;
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
        avoidanceMovement = Vector2.SmoothDamp(agent.transform.right, avoidanceMovement, ref currentVelocity, flock.maxSpeed * Time.deltaTime);
        
        return avoidanceMovement;
    }

}
