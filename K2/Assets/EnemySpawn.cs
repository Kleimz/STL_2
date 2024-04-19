using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    int expectedEnemy = 4;
    [SerializeField] List<GameObject> enemies;
    [SerializeField]
    GameObject[] spawnPoints;
    [SerializeField]
    GameObject enemyPrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var enemy in enemies)
        {
            if (enemy == null)
            {
                enemies.Remove(enemy);
            }
        }
        if (enemies.Count < expectedEnemy)
        {
            enemies.Add(SpawnEnemy());
        }
    }

    GameObject SpawnEnemy()
    {
        GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        return enemy;
    }
}
