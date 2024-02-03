using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
     
    [SerializeField]
    private GameObject enemyPrefab;

    private bool isPlayerAlive = true;

    [SerializeField]
    private GameObject [] powerUps;

    [SerializeField]
    private float timeBetweenWaves = 1f;
    
    int currentWave = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutineEnemy());
        StartCoroutine(SpawnRoutinePowerUp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRoutineEnemy()
    {
        int baseEnemiesPerWave = 1; // Initial number of enemies per wave
        
        while (isPlayerAlive)
        {
            int minEnemies = baseEnemiesPerWave + currentWave; 
            int maxEnemies = minEnemies + 2; 

            int enemiesToSpawn = Random.Range(minEnemies, maxEnemies + 3); 

            // Spawn enemies in a loop
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Vector3 enemyPos = new Vector3(Random.Range(-9f, 9f), 7, 0);
                Instantiate(enemyPrefab, enemyPos, Quaternion.identity);

                yield return new WaitForSeconds(timeBetweenWaves);
            }

            currentWave++;

            // Wait for a longer interval before starting a new wave
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    IEnumerator SpawnRoutinePowerUp()
    {
        while(isPlayerAlive)
        {

            Vector3 tripleShotPos = new Vector3(Random.Range(-9f, 9f), 7, 0);

            int randomPowerUp = Random.Range(0, 2);
            Instantiate(powerUps[randomPowerUp], tripleShotPos, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(2, 5));
        }
    }

    public void PlayerDied()
    {
        isPlayerAlive = false;
    }
}
