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

        // ����Ʈ�� ��ȸ�ϸ� ���� ������ ��� Ÿ�� ����
        for (int i = monsters.Count - 1; i >= 0; i--) // ���� �ݺ�
        {
            Monster target = monsters[i];
            if (target == null || Vector2.Distance(transform.position, target.transform.position) > targetRanage)
            {
                monsters.RemoveAt(i);
            }
        }

        // ����Ʈ ���� ����� ���
        //Debug.Log("Ž���� Ÿ�� ��: " + monsters.Count);

        foreach(Monster target in monsters)
        {
            //Debug.Log("Ž���� Ÿ��: " + target.name);
        }
    }

}
