using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
public class QuestData
{
    public string QuestId;  // 퀘스트 식별번호. 단순 열거
    public int QuestIndex;  // 퀘스트 번호. 개수
    public int QuestType;   // 0: 일반몹, 1: 중간몹, 2:보스몹
    public int QuestExp;    // 퀘스트 성공 경험치
    public int QuestGiftNumber; // 퀘스트 성공시 Gife 선택 수
}

public class QuestSystem : MonoBehaviour
{
    private static QuestSystem instance;
    public static QuestSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<QuestSystem>();
                if (instance == null)
                {
                    GameObject go = new GameObject("QuestSystem");
                    instance = go.AddComponent<QuestSystem>();
                }
            }
            return instance;
        }
    }
    
    private List<QuestData> questDataList;
    private CharacterLevelSystem levelSystem;
    
    private Dictionary<int, List<QuestData>> questDictionary;
    
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);  // 씬 전환시에도 유지

        levelSystem = CharacterLevelSystem.Instance;
        if (levelSystem == null)
        {
            Debug.LogError("CharacterLevelSystem not found");
        }
        
        LoadQuestData();
        OrganizeQuestsByType();
    }

    private void LoadQuestData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("QuestDataTableJSON");
        if (jsonFile != null)
        {
            string jsonString = jsonFile.text;
            questDataList = JsonUtility.FromJson<Wrapper<QuestData>>(
                "{\"Items\":" + jsonString + "}"
            ).Items.ToList();
            
            // Debug.Log($"{questDataList.Count}개의 퀘스트 불러오기 완료");
        }
        else
        {
            Debug.LogError("퀘스트 파일 없음");
        }
    }

    // 퀘스트 Type, 맵 종류에 따른 퀘스트 분류
    private void OrganizeQuestsByType()
    {
        questDictionary = new Dictionary<int, List<QuestData>>();
        
        foreach (var quest in questDataList)
        {
            if (!questDictionary.ContainsKey(quest.QuestType))
            {
                questDictionary[quest.QuestType] = new List<QuestData>();
            }
            questDictionary[quest.QuestType].Add(quest);
        }
    }

    // 퀘스트 ID로 퀘스트 찾기
    public QuestData GetQuestById(string questId)
    {
        return questDataList.Find(q => q.QuestId == questId);
    }

    // 특정 타입의 모든 퀘스트 가져오기
    public List<QuestData> GetQuestsByType(int questType)
    {
        return questDictionary.ContainsKey(questType) ? questDictionary[questType] : new List<QuestData>();
    }

    // 특정 타입의 특정 인덱스 퀘스트 가져오기
    public QuestData GetQuestByTypeAndIndex(int questType, int questIndex)
    {
        if (questDictionary.ContainsKey(questType))
        {
            return questDictionary[questType].Find(q => q.QuestIndex == questIndex);
        }
        return null;
    }

    // 퀘스트 완료 처리
    public void CompleteQuest(string questId)
    {
        QuestData quest = GetQuestById(questId);
        if (quest != null)
        {
            // 경험치 보상 지급
            levelSystem.AddExperience(quest.QuestExp);
            
            // 아이템 보상 지급 (여기에 구현)
            GiveQuestRewards(quest);
            
            Debug.Log($"Quest {questId} completed! Earned {quest.QuestExp} exp");
        }
        else
        {
            Debug.LogError($"Quest {questId} not found!");
        }
    }

    private void GiveQuestRewards(QuestData quest)
    {
        
    }

    // 퀘스트 진행 상황을 저장하기 위한 클래스 (필요에 따라 구현)
    [Serializable]
    public class QuestProgress
    {
        public string QuestId;
        public bool IsCompleted;
        public int Progress;
    }
}

// JSON 파싱을 위한 래퍼 클래스
[Serializable]
public class Wrapper<T>
{
    public T[] Items;
}