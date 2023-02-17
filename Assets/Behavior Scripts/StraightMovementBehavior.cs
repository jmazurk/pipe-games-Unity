using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Straight Movement")]
public class StraightMovementBehavior : FlockBehavior
{
    public override Vector2 CalculateMovement(FlockAgent agent, List<Transform> surrounding, Flock flock)
    {
        //return (Vector2)agent.currentVelocity;
        return agent.transform.right; //up
    }

}
