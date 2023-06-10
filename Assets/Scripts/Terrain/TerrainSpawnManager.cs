using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawnManager : MonoBehaviour
{

    [SerializeField] private Player player;
    [SerializeField] private Transform terrain;
    [SerializeField] private Transform spawnLine;
    [SerializeField] private float xMargin;
    [SerializeField] private GameObject[] terrainsPrefabs;
    [SerializeField] private float spawnDistanceMax  = 10f;
    private float checkpointyPosition;
    private Vector3 spawnPosition;

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
        GameObject terrainToSpawn = terrainsPrefabs[Random.Range(0, terrainsPrefabs.Length)];
        spawnPosition.x = Random.Range(-xMargin, xMargin);
        spawnPosition.y = checkpointyPosition;

        Instantiate(terrainToSpawn, spawnPosition, Quaternion.identity, terrain);

    }

}
