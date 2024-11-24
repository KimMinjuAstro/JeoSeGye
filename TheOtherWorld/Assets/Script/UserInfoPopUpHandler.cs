using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UserInfoPopUpHandler : MonoBehaviour
{
    public GameObject commonUI;

    private float targetScale = 3.687f;
    private float zoomRate = 0.1f;
    
    public void Show()
    {
        commonUI.SetActive(false);
        gameObject.SetActive(true);
        
        transform.localScale = Vector3.one * 0.1f;
        
        var seq = DOTween.Sequence();
        seq.Append(transform.DOScale(targetScale + targetScale * zoomRate, 0.3f));
        seq.Append(transform.DOScale(targetScale, 0.1f));
        seq.Play();
    }
    
    public void Hide()
    {
        commonUI.SetActive(true);
        
        var seq = DOTween.Sequence();
        seq.Append(transform.DOScale(targetScale + targetScale * zoomRate, 0.1f));
        seq.Append(transform.DOScale(0.1f, 0.3f));
        
        seq.Play().OnComplete(() => {
            gameObject.SetActive(false);
        });
    }
}
