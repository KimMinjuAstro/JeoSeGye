using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoTextHandler : MonoBehaviour
{
   public int levelInt = 1;
   public int tierInts = 0;
   public string tierString = "NORMAL";
   public int buttonID;

    private TextMeshProUGUI textMeshProUGUI;
    
    void Awake()
    {
       // Start 대신 Awake에서 초기화
       InitializeTextComponent();
    }

    void OnEnable()
    {
       // 게임오브젝트가 활성화될 때마다 컴포넌트 확인
       InitializeTextComponent();
    }
    
    private void InitializeTextComponent()
    {
       if (textMeshProUGUI == null)
       {
          textMeshProUGUI = GetComponent<TextMeshProUGUI>();
          if (textMeshProUGUI == null)
          {
             Debug.LogError($"TextMeshProUGUI component not found on {gameObject.name}");
          }
       }
    }
    
    void Start()
    {
       textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    public void ShowGiftInfo(int btnID)
    {
       // buttonID가 일치할 때만 갱신
       if (buttonID != btnID) return;
       
       // 컴포넌트 재확인
       InitializeTextComponent();
        
       if (textMeshProUGUI == null)
       {
          Debug.LogError($"TextMeshProUGUI still null on {gameObject.name} when trying to show gift info");
          return;
       }

       levelInt = CheckButtonHandler.GetGiftLevel(btnID);
       tierInts = CheckButtonHandler.GetGiftTier(btnID);
       tierString = CheckButtonHandler.GetTierName(btnID);
        
       textMeshProUGUI.text = $"{tierString}\nLv. {levelInt}";
    }
}
