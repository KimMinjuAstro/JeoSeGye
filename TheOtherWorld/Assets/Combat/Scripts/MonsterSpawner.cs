using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner instance;
  
    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
    }

    public TextMeshProUGUI text;
    public MonstersData monsterInfo;
    public List<Monster> monsters = new List<Monster>();
    public List<RectTransform> indicator = new List<RectTransform>();

    private GameObject indicators;
    private PlayerController Player;

    public GameObject[] enemyPrefab; // 생성할 적 프리팹
    public GameObject[] spawnerPosition; // 스폰 위치

    private int wave = 0;
    private int maxWave = 30;
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
                Invoke("Test", .3f);
                SpawnEnemies(wave);            
            }
        }

        text.text = monsters.Count.ToString() + "/ 10";

        if (monsters.Count <= 0)
        {
            //Time.timeScale = 

            //result.gameObject.SetActive(true);
        }
    }

    public void Test()
    {
        UIManager.Instance.Show();
    }

    void SpawnEnemies(int spawnIndex)
    {
        MonsterData monsterData = null;

        for (int i = 0; i < monsterInfo.monsterInfos.Count; i++)
        {
            if (monsterInfo.monsterInfos[i].MonsterId == enemyPrefab[spawnIndex].GetComponent<Monster>().name)
            {
                monsterData = monsterInfo.monsterInfos[i];
            }            
        }

        for (int i = 0; i < spawnerPosition.Length; i++)
        {
            
            Monster instance = Instantiate(enemyPrefab[spawnIndex], spawnerPosition[i].transform.position, Quaternion.identity).GetComponent<Monster>();

            instance.Init(monsterData.MonsterHp, monsterData.MonsterLevel, monsterData.MonsterPower, monsterData.MonsterExp);
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
