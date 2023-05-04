using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    private Rigidbody2D rg;
    [SerializeField] private GameObject[] entitiesPrefabs;
    [SerializeField] private float entitySpeed = 2f;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private float xMargin = 3;
    [SerializeField] private float spawnTimer;
    [SerializeField] private float spawnTimerMax = 0.5f;
    private int numTerrain = 0;

    [SerializeField] private Transform camera;

    private void Start() {
        rg = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
        }
        else
        {
            if (numTerrain < 10) {
                spawnTimer = spawnTimerMax;
                SpawnEntity();
            }
        }
        
    }


    private void SpawnEntity()
    {
        GameObject entityToSpawn = entitiesPrefabs[Random.Range(0, entitiesPrefabs.Length)];
        spawnPosition.x = Random.Range(-xMargin, xMargin);
        spawnPosition.y = rg.position.y - Random.Range(0,20);
       
        GameObject spawnedEntity = Instantiate(entityToSpawn, spawnPosition, Quaternion.identity);
        numTerrain += 1;
    }
}
