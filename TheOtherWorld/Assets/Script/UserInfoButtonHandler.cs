using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoButtonHandler : MonoBehaviour
{
    public UserInfoPopUpHandler userInfoPopUpHandler;
    private Button userInfoButton;

    public int buttonId;
    public UserInfoTextHandler[] userInfoTexts;
    
    private void Start()
    {
        userInfoButton = gameObject.GetComponent<Button>();
        userInfoButton.onClick.AddListener(OnButtonClick);
    }
    //
    // private void OnEnable()
    // {
    //     CheckButtonHandler.OnGiftUpdated += OnGiftUpdated;
    // }
    //
    // private void OnDisable()
    // {
    //     CheckButtonHandler.OnGiftUpdated -= OnGiftUpdated;
    // }
    
    // private void OnGiftUpdated(int updatedButtonId)
    // {
    //     // 업데이트된 버튼 ID가 현재 버튼의 ID와 일치할 때만 UI 갱신
    //     if (updatedButtonId == buttonId)
    //     {
    //         foreach (var infoText in userInfoTexts)
    //         {
    //             if (infoText != null)
    //             {
    //                 infoText.ShowGiftInfo(buttonId);
    //             }
    //         }
    //         userInfoPopUpHandler.Show();
    //     }
    // }
    
    public void OnButtonClick()
    {      
        var seq = DOTween.Sequence();

        float originScale = gameObject.transform.localScale.x;
        float shrinkRate = 0.2f;
    
        seq.Append(transform.DOScale(originScale - originScale * shrinkRate, 0.1f));
        seq.Append(transform.DOScale(originScale + originScale * shrinkRate, 0.1f));
        seq.Append(transform.DOScale(originScale, 0.1f));

        seq.Play().OnComplete(() => {
            userInfoPopUpHandler.Show();
        });
    }

    // private IEnumerator UpdateUIWithDelay()
    // {
    //     // CheckButtonHandler의 업데이트가 완료되기를 기다림
    //     yield return new WaitForSeconds(1.1f); // GiftPopUp의 SetActiveDelay보다 약간 더 길게 설정
    //
    //     // 모든 UI 텍스트에 대해 ShowGiftInfo 호출
    //     foreach (var infoText in userInfoTexts)
    //     {
    //         if (infoText != null)
    //         {
    //             infoText.ShowGiftInfo(buttonId);
    //         }
    //     }
    //     userInfoPopUpHandler.Show();
    // }
}
