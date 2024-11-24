using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/*
[Serializable]
public class MonsterData
{
    public string MonsterId;  // 식별 번호         
    public int MonsterKind; //몬스터 종류
    public int MonsterTier; //몬스터 등급
    public int MonsterIndex; //몬스터 인덱스
    public int MonsterLevel; //몬스터 레벨
    public int MonsterPower; //몬스터 공격력
    public int MonsterHp; //몬스터 체력
    public int MonsterRegen; //몬스터 리젠 여부
    public int MonsterRegenTime; //몬스터 리젠 시간
    public int MonsterExp       ; //몬스터 사망시 경험치량
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
                // Scene에서 찾아보기
                instance = FindObjectOfType<MonsterDataSystem>();

                // Scene에 없다면 새로 생성
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
            Destroy(gameObject);    // 중복된 인스턴스 제거
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);  // 씬 전환시에도 유지

        LoadMonsterData();
    }


    public List<MonsterData> monsterDataList;
    private int currentExp = 0;
    public MonstersData monstersData;


    private void LoadMonsterData()
    {
        // Resources 폴더에서 JSON 파일 로드
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
            Debug.LogError("리소스 폴더에 JSON 파일 없음");
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
            Debug.LogError($"{monsterId} ID를 가진 캐릭터 없음");
        }
        return currentMonster;
    }

}
