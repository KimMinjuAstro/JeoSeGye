using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{
    public Transform enemy;          // 적의 Transform
    public RectTransform indicator; // 인디케이터 이미지 (UI)
    public Camera mainCamera;        // 메인 카메라
    public float edgeOffset = 50f;   // 화면 가장자리에서의 간격

    void Update()
    {
        if (enemy == null) // 적이 제거된 경우
        {
            //Destroy(indicator.gameObject); // 인디케이터 제거
            //Destroy(this); // 스크립트도 제거

            indicator.gameObject.SetActive(false);
            return;
        }

        Vector3 screenPos = mainCamera.WorldToScreenPoint(enemy.position);
        bool isOffScreen = screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height;

        if (isOffScreen)
        {
            // 적이 화면 밖에 있을 경우
            Vector3 clampedScreenPos = screenPos;

            // 화면 밖 위치를 가장자리로 클램프
            clampedScreenPos.x = Mathf.Clamp(screenPos.x, edgeOffset, Screen.width - edgeOffset);
            clampedScreenPos.y = Mathf.Clamp(screenPos.y, edgeOffset, Screen.height - edgeOffset);
            clampedScreenPos.z = 0; // Z값은 UI에서 사용하지 않음

            indicator.position = clampedScreenPos;
            indicator.gameObject.SetActive(true);

            // 적의 방향을 표시 (회전)
            Vector3 direction = enemy.position - mainCamera.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            indicator.rotation = Quaternion.Euler(0, 0, angle - 90); // 회전 보정
        }
        else
        {
            // 적이 화면 안에 있으면 비활성화
            indicator.gameObject.SetActive(false);
        }
    }
}
