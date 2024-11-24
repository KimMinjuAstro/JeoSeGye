using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;


public class GiftPopUpHandler : MonoBehaviour
{
    public GameObject commonUI;
    public GameObject questPopUp;

    private float targetScale = 4.182f;
    private float shrinkRate = 0.1f;

    private GameObject gift1;
    private GameObject gift2;
    private GameObject gift3;
    
    // 비활성화된 선물 인덱스를 추적하기 위한 정적 HashSet
    private static HashSet<int> disabledGiftIndices = new HashSet<int>();
    
    private void Start()
    {
        gift1 = GameObject.Find("UI Canvas").transform.Find("GiftPopUp").transform.Find("GiftBox").GetChild(0).gameObject;
        gift2 = GameObject.Find("UI Canvas").transform.Find("GiftPopUp").transform.Find("GiftBox").GetChild(1).gameObject;
        gift3= GameObject.Find("UI Canvas").transform.Find("GiftPopUp").transform.Find("GiftBox").GetChild(2).gameObject;
        // Debug.Log(gift3);
    }
    
    public static void DisableGift(int index)
    {
        disabledGiftIndices.Add(index);
    }
    
    private Vector3[] GetPositionsForGifts(int count)
    {
        switch (count)
        {
            case 1:
                return new Vector3[] { new Vector3(122f, -50f, 0) };
            case 2:
                return new Vector3[] { 
                    new Vector3(81.5f, -50f, 0),
                    new Vector3(163.5f, -50f, 0)
                };
            case 3:
                return new Vector3[] {
                    new Vector3(40f, -50f, 0),
                    new Vector3(123f, -50f, 0),
                    new Vector3(206f, -50f, 0)
                };
            default:
                return new Vector3[0];
        }
    }
    

    public void Show()
    {
        // 활성화 여부 확인 및 초기화
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        // 활성화된 선물 개수 계산
        int activeGiftCount = 6 - disabledGiftIndices.Count;
        if (activeGiftCount <= 0)
        {
            Debug.LogWarning("No active gifts to display!");
            return;
        }
        
        // 비활성화된 선물을 제외한 인덱스 목록 생성
        List<int> availableIndices = new List<int>();
        for (int i = 0; i < 6; i++)
        {
            if (!disabledGiftIndices.Contains(i))
            {
                availableIndices.Add(i);
            }
        }
        
        // 표시할 선물 개수 결정 (최대 3개, 최소 1개)
        int displayCount = Mathf.Min(3, activeGiftCount);
        int[] ranNums = Utils.RandomNumbers(availableIndices.Count, displayCount);
        
        // 모든 선물 비활성화
        foreach (Transform child in transform.GetChild(0))
        {
            child.gameObject.SetActive(false);
        }
        
        // 선택된 위치 배열 가져오기
        Vector3[] positions = GetPositionsForGifts(displayCount);

        // 선택된 선물만 활성화 및 위치 설정
        for (int i = 0; i < displayCount; i++)
        {
            int selectedIndex = availableIndices[ranNums[i]];
            Transform selectedGift = transform.GetChild(0).GetChild(selectedIndex);
            selectedGift.gameObject.SetActive(true);
            selectedGift.localScale = Vector3.one;
            
            RectTransform selectedGiftRect = selectedGift.GetComponent<RectTransform>();
            selectedGiftRect.anchoredPosition = positions[i];
        }
        
        // // 선택된 선물만 활성화 및 위치 설정
        // for (int i = 0; i < ranNums.Length; i++)
        // {
        //     int selectedIndex = availableIndices[ranNums[i]];
        //     Transform selectedGift = transform.GetChild(0).GetChild(selectedIndex);
        //     selectedGift.gameObject.SetActive(true);
        //     selectedGift.localScale = Vector3.one;
        //     
        //     RectTransform selectedGiftRect = selectedGift.GetComponent<RectTransform>();
        //     selectedGiftRect.anchoredPosition = new Vector3(40f + (83f * i), -50f, 0);
        // }
        
        
        transform.GetChild(1).localScale = new Vector3(1.0f, 1.0f, 1.0f);
        
        commonUI.SetActive(false);
        questPopUp.gameObject.SetActive(false);
        
        transform.localScale = Vector3.one * 0.1f; // 초기 크기 설정

        var seq = DOTween.Sequence();
        
        seq.Append(transform.DOScale(targetScale + targetScale * shrinkRate, 0.3f));
        seq.Append(transform.DOScale(targetScale, 0.1f));

        seq.Play();
    }
}
