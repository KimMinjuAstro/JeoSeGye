using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{
    public Transform enemy;          // ���� Transform
    public RectTransform indicator; // �ε������� �̹��� (UI)
    public Camera mainCamera;        // ���� ī�޶�
    public float edgeOffset = 50f;   // ȭ�� �����ڸ������� ����

    void Update()
    {
        if (enemy == null) // ���� ���ŵ� ���
        {
            //Destroy(indicator.gameObject); // �ε������� ����
            //Destroy(this); // ��ũ��Ʈ�� ����

            indicator.gameObject.SetActive(false);
            return;
        }

        Vector3 screenPos = mainCamera.WorldToScreenPoint(enemy.position);
        bool isOffScreen = screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height;

        if (isOffScreen)
        {
            // ���� ȭ�� �ۿ� ���� ���
            Vector3 clampedScreenPos = screenPos;

            // ȭ�� �� ��ġ�� �����ڸ��� Ŭ����
            clampedScreenPos.x = Mathf.Clamp(screenPos.x, edgeOffset, Screen.width - edgeOffset);
            clampedScreenPos.y = Mathf.Clamp(screenPos.y, edgeOffset, Screen.height - edgeOffset);
            clampedScreenPos.z = 0; // Z���� UI���� ������� ����

            indicator.position = clampedScreenPos;
            indicator.gameObject.SetActive(true);

            // ���� ������ ǥ�� (ȸ��)
            Vector3 direction = enemy.position - mainCamera.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            indicator.rotation = Quaternion.Euler(0, 0, angle - 90); // ȸ�� ����
        }
        else
        {
            // ���� ȭ�� �ȿ� ������ ��Ȱ��ȭ
            indicator.gameObject.SetActive(false);
        }
    }
}
