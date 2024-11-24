using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner Instance;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
    }

    public List<Monster> monsters = new List<Monster>();

    private PlayerController Player;

    public GameObject enemyPrefab; // 생성할 적 프리팹
    public Vector2 spawnAreaMin;  // 스폰 영역 최소값
    public Vector2 spawnAreaMax;  // 스폰 영역 최대값
    public int enemyCount = 5;    // 생성할 적 수

    private void Start()
    {
        Player = PlayerManager.instance.Player;

        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0); // 2D 게임이라면 Z=0
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
