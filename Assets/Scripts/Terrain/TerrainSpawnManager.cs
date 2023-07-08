using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawnManager : MonoBehaviour
{
    public static TerrainSpawnManager Instance;
    [SerializeField] private Player player;
    [SerializeField] private Transform terrain;
    [SerializeField] private Transform spawnLine;
    [SerializeField] private float xMargin;
    [SerializeField] private GameObject[] terrainsPrefabs;
    [SerializeField] private float spawnDistanceMax  = 10f;
    [SerializeField] private int timeFrames;
    private float checkpointyPosition;
    private Vector3 spawnPosition;
    private GameObject terrainToSpawn;
    private int countLimit;

    public TerrainType.TerrainTypes terrainType;


    private void Awake() {
        Instance = this;
    }

    private void Start() {
        checkpointyPosition = spawnLine.position.y;
    }
    private void FixedUpdate() {
        if (checkpointyPosition - spawnLine.position.y  >= spawnDistanceMax)
        {
            checkpointyPosition = spawnLine.position.y;
            SpawnTerrain();
        }
        timeFrames = Time.frameCount;
    }

    private void SpawnTerrain()
    {
        countLimit = TerrainCountManager.Instance.countObstacle + TerrainCountManager.Instance.countMonster;

        // limit randomly spawned entities according to the number of time frames
        if ( ((Time.frameCount < 1e5 ) && (countLimit > 1))                                 // easy
            || ((Time.frameCount > 1e5 ) && (Time.frameCount < 1e7) && (countLimit > 2))    // immediate
            || ((Time.frameCount > 1e7) && (Time.frameCount < 1e9) && (countLimit > 3))     // difficult
            || ((Time.frameCount > 1e9) && (countLimit > 4)))                               // expert
        {
            terrainToSpawn = terrainsPrefabs[Random.Range(0, 2)];
        }
        else
            terrainToSpawn = terrainsPrefabs[Random.Range(0, terrainsPrefabs.Length)];

        spawnPosition.x = Random.Range(-xMargin, xMargin);
        spawnPosition.y = checkpointyPosition;

        Instantiate(terrainToSpawn, spawnPosition, Quaternion.identity, terrain);

        terrainType = terrainToSpawn.GetComponent<TerrainType>().terrainType;
        Debug.Log(terrainType);


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
    }

}
