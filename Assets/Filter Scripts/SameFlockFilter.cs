using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Same Flock Filter")]
public class SameFlockFilter : ContextFilter
{
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filteredList = new List<Transform>();

        foreach(Transform obj in original){
            FlockAgent objAgent = obj.GetComponent<FlockAgent>();
            if(objAgent == null || objAgent.AgentFlock != agent.AgentFlock) continue;

            filteredList.Add(obj);
        }

        return filteredList;
    }
}
