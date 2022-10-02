using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject checkPointPrefab;
    [SerializeField] int checkPointSpawnDelay = 10;
    [SerializeField] float spawnRadius = 6;
    [SerializeField] GameObject [] powerUpPrefab;

    [SerializeField] int powerUpSpawnDelay = 15;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCheckPointRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }
     
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnCheckPointRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkPointSpawnDelay);
            Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;   //En un circulo de 5 unidades, se generan posiciones aleatorias
            Instantiate(checkPointPrefab, randomPosition, Quaternion.identity);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerUpSpawnDelay);
            Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;   //En un circulo de 5 unidades, se generan posiciones aleatorias
            int random = Random.Range(0, powerUpPrefab.Length);
            Instantiate(powerUpPrefab[random], randomPosition, Quaternion.identity);
        }
    }
}
