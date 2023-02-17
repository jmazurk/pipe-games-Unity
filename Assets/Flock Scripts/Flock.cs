using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    [Range(1, 500)]
    public int startingCount = 250;
    const float agentDensity = 0.08f;
    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float detectionRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplayer = 0.5f;
    public float agentSmoothTime = 0.5f;
    [Range(1f, 1000f)]
    public float maxTurnSpeed = 5f;
    private float maxSpawningX, maxSpawningY, minSpawningX, minSpawningY;

    float squareMaxSpeed;
    float squareDetectionRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius{ get { return squareAvoidanceRadius; } }
    void Start()
    {
        maxSpawningX = 10.5f;
        maxSpawningY = 6;
        minSpawningX = -10.5f;
        minSpawningY = -6;
        
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareDetectionRadius = detectionRadius * detectionRadius;
        squareAvoidanceRadius = squareDetectionRadius * avoidanceRadiusMultiplayer * avoidanceRadiusMultiplayer;
        
        for(int i = 0; i < startingCount; i++){

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

            float angle =  Random.Range(0, 360);
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                //Random.insideUnitCircle * startingCount * agentDensity,
                spawnPositionVector3,
                Quaternion.Euler(Vector3.forward * angle),
                transform
            );

            //newAgent.transform.Rotate(Vector3.right * Random.Range(0, 360));
            newAgent.setAngle(angle);
            newAgent.name = "Agent "+ i;
            newAgent.initialize(this);
            agents.Add(newAgent);
        }
    }

    void Update()
    {
        foreach(FlockAgent agent in agents){
            List<Transform> surrounding = GetNearbyObjects(agent);
            Vector2 movement = behavior.CalculateMovement(agent, surrounding, this);
            movement *= driveFactor;

            if(movement.sqrMagnitude > squareMaxSpeed) {
                movement = movement.normalized * maxSpeed;
            }

            agent.Move(movement);
        }

        squareMaxSpeed = maxSpeed * maxSpeed;
        squareDetectionRadius = detectionRadius * detectionRadius;
        squareAvoidanceRadius = squareDetectionRadius * avoidanceRadiusMultiplayer * avoidanceRadiusMultiplayer;
    }

    List<Transform> GetNearbyObjects(FlockAgent agent){
        List<Transform> surrounding = new List<Transform>();
        Collider2D[] surroundingColliders = Physics2D.OverlapCircleAll(agent.transform.position, detectionRadius);

        foreach(Collider2D collider in surroundingColliders){
            if(collider == agent.AgentCollider) continue;

            surrounding.Add(collider.transform);
        }

        return surrounding;
    } 
}
