using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoCloseButtonHandler : MonoBehaviour
{
    public UserInfoPopUpHandler userInfoPopUp;

    private Button userInfoCloseButton;
    
    private void Start()
    {
        userInfoPopUp = transform.parent.gameObject.GetComponent<UserInfoPopUpHandler>();
        userInfoCloseButton = gameObject.GetComponent<Button>();
        userInfoCloseButton.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        var seq = DOTween.Sequence();

        float originScale = gameObject.transform.localScale.x;
        float shrinkRate = 0.2f;
        
        seq.Append(transform.DOScale(originScale - originScale * shrinkRate, 0.1f));
        seq.Append(transform.DOScale(originScale + originScale * shrinkRate, 0.1f));
        seq.Append(transform.DOScale(originScale, 0.1f));

        seq.Play().OnComplete(() => {
            userInfoPopUp.Hide();
        });
    }
}
