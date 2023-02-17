using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance Realistic")]
public class AvoidanceRealisticBehavior : FilterFlockBehavior
{
    public override Vector2 CalculateMovement(FlockAgent agent, List<Transform> surrounding, Flock flock)
    {
        if(surrounding.Count == 0) 
            return Vector2.zero; 

        Vector2 avoidanceMovement = Vector2.zero;
        int totalAvoidanceWeight = 0;
        List<Transform> filteredSurrounding = (filter == null) ? surrounding : filter.Filter(agent, surrounding); 
        foreach(Transform obj in filteredSurrounding){
            Vector3 closestPoint = obj.gameObject.GetComponent<Collider2D>().ClosestPoint((Vector2) agent.transform.position);
            if(closestPoint == agent.transform.position) closestPoint = obj.position;
            //Vector3 closestPoint = obj.transform.position;

            totalAvoidanceWeight += 1;
            //float distancePercantage = Vector3.SqrMagnitude((agent.transform.position - obj.position)) / flock.SquareAvoidanceRadius;
            float distancePercantage = Vector3.SqrMagnitude((agent.transform.position - (Vector3)closestPoint)) / flock.SquareAvoidanceRadius;
            distancePercantage = Mathf.Sqrt(distancePercantage);
            //avoidanceMovement += (Vector2) (agent.transform.position - obj.position) / distancePercantage;
            avoidanceMovement += (Vector2) (agent.transform.position - (Vector3)closestPoint) / distancePercantage;
        }

        if(totalAvoidanceWeight == 0){
            return Vector2.zero;
        }

        avoidanceMovement /= totalAvoidanceWeight;
        ///avoidanceMovement += (Vector2) agent.transform.position;
        return avoidanceMovement;
    }

}
