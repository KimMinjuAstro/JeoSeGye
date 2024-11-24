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
    public string MonsterId;  // �ĺ� ��ȣ         
    public int MonsterKind; //���� ����
    public int MonsterTier; //���� ���
    public int MonsterIndex; //���� �ε���
    public int MonsterLevel; //���� ����
    public int MonsterPower; //���� ���ݷ�
    public int MonsterHp; //���� ü��
    public int MonsterRegen; //���� ���� ����
    public int MonsterRegenTime; //���� ���� �ð�
    public int MonsterExp; //���� ����� ����ġ��
}
