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
    
    private void Start()
    {
        gift1 = GameObject.Find("UI Canvas").transform.Find("GiftPopUp").transform.Find("GiftBox").GetChild(0).gameObject;
        gift2 = GameObject.Find("UI Canvas").transform.Find("GiftPopUp").transform.Find("GiftBox").GetChild(1).gameObject;
        gift3= GameObject.Find("UI Canvas").transform.Find("GiftPopUp").transform.Find("GiftBox").GetChild(2).gameObject;
        Debug.Log(gift3);
    }
    


    // Gift 팝업창 나타내기
    public void Show()
    {
        // 활성화 여부 확인 및 초기화
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        foreach (Transform child in transform.GetChild(0))
        {
            child.gameObject.SetActive(true);
            child.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        int[] ranNums = Utils.RandomNumbers(6, 3);
        // Debug.Log(ranNums[0]);
        // Debug.Log(ranNums[1]);
        // Debug.Log(ranNums[2]);

        // 고정된 위치를 반복적으로 자식에 할당
        for (int i = 0; i < ranNums.Length; i++)
        {
            RectTransform selectedGift = transform.GetChild(0).transform.GetChild(ranNums[i]).GetComponent<RectTransform>();
            selectedGift.anchoredPosition = new Vector3(40f + (83f * i), -50f, 0); // 각 자식 위치를 83f씩 옆으로 이동
        }

             // selectedGift = transform.GetChild(0).transform.GetChild(ranNums[1]).GetComponent<RectTransform>();
             // selectedGift.anchoredPosition  = new Vector3(123f, -50f, 0);
             // selectedGift = transform.GetChild(0).transform.GetChild(ranNums[2]).GetComponent<RectTransform>();
             // selectedGift.anchoredPosition  = new Vector3(205f, -50f, 0);
             
        
        
        transform.GetChild(1).localScale = new Vector3(1.0f, 1.0f, 1.0f);
        
        commonUI.SetActive(false);
        questPopUp.gameObject.SetActive(false);
        
        transform.localScale = Vector3.one * 0.1f; // 초기 크기 설정

        // DOTween 함수를 차례대로 수행하게 해줍니다.
        var seq = DOTween.Sequence();
        
        // DOScale 의 첫 번째 파라미터는 목표 Scale 값, 두 번째는 시간입니다.
        seq.Append(transform.DOScale(targetScale + targetScale * shrinkRate, 0.3f));
        seq.Append(transform.DOScale(targetScale, 0.1f));

        seq.Play();
    }
    
    
    
    
}
