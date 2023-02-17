using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetScript : MonoBehaviour
{

    public Rigidbody2D myRigidBody2D;
    public LogicScript logicScript;
    public float upThrustSpeed = 8;
    public float horizontalThrust = 10;
    public float boostSpeed = 15;
    private float myGravityScale = 2;
    private float horizontalForce = 0;
    private float scale = 1;
    private float gravityOffTimer;
    public float boostDuration = 0.3f;
    private bool isBoosted = false;
    // Start is called before the first frame update
    void Start()
    {
        //scale = 1;
        //myGravityScale = 2 * scale;
        //upThrustSpeed = 8 * scale;
        //horizontalThrust = 10 * scale;
        //boostSpeed = 15;

        myRigidBody2D.gravityScale = myGravityScale * scale; 
        myRigidBody2D.drag = 1f * scale;

        myRigidBody2D.freezeRotation = true;
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isBoosted)
        {
            myRigidBody2D.velocity = Vector2.up *  upThrustSpeed + Vector2.right * myRigidBody2D.velocity;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidBody2D.velocity = Vector2.right * boostSpeed;
            isBoosted = true;
            gravityOffTimer = 0;
            myRigidBody2D.gravityScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            //myRigidBody2D.velocity = Vector2.left *  horizontalThrustSpeed + Vector2.up * myRigidBody2D.velocity;
            horizontalForce -= 2 * horizontalThrust;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            //myRigidBody2D.velocity = Vector2.up * myRigidBody2D.velocity;
            horizontalForce += 2 * horizontalThrust;
        }

         if (Input.GetKeyDown(KeyCode.D))
        {
            //myRigidBody2D.velocity = Vector2.left *  horizontalThrustSpeed + Vector2.up * myRigidBody2D.velocity;
            horizontalForce += horizontalThrust;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            //myRigidBody2D.velocity = Vector2.up * myRigidBody2D.velocity;
            horizontalForce -= horizontalThrust;
        }

        GetComponent<ConstantForce2D>().force = Vector2.right * horizontalForce + Vector2.up *  GetComponent<ConstantForce2D>().force;

        if(!isBoosted) return;

        gravityOffTimer+= Time.deltaTime;
       if(gravityOffTimer >= boostDuration){
            isBoosted = false;
            myRigidBody2D.gravityScale = myGravityScale * scale;
            return;
       }
    }
}
