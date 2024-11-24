using UnityEngine;
using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class QuestPopUpHandler : MonoBehaviour 
{
    public float targetScale = 4.182f;
    public float shrinkRate = 0.1f;
    
    public GameObject commonUI;
    public GiftPopUpHandler giftPopUpWindow;
    public QuestPopUpHandler questWindow;
    public TextMeshProUGUI questTitle;
    // public TextMeshProUGUI questTitle2;
    public TextMeshProUGUI questDetail;
    public TextMeshProUGUI giftNum;
    public Button clearButton;
    
    [Header("퀘스트 Type과 Index")]
    public int currentQuestType = 0;
    
    private Dictionary<int, HashSet<int>> usedQuestIndices;
    private System.Random random;
    
    void Awake()
    {
        InitializeComponents();
    }
    
    private void InitializeComponents()
    {
        try 
        {
            DOTween.Init();
            transform.localScale = Vector3.one * 0.1f;
            gameObject.SetActive(false);
            
            // UI 컴포넌트 유효성 검사
            ValidateRequiredComponents();
            
            clearButton.onClick.AddListener(OnClickClearButton);
            
            usedQuestIndices = new Dictionary<int, HashSet<int>>();
            random = new System.Random();
        }
        catch (Exception e)
        {
            Debug.LogError($"InitializeComponents 에러 발생: {e.Message}");
        }
    }
    
    private void ValidateRequiredComponents()
    {
        if (clearButton == null) throw new NullReferenceException("Clear Button이 설정되지 않았습니다.");
        if (questTitle == null) throw new NullReferenceException("Quest Title이 설정되지 않았습니다.");
        // if (questTitle2 == null) throw new NullReferenceException("Quest Title 2가 설정되지 않았습니다.");
        if (questDetail == null) throw new NullReferenceException("Quest Detail이 설정되지 않았습니다.");
        if (giftNum == null) throw new NullReferenceException("Gift Number가 설정되지 않았습니다.");
        if (commonUI == null) throw new NullReferenceException("Common UI가 설정되지 않았습니다.");
        if (giftPopUpWindow == null) throw new NullReferenceException("Gift Popup Window가 설정되지 않았습니다.");
    }
    
    private void Start()
    {
        try 
        {
            // QuestSystem 인스턴스 확인
            if (QuestSystem.Instance == null)
            {
                Debug.LogError("QuestSystem.Instance가 null입니다. QuestSystem이 씬에 존재하는지 확인하세요.");
                return;
            }
            
            QuestApply(currentQuestType);
        }
        catch (Exception e)
        {
            Debug.LogError($"Start 메서드 에러 발생: {e.Message}");
        }
    }
    
    public void QuestApply(int type)
    {
        try
        {
            // QuestSystem 존재 확인
            if (QuestSystem.Instance == null)
            {
                Debug.LogError("QuestSystem.Instance가 null입니다.");
                return;
            }
            
            List<QuestData> typeQuestList = QuestSystem.Instance.GetQuestsByType(type);
            
            if (typeQuestList == null || typeQuestList.Count == 0)
            {
                Debug.LogWarning($"Type {type}에 해당하는 퀘스트가 없습니다.");
                return;
            }
            
            if (!usedQuestIndices.ContainsKey(type))
            {
                usedQuestIndices.Add(type, new HashSet<int>());
            }
            
            if (usedQuestIndices[type].Count >= typeQuestList.Count)
            {
                usedQuestIndices[type].Clear();
            }
            
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
            
            int randomIndex = availableIndices[random.Next(availableIndices.Count)];
            usedQuestIndices[type].Add(randomIndex);
            
            QuestData selectedQuest = typeQuestList.Find(q => q.QuestIndex == randomIndex);
            
            if (selectedQuest != null)
            {
                UpdateQuestUI(selectedQuest);
            }
            else
            {
                Debug.LogError($"선택된 인덱스 {randomIndex}에 해당하는 퀘스트를 찾을 수 없습니다.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"QuestApply 에러 발생: {e.Message}\n{e.StackTrace}");
        }
    }

    private void UpdateQuestUI(QuestData quest)
    {
        try
        {
            // questTitle2.text = quest.QuestDetail;
            questTitle.text = quest.QuestDetail;
            questDetail.text = $"성공 경험치 : {quest.QuestExp}";
            giftNum.text = $"x {quest.QuestGiftNumber}";
        }
        catch (Exception e)
        {
            Debug.LogError($"UpdateQuestUI 에러 발생: {e.Message}");
        }
    }

    public void OnClickClearButton()
    {
        try
        {
            QuestApply(currentQuestType);
            
            var seq = DOTween.Sequence();
            seq.Play().OnComplete(() => {
                if (giftPopUpWindow != null)
                {
                    giftPopUpWindow.Show();
                }
                else
                {
                    Debug.LogError("Gift Popup Window reference is missing");
                }
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"OnClickClearButton 에러 발생: {e.Message}");
        }
    }
    
    public void Show()
    {
        try
        {
            if (commonUI != null)
            {
                commonUI.SetActive(false);
            }
            
            gameObject.SetActive(true);
            transform.localScale = Vector3.one * 0.1f;
            
            var seq = DOTween.Sequence();
            seq.Append(transform.DOScale(targetScale + targetScale * shrinkRate, 0.3f));
            seq.Append(transform.DOScale(targetScale, 0.1f));
            seq.Play();
        }
        catch (Exception e)
        {
            Debug.LogError($"Show 메서드 에러 발생: {e.Message}");
        }
    }
    
    public void Hide()
    {
        try
        {
            if (commonUI != null)
            {
                commonUI.SetActive(true);
            }
            
            var seq = DOTween.Sequence();
            seq.Append(transform.DOScale(targetScale + targetScale * shrinkRate, 0.1f));
            seq.Append(transform.DOScale(0.1f, 0.3f));
            
            seq.Play().OnComplete(() => {
                gameObject.SetActive(false);
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"Hide 메서드 에러 발생: {e.Message}");
        }
    }
}
