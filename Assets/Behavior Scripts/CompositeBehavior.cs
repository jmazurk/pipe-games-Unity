using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehavior
{
    public FlockBehavior[] behaviors;
    public float[] weights;
    public float[] multipliers;
    public override Vector2 CalculateMovement(FlockAgent agent, List<Transform> surrounding, Flock flock)
    {
        if(weights.Length != behaviors.Length) {
            Debug.LogError("Behaviours array and weights array different size, while they should be equal, name: " + name, this);
            return Vector2.zero;
        }

        Vector2 resultantMovement = Vector2.zero;

        for(int i = 0; i < behaviors.Length; i++){
            Vector2 currentMovement = behaviors[i].CalculateMovement(agent, surrounding, flock);
            currentMovement *= multipliers[i];
            
            if(currentMovement.sqrMagnitude > weights[i] * weights[i]){
                currentMovement = currentMovement.normalized * weights[i];
            }

            resultantMovement += currentMovement;
        }

        return resultantMovement;
    }

    //   FOR MIGRATION PURPOSES ONLY   //
    /*public void update(int a){
        if(multipliers.Length < a){
            float[] newArr1 = new float [a];
            multipliers = newArr1;
        }

    }*/
}
