using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    Flock agentFlock;
    public Flock AgentFlock{ get {return agentFlock; } }
    Collider2D agentCollider;
    public Vector2 currentVelocity;
    public Collider2D AgentCollider {get { return agentCollider; } }
    float angle = 0f;
    float currentVelocitySmoothDamp;
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void initialize(Flock flock){
        agentFlock = flock;
    }

    public void setAngle(float newAngle){
        angle = newAngle;
    }

    public void Move(Vector2 velocity){
        float targetAngle = Vector2.SignedAngle(transform.right, velocity);
        angle = Mathf.SmoothDampAngle(angle,angle + targetAngle,ref currentVelocitySmoothDamp, agentFlock.agentSmoothTime, agentFlock.maxTurnSpeed);
        
        float velocityMagnitude = velocity.magnitude;
        velocity.x = velocityMagnitude * Mathf.Cos(angle);
        velocity.y = velocityMagnitude * Mathf.Sin(angle);
        currentVelocity = velocity;
        transform.right = (Vector3) velocity;
        transform.position += (Vector3) velocity * Time.deltaTime;
    }
}
