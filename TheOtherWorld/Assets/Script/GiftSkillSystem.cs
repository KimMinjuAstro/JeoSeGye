using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class GiftSkillSystem : MonoBehaviour
{
    public class GiftUI
    {
        public GameObject giftObject;
        public TextMeshProUGUI giftTierText;
        public TextMeshProUGUI giftTypeText;
        public TextMeshProUGUI giftDetailText;
        public TextMeshProUGUI giftLevelText;
    }

    public GiftUI[] giftUIs = new GiftUI[3];
    
    private HashSet<int> selectedActiveSkillIndexes = new HashSet<int>();
    private Dictionary<int, SkillData> currentSkills = new Dictionary<int, SkillData>();
    
    private readonly int[] passiveSkillIndexes = { 1, 2 };
    private readonly int[] activeSkillIndexes = { 3, 4, 5, 6, 7, 8 };
    private const int requiredActiveSkills = 4;

    private static GiftSkillSystem instance;
    public static GiftSkillSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GiftSkillSystem>();
                if (instance == null)
                {
                    GameObject go = new GameObject("QuestSystem");
                    instance = go.AddComponent<GiftSkillSystem>();
                }
            }
            return instance;
        }
    }
    
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);  // 씬 전환시에도 유지
    }
    
    
    void Start()
    {
        InitializeUI();
        GenerateRandomSkills(); // 초기 스킬 생성
    }

    private void InitializeUI()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject giftObj = GameObject.Find("UI Canvas").transform.Find("GiftPopUp").transform.Find("GiftBox").transform.Find($"Gift{i}").gameObject;
            if (giftObj == null)
            {
                Debug.LogError($"Gift{i} 오브젝트를 찾을 수 없습니다!");
                continue;
            }

            giftUIs[i] = new GiftUI
            {
                giftObject = giftObj,
                giftTierText = FindTextComponent(giftObj, "GiftTier"),
                giftTypeText = FindTextComponent(giftObj, "GiftType"),
                giftDetailText = FindTextComponent(giftObj, "GiftDetail"),
                giftLevelText = FindTextComponent(giftObj, "GiftLevel")
            };

            // UI 컴포넌트 확인
            if (giftUIs[i].giftTierText == null || giftUIs[i].giftTypeText == null || 
                giftUIs[i].giftDetailText == null || giftUIs[i].giftLevelText == null)
            {
                Debug.LogError($"Gift{i}의 일부 TextMeshProUGUI 컴포넌트를 찾을 수 없습니다!");
            }
        }
    }

    private TextMeshProUGUI FindTextComponent(GameObject parent, string childName)
    {
        Transform child = parent.transform.Find(childName);
        if (child == null)
        {
            Debug.LogError($"{parent.name}에서 {childName}을 찾을 수 없습니다!");
            return null;
        }

        TextMeshProUGUI textComponent = child.GetComponent<TextMeshProUGUI>();
        if (textComponent == null)
        {
            Debug.LogError($"{childName}에서 TextMeshProUGUI 컴포넌트를 찾을 수 없습니다!");
        }
        return textComponent;
    }

    public void GenerateRandomSkills()
    {
        List<int> availableSkillIndexes = GetAvailableSkillIndexes();
        if (availableSkillIndexes.Count < 3)
        {
            Debug.LogWarning("사용 가능한 스킬이 3개 미만입니다!");
            return;
        }

        // 3개의 랜덤 스킬 선택
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, availableSkillIndexes.Count);
            int selectedSkillIndex = availableSkillIndexes[randomIndex];
            
            // 현재 스킬의 다음 레벨 데이터 가져오기
            SkillData nextSkill = GetNextSkillLevel(selectedSkillIndex);
            if (nextSkill != null)
            {
                DisplaySkill(i, nextSkill);
                currentSkills[selectedSkillIndex] = nextSkill;
            }
            else
            {
                Debug.LogWarning($"SkillIndex {selectedSkillIndex}의 다음 레벨을 찾을 수 없습니다!");
            }
            
            availableSkillIndexes.RemoveAt(randomIndex);
        }
    }

    private List<int> GetAvailableSkillIndexes()
    {
        List<int> available = new List<int>();
        
        // 패시브 스킬 추가
        foreach (int passiveIndex in passiveSkillIndexes)
        {
            if (HasNextLevel(passiveIndex))
            {
                available.Add(passiveIndex);
            }
        }
        
        // 액티브 스킬 처리
        if (selectedActiveSkillIndexes.Count < requiredActiveSkills)
        {
            // 아직 4개의 액티브 스킬이 선택되지 않은 경우
            var remainingActives = activeSkillIndexes
                .Where(index => !selectedActiveSkillIndexes.Contains(index))
                .ToList();
            available.AddRange(remainingActives);
        }
        else
        {
            // 이미 선택된 액티브 스킬들만 추가
            foreach (int activeIndex in selectedActiveSkillIndexes)
            {
                if (HasNextLevel(activeIndex))
                {
                    available.Add(activeIndex);
                }
            }
        }
        
        return available;
    }

    private SkillData GetNextSkillLevel(int skillIndex)
    {
        if (!currentSkills.TryGetValue(skillIndex, out SkillData currentSkill))
        {
            // 첫 스킬 데이터 가져오기
            var firstSkill = SkillSystem.Instance.GetSkills(skillIndex, 0, 1).FirstOrDefault();
            if (firstSkill == null)
            {
                Debug.LogError($"SkillIndex {skillIndex}의 초기 스킬을 찾을 수 없습니다!");
            }
            return firstSkill;
        }

        int nextLevel = currentSkill.SkillLevel + 1;
        int currentGrade = currentSkill.SkillGrade;

        if (nextLevel > 10)
        {
            nextLevel = 1;
            currentGrade++;
            if (currentGrade > 3) return null;
        }

        var nextSkill = SkillSystem.Instance.GetSkills(skillIndex, currentGrade, nextLevel).FirstOrDefault();
        if (nextSkill == null)
        {
            Debug.LogError($"SkillIndex {skillIndex}, Grade {currentGrade}, Level {nextLevel}의 스킬을 찾을 수 없습니다!");
        }
        return nextSkill;
    }

    private bool HasNextLevel(int skillIndex)
    {
        return GetNextSkillLevel(skillIndex) != null;
    }

    private void DisplaySkill(int uiIndex, SkillData skill)
    {
        if (uiIndex < 0 || uiIndex >= giftUIs.Length || giftUIs[uiIndex] == null)
        {
            Debug.LogError($"유효하지 않은 UI 인덱스: {uiIndex}");
            return;
        }
        
        var ui = giftUIs[uiIndex];
        
        // UI가 제대로 설정되어 있는지 확인
        if (ui.giftTierText == null || ui.giftTypeText == null || 
            ui.giftDetailText == null || ui.giftLevelText == null)
        {
            Debug.LogError($"Gift{uiIndex}의 UI 컴포넌트가 제대로 설정되지 않았습니다!");
            return;
        }

        // 등급 표시 (0: Normal, 1: Rare, 2: Epic, 3: Legendary)
        string[] grades = { "Normal", "Rare", "Epic", "Legendary" };
        ui.giftTierText.text = grades[skill.SkillGrade];
        ui.giftTypeText.text = skill.SkillName;
        ui.giftDetailText.text = skill.SkillDescription;
        ui.giftLevelText.text = $"Lv.{skill.SkillLevel}";

        // 액티브 스킬인 경우 선택된 스킬 목록에 추가
        if (activeSkillIndexes.Contains(skill.SkillIndex))
        {
            selectedActiveSkillIndexes.Add(skill.SkillIndex);
        }
    }

    // 스킬 선택 시 호출되는 메서드
    public void SelectSkill(int uiIndex)
    {
        if (uiIndex < 0 || uiIndex >= giftUIs.Length)
        {
            Debug.LogError($"유효하지 않은 UI 인덱스: {uiIndex}");
            return;
        }

        GenerateRandomSkills();
    }

    // 디버그용 메서드
    private void DebugLogUIComponents()
    {
        for (int i = 0; i < giftUIs.Length; i++)
        {
            var ui = giftUIs[i];
            if (ui == null)
            {
                Debug.LogError($"GiftUI[{i}] is null");
                continue;
            }

            Debug.Log($"Gift{i} Components:");
            Debug.Log($"- GiftTier: {(ui.giftTierText != null ? "Found" : "Missing")}");
            Debug.Log($"- GiftType: {(ui.giftTypeText != null ? "Found" : "Missing")}");
            Debug.Log($"- GiftDetail: {(ui.giftDetailText != null ? "Found" : "Missing")}");
            Debug.Log($"- GiftLevel: {(ui.giftLevelText != null ? "Found" : "Missing")}");
        }
    }
}