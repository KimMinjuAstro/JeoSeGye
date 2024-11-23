using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CheckButtonHandler : MonoBehaviour
{
   [System.Serializable]
   public class ButtonSelectedEvent : UnityEvent<int> { }

   public ButtonSelectedEvent onSelectedButtonInfo;

   private void Awake()
   {
      Button button = GetComponent<Button>();
      button.onClick.AddListener(OnInfoButtonClick);
   }

   private void OnInfoButtonClick()
   {
      GiftBoxHandler giftBox = GiftBoxHandler.GiftBox;

      if (giftBox != null)
      {
         // 선택된 버튼의 ID 정보를 이벤트로 전달
         onSelectedButtonInfo?.Invoke(giftBox.ButtonId);
         
         Debug.Log($"Selected Button ID: {giftBox.ButtonId}");

         Transform giftBoxes = GameObject.Find("GiftBox").transform;
         foreach (Transform gB in giftBoxes)
         {
            int buttonId = gB.gameObject.GetComponent<GiftBoxHandler>().ButtonId;
            if (buttonId != giftBox.ButtonId)
            {
               Destroy(gB.gameObject);
            }
         }
         giftBox.StopAnimation();
         gameObject.SetActive(false);
         
         Vector2 centerPosition = new Vector2(123f, -50f);
         RectTransform rectTransform = giftBox.GetComponent<RectTransform>();

         Sequence sequence = DOTween.Sequence();

         sequence.Append(rectTransform.DOAnchorPos(centerPosition, 1f).SetEase(Ease.InQuart))
            .Join(rectTransform.DOScale(1.5f, 0.5f)) 
            .Append(rectTransform.DOScale(1.3f, 0.5f)) 
            .Join(rectTransform.DORotate(new Vector3(0, 0, 360), 0.5f, RotateMode.FastBeyond360)) // 회전 효과
            .OnComplete(() => {
               
            });
      }
      else
      {
         Debug.Log("No button currently selected");
      }
   }

   private void OnDestroy()
   {
      Button button = GetComponent<Button>();
      button.onClick.RemoveListener(OnInfoButtonClick);
   }
}