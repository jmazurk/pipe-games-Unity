using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FakeJetScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public Quaternion currentRotation;
    public Quaternion desiredRotation;
    public Vector2 desiredDirection;
    public float desiredAngle;
    public float speed;
    public float rotationSpeed;
    void Start()
    {
        myRigidBody.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        desiredDirection.Normalize();
        
        despawner();
    }

    void despawner(){
        if(Mathf.Abs(transform.position.x) > 13) {
            Destroy(gameObject);
            return;
        }
        if(Mathf.Abs(transform.position.y) > 9) {
            Destroy(gameObject);
            return;
        }
    }

    //void set
}
