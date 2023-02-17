using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Layer Physics Filter")]
public class LayerPhysiscFilter : ContextFilter
{
    public LayerMask layerMask;
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filteredList = new List<Transform>();

        foreach(Transform obj in original){
            if( (layerMask | (1 << obj.gameObject.layer) ) != layerMask) continue;
            filteredList.Add(obj);
        }

        return filteredList;
    }
}
