using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMiddleScript : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isCounted = false;
    public LogicScript logicScript;
    void Start()
    {
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(isCounted) return;
        if(collision.gameObject.layer != 3) return;
        isCounted = true;
        logicScript.addScore(1);
    }
}
