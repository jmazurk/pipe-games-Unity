using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetSpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxSpawningX, maxSpawningY, minSpawningX, minSpawningY;
    private float timer;
    public float x;
    public float y;
    public float z;
    public float w;
    public float spawnAngle;
    public float spawnRate = 0.01f;
    public float minJetSpeed = 3;
    public float maxJetSpeed = 10;
    public GameObject fakeJet;
    private float radToDeg = 0.01745f;
    void Start()
    {
        radToDeg = 0.01745f;
        
        minJetSpeed = 3;
        maxJetSpeed = 10;

        maxSpawningX = 10.5f;
        maxSpawningY = 6;
        minSpawningX = -10.5f;
        minSpawningY = -6;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer< spawnRate) return;
        timer = 0;

        int spawnPoint = Random.Range(0,4);
        float spawnX = 0;
        float spawnY = 0;

       

        switch(spawnPoint){
            case 0:
                spawnX = minSpawningX;
                spawnY = Random.Range(minSpawningY, maxSpawningY);
                break;
            case 1:
                spawnX = maxSpawningX;
                spawnY = Random.Range(minSpawningY, maxSpawningY);
                break;
            case 2:
                spawnY = maxSpawningY;
                spawnX = Random.Range(minSpawningX, maxSpawningX);
                break;
            case 3:
                spawnY = minSpawningY;
                spawnX = Random.Range(minSpawningX, maxSpawningX);
                break;

        }

        Vector3 spawnPositionVector3 = new Vector3(spawnX, spawnY, -1);
        //Vector3 spawnPositionVector3 = new Vector3(0, 0, 0);

        float spawnAngle = Random.Range(0, 3.1415f);
        //spawnAngle = 3.1415f / 12;
        float cosSpawnAngle = Mathf.Cos(spawnAngle);
        float sinSpawnAngle = Mathf.Sin(spawnAngle);
        //Quaternion spawnRotation = new Quaternion(cosSpawnAngle, sinSpawnAngle, 0,0);
        //cosSpawnAngle = directionTest.x/directionTest.magnitude;
        //sinSpawnAngle = directionTest.y/directionTest.magnitude;

        float spawnVelocityValue = Random.Range(minJetSpeed, maxJetSpeed);
        Vector2 spawnVelocity = new Vector2(spawnVelocityValue * (cosSpawnAngle * cosSpawnAngle * 2 - 1), spawnVelocityValue * (sinSpawnAngle * cosSpawnAngle * 2));
        Vector2 spawnDirection = new Vector2(spawnVelocityValue * (cosSpawnAngle ), spawnVelocityValue * (sinSpawnAngle));
        spawnDirection.Normalize();

        //Quaternion spawnRotation = Quaternion.LookRotation(Vector3.forward, new Vector3(spawnDirection.y, spawnDirection.x, 0));
        Quaternion spawnRotation = new Quaternion(0,0, spawnDirection.y, spawnDirection.x);

        GameObject newJet = Instantiate(fakeJet, spawnPositionVector3, spawnRotation);

        newJet.GetComponent<Rigidbody2D>().velocity = spawnVelocity;

         Debug.Log(spawnAngle);
    }
}
