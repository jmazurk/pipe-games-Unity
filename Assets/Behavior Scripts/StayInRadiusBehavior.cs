using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Stay in Radius")]
public class StayInRadiusBehavior : FlockBehavior
{
    public Vector2 center = Vector2.zero;
    public float radius = 15f;
    public override Vector2 CalculateMovement(FlockAgent agent, List<Transform> surrounding, Flock flock)
    {
        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        float offsetPercentage = centerOffset.magnitude / radius;
        if(offsetPercentage < 0.9f){
            return Vector2.zero;
        }

        return centerOffset * offsetPercentage * offsetPercentage;
        throw new System.NotImplementedException();
    }
}
