using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Steered Avoidance Realistic")]
public class SteeredAvoidanceRealisticBehavior : FilterFlockBehavior
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
            if(flock.SquareAvoidanceRadius < Vector2.SqrMagnitude(agent.transform.position - obj.position)) continue;

            totalAvoidanceWeight += 1;
            float distancePercantage = Vector3.SqrMagnitude((agent.transform.position - obj.position)) / flock.SquareAvoidanceRadius;
            avoidanceMovement += (Vector2) (agent.transform.position - obj.position)/Mathf.Sqrt(distancePercantage);
        }

        if(totalAvoidanceWeight == 0){
            return Vector2.zero;
        }

        avoidanceMovement /= totalAvoidanceWeight;
        ///avoidanceMovement += (Vector2) agent.transform.position;
        avoidanceMovement = 2 * Vector2.SmoothDamp(agent.transform.right, avoidanceMovement, ref currentVelocity, agentSmoothTime);

        return avoidanceMovement;
    }

}
