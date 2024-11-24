using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/*
[Serializable]
public class MonsterData
{
    public string MonsterId;  // �ĺ� ��ȣ         
    public int MonsterKind; //���� ����
    public int MonsterTier; //���� ���
    public int MonsterIndex; //���� �ε���
    public int MonsterLevel; //���� ����
    public int MonsterPower; //���� ���ݷ�
    public int MonsterHp; //���� ü��
    public int MonsterRegen; //���� ���� ����
    public int MonsterRegenTime; //���� ���� �ð�
    public int MonsterExp       ; //���� ����� ����ġ��
}
*/
public class MonsterDataSystem : MonoBehaviour
{
    private static MonsterDataSystem instance;
    public static MonsterDataSystem Instance
    {
        get
        {
            if (instance == null)
            {
                // Scene���� ã�ƺ���
                instance = FindObjectOfType<MonsterDataSystem>();

                // Scene�� ���ٸ� ���� ����
                if (instance == null)
                {
                    GameObject go = new GameObject("MonsterDataManager");
                    instance = go.AddComponent<MonsterDataSystem>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);    // �ߺ��� �ν��Ͻ� ����
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);  // �� ��ȯ�ÿ��� ����

        LoadMonsterData();
    }


    public List<MonsterData> monsterDataList;
    private int currentExp = 0;
    public MonstersData monstersData;


    private void LoadMonsterData()
    {
        // Resources �������� JSON ���� �ε�
        TextAsset jsonFile = Resources.Load<TextAsset>("MonsterDataTableJSON");
        if (jsonFile != null)
        {
            string jsonString = jsonFile.text;

            monsterDataList = JsonUtility.FromJson<Wrapper<MonsterData>>(
                "{\"Items\":" + jsonString + "}"
            ).Items.ToList();
        }
        else
        {
            Debug.LogError("���ҽ� ������ JSON ���� ����");
        }

        Debug.Log($"{monsterDataList.Count}");

        for (int i = 0; i < monsterDataList.Count; i++)
        {
            monstersData.monsterInfos.Add(monsterDataList[i]);
        }

    }

    public MonsterData GetMonster(string monsterId)
    {
        MonsterData currentMonster = monsterDataList.Find(x => x.MonsterId == monsterId);
        if (currentMonster == null)
        {
            Debug.LogError($"{monsterId} ID�� ���� ĳ���� ����");
        }
        return currentMonster;
    }

}
