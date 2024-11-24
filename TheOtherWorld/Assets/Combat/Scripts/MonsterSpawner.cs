using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    public GameObject[] enemyPrefab; // 생성할 적 프리팹
    public GameObject[] spawnerPosition; // 스폰 위치

    private int wave = 0;
    private int maxWave = 5;
    bool end = false;

    private void Start()
    {
        Player = PlayerManager.instance.Player;
        SpawnEnemies(0);
    }

    private void Update()
    {
        if (monsters.Count == 0)
        {
            if (end) return;
            end = true;
            wave++;

            if (wave >= maxWave)
            {
                // 2라운드
            }
            else
            { 
                SpawnEnemies(wave);            
            }
        }
    }

    void SpawnEnemies(int spawnIndex)
    {
        for (int i = 0; i < spawnerPosition.Length; i++)
        {
            Monster instance = Instantiate(enemyPrefab[spawnIndex], spawnerPosition[i].transform.position, Quaternion.identity).GetComponent<Monster>();
            monsters.Add(instance);
        }

        end = false;
    }

    public void SpawnerEenemy(Monster prefab)
    {
        for (int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i].GetInstanceID() == prefab.GetInstanceID())
            {
                monsters.Remove(monsters[i]);
            }
        }

    }

}
