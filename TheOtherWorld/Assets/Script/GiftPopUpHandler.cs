using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class GiftPopUpHandler : MonoBehaviour
{
    public GameObject commonUI;
    public GameObject questPopUp;

    private float targetScale = 4.182f;
    private float shrinkRate = 0.1f;
    
    
    
    // Gift 팝업창 나타내기
    public void Show()
    {
        // 활성화 여부 확인 및 초기화
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        
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
