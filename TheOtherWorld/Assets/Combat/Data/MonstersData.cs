using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MonstersData : ScriptableObject
{
    public List<MonsterData> monsterInfos;
}

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
    public int MonsterExp; //몬스터 사망시 경험치량
}
