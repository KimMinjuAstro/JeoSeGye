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

    [FormerlySerializedAs("questPopUp")] public QuestPopUpHandler questWindow;
    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questDetail;
    public TextMeshProUGUI giftNum;
    public Button clearButton;
    
    [Header("퀘스트 Type과 Index")]
    public int questType = 0;
    public int questIdx = 0;
    
    
    
    
    void Awake()
    {      
        DOTween.Init();
        transform.localScale = Vector3.one * 0.1f;
        gameObject.SetActive(false);
        clearButton.onClick.AddListener(OnClickClearButton);
    }

    private void Start()
    {
        QuestApply(questType, questIdx);

    }

    public void QuestApply(int type, int idx)
    {
        // 타입에 따른 퀘스트들 모두 불러옴
        List<QuestData> typeQuestList = QuestSystem.Instance.GetQuestsByType(type);

        // 리스트가 비어있거나 null인지 먼저 체크
        if (typeQuestList == null || typeQuestList.Count == 0)
        {
            Debug.Log($"Type {type}에 해당하는 퀘스트가 없습니다.");
            return;
        }

        // 퀘스트 완료될 때마다 카운트
        int completedQuestNum = 0;

        if (idx < typeQuestList.Count)
        {
            questTitle.text = $"{type} Map {typeQuestList[idx].QuestIndex} Title";
            questDetail.text = $"{type} Map {typeQuestList[idx].QuestIndex} Detail"+"\n"+$"Reward : {typeQuestList[idx].QuestExp}";
            giftNum.text = $"x {typeQuestList[idx].QuestGiftNumber}";
        }
        else
        {
            Debug.Log($"idx({idx})가 퀘스트 리스트 크기({typeQuestList.Count})를 벗어났습니다. type {type}에 맞는 퀘스트 완료");
        }
    }

    public void OnClickClearButton()
    {
        var seq = DOTween.Sequence();
        seq.Play().OnComplete(() => {
            giftPopUpWindow.Show();
        });
    }
    
    
    
    public void Show()
    {
        commonUI.SetActive(false);
        gameObject.SetActive(true);
        
        transform.localScale = Vector3.one * 0.1f; // 초기 크기 설정

        // DOTween 함수를 차례대로 수행하게 해줍니다.
        var seq = DOTween.Sequence();
        
        // DOScale 의 첫 번째 파라미터는 목표 Scale 값, 두 번째는 시간입니다.
        seq.Append(transform.DOScale(targetScale + targetScale * shrinkRate, 0.3f));
        seq.Append(transform.DOScale(targetScale, 0.1f));

        seq.Play();
    }   // 퀘스트 팝업창 보이기

    public void Hide()      // 퀘스트 팝업창 숨기기
    {
        
        commonUI.SetActive(true);
        
        var seq = DOTween.Sequence();

        // transform.localScale = Vector3.one * 0.2f;

        seq.Append(transform.DOScale(targetScale + targetScale * shrinkRate, 0.1f));
        seq.Append(transform.DOScale(0.1f, 0.3f));

        // OnComplete 는 seq 에 설정한 애니메이션의 플레이가 완료되면
        // { } 안에 있는 코드가 수행된다는 의미입니다.
        // 여기서는 닫기 애니메이션이 완료된 후 객첼르 비활성화 합니다.
        seq.Play().OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
    
    
}
