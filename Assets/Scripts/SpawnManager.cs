using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string EnemyType;
        public int Count;
    }

    public GameManager gameManager;

    public PoolManager poolManager;

    public UIManager uiManager;

    public Transform spawnPoint;

    private int currentWave = 0;
    private int currentWaveEnemyCount;

    private int currentTotalWaveEnemyCount;
    private int totalWaveEnemyCount;

    public List<Wave> waves = new List<Wave>(); 

    private List<GameObject> activeEnemies = new List<GameObject>();


    public void StartSpawn()
    {
        StartCoroutine("SpawnEnemies");
        totalWaveEnemyCount = GetTotalEnemyCount();
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(6f);

        while (true)
        {
            if (gameManager.gameOver)
            {
                yield break; 
            }

            if (currentWave >= waves.Count)
            {
                yield break;
            }

            if (currentWave < waves.Count)
            {
                Wave wave = waves[currentWave]; 
                currentWaveEnemyCount = 0; 

                while (currentWaveEnemyCount < wave.Count)
                {
                    currentWaveEnemyCount++;

                    currentTotalWaveEnemyCount++;

                    GameObject enemy = poolManager.MakeObj(wave.EnemyType); 

                    if (enemy != null)
                    {
                        enemy.transform.position = spawnPoint.position; 
                        activeEnemies.Add(enemy); 
                    }

                    yield return new WaitForSeconds(Random.Range(0.5f, 3f));
                }
            }

            currentWave++; 
        }
    }

    public void EnemyDied(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);

            if(totalWaveEnemyCount <= currentTotalWaveEnemyCount+1)
            {
                if (activeEnemies.Count <= 0)
                {
                    uiManager.ShowResultScreen("Success");
                }
            }
        }
    }

    public int GetTotalEnemyCount()
    {
        int totalEnemyCount = 0;

        for (int i = 0; i < waves.Count; i++)
        {
            totalEnemyCount += waves[i].Count;
        }
        return totalEnemyCount;
    }
}
