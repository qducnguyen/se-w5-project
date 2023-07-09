using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawnManager : MonoBehaviour
{
    public static TerrainSpawnManager Instance;
    [SerializeField] private Player player;
    [SerializeField] private Transform terrain;
    [SerializeField] private Transform startSpawnLine;
    [SerializeField] private Transform spawnLine;
    [SerializeField] private float xMargin;
    [SerializeField] private GameObject[] terrainsPrefabs;
    [SerializeField] private float spawnDistanceMax;

    private TerrainType.TerrainTypes previousTerrainType =  TerrainType.TerrainTypes.money;
    private TerrainType.TerrainTypes previousPreviousTerrainType = TerrainType.TerrainTypes.money;

    private float previousPositionX = 0f;
    private float previousPreviousPositionX = 0f;
    
    private int timeFrames;
    private float checkpointyPosition;
    private Vector3 spawnPosition;
    private GameObject terrainToSpawn;
    private int countLimit;
    private TerrainType.TerrainTypes terrainType;


    private void Awake() {
        Instance = this;
    }
    private void Start() {
        checkpointyPosition = spawnLine.position.y;

        StartSpawnTerrain();   


    }
    private void FixedUpdate() {
        if (checkpointyPosition - spawnLine.position.y  >= spawnDistanceMax)
        {
            checkpointyPosition = spawnLine.position.y;
            SpawnTerrain();
        }
        timeFrames = Time.frameCount;
    }



    private void StartSpawnTerrain(){
        float startSpawnLinePosition =  startSpawnLine.position.y;
        float spawnLinePosition = spawnLine.position.y;

        for (float i = startSpawnLinePosition; i >= spawnLinePosition; i = i-spawnDistanceMax){

            // Just base and coin spawn

            if (i == startSpawnLinePosition){ // First spawn
                terrainToSpawn = terrainsPrefabs[0];
                spawnPosition.x = new float[] {Random.Range(-xMargin, -xMargin / 3), Random.Range(xMargin / 3, xMargin)}[Random.Range(0, 2)] ; 

            }

            else{
                
                if (previousTerrainType == TerrainType.TerrainTypes.money && Random.Range(0, 1f) >= 0.75f){
                    terrainToSpawn = terrainsPrefabs[1];
                }
                else{
                    terrainToSpawn = terrainsPrefabs[Random.Range(0, 2)];
                }

                spawnPosition.x = Random.Range(-xMargin, xMargin);
            }


            spawnPosition.y = i;
            Instantiate(terrainToSpawn, spawnPosition, Quaternion.identity, terrain);

            keepTrackTerrainCount(terrainToSpawn, spawnPosition);
        }   

    }

    private void SpawnTerrain()
    {
        countLimit = TerrainCountManager.Instance.countObstacle + TerrainCountManager.Instance.countMonster;

        // limit randomly spawned entities according to the number of time frames
        if ( ((Time.frameCount < 2e5 ) && (countLimit > 3))                                 // easy
            || ((Time.frameCount > 2e5 ) && (Time.frameCount < 1e8) && (countLimit > 4))    // immediate
            || ((Time.frameCount > 1e8) && (Time.frameCount < 1e10) && (countLimit > 5))     // difficult
            || ((Time.frameCount > 1e10) && (countLimit > 6)))                               // expert
        {
            // Coin or base
            terrainToSpawn = terrainsPrefabs[Random.Range(0, 2)];
        }


        else{
            terrainToSpawn = terrainsPrefabs[Random.Range(0, terrainsPrefabs.Length)];
        }
        
        if (previousTerrainType != TerrainType.TerrainTypes._base && previousPreviousTerrainType != TerrainType.TerrainTypes._base){

                terrainToSpawn = terrainsPrefabs[new int[] {0, 0, 2, 0, 1, 1 ,2 ,0, 1, 3}[Random.Range(0, 10)]];

               if (previousPositionX < 0){
                    spawnPosition.x = Random.Range(xMargin/3, xMargin);
               }
               else{
                    spawnPosition.x = Random.Range(-xMargin, -xMargin/3);
               }
        }

        if (terrainToSpawn.GetComponent<TerrainType>().terrainType == TerrainType.TerrainTypes.monster){
            spawnPosition.x = Mathf.Clamp(spawnPosition.x, -xMargin+1.25f, xMargin-1.25f);
        }
        else{
            spawnPosition.x = Random.Range(-xMargin, xMargin);
        }

        spawnPosition.y = checkpointyPosition;

        Instantiate(terrainToSpawn, spawnPosition, Quaternion.identity, terrain);

        keepTrackTerrainCount(terrainToSpawn, spawnPosition);
    }

    private void keepTrackTerrainCount(GameObject terrainToSpawn, Vector3 position){

        terrainType = terrainToSpawn.GetComponent<TerrainType>().terrainType;


        // For counting
        switch (terrainType)
        {
            case TerrainType.TerrainTypes._base:
                TerrainCountManager.Instance.countBase += 1;
                break;
            
            case TerrainType.TerrainTypes.money:
                TerrainCountManager.Instance.countMoney += 1;
                break;

            case TerrainType.TerrainTypes.obstacle:
                TerrainCountManager.Instance.countObstacle += 1;
                break;

            case TerrainType.TerrainTypes.monster:
                TerrainCountManager.Instance.countMonster += 1;
                break;
        }

        previousPreviousTerrainType = previousTerrainType;
        previousTerrainType = terrainType;
        previousPreviousPositionX = previousPositionX;
        previousPositionX = position.x;
    }
        
}

