using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FilterFlockBehavior
{
    public override Vector2 CalculateMovement(FlockAgent agent, List<Transform> surrounding, Flock flock)
    {
        if(surrounding.Count == 0) 
            return agent.transform.right; //up

        Vector2 alignmentMovement = Vector2.zero;
        List<Transform> filteredSurrounding = (filter == null) ? surrounding : filter.Filter(agent, surrounding); 
        foreach(Transform obj in filteredSurrounding){
            alignmentMovement += (Vector2) obj.transform.right; //up
        }
        //cohesionMovement += (Vector2) agent.transform.right; //up
        //alignmentMovement += (Vector2) agent.transform.right; // if you want to include yourself into avarage
        alignmentMovement /= surrounding.Count;

        return alignmentMovement;
    }

}
