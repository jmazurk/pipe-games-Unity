using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePairMoveScript : MonoBehaviour
{

    public float moveSpeed = 4;
    public float verticalOffset = 0;
    // Start is called before the first frame update
    void Start()
    {
        //transform.position += Vector3.up * verticalOffset;
        verticalOffset += transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        transform.position = Vector3.up * verticalOffset + Vector3.right * transform.position.x;

        if(transform.position.x <= -20){
            Destroy(gameObject);
        }
    }
}
