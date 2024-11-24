using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class QuestPopUpHandler : MonoBehaviour 
{
    public float targetScale = 4.182f;
    public float shrinkRate = 0.1f;
    
    public GameObject commonUI;
    public GiftPopUpHandler giftPopUpWindow;
    
    [FormerlySerializedAs("questPopUp")] 
    public QuestPopUpHandler questWindow;
    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questTitle2;
    
    public TextMeshProUGUI questDetail;
    public TextMeshProUGUI giftNum;
    public Button clearButton;
    
    [Header("퀘스트 Type과 Index")]
    public int currentQuestType = 0;
    
    private Dictionary<int, HashSet<int>> usedQuestIndices;
    private System.Random random;
    
    void Awake()
    {
        DOTween.Init();
        transform.localScale = Vector3.one * 0.1f;
        gameObject.SetActive(false);
        clearButton.onClick.AddListener(OnClickClearButton);
        
        // Dictionary와 Random 초기화
        usedQuestIndices = new Dictionary<int, HashSet<int>>();
        random = new System.Random();
    }
    
    private void Start()
    {
        QuestApply(currentQuestType);
    }
    
    public void QuestApply(int type)
    {
        try
        {
            // 타입에 따른 퀘스트들 모두 불러옴
            List<QuestData> typeQuestList = QuestSystem.Instance.GetQuestsByType(type);
            
            // 리스트가 비어있거나 null인지 먼저 체크
            if (typeQuestList == null || typeQuestList.Count == 0)
            {
                Debug.LogWarning($"Type {type}에 해당하는 퀘스트가 없습니다.");
                return;
            }
            
            // 해당 타입에 대한 HashSet이 없으면 새로 생성
            if (!usedQuestIndices.ContainsKey(type))
            {
                usedQuestIndices.Add(type, new HashSet<int>());
                Debug.Log($"Type {type}에 대한 새로운 HashSet을 생성했습니다.");
            }
            
            // 모든 퀘스트를 다 사용했다면 리셋
            if (usedQuestIndices[type].Count >= typeQuestList.Count)
            {
                usedQuestIndices[type].Clear();
                Debug.Log($"Type {type}의 모든 퀘스트를 사용했습니다. 리셋했습니다.");
            }
            
            // 사용하지 않은 인덱스들 중에서 랜덤으로 선택
            List<int> availableIndices = new List<int>();
            foreach (var quest in typeQuestList)
            {
                if (!usedQuestIndices[type].Contains(quest.QuestIndex))
                {
                    availableIndices.Add(quest.QuestIndex);
                }
            }
            
            if (availableIndices.Count == 0)
            {
                Debug.LogError($"Type {type}에 대해 사용 가능한 퀘스트가 없습니다.");
                return;
            }
            
            // 랜덤하게 인덱스 선택
            int randomIndex = availableIndices[random.Next(availableIndices.Count)];
            
            // 선택된 인덱스를 사용된 목록에 추가
            usedQuestIndices[type].Add(randomIndex);
            
            // 선택된 퀘스트 찾기
            QuestData selectedQuest = typeQuestList.Find(q => q.QuestIndex == randomIndex);
            
            // UI 업데이트
            if (selectedQuest != null)
            {
                questTitle2.text = $"{selectedQuest.QuestDetail}";
                questTitle.text = $"{selectedQuest.QuestDetail}";
                questDetail.text = $"성공 경험치 : {selectedQuest.QuestExp}";
                giftNum.text = $"x {selectedQuest.QuestGiftNumber}";
                
                // Debug.Log($"Type {type}에서 Quest Index {randomIndex} 선택됨. 남은 퀘스트 수: {typeQuestList.Count - usedQuestIndices[type].Count}");
            }
            else
            {
                Debug.LogError($"선택된 인덱스 {randomIndex}에 해당하는 퀘스트를 찾을 수 없습니다.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"QuestApply 에러 발생: {e.Message}\n{e.StackTrace}");
        }
    }

    public void OnClickClearButton()
    {
        // 다음 퀘스트 미리 생성
        QuestApply(currentQuestType);
        
        var seq = DOTween.Sequence();
        seq.Play().OnComplete(() => {
            giftPopUpWindow.Show();
        });
        
        // 새로운 랜덤 스킬 생성 및 UI에 표시
        // GiftSkillSystem.Instance.GenerateRandomSkills();
    }
    
    public void Show()
    {
        commonUI.SetActive(false);
        gameObject.SetActive(true);
        
        transform.localScale = Vector3.one * 0.1f;
        
        var seq = DOTween.Sequence();
        seq.Append(transform.DOScale(targetScale + targetScale * shrinkRate, 0.3f));
        seq.Append(transform.DOScale(targetScale, 0.1f));
        seq.Play();
    }
    
    public void Hide()
    {
        commonUI.SetActive(true);
        
        var seq = DOTween.Sequence();
        seq.Append(transform.DOScale(targetScale + targetScale * shrinkRate, 0.1f));
        seq.Append(transform.DOScale(0.1f, 0.3f));
        
        seq.Play().OnComplete(() => {
            gameObject.SetActive(false);
        });
    }
}