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
    private float checkpointyPosition;
    private Vector3 spawnPosition;
    private GameObject terrainToSpawn;
    private int countLimit;

    public List<TerrainType.TerrainTypes> currentTerrain = new List<TerrainType.TerrainTypes>();

    public TerrainType.TerrainTypes terrainType;

    public int countBase = 0;

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
    }

    private void SpawnTerrain()
    {
        countLimit = TerrainCountManager.Instance.countObstacle + TerrainCountManager.Instance.countMonster;

        // limit randomly spawned entities according to the number of time frames
        if (((Time.frameCount > 1e3 ) && (countLimit > 2))      // first 20s: easy
            || ((Time.frameCount > 1e4) && (countLimit > 3))    // 21s-200s: immediate
            || ((Time.frameCount > 1e5) && (countLimit > 4)))   // after 200s: difficult
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
