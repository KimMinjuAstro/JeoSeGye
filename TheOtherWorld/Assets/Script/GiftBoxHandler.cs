using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GiftBoxHandler : MonoBehaviour
{
    [SerializeField] private float zoomRate = 0.2f;
    
    private Vector3 originScale;
    private Sequence currentSequence;
    private static GiftBoxHandler giftBox;
    private Button button;
    
    [SerializeField] private int buttonId;
    public int ButtonId => buttonId;
    public static GiftBoxHandler GiftBox => giftBox;

    private QuestData currentQuestData;
    
    private void Awake()
    {
        originScale = transform.localScale;
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        // 현재 선택된 버튼이 있고, 그게 자신이 아니라면 애니메이션 정지
        if (giftBox != null && giftBox != this)
        {
            giftBox.StopAnimation();
        }

        // 자신이 이미 선택된 버튼이라면 무시
        if (giftBox == this)
            return;

        // 새로운 버튼 선택 및 애니메이션 시작
        giftBox = this;
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        // 기존 애니메이션이 있다면 정지
        if (currentSequence != null)
        {
            currentSequence.Kill();
        }

        // 새로운 애니메이션 시작
        currentSequence = DOTween.Sequence();
        currentSequence.Append(transform.DOScale(originScale + originScale * zoomRate, 1f))
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void StopAnimation()
    {
        if (currentSequence != null)
        {
            currentSequence.Kill();
            currentSequence = null;
        }
        
        // 크기를 원래대로 복구
        transform.localScale = originScale;
    }

    private void OnDestroy()
    {
        // 오브젝트가 파괴될 때 애니메이션 정리
        if (currentSequence != null)
        {
            currentSequence.Kill();
        }
        
        button.onClick.RemoveListener(OnButtonClick);
        
        // 선택된 버튼이 자신이었다면 참조 제거
        if (giftBox == this)
        {
            giftBox = null;
        }
    }
    
}
