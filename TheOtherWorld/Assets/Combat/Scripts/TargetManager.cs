using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static TargetManager instance;
    public List<Monster> monsters = new List<Monster>();

    private PlayerController player;
    public float targetRanage = 5f;
    public LayerMask targetLayer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    void Start()
    {
        player = PlayerManager.instance.Player;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, targetRanage, targetLayer);
        monsters.Clear();

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent<Monster>(out Monster monster))
            { 
                monsters.Add(monster);
            }
        }

        // 리스트를 순회하며 범위 밖으로 벗어난 타겟 제거
        for (int i = monsters.Count - 1; i >= 0; i--) // 역순 반복
        {
            Monster target = monsters[i];
            if (target == null || Vector2.Distance(transform.position, target.transform.position) > targetRanage)
            {
                monsters.RemoveAt(i);
            }
        }

        // 리스트 내용 디버그 출력
        //Debug.Log("탐지된 타겟 수: " + monsters.Count);

        foreach(Monster target in monsters)
        {
            //Debug.Log("탐지된 타겟: " + target.name);
        }
    }

}
