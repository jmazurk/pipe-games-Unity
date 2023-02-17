using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pipePair;
    public GameObject pipePairHard;
    private float maxOffset = 3.6f;
    private float timer = 2;
    private float spawnRate = 2f;
    private float minPipeSpeed = 3;
    private float maxPipeSpeed = 4;
    private float minRedPipeSpeed = 3;
    private float maxRedPipeSpeed = 4;
    private int stage = 0;
    private int redPipeChance = 0;
    private float redPipeDelay = 2f;
    private int level = 0;
    void Start()
    {
        stage = 0;
        level = 0;
        redPipeChance = 0;
        redPipeDelay = 2f;
        spawnRate = 2f;
        minRedPipeSpeed = 3;
        maxRedPipeSpeed = 4;
        minPipeSpeed = 3;
        maxPipeSpeed = 4;
    }

    // Update is called once per frame
    void Update()
    {
        timer+= Time.deltaTime;

        if(timer >= spawnRate){
            timer = 0;
            spawnPipe();        
        }
    }

    void spawnPipe(){
        
         Vector3 newPipePosition = new Vector3(transform.position.x, transform.position.y + Random.Range(-maxOffset, maxOffset),transform.position.z);
         GameObject newPipe;

        if(Random.Range(1, 100) <= redPipeChance){
            newPipe = Instantiate(pipePairHard, newPipePosition, transform.rotation);
            newPipe.GetComponent<PipePairMoveScript>().moveSpeed = Random.Range(minRedPipeSpeed, maxRedPipeSpeed);
            timer -=redPipeDelay;
            return;
        }

        newPipe = Instantiate(pipePair, newPipePosition, transform.rotation);
        newPipe.GetComponent<PipePairMoveScript>().moveSpeed = Random.Range(minPipeSpeed, maxPipeSpeed);

        level++;
        
        if(level > 8 && stage == 0){
            redPipeChance = 20;
            stage++;
            timer-=1;
        }
        if(level > 16 && stage == 1){
            redPipeChance = 35;
            redPipeDelay = 1.5f;
            stage++;
            timer-=1;
        }
        if(level > 25 && stage == 2){
            maxRedPipeSpeed = 4.75f;
            minRedPipeSpeed = 3.5f;
            redPipeDelay = 1.25f;
            stage++;
            timer-=1;
        }
        if(level > 35 && stage == 3){
            spawnRate = 1.5f;
            redPipeDelay = 1f;
            stage++;
            timer-=1;
        }
        if(level > 45 && stage == 4){
            maxPipeSpeed = 5.25f;
            minPipeSpeed = 4.5f;
            maxRedPipeSpeed = 5.25f;
            minRedPipeSpeed = 3.75f;
            redPipeDelay = 0.75f;
            spawnRate = 1.3f;
            stage++;
            timer-=1;
        }
        if(level > 55 && stage == 5){
            maxPipeSpeed = 8f;
            minPipeSpeed = 6.5f;
            maxRedPipeSpeed = 8f;
            minRedPipeSpeed = 6.75f;
            spawnRate = 1f;
            stage++;
            timer-=1;
        }
        if(level > 69 && stage == 6){
            maxPipeSpeed = 10f;
            minPipeSpeed = 8.5f;
            maxRedPipeSpeed = 10f;
            minRedPipeSpeed = 8.75f;
            spawnRate = 1f;
            stage++;
            timer-=1;
        }
    }
}
