using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/SteeredCohesion")]
public class SteeredCohesionBehavior : FilterFlockBehavior
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;
    public override Vector2 CalculateMovement(FlockAgent agent, List<Transform> surrounding, Flock flock)
    {
        if(surrounding.Count == 0) return Vector2.zero;


        //arithmetic avarage off all surrounding objects's positions
        Vector2 cohesionMovement = Vector2.zero;

        List<Transform> filteredSurrounding = (filter == null) ? surrounding : filter.Filter(agent, surrounding); 
        foreach(Transform obj in filteredSurrounding){
            cohesionMovement += (Vector2) obj.position;
        }
        cohesionMovement /= surrounding.Count;

        //getting the difference between our position and avarage position
        //cohesionMovement -= (Vector2) agent.transform.position;
        cohesionMovement = Vector2.SmoothDamp((Vector2) agent.transform.position, cohesionMovement, ref currentVelocity, agentSmoothTime, flock.maxSpeed);
        //cohesionMovement = Vector2.SmoothDamp(agent.transform.right, cohesionMovement, ref currentVelocity, flock.maxSpeed * Time.deltaTime);

        return cohesionMovement;
    }

}
