using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MonsterData : ScriptableObject
{
    public MonsterInfo[] monsterInfos;
}

[Serializable]
public class MonsterInfo
{
    public float MonsterId; 
    public float MonsterKind ;   
    public float MonsterTier ;   
    public float MonsterIndex    ;   
    public float MonsterLevel ;  
    public float MonsterPower    ;   
    public float MonsterHp ; 
    public float MonsterRegen    ;   
    public float MonsterRegenTime ;  
    public float MonsterExp;
    public float MonsterMap;
}
