using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SkillData
{
    public string SkillId;
    public int characterType;
    
    // ----- 인게임 상에 보여야 할 정보
    public int SkillIndex;      // 스킬 인덱스. 종류
    public string SkillName;    // 스킬 이름. 단어
    public string SkillDescription; // 스킬 설명
    public int SkillGrade;       // 스킬 등급 0:Normal, 1:Rare, 2:Epic, 3:Legendary
    public int SkillLevel;       // 스킬 레벨. 한 등급당 1~10레벨 존재
    // -----
    
    public int skillValue;
    public int SkillRange1;
    public int SkillRange2;
    public int SkillType;
    public int IsSingleTarget;
    public int SkillRangeType;
    public int SkillExtraEffectType;
    public int SkillDuration;
    public int SkillTime;
}

public class SkillSystem : MonoBehaviour
{
    private static SkillSystem instance;
    public static SkillSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SkillSystem>();
                if (instance == null)
                {
                    GameObject go = new GameObject("SkillSystem");
                    instance = go.AddComponent<SkillSystem>();
                }
            }
            return instance;
        }
    }

    private List<SkillData> skillDataList;
    private Dictionary<int, List<SkillData>> skillsByIndex;
    private Dictionary<string, List<SkillData>> skillsByName;
    private Dictionary<int, List<SkillData>> skillsByGrade;
    

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSkillData();
        OrganizeSkills();
    }

    private void LoadSkillData()
    {
        TextAsset skillDataFile = Resources.Load<TextAsset>("CharacterSkillDataTableJSON");
        if (skillDataFile == null)
        {
            Debug.LogError("스킬 데이터 파일을 찾을 수 없습니다!");
            return;
        }

        string jsonData = skillDataFile.text;
        Debug.Log("JSON 파일 로드 시도: 성공");
        Debug.Log("JSON 문자열 길이: " + jsonData.Length);

        if (string.IsNullOrEmpty(jsonData))
        {
            Debug.LogError("JSON 파일이 비어있습니다!");
            return;
        }

        try
        {
            skillDataList = new List<SkillData>(JsonUtility.FromJson<SkillData[]>(jsonData));

            Debug.Log("JSON 파싱 결과: 성공");
        }
        catch (System.Exception e)
        {
            // Debug.LogError("JSON 파싱 실패: " + e.Message);
            return;
        }

        if (skillDataList == null || skillDataList.Count == 0)
        {
            Debug.LogError("파싱된 스킬 데이터가 없습니다!");
            return;
        }

        Debug.Log("로드된 스킬 데이터 수: " + skillDataList.Count);
    }

    private void OrganizeSkills()
    {
        skillsByIndex = new Dictionary<int, List<SkillData>>();

        foreach (var skill in skillDataList)
        {
            if (!skillsByIndex.ContainsKey(skill.SkillIndex))
            {
                skillsByIndex[skill.SkillIndex] = new List<SkillData>();
            }
            skillsByIndex[skill.SkillIndex].Add(skill);
        }

        foreach (var key in skillsByIndex.Keys)
        {
            Debug.Log($"SkillIndex {key}: {skillsByIndex[key].Count}개");
        }
    }
    
    public List<SkillData> GetSkills(int skillIndex, int skillGrade, int skillLevel)
    {
        if (skillsByIndex.ContainsKey(skillIndex))
        {
            List<SkillData> skillList = skillsByIndex[skillIndex];
            List<SkillData> filteredSkills = skillList.FindAll(skill => skill.SkillGrade == skillGrade && skill.SkillLevel == skillLevel);

            if (filteredSkills.Count == 0)
            {
                Debug.LogWarning($"SkillIndex {skillIndex}, Grade {skillGrade}, Level {skillLevel}의 스킬을 찾을 수 없습니다!");
            }

            return filteredSkills;
        }
        else
        {
            Debug.LogWarning($"SkillIndex {skillIndex}에 대한 스킬을 찾을 수 없습니다!");
            return new List<SkillData>();
        }
    }
    

    // 스킬 종류로 스킬 목록 가져오기
    public List<SkillData> GetSkillsByIndex(int SkillIndex)
    {
        return skillsByIndex.ContainsKey(SkillIndex) 
            ? skillsByIndex[SkillIndex] 
            : new List<SkillData>();
    }

    // 스킬 이름으로 스킬 목록 가져오기
    public List<SkillData> GetSkillsByName(string SkillName)
    {
        return skillsByName.ContainsKey(SkillName) 
            ? skillsByName[SkillName] 
            : new List<SkillData>();
    }
    

    // // 특정 레벨의 스킬 데이터 가져오기
    // public SkillData GetSkillByLevel(int SkillIndex, int level)
    // {
    //     if (skillsByIndex.ContainsKey(SkillIndex))
    //     {
    //         return skillsByIndex[SkillIndex].Find(s => s.SkillLevel == level);
    //     }
    //     return null;
    // }

    // 특정 등급의 스킬 목록 가져오기
    public List<SkillData> GetSkillsByGrade(int grade)
    {
        return skillsByGrade.ContainsKey(grade) 
            ? skillsByGrade[grade] 
            : new List<SkillData>();
    }
    
    // 스킬 종류, 등급, 레벨로 스킬 목록 가져오기
    // public List<SkillData> GetSkills(int SkillIndex, int SkillGrade, int SkillLevel)
    // {
    //     return skillDataList.Where(s => s.SkillIndex == SkillIndex && s.SkillGrade == SkillGrade && s.SkillLevel == SkillLevel).ToList();
    // }
    
    
    // // 스킬의 최대 레벨 가져오기
    // public int GetMaxSkillLevel(int SkillIndex)
    // {
    //     if (skillsByIndex.ContainsKey(SkillIndex))
    //     {
    //         return skillsByIndex[SkillIndex].Max(s => s.SkillLevel);
    //     }
    //     return 0;
    // }

    
    // 디버그용: 모든 스킬 출력
    public void DebugPrintAllSkills()
    {
        foreach (var skill in skillDataList)
        {
            Debug.Log($"스킬: {skill.SkillName} (Index: {skill.SkillIndex}, Level: {skill.SkillLevel}, Grade: {skill.SkillGrade})");
        }
    }
}

