using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCollisionScript : MonoBehaviour
{
    public LogicScript logicScript;

    void Start(){
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer != 3) return;
        
        logicScript.gameOver();
    }
}
